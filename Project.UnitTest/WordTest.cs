using System.Collections.Generic;
using Project.DAL;
using Project.Model;
using SqlSugar;
using Xunit;

namespace Project.UnitTest
{
    public class WordTest : BaseTest
    {
        /// <summary>
        ///     服务
        /// </summary>
        public ManageWord ManageWordService = new ManageWord();

        public class ManageWord : ManageBase<Word>
        {
			public ManageWord()
			{
				Db.CurrentConnectionConfig = new ConnectionConfig
				{
					ConnectionString = MySqlConnectionString,
				};
			}
		}

		[Fact(DisplayName = "新增Word")]
        public void Insert()
        {
            var result = ManageWordService.Insert(new Word());
            Assert.True(result > 0);
        }

		[Fact(DisplayName = "批量新增Word")]
        public void BulkInsert()
        {
            var result = ManageWordService.InsertWithNoTran(new List<Word>
            {
                new Word()
            });
            Assert.True(result);
        }

        [Fact(DisplayName = "批量删除Word")]
        public void BulkDelete()
        {
            var result = ManageWordService.SoftDeleteWithNoTran(new List<int> {1, 2});
            Assert.True(result);
        }

        [Fact(DisplayName = "逻辑删除Word")]
        public void Delete()
        {
            var result = ManageWordService.SoftDelete(1);
            Assert.True(result);
        }

        [Fact(DisplayName = "Word是否存在")]
        public void Exists()
        {
            var result = ManageWordService.Exists(1);
            Assert.True(result);
        }

        [Fact(DisplayName = "分页查询Word")]
        public void GetByPage()
        {
            var result = ManageWordService.GetByPage(1, 10, "addTime desc", new List<DbCondition<Word>>());
            Assert.True(result.Total > 0);
        }

        [Fact(DisplayName = "根据Id获取Word")]
        public void GetByPk()
        {
            var result = ManageWordService.GetByPk(1);
            Assert.True(result.Id > 0);
        }

        [Fact(DisplayName = "根据Id集合获取Word列表")]
        public void GetList()
        {
            var result = ManageWordService.GetList(new List<int> {1, 2});
            Assert.True(result.Count > 0);
        }
    }
}
