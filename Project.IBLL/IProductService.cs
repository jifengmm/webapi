using System.Collections.Generic;
using Project.Model;
using Project.Model.Conditions;
using Project.Model.Dtos;

namespace Project.IBLL
{
    public interface IProductService
    {
        /// <summary>
        /// 新增产品
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        int Insert(Product entity);

        /// <summary>
        ///     批量新增产品
        /// </summary>
        /// <param name="entitys"></param>
        /// <returns></returns>
        bool Insert(List<Product> entitys);

        /// <summary>
        /// 更新产品
        /// </summary>
        /// <returns></returns>
        bool Update(Product entity);

        /// <summary>
        /// 批量更新(不使用事务)
        /// </summary>
        /// <param name="entitys"></param>
        /// <returns></returns>
        bool Update(List<Product> entitys);

        /// <summary>
        /// 逻辑删除产品
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool SoftDelete(int id);

        /// <summary>
        /// 批量逻辑删除产品(不使用事务)
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        bool SoftDelete(List<int> ids);

        /// <summary>
        /// 判断产品是否存在
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool Exists(int id);

        /// <summary>
        ///     根据主键获取id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Product GetByPk(int id);

        /// <summary>
        ///     根据主键获取id
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        List<Product> GetList(List<int> ids);

        /// <summary>
        ///     分页方法
        /// </summary>
        /// <param name="page"></param>
        /// <param name="size"></param>
        /// <param name="sort"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        PageModel<Product> GetByPage(int page, int size, string sort, ProductCondition condition);
    }
}