using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace Project.Model
{
    ///<summary>
    ///txt文档列表
    ///</summary>
    [SugarTable("txt")]
    public class Txt:BaseField
    {
    
           /// <summary>
           /// Desc_New:自增主键
           /// Default_New:
           /// Nullable_New:False
           /// </summary>           
           [SugarColumn(IsPrimaryKey=true,IsIdentity=true)]
           public int Id {get;set;}


           /// <summary>
           /// Desc_New:
           /// Default_New:0
           /// Nullable_New:False
           /// </summary>           
           public int Type {get;set;}


           /// <summary>
           /// Desc_New:文档名
           /// Default_New:
           /// Nullable_New:False
           /// </summary>           
           public string Name {get;set;}


           /// <summary>
           /// Desc_New:内容
           /// Default_New:
           /// Nullable_New:False
           /// </summary>           
           public string Info {get;set;}

    }
}