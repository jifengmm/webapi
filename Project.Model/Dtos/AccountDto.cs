//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由T4模板自动生成
//     生成时间 03/21/2019 08:57:52 By 朱峰
//     对此文件的更改可能会导致不正确的行为，并且如果
//     文件已存在，不会覆盖。
// </auto-generated>
//------------------------------------------------------------------------------

using System;

namespace Project.Model.Dtos
{
    public class AccountDto
    {
        /// <summary>
        ///     自增主键
        /// </summary>           
        public int Id { get; set; }


        /// <summary>
        ///     员工姓名
        /// </summary>           
        public string EmployeeName { get; set; }


        /// <summary>
        ///     登陆名
        /// </summary>           
        public string LoginName { get; set; }


        /// <summary>
        ///     密码
        /// </summary>           
        public string UserPass { get; set; }

        /// <summary>
        ///     添加人
        /// </summary>
        public int AddUser { get; set; }

        /// <summary>
        ///     添加时间
        /// </summary>
        public DateTime AddTime { get; set; }

        /// <summary>
        ///     修改人
        /// </summary>
        public int UpdateUser { get; set; }

        /// <summary>
        ///     修改时间
        /// </summary>
        public DateTime UpdateTime { get; set; }

        public bool Marks { get; set; }
    }
}
