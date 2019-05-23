//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由T4模板自动生成
//     生成时间 05/06/2019 13:27:04 By 朱峰
//     对此文件的更改可能会导致不正确的行为，并且如果
//     文件已存在，不会覆盖。
// </auto-generated>
//------------------------------------------------------------------------------

using System.Collections.Generic;
using Project.IBLL;
using Project.IDAL;
using Project.Model;
using Project.Model.Conditions;
using Project.Model.Dtos;

namespace Project.BLL
{
    public class FtpConfigService : IFtpConfigService
    {
        private readonly IUnitOfWork _unitOfWork;

        public FtpConfigService(IFtpConfigManage ftpConfigManage, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        ///     新增FtpConfig
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int Insert(FtpConfig entity)
        {
            return _unitOfWork.FtpConfigManage.Insert(entity);
        }

        /// <summary>
        ///     批量新增FtpConfig
        /// </summary>
        /// <param name="entitys"></param>
        /// <returns></returns>
        public bool Insert(List<FtpConfig> entitys)
        {
            return _unitOfWork.FtpConfigManage.InsertWithNoTran(entitys);
        }

        /// <summary>
        ///     更新FtpConfig
        /// </summary>
        /// <returns></returns>
        public bool Update(FtpConfig entity)
        {
            return _unitOfWork.FtpConfigManage.Update(entity);
        }

        /// <summary>
        ///     批量更新(不使用事务)
        /// </summary>
        /// <param name="entitys"></param>
        /// <returns></returns>
        public bool Update(List<FtpConfig> entitys)
        {
            return _unitOfWork.FtpConfigManage.UpdateWithNoTran(entitys);
        }

        /// <summary>
        ///     逻辑删除FtpConfig
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool SoftDelete(int id)
        {
            return _unitOfWork.FtpConfigManage.SoftDelete(id);
        }

        /// <summary>
        ///     批量逻辑删除FtpConfig(不使用事务)
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public bool SoftDelete(List<int> ids)
        {
            return _unitOfWork.FtpConfigManage.SoftDeleteWithNoTran(ids);
        }

        /// <summary>
        ///     判断FtpConfig是否存在
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Exists(int id)
        {
            return _unitOfWork.FtpConfigManage.Exists(id);
        }

        /// <summary>
        ///     根据主键获取实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public FtpConfig GetByPk(int id)
        {
            return _unitOfWork.FtpConfigManage.GetByPk(id);
        }

        /// <summary>
        ///     根据主键获取实体集合
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public List<FtpConfig> GetList(List<int> ids)
        {
            return _unitOfWork.FtpConfigManage.GetList(ids);
        }

        /// <summary>
        ///     分页方法
        /// </summary>
        /// <param name="page"></param>
        /// <param name="size"></param>
        /// <param name="sort"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public PageModel<FtpConfig> GetByPage(int page, int size, string sort, FtpConfigCondition condition)
        {
            var dbCondition = new List<DbCondition<FtpConfig>>
            {
                new DbCondition<FtpConfig>
                {
                    IsWhere = !string.IsNullOrEmpty(condition.Keyword),
                    Expression = o=>o.Name.Contains(condition.Keyword)
                }
            };
            return _unitOfWork.FtpConfigManage.GetByPage(page, size, sort, dbCondition);
        }
    }
}
