using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace Project.Model
{
    ///<summary>
    ///邮件配置
    ///</summary>
    [SugarTable("email_config")]
    public class EmailConfig:BaseField
    {
    
           /// <summary>
           /// Desc_New:自增主键
           /// Default_New:
           /// Nullable_New:False
           /// </summary>           
           [SugarColumn(IsPrimaryKey=true,IsIdentity=true)]
           public int Id {get;set;}


           /// <summary>
           /// Desc_New:名称
           /// Default_New:
           /// Nullable_New:False
           /// </summary>           
           public string Name {get;set;}


           /// <summary>
           /// Desc_New:地址
           /// Default_New:
           /// Nullable_New:False
           /// </summary>           
           public string Address {get;set;}

    }
}