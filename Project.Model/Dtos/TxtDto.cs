//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由T4模板自动生成
//     生成时间 04/26/2019 11:17:17 By 朱峰
//     对此文件的更改可能会导致不正确的行为，并且如果
//     文件已存在，不会覆盖。
// </auto-generated>
//------------------------------------------------------------------------------

using System;

namespace Project.Model.Dtos
{
    public class TxtDto
    {
        /// <summary>
        /// Desc_New:自增主键
        /// Default_New:
        /// Nullable_New:False
        /// </summary>           
        public int Id { get; set; }


        /// <summary>
        /// Desc_New:
        /// Default_New:0
        /// Nullable_New:False
        /// </summary>           
        public int Type { get; set; }


        /// <summary>
        /// Desc_New:文档名
        /// Default_New:
        /// Nullable_New:False
        /// </summary>           
        public string Name { get; set; }


        /// <summary>
        /// Desc_New:内容
        /// Default_New:
        /// Nullable_New:False
        /// </summary>           
        public string Info { get; set; }

        /// <summary>
        ///     添加时间
        /// </summary>
        public DateTime AddTime { get; set; }
    }
}
