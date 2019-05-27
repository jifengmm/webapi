using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace Project.Model
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("appointment")]
    public class Appointment:BaseField
    {
    
           /// <summary>
           /// Desc_New:自增主键
           /// Default_New:
           /// Nullable_New:False
           /// </summary>           
           [SugarColumn(IsPrimaryKey=true,IsIdentity=true)]
           public int Id {get;set;}


           /// <summary>
           /// Desc_New:预约日期
           /// Default_New:
           /// Nullable_New:False
           /// </summary>           
           public DateTime AppointmentDate {get;set;}


           /// <summary>
           /// Desc_New:预约时间ID
           /// Default_New:
           /// Nullable_New:True
           /// </summary>           
           public int? AppointmentTimeId {get;set;}


           /// <summary>
           /// Desc_New:预约时间名称
           /// Default_New:
           /// Nullable_New:True
           /// </summary>           
           public string AppointmentTimeName {get;set;}


           /// <summary>
           /// Desc_New:预约客户姓名
           /// Default_New:
           /// Nullable_New:True
           /// </summary>           
           public string AppointmentCustomerName {get;set;}


           /// <summary>
           /// Desc_New:预约项目ID
           /// Default_New:
           /// Nullable_New:True
           /// </summary>           
           public int? AppointmentProjectId {get;set;}


           /// <summary>
           /// Desc_New:预约项目名称
           /// Default_New:
           /// Nullable_New:True
           /// </summary>           
           public string AppointmentProjectName {get;set;}


           /// <summary>
           /// Desc_New:备注
           /// Default_New:
           /// Nullable_New:True
           /// </summary>           
           public string Remarks {get;set;}

    }
}