using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Project.IBLL;
using Project.Model.Dtos;
using Project.WebApi.Filters;
using Project.Model;
using Project.Core.Utility;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace Project.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/files")]
    [EnableCors("any")]
    public class FileUploadController : Controller
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private IConfiguration _configuration;
        private readonly IWordService _wordService;

        public FileUploadController(IHostingEnvironment hostingEnvironment, IConfiguration configuration, IWordService wordService)
        {
            this._hostingEnvironment = hostingEnvironment;
            _configuration = configuration;
            _wordService = wordService;
        }

        /// <summary>
        /// 上传各类文件
        /// </summary>
        /// <param name="files"></param>
        /// <returns></returns>
        [RequestFormSizeLimit(valueCountLimit: 2147483647)]
        [Consumes("application/json", "multipart/form-data")]
        [HttpPost("")]
        public async Task<IActionResult> GatherFileUploadPost(IFormCollection files)
        {
            var result = "";
            foreach (var formFile in files.Files)
            {
                if (formFile.Length > 0)
                {
                    string fileExt = Path.GetExtension(formFile.FileName);
                    long fileSize = formFile.Length;
                    string newName = Guid.NewGuid() + fileExt;
                    var fileDire = _configuration["UploadPath"];
                    if (!Directory.Exists(fileDire))
                    {
                        Directory.CreateDirectory(fileDire);
                    }
                    var filePath = fileDire + newName;
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await formFile.CopyToAsync(stream);
                    }
                    result = _configuration["Host"] + newName;
                }
            }
            return Ok(result);
        }

        /// <summary>
        /// 上传word，替换指定文件
        /// </summary>
        /// <param name="files"></param>
        /// <returns></returns>
        [RequestFormSizeLimit(valueCountLimit: 2147483647)]
        [Consumes("application/json", "multipart/form-data")]
        [HttpPost("word")]
        public async Task<IActionResult> GatherWordUploadPost(IFormCollection files)
        {
            var word = _wordService.GetByPk(int.Parse(files["id"]));
            foreach (var formFile in files.Files)
            {
                if (formFile.Length > 0)
                {
                    using (var stream = new FileStream(word.LocalPath, FileMode.Create))
                    {
                        await formFile.CopyToAsync(stream);
                    }
                }
            }
            return Ok();
        }
    }
}
