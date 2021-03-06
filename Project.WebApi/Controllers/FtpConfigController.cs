//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由T4模板自动生成
//     生成时间 05/06/2019 13:27:04 By 朱峰
//     对此文件的更改可能会导致不正确的行为，并且如果
//     文件已存在，不会覆盖。
// </auto-generated>
//------------------------------------------------------------------------------

using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Project.Core.Utility;
using Project.IBLL;
using Project.Model;
using Project.Model.Conditions;
using Project.Model.Dtos;
using Project.WebApi.Filters;
using Microsoft.AspNetCore.Cors;

namespace Project.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/ftpconfig")]
    [EnableCors("any")]
    public class FtpConfigController : Controller
    {
        private readonly IFtpConfigService _ftpConfigService;

        public FtpConfigController(IFtpConfigService ftpConfigService)
        {
            _ftpConfigService = ftpConfigService;
        }

        /// <summary>
        ///     根据id获取
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult GetByPk(int id)
        {
            var model = _ftpConfigService.GetByPk(id);
            return Ok(Mapper.Map<FtpConfigDto>(model));
        }

        /// <summary>
        ///     添加FtpConfig
        /// </summary>
        /// <param name="ftpConfigDto">实体</param>
        /// <returns></returns>
        [HttpPost("")]
        [Validate]
        public IActionResult Insert([FromBody] FtpConfigDto ftpConfigDto)
        {
            var ftpConfig = Mapper.Map<FtpConfig>(ftpConfigDto);
            return Ok(_ftpConfigService.Insert(ftpConfig));
        }

        /// <summary>
        ///     更新FtpConfig
        /// </summary>
        /// <param name="id">主键</param>
        /// <param name="ftpConfigDto">实体</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Validate]
        public IActionResult Update(int id, [FromBody] FtpConfigDto ftpConfigDto)
        {
            var model = Mapper.Map<FtpConfig>(ftpConfigDto);
            var result = _ftpConfigService.Update(model);
            return Ok();
        }

        /// <summary>
        ///     逻辑删除ftpConfig
        /// </summary>
        /// <param name="id">主键Id</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = _ftpConfigService.SoftDelete(id);
            return Ok();
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="pi"></param>
        /// <param name="ps"></param>
        /// <param name="condition"></param>
        [HttpGet("get_by_page")]
        public IActionResult GetByPage(int pi, int ps, FtpConfigCondition condition = null)
        {
            var result = _ftpConfigService.GetByPage(pi, ps, null, condition);
            return Ok(new
            {
                Total = result.Total,
                List = Mapper.Map<List<FtpConfigDto>>(result.Data)
            });
        }
    }
}
