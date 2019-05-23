using System.Collections.Generic;
using Project.DAL;
using Project.Model;
using SqlSugar;
using Xunit;

namespace Project.UnitTest
{
    public class EmailConfigTest : BaseTest
    {
        /// <summary>
        ///     服务
        /// </summary>
        public ManageEmailConfig ManageEmailConfigService = new ManageEmailConfig();

        public class ManageEmailConfig : ManageBase<EmailConfig>
        {
			public ManageEmailConfig()
			{
				Db.CurrentConnectionConfig = new ConnectionConfig
				{
					ConnectionString = MySqlConnectionString,
				};
			}
		}

		[Fact(DisplayName = "新增EmailConfig")]
        public void Insert()
        {
            var result = ManageEmailConfigService.Insert(new EmailConfig());
            Assert.True(result > 0);
        }

		[Fact(DisplayName = "批量新增EmailConfig")]
        public void BulkInsert()
        {
            var result = ManageEmailConfigService.InsertWithNoTran(new List<EmailConfig>
            {
                new EmailConfig()
            });
            Assert.True(result);
        }

        [Fact(DisplayName = "批量删除EmailConfig")]
        public void BulkDelete()
        {
            var result = ManageEmailConfigService.SoftDeleteWithNoTran(new List<int> {1, 2});
            Assert.True(result);
        }

        [Fact(DisplayName = "逻辑删除EmailConfig")]
        public void Delete()
        {
            var result = ManageEmailConfigService.SoftDelete(1);
            Assert.True(result);
        }

        [Fact(DisplayName = "EmailConfig是否存在")]
        public void Exists()
        {
            var result = ManageEmailConfigService.Exists(1);
            Assert.True(result);
        }

        [Fact(DisplayName = "分页查询EmailConfig")]
        public void GetByPage()
        {
            var result = ManageEmailConfigService.GetByPage(1, 10, "addTime desc", new List<DbCondition<EmailConfig>>());
            Assert.True(result.Total > 0);
        }

        [Fact(DisplayName = "根据Id获取EmailConfig")]
        public void GetByPk()
        {
            var result = ManageEmailConfigService.GetByPk(1);
            Assert.True(result.Id > 0);
        }

        [Fact(DisplayName = "根据Id集合获取EmailConfig列表")]
        public void GetList()
        {
            var result = ManageEmailConfigService.GetList(new List<int> {1, 2});
            Assert.True(result.Count > 0);
        }
    }
}
