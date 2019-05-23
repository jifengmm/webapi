using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Project.Model
{
    public enum AttributeName
    {
        PropertyDetailType
    }

    public enum ConvertDynamicType
    {
        Insert,
        Update,
        Delete
    }

    /// <summary>
    /// 资产明细类型
    /// </summary>
    public enum Status
    {
        [Description("待上传")]
        Waiting = 0,
        [Description("已上传")]
        Ok = 1,
        [Description("上传失败")]
        Error = -1,
    }
}
