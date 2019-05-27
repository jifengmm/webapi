//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由T4模板自动生成
//     生成时间 05/27/2019 13:55:17 By 朱峰
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
    [Route("api/appointment")]
    [EnableCors("any")]
    public class AppointmentController : Controller
    {
        private readonly IAppointmentService _appointmentService;

        public AppointmentController(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        /// <summary>
        ///     根据id获取
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult GetByPk(int id)
        {
            var model = _appointmentService.GetByPk(id);
            return Ok(Mapper.Map<AppointmentDto>(model));
        }

        /// <summary>
        ///     添加Appointment
        /// </summary>
        /// <param name="appointmentDto">实体</param>
        /// <returns></returns>
        [HttpPost("")]
        [Validate]
        public IActionResult Insert([FromBody] AppointmentDto appointmentDto)
        {
            var appointment = Mapper.Map<Appointment>(appointmentDto);
            return Ok(_appointmentService.Insert(appointment));
        }

        /// <summary>
        ///     更新Appointment
        /// </summary>
        /// <param name="id">主键</param>
        /// <param name="appointmentDto">实体</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Validate]
        public IActionResult Update(int id, [FromBody] AppointmentDto appointmentDto)
        {
            var model = Mapper.Map<Appointment>(appointmentDto);
            var result = _appointmentService.Update(model);
            return Ok();
        }

        /// <summary>
        ///     逻辑删除appointment
        /// </summary>
        /// <param name="id">主键Id</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = _appointmentService.SoftDelete(id);
            return Ok();
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="pi"></param>
        /// <param name="ps"></param>
        /// <param name="condition"></param>
        [HttpGet("get_by_page")]
        public IActionResult GetByPage(int pi, int ps, AppointmentCondition condition = null)
        {
            var result = _appointmentService.GetByPage(pi, ps, null, condition);
            return Ok(new
            {
                Total = result.Total,
                List = Mapper.Map<List<AppointmentDto>>(result.Data)
            });
        }
    }
}