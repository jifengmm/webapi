using System.Collections.Generic;
using Project.DAL;
using Project.Model;
using SqlSugar;
using Xunit;

namespace Project.UnitTest
{
    public class AppointmentTest : BaseTest
    {
        /// <summary>
        ///     服务
        /// </summary>
        public ManageAppointment ManageAppointmentService = new ManageAppointment();

        public class ManageAppointment : ManageBase<Appointment>
        {
			public ManageAppointment()
			{
				Db.CurrentConnectionConfig = new ConnectionConfig
				{
					ConnectionString = MySqlConnectionString,
				};
			}
		}

		[Fact(DisplayName = "新增Appointment")]
        public void Insert()
        {
            var result = ManageAppointmentService.Insert(new Appointment());
            Assert.True(result > 0);
        }

		[Fact(DisplayName = "批量新增Appointment")]
        public void BulkInsert()
        {
            var result = ManageAppointmentService.InsertWithNoTran(new List<Appointment>
            {
                new Appointment()
            });
            Assert.True(result);
        }

        [Fact(DisplayName = "批量删除Appointment")]
        public void BulkDelete()
        {
            var result = ManageAppointmentService.SoftDeleteWithNoTran(new List<int> {1, 2});
            Assert.True(result);
        }

        [Fact(DisplayName = "逻辑删除Appointment")]
        public void Delete()
        {
            var result = ManageAppointmentService.SoftDelete(1);
            Assert.True(result);
        }

        [Fact(DisplayName = "Appointment是否存在")]
        public void Exists()
        {
            var result = ManageAppointmentService.Exists(1);
            Assert.True(result);
        }

        [Fact(DisplayName = "分页查询Appointment")]
        public void GetByPage()
        {
            var result = ManageAppointmentService.GetByPage(1, 10, "addTime desc", new List<DbCondition<Appointment>>());
            Assert.True(result.Total > 0);
        }

        [Fact(DisplayName = "根据Id获取Appointment")]
        public void GetByPk()
        {
            var result = ManageAppointmentService.GetByPk(1);
            Assert.True(result.Id > 0);
        }

        [Fact(DisplayName = "根据Id集合获取Appointment列表")]
        public void GetList()
        {
            var result = ManageAppointmentService.GetList(new List<int> {1, 2});
            Assert.True(result.Count > 0);
        }
    }
}
