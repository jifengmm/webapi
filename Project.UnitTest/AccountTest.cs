using System.Collections.Generic;
using Project.DAL;
using Project.Model;
using SqlSugar;
using Xunit;

namespace Project.UnitTest
{
    public class AccountTest : BaseTest
    {
        /// <summary>
        ///     服务
        /// </summary>
        public ManageAccount ManageAccountService = new ManageAccount();

        public class ManageAccount : ManageBase<Account>
        {
			public ManageAccount()
			{
				Db.CurrentConnectionConfig = new ConnectionConfig
				{
					ConnectionString = MySqlConnectionString,
				};
			}
		}

		[Fact(DisplayName = "新增Account")]
        public void Insert()
        {
            var result = ManageAccountService.Insert(new Account());
            Assert.True(result > 0);
        }

		[Fact(DisplayName = "批量新增Account")]
        public void BulkInsert()
        {
            var result = ManageAccountService.InsertWithNoTran(new List<Account>
            {
                new Account()
            });
            Assert.True(result);
        }

        [Fact(DisplayName = "批量删除Account")]
        public void BulkDelete()
        {
            var result = ManageAccountService.SoftDeleteWithNoTran(new List<int> {1, 2});
            Assert.True(result);
        }

        [Fact(DisplayName = "逻辑删除Account")]
        public void Delete()
        {
            var result = ManageAccountService.SoftDelete(1);
            Assert.True(result);
        }

        [Fact(DisplayName = "Account是否存在")]
        public void Exists()
        {
            var result = ManageAccountService.Exists(1);
            Assert.True(result);
        }

        [Fact(DisplayName = "分页查询Account")]
        public void GetByPage()
        {
            var result = ManageAccountService.GetByPage(1, 10, "addTime desc", new List<DbCondition<Account>>());
            Assert.True(result.Total > 0);
        }

        [Fact(DisplayName = "根据Id获取Account")]
        public void GetByPk()
        {
            var result = ManageAccountService.GetByPk(1);
            Assert.True(result.Id > 0);
        }

        [Fact(DisplayName = "根据Id集合获取Account列表")]
        public void GetList()
        {
            var result = ManageAccountService.GetList(new List<int> {1, 2});
            Assert.True(result.Count > 0);
        }
    }
}
