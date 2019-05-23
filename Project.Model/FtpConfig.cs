using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace Project.Model
{
    ///<summary>
    ///ftp配置
    ///</summary>
    [SugarTable("ftp_config")]
    public class FtpConfig:BaseField
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
           /// Desc_New:账户
           /// Default_New:
           /// Nullable_New:False
           /// </summary>           
           public string Account {get;set;}


           /// <summary>
           /// Desc_New:密码
           /// Default_New:
           /// Nullable_New:False
           /// </summary>           
           public string Password {get;set;}


           /// <summary>
           /// Desc_New:地址
           /// Default_New:
           /// Nullable_New:False
           /// </summary>           
           public string Address {get;set;}


           /// <summary>
           /// Desc_New:端口
           /// Default_New:0
           /// Nullable_New:False
           /// </summary>           
           public int Port {get;set;}

    }
}