using System.Collections.Generic;
using Project.Model;

namespace Project.IDAL
{
    /// <summary>
    /// 封装DB
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IManageBase<T>
    {
        /// <summary>
        /// 统一新增
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        int Insert(T entity);

        /// <summary>
        /// 批量插入(带事务)
        /// </summary>
        /// <param name="entitys"></param>
        /// <returns></returns>
        bool InsertWithTran(List<T> entitys);

        /// <summary>
        /// 批量插入(不带事务)
        /// </summary>
        /// <param name="entitys"></param>
        /// <returns></returns>
        bool InsertWithNoTran(List<T> entitys);

        /// <summary>
        /// 统一更新
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        bool Update(T entity);

        /// <summary>
        /// 批量更新(带事务)
        /// </summary>
        /// <param name="entitys"></param>
        /// <returns></returns>
        bool UpdateWithTran(List<T> entitys);

        /// <summary>
        /// 批量更新(不带事务)
        /// </summary>
        /// <param name="entitys"></param>
        /// <returns></returns>
        bool UpdateWithNoTran(List<T> entitys);

        /// <summary>
        /// 逻辑删除
        /// </summary>
        /// <returns></returns>
        bool SoftDelete(int id);

        /// <summary>
        /// 批量逻辑删除(带事务)
        /// </summary>
        /// <returns></returns>
        bool SoftDeleteWithTran(List<int> ids);

        /// <summary>
        /// 批量逻辑删除(不带事务)
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        bool SoftDeleteWithNoTran(List<int> ids);

        /// <summary>
        /// 物理删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool Delete(int id);

        /// <summary>
        /// 批量物理删除(带事务)
        /// </summary>
        /// <returns></returns>
        bool DeleteWithTran(List<int> ids);

        /// <summary>
        /// 批量物理删除(不带事务)
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        bool DeleteWithNoTran(List<int> ids);

        /// <summary>
        /// 判断主键是否存在
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool Exists(int id);

        /// <summary>
        /// 根据主键获取id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        T GetByPk(int id);

        /// <summary>
        /// 根据主键获取列表
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        List<T> GetList(List<int> ids);

        /// <summary>
        ///     分页方法
        /// </summary>
        /// <param name="page"></param>
        /// <param name="size"></param>
        /// <param name="sort"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        Model.Dtos.PageModel<T> GetByPage(int page, int size, string sort, List<DbCondition<T>> condition);
    }
}