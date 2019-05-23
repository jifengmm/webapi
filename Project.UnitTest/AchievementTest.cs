using System.Collections.Generic;
using Project.DAL;
using Project.Model;
using SqlSugar;
using Xunit;

namespace Project.UnitTest
{
    public class AchievementTest : BaseTest
    {
        /// <summary>
        ///     服务
        /// </summary>
        public ManageAchievement ManageAchievementService = new ManageAchievement();

        public class ManageAchievement : ManageBase<Achievement>
        {
			public ManageAchievement()
			{
				Db.CurrentConnectionConfig = new ConnectionConfig
				{
					ConnectionString = MySqlConnectionString,
				};
			}
		}

		[Fact(DisplayName = "新增Achievement")]
        public void Insert()
        {
            var result = ManageAchievementService.Insert(new Achievement());
            Assert.True(result > 0);
        }

		[Fact(DisplayName = "批量新增Achievement")]
        public void BulkInsert()
        {
            var result = ManageAchievementService.InsertWithNoTran(new List<Achievement>
            {
                new Achievement()
            });
            Assert.True(result);
        }

        [Fact(DisplayName = "批量删除Achievement")]
        public void BulkDelete()
        {
            var result = ManageAchievementService.SoftDeleteWithNoTran(new List<int> {1, 2});
            Assert.True(result);
        }

        [Fact(DisplayName = "逻辑删除Achievement")]
        public void Delete()
        {
            var result = ManageAchievementService.SoftDelete(1);
            Assert.True(result);
        }

        [Fact(DisplayName = "Achievement是否存在")]
        public void Exists()
        {
            var result = ManageAchievementService.Exists(1);
            Assert.True(result);
        }

        [Fact(DisplayName = "分页查询Achievement")]
        public void GetByPage()
        {
            var result = ManageAchievementService.GetByPage(1, 10, "addTime desc", new List<DbCondition<Achievement>>());
            Assert.True(result.Total > 0);
        }

        [Fact(DisplayName = "根据Id获取Achievement")]
        public void GetByPk()
        {
            var result = ManageAchievementService.GetByPk(1);
            Assert.True(result.Id > 0);
        }

        [Fact(DisplayName = "根据Id集合获取Achievement列表")]
        public void GetList()
        {
            var result = ManageAchievementService.GetList(new List<int> {1, 2});
            Assert.True(result.Count > 0);
        }
    }
}
