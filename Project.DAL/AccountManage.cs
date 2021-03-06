//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由T4模板自动生成
//     生成时间 03/21/2019 08:57:52 By 朱峰
//     对此文件的更改可能会导致不正确的行为，并且如果
//     文件已存在，不会覆盖。
// </auto-generated>
//------------------------------------------------------------------------------

using Project.IDAL;
using Project.Model;
using SqlSugar;

namespace Project.DAL
{
    public class AccountManage : ManageBase<Account>,IAccountManage
    {
		public AccountManage()
		{
		}

        /// <summary>
        /// 判断LoginName是否存在
        /// </summary>
        /// <param name="loginName"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool ExistLoginName(string loginName, int? id)
        {
            return Db.Queryable<Account>().Where(o => o.LoginName == loginName && o.Marks).WhereIF(id.HasValue, o => o.Id != id).Any();
        }

        /// <summary>
        /// 根据loginName和password获取账号
        /// </summary>
        /// <param name="loginName"></param>
        /// <param name="password"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public Account GetAccount(string loginName, string password)
        {
            return Db.Queryable<Account>().Where(o=>o.LoginName == loginName && o.UserPass == password && o.Marks).First();
        }
    }
}
