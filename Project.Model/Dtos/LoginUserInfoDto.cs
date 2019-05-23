using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Model.Dtos
{
    public class LoginUserInfoDto
    {
        /// <summary>
        ///     员工姓名
        /// </summary>           
        public string EmployeeName { get; set; }


        /// <summary>
        ///     登陆名
        /// </summary>           
        public string LoginName { get; set; }

        public string Token => DateTime.Now.ToString("yyyyMMddHHmmss");
    }
}
