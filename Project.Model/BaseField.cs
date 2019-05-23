using System;
using SqlSugar;

namespace Project.Model
{
    /// <summary>
    ///     基础字段
    /// </summary>
    public class BaseField
    {
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