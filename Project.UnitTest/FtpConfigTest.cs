using System.Collections.Generic;
using Project.DAL;
using Project.Model;
using SqlSugar;
using Xunit;

namespace Project.UnitTest
{
    public class FtpConfigTest : BaseTest
    {
        /// <summary>
        ///     服务
        /// </summary>
        public ManageFtpConfig ManageFtpConfigService = new ManageFtpConfig();

        public class ManageFtpConfig : ManageBase<FtpConfig>
        {
			public ManageFtpConfig()
			{
				Db.CurrentConnectionConfig = new ConnectionConfig
				{
					ConnectionString = MySqlConnectionString,
				};
			}
		}

		[Fact(DisplayName = "新增FtpConfig")]
        public void Insert()
        {
            var result = ManageFtpConfigService.Insert(new FtpConfig());
            Assert.True(result > 0);
        }

		[Fact(DisplayName = "批量新增FtpConfig")]
        public void BulkInsert()
        {
            var result = ManageFtpConfigService.InsertWithNoTran(new List<FtpConfig>
            {
                new FtpConfig()
            });
            Assert.True(result);
        }

        [Fact(DisplayName = "批量删除FtpConfig")]
        public void BulkDelete()
        {
            var result = ManageFtpConfigService.SoftDeleteWithNoTran(new List<int> {1, 2});
            Assert.True(result);
        }

        [Fact(DisplayName = "逻辑删除FtpConfig")]
        public void Delete()
        {
            var result = ManageFtpConfigService.SoftDelete(1);
            Assert.True(result);
        }

        [Fact(DisplayName = "FtpConfig是否存在")]
        public void Exists()
        {
            var result = ManageFtpConfigService.Exists(1);
            Assert.True(result);
        }

        [Fact(DisplayName = "分页查询FtpConfig")]
        public void GetByPage()
        {
            var result = ManageFtpConfigService.GetByPage(1, 10, "addTime desc", new List<DbCondition<FtpConfig>>());
            Assert.True(result.Total > 0);
        }

        [Fact(DisplayName = "根据Id获取FtpConfig")]
        public void GetByPk()
        {
            var result = ManageFtpConfigService.GetByPk(1);
            Assert.True(result.Id > 0);
        }

        [Fact(DisplayName = "根据Id集合获取FtpConfig列表")]
        public void GetList()
        {
            var result = ManageFtpConfigService.GetList(new List<int> {1, 2});
            Assert.True(result.Count > 0);
        }
    }
}
