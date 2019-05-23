﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由T4模板自动生成
//     生成时间 07/09/2018 16:39:33 By 朱峰
//     对此文件的更改可能会导致不正确的行为，并且如果
//     文件已存在，不会覆盖。
// </auto-generated>
//------------------------------------------------------------------------------

using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Project.IBLL;
using Project.Model.Dtos;
using Project.WebApi.Filters;
using Project.Model;
using Project.Core.Utility;
using Microsoft.AspNetCore.Cors;

namespace Project.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/attributes")]
    [EnableCors("any")]
    public class AttributeController : Controller
    {
        /// <summary>
        ///     通过枚举查询
        /// </summary>
        /// <param name="names">appcode</param>
        /// <returns></returns>
        [HttpGet("{names}")]
        public IActionResult GetAttributes(string names)
        {
            Dictionary<AttributeName, List<NameValuePair>> attrDic =
                new Dictionary<AttributeName, List<NameValuePair>>();
            foreach (var name in Utils.SplitCommaString(names))
            {
                switch (name.ToLower())
                {
                    //case "status":
                    //    attrDic.Add(AttributeName.Status, Utils.GetNameValuePairs(typeof(Status)));
                    //    break;
                }
            }
            return Ok(attrDic);
        }
    }
}