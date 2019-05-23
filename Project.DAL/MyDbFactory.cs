using SqlSugar;

namespace Project.DAL
{
    public static class MyDbFactory
    {
        private static string MysqlConnection { get; set; }
        private static string SqlServerConnection { get; set; }       
        private static string SqliteConnection { get; set; }
        private static string OracleConnection { get; set; }
        private static string PostgreSQLConnection { get; set; }
        public static void Setup(string mysqlConnection, string sqlServerConnection = "" , string sqliteConnection = "" ,string oracleConnection="",string postgreSQLConnection = "")
        {
            MysqlConnection = mysqlConnection;
            SqlServerConnection = sqlServerConnection;
            SqliteConnection = sqliteConnection;
            OracleConnection = oracleConnection;
            PostgreSQLConnection = postgreSQLConnection;
        }

        public static SqlSugarClient GetDatabase(DbType dbType = DbType.MySql)
        {
            string connectionString = "";
            switch (dbType)
            {
                case DbType.MySql:
                    connectionString = MysqlConnection;
                    break;
                case DbType.SqlServer:
                    connectionString = SqlServerConnection;
                    break;
                case DbType.Sqlite:
                    connectionString = SqliteConnection;
                    break;
                case DbType.Oracle:
                    connectionString = OracleConnection;
                    break;
                case DbType.PostgreSQL:
                    connectionString = PostgreSQLConnection;
                    break;

            }

            SqlSugarClient db = new SqlSugarClient(new ConnectionConfig()
            {
                ConnectionString = connectionString, //必填
                DbType = dbType, //必填
                IsAutoCloseConnection = true, //默认false
                     InitKeyType = InitKeyType.SystemTable
            }); //默认SystemTable
            return db;
        }
    }
}
