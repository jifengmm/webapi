using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace Project.Model
{
    ///<summary>
    ///业绩
    ///</summary>
    [SugarTable("achievement")]
    public class Achievement:BaseField
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
           /// Default_New:
           /// Nullable_New:False
           /// </summary>           
           public int UserId {get;set;}


           /// <summary>
           /// Desc_New:
           /// Default_New:
           /// Nullable_New:False
           /// </summary>           
           public int AchievementNum {get;set;}


           /// <summary>
           /// Desc_New:
           /// Default_New:
           /// Nullable_New:False
           /// </summary>           
           public int AchievementType {get;set;}


           /// <summary>
           /// Desc_New:
           /// Default_New:
           /// Nullable_New:True
           /// </summary>           
           public string Remark {get;set;}

    }
}