using System.Collections.Generic;
using Project.IBLL;
using Project.IDAL;
using Project.Model;
using Project.Model.Conditions;
using Project.Model.Dtos;

namespace Project.BLL
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductService(IProductManage productManage, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        ///     新增产品
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int Insert(Product entity)
        {
            return _unitOfWork.ProductManage.Insert(entity);
        }

        /// <summary>
        ///     批量新增产品
        /// </summary>
        /// <param name="entitys"></param>
        /// <returns></returns>
        public bool Insert(List<Product> entitys)
        {
            return _unitOfWork.ProductManage.InsertWithNoTran(entitys);
        }

        /// <summary>
        ///     更新产品
        /// </summary>
        /// <returns></returns>
        public bool Update(Product entity)
        {
            return _unitOfWork.ProductManage.Update(entity);
        }

        /// <summary>
        ///     批量更新(不使用事务)
        /// </summary>
        /// <param name="entitys"></param>
        /// <returns></returns>
        public bool Update(List<Product> entitys)
        {
            return _unitOfWork.ProductManage.UpdateWithNoTran(entitys);
        }

        /// <summary>
        ///     逻辑删除产品
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool SoftDelete(int id)
        {
            return _unitOfWork.ProductManage.SoftDelete(id);
        }

        /// <summary>
        ///     批量逻辑删除产品(不使用事务)
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public bool SoftDelete(List<int> ids)
        {
            return _unitOfWork.ProductManage.SoftDeleteWithNoTran(ids);
        }

        /// <summary>
        ///     判断产品是否存在
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Exists(int id)
        {
            return _unitOfWork.ProductManage.Exists(id);
        }

        /// <summary>
        ///     根据主键获取实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Product GetByPk(int id)
        {
            return _unitOfWork.ProductManage.GetByPk(id);
        }

        /// <summary>
        ///     根据主键获取实体集合
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public List<Product> GetList(List<int> ids)
        {
            return _unitOfWork.ProductManage.GetList(ids);
        }

        /// <summary>
        ///     分页方法
        /// </summary>
        /// <param name="page"></param>
        /// <param name="size"></param>
        /// <param name="sort"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public PageModel<Product> GetByPage(int page, int size, string sort, ProductCondition condition)
        {
            var dbCondition = new List<DbCondition<Product>>
            {
                new DbCondition<Product>
                {
                    IsWhere = !string.IsNullOrEmpty(condition.Name),
                    Expression = o => o.Name.Contains(condition.Name)
                }
            };
            return _unitOfWork.ProductManage.GetByPage(page, size, sort, dbCondition);
        }
    }
}