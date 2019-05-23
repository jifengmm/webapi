using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace Project.Model
{
    ///<summary>
    ///word文档列表
    ///</summary>
    [SugarTable("word")]
    public class Word : BaseField
    {

        /// <summary>
        /// Desc_New:自增主键
        /// Default_New:
        /// Nullable_New:False
        /// </summary>           
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
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
        /// Desc_New:文档路径
        /// Default_New:
        /// Nullable_New:False
        /// </summary>           
        public string RemotePath { get; set; }

        /// <summary>
        /// Desc_New:文档路径
        /// Default_New:
        /// Nullable_New:False
        /// </summary>           
        public string LocalPath { get; set; }

        /// <summary>
        /// Desc_New:日期
        /// Default_New:
        /// Nullable_New:False
        /// </summary>           
        public DateTime Date { get; set; }


        /// <summary>
        /// Desc_New:状态
        /// Default_New:0
        /// Nullable_New:False
        /// </summary>           
        public int Status { get; set; }

    }
}