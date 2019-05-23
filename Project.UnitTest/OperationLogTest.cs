using System.Collections.Generic;
using Project.DAL;
using Project.Model;
using SqlSugar;
using Xunit;

namespace Project.UnitTest
{
    public class OperationLogTest : BaseTest
    {
        /// <summary>
        ///     服务
        /// </summary>
        public ManageOperationLog ManageOperationLogService = new ManageOperationLog();

        public class ManageOperationLog : ManageBase<OperationLog>
        {
			public ManageOperationLog()
			{
				Db.CurrentConnectionConfig = new ConnectionConfig
				{
					ConnectionString = MySqlConnectionString,
				};
			}
		}

		[Fact(DisplayName = "新增OperationLog")]
        public void Insert()
        {
            var result = ManageOperationLogService.Insert(new OperationLog());
            Assert.True(result > 0);
        }

		[Fact(DisplayName = "批量新增OperationLog")]
        public void BulkInsert()
        {
            var result = ManageOperationLogService.InsertWithNoTran(new List<OperationLog>
            {
                new OperationLog()
            });
            Assert.True(result);
        }

        [Fact(DisplayName = "批量删除OperationLog")]
        public void BulkDelete()
        {
            var result = ManageOperationLogService.SoftDeleteWithNoTran(new List<int> {1, 2});
            Assert.True(result);
        }

        [Fact(DisplayName = "逻辑删除OperationLog")]
        public void Delete()
        {
            var result = ManageOperationLogService.SoftDelete(1);
            Assert.True(result);
        }

        [Fact(DisplayName = "OperationLog是否存在")]
        public void Exists()
        {
            var result = ManageOperationLogService.Exists(1);
            Assert.True(result);
        }

        [Fact(DisplayName = "分页查询OperationLog")]
        public void GetByPage()
        {
            var result = ManageOperationLogService.GetByPage(1, 10, "addTime desc", new List<DbCondition<OperationLog>>());
            Assert.True(result.Total > 0);
        }

        [Fact(DisplayName = "根据Id获取OperationLog")]
        public void GetByPk()
        {
            var result = ManageOperationLogService.GetByPk(1);
            Assert.True(result.Id > 0);
        }

        [Fact(DisplayName = "根据Id集合获取OperationLog列表")]
        public void GetList()
        {
            var result = ManageOperationLogService.GetList(new List<int> {1, 2});
            Assert.True(result.Count > 0);
        }
    }
}
