# Aspnetcore 项目说明

## 安装准备
> 安装 [.NET Core SDK](https://www.microsoft.com/net/learn/get-started-with-dotnet-tutorial)
>
> VScode需安装 `C# for Visual Studio Code` 官方插件

## 自动生成实体和CRUD方法
> 在文件 `GenerateConfig.xml` 添加实体和表对应关系
```xml
    <Table ClassName="Roles">cm_roles</Table>
```
> 对文件 `Project.AutoGenerateCode/GenerateCode.tt` 保存或右键运行

## 各层代码编写要求
> Project.WebApi 
* 路由写法符合 Restful 标准
* 禁止数据库实体model直接返回，所有有对象的返回值需写Dto
* 禁止编写复杂的业务逻辑
* summary注释写完整，具体实现可添加其他注释

> Project.BLL 
* 项目的业务逻辑都在此层完成
* 禁止直接与数据库相关操作

> Project.DAL
* 注意程序级事务的使用
* 注意查看框架生成的SQL是否符合要求，是否有性能问题

> Project.UnitTest
* 每个Controller中的方法均需要些测试用例