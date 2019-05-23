using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace Project.Model
{
    ///<summary>
    ///账号信息
    ///</summary>
    [SugarTable("account")]
    public class Account:BaseField
    {
    
           /// <summary>
           /// Desc_New:自增主键
           /// Default_New:
           /// Nullable_New:False
           /// </summary>           
           [SugarColumn(IsPrimaryKey=true,IsIdentity=true)]
           public int Id {get;set;}


           /// <summary>
           /// Desc_New:员工姓名
           /// Default_New:
           /// Nullable_New:True
           /// </summary>           
           public string EmployeeName {get;set;}


           /// <summary>
           /// Desc_New:登陆名
           /// Default_New:
           /// Nullable_New:True
           /// </summary>           
           public string LoginName {get;set;}


           /// <summary>
           /// Desc_New:密码
           /// Default_New:
           /// Nullable_New:True
           /// </summary>           
           public string UserPass {get;set;}

    }
}