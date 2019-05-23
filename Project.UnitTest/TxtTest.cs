using System.Collections.Generic;
using Project.DAL;
using Project.Model;
using SqlSugar;
using Xunit;

namespace Project.UnitTest
{
    public class TxtTest : BaseTest
    {
        /// <summary>
        ///     服务
        /// </summary>
        public ManageTxt ManageTxtService = new ManageTxt();

        public class ManageTxt : ManageBase<Txt>
        {
			public ManageTxt()
			{
				Db.CurrentConnectionConfig = new ConnectionConfig
				{
					ConnectionString = MySqlConnectionString,
				};
			}
		}

		[Fact(DisplayName = "新增Txt")]
        public void Insert()
        {
            var result = ManageTxtService.Insert(new Txt());
            Assert.True(result > 0);
        }

		[Fact(DisplayName = "批量新增Txt")]
        public void BulkInsert()
        {
            var result = ManageTxtService.InsertWithNoTran(new List<Txt>
            {
                new Txt()
            });
            Assert.True(result);
        }

        [Fact(DisplayName = "批量删除Txt")]
        public void BulkDelete()
        {
            var result = ManageTxtService.SoftDeleteWithNoTran(new List<int> {1, 2});
            Assert.True(result);
        }

        [Fact(DisplayName = "逻辑删除Txt")]
        public void Delete()
        {
            var result = ManageTxtService.SoftDelete(1);
            Assert.True(result);
        }

        [Fact(DisplayName = "Txt是否存在")]
        public void Exists()
        {
            var result = ManageTxtService.Exists(1);
            Assert.True(result);
        }

        [Fact(DisplayName = "分页查询Txt")]
        public void GetByPage()
        {
            var result = ManageTxtService.GetByPage(1, 10, "addTime desc", new List<DbCondition<Txt>>());
            Assert.True(result.Total > 0);
        }

        [Fact(DisplayName = "根据Id获取Txt")]
        public void GetByPk()
        {
            var result = ManageTxtService.GetByPk(1);
            Assert.True(result.Id > 0);
        }

        [Fact(DisplayName = "根据Id集合获取Txt列表")]
        public void GetList()
        {
            var result = ManageTxtService.GetList(new List<int> {1, 2});
            Assert.True(result.Count > 0);
        }
    }
}
