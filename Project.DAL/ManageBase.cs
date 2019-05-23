using System;
using System.Collections.Generic;
using System.Dynamic;
using Project.Core.User;
using Project.IDAL;
using Project.Model;
using Project.Model.Dtos;
using SqlSugar;

namespace Project.DAL
{
    /// <summary>
    ///     封装DB
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class ManageBase<T> : IManageBase<T> where T : class, new()
    {
        private readonly IUserContext _userContext;
        protected SqlSugarClient Db;

        protected ManageBase(DbType dbType = DbType.MySql)
        {
            Db = MyDbFactory.GetDatabase(dbType);
            _userContext = new DefaultUserContext();
        }

        protected ManageBase(IUserContext userContext, DbType dbType = DbType.MySql)
        {
            Db = MyDbFactory.GetDatabase(dbType);
            _userContext = userContext;
        }

        /// <summary>
        ///     统一新增
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int Insert(T entity)
        {
            var obj = GetDynamicObj(entity, ConvertDynamicType.Insert);
            return Db.Insertable<T>(obj).ExecuteReturnIdentity();
        }

        /// <summary>
        ///     批量插入(带事务)
        /// </summary>
        /// <param name="entitys"></param>
        /// <returns></returns>
        public bool InsertWithTran(List<T> entitys)
        {
            var result = Db.Ado.UseTran(() =>
            {
                entitys.ForEach(entity =>
                {
                    var obj = GetDynamicObj(entity, ConvertDynamicType.Insert);
                    Db.Insertable<T>(obj).ExecuteCommand();
                });
            });
            return result.IsSuccess;
        }

        /// <summary>
        ///     批量插入(不带事务)
        /// </summary>
        /// <param name="entitys"></param>
        /// <returns></returns>
        public bool InsertWithNoTran(List<T> entitys)
        {
            entitys.ForEach(entity =>
            {
                var obj = GetDynamicObj(entity, ConvertDynamicType.Insert);
                Db.Insertable(obj).ExecuteCommand();
            });
            return true;
        }

        /// <summary>
        ///     统一更新
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Update(T entity)
        {
            var obj = GetDynamicObj(entity, ConvertDynamicType.Update);
            var list = GetIgnoreList();
            return Db.Updateable((T)obj).IgnoreColumns(o => list.Contains(o)).ExecuteCommand() > -1;
        }

        /// <summary>
        ///     批量更新(带事务)
        /// </summary>
        /// <param name="entitys"></param>
        /// <returns></returns>
        public bool UpdateWithTran(List<T> entitys)
        {
            var result = Db.Ado.UseTran(() =>
            {
                entitys.ForEach(entity =>
                {
                    var obj = GetDynamicObj(entity, ConvertDynamicType.Update);
                    var list = GetIgnoreList();
                    Db.Updateable((T)obj).IgnoreColumns(o => list.Contains(o)).ExecuteCommand();
                });
            });
            return result.IsSuccess;
        }

        /// <summary>
        ///     批量更新(不带事务)
        /// </summary>
        /// <param name="entitys"></param>
        /// <returns></returns>
        public bool UpdateWithNoTran(List<T> entitys)
        {
            entitys.ForEach(entity =>
            {
                var obj = GetDynamicObj(entity, ConvertDynamicType.Update);
                var list = GetIgnoreList();
                Db.Updateable((T)obj).IgnoreColumns(o => list.Contains(o)).ExecuteCommand();
            });
            return true;
        }

        /// <summary>
        ///     逻辑删除
        /// </summary>
        /// <returns></returns>
        public bool SoftDelete(int id)
        {
            var obj = GetDynamicObj(ConvertDynamicType.Delete);
            obj.Add("Id", id);
            return Db.Updateable<T>(obj)
                       .ExecuteCommand() > -1;
        }

        /// <summary>
        ///     批量逻辑删除(带事务)
        /// </summary>
        /// <returns></returns>
        public bool SoftDeleteWithTran(List<int> ids)
        {
            var result = Db.Ado.UseTran(() =>
            {
                ids.ForEach(id =>
                {
                    var obj = GetDynamicObj(ConvertDynamicType.Delete);
                    obj.Add("Id", id);
                    Db.Updateable<T>(obj)
                        .ExecuteCommand();
                });
            });
            return result.IsSuccess;
        }

        /// <summary>
        ///     批量逻辑删除(不带事务)
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public bool SoftDeleteWithNoTran(List<int> ids)
        {
            var count = 0;
            ids.ForEach(id =>
            {
                var obj = GetDynamicObj(ConvertDynamicType.Delete);
                obj.Add("Id", id);
                var tempCount = Db.Updateable<T>(obj)
                    .ExecuteCommand();
                if (tempCount > -1)
                {
                    count++;
                }
            });
            return count > 0;
        }

        /// <summary>
        ///     物理删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Delete(int id)
        {
            return Db.Deleteable<T>().In(id).ExecuteCommand() > -1;
        }

        /// <summary>
        ///     批量物理删除(带事务)
        /// </summary>
        /// <returns></returns>
        public bool DeleteWithTran(List<int> ids)
        {
            var result = Db.Ado.UseTran(() => { Db.Deleteable<T>().In(ids).ExecuteCommand(); });
            return result.IsSuccess;
        }

        /// <summary>
        ///     批量物理删除(不带事务)
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public bool DeleteWithNoTran(List<int> ids)
        {
            return Db.Deleteable<T>().In(ids).ExecuteCommand() > -1;
        }

        /// <summary>
        ///     判断主键是否存在
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Exists(int id)
        {
            return Db.Queryable<T>().In(id).Any();
        }

        /// <summary>
        ///     根据主键获取id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public T GetByPk(int id)
        {
            return Db.Queryable<T>().In(id).First();
        }

        /// <summary>
        ///     根据主键获取列表
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public List<T> GetList(List<int> ids)
        {
            return Db.Queryable<T>().In(ids).ToList();
        }

        /// <summary>
        ///     分页方法
        /// </summary>
        /// <param name="page"></param>
        /// <param name="size"></param>
        /// <param name="sort"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public PageModel<T> GetByPage(int page, int size, string sort, List<DbCondition<T>> condition)
        {
            var type = typeof(T);
            var result = new PageModel<T>();
            var count = 0;
            var query = Db.Queryable<T>();
            // ReSharper disable once AccessToModifiedClosure
            condition.ForEach(item => { query = query.WhereIF(item.IsWhere, item.Expression); });
            if (type.GetProperty("Marks") != null)
            {
                query = query.Where("marks=1");
            }
            query = query
                .OrderByIF(!string.IsNullOrEmpty(sort), sort);
            var addTimeProp = type.GetProperty("AddTime");
            if (addTimeProp != null)
            {
                query = query
                    .OrderByIF(string.IsNullOrEmpty(sort), "addTime desc");
            }
            var list = query.ToPageList(page, size, ref count);
            result.Data = list;
            result.Total = count;
            return result;
        }

        private dynamic GetDynamicObj(T entity, ConvertDynamicType type)
        {
            dynamic obj = entity;
            var time = DateTime.Now;
            switch (type)
            {
                case ConvertDynamicType.Insert:
                    if (IsPropertyExist(obj, "UpdateTime"))
                    {
                        obj.UpdateTime = time;
                    }
                    if (IsPropertyExist(obj, "UpdateUser"))
                    {
                        obj.UpdateUser = _userContext.UserId;
                    }
                    if (IsPropertyExist(obj, "AddTime"))
                    {
                        obj.AddTime = time;
                    }
                    if (IsPropertyExist(obj, "AddUser"))
                    {
                        obj.AddUser = _userContext.UserId;
                    }
                    if (IsPropertyExist(obj, "Marks"))
                    {
                        obj.Marks = true;
                    }
                    break;
                case ConvertDynamicType.Update:
                    if (IsPropertyExist(obj, "UpdateTime"))
                    {
                        obj.UpdateTime = time;
                    }
                    if (IsPropertyExist(obj, "UpdateUser"))
                    {
                        obj.UpdateUser = _userContext.UserId;
                    }
                    if (IsPropertyExist(obj, "Marks"))
                    {
                        obj.Marks = true;
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
            return obj;
        }

        private List<string> GetIgnoreList()
        {
            var obj = new T();
            var list = new List<string>();
            if (IsPropertyExist(obj, "AddTime"))
            {
                list.Add("AddTime");
            }
            if (IsPropertyExist(obj, "AddUser"))
            {
                list.Add("AddUser");
            }
            return list;
        }

        private Dictionary<string, object> GetDynamicObj(ConvertDynamicType type)
        {
            var obj = new Dictionary<string, object>();
            var t = new T();
            var time = DateTime.Now;
            switch (type)
            {
                case ConvertDynamicType.Delete:
                    if (IsPropertyExist(t, "UpdateTime"))
                    {
                        obj.Add("UpdateTime", time);
                    }
                    if (IsPropertyExist(t, "UpdateUser"))
                    {
                        obj.Add("UpdateUser", _userContext.UserId);
                    }
                    if (IsPropertyExist(t, "Marks"))
                    {
                        obj.Add("Marks", false);
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
            return obj;
        }

        public bool IsPropertyExist(dynamic data, string propertyname)
        {
            if (data is ExpandoObject)
                return ((IDictionary<string, object>)data).ContainsKey(propertyname);
            return data.GetType().GetProperty(propertyname) != null;
        }
    }
}