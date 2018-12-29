using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyHabits.Common;
using Dapper;
using MySql.Data.MySqlClient;

namespace MyHabits.DataAccess
{
    /// <summary>
    /// 封装对数据库连接或访问的一系列操作
    /// </summary>
    public static class DbHelper
    {
        static DbHelper()
        {
            //Dapper.SimpleCRUD.SetDialect(Dapper.SimpleCRUD.Dialect.SQLServer);
        }

        /// <summary>
        /// 获得数据库连接
        /// </summary>
        /// <param name="dbType">数据库类型，默认mysql</param>
        /// <returns></returns>
        public static IDbConnection GetConnection(DataBase dbType = DataBase.Mssql)
        {
            switch (dbType)
            {
                case DataBase.Mssql:
                    Dapper.SimpleCRUD.SetDialect(Dapper.SimpleCRUD.Dialect.SQLServer);
                    return SqlConnection();
                case DataBase.Mysql:
                    Dapper.SimpleCRUD.SetDialect(Dapper.SimpleCRUD.Dialect.MySQL);
                    return MySqlConnection();
                case DataBase.Oracle:
                    throw new NotImplementedException("未实现与Oracle数据库互连");
                default:
                    throw new ArgumentOutOfRangeException(nameof(dbType), dbType, "未找到你选择的数据库驱动！");
            }
        }

        /// <summary>
        /// 创建并返回一个sqlserver连接
        /// </summary>
        /// <returns></returns>
        private static SqlConnection SqlConnection()
        {
            var sqlconnectionString = ConfigurationManager.ConnectionStrings["sqlDB"].ToString();
            var connection = new SqlConnection(sqlconnectionString);
            connection.Open();
            return connection;
        }

        /// <summary>
        /// 创建并返回一个mysql连接
        /// </summary>
        /// <returns></returns>
        private static MySqlConnection MySqlConnection()
        {
            var mysqlconnectionString = ConfigurationManager.ConnectionStrings["mysqlDB"].ToString();
            var connection = new MySqlConnection(mysqlconnectionString);
            connection.Open();
            return connection;
        }

        /// <summary>
        /// 插入一条记录，返回自增的int类型主键值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static int? Insert<T>(T entity) where T : class
        {
            return ExcuateByTransaction((conn, trans) => conn.Insert(entity, trans));
        }

        /// <summary>
        /// 插入一条新记录，返回主键值（单主键或复合主键）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TKey">主键类型</typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static TKey Insert<TKey, T>(T entity) where T : class
        {
            return ExcuateByTransaction((conn, trans) => conn.Insert<TKey, T>(entity, trans));
        }

        /// <summary>
        /// 批量插入记录，返回主键集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entities">要添加的实体集合</param>
        /// <returns></returns>
        public static List<dynamic> InsertBatch<T>(IEnumerable<T> entities) where T : class
        {
            return ExcuateByTransaction((conn, trans) =>
            {
                return entities.Select(entity => conn.Insert<object, T>(entity, trans)).ToList();
            });
        }

        /// <summary>
        /// 更新一条记录，注意实体应带有主键值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static bool Update<T>(T entity) where T : class
        {
            return ExcuateByTransaction((conn, trans) => conn.Update(entity, trans) > 0);
        }

        /// <summary>
        /// 批量修改
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entities">要修改的实体集合，注意集合中的实体应带有主键值</param>
        /// <returns></returns>
        public static bool UpdateBatch<T>(IEnumerable<T> entities) where T : class
        {
            return ExcuateByTransaction((conn, trans) =>
            {
                int rows = 0;
                var enumerable = entities as T[] ?? entities.ToArray();
                foreach (var entity in enumerable)
                {
                    conn.Update(entity, trans);
                    rows += 1;
                }
                return rows == enumerable.Length;
            });
        }

        /// <summary>
        /// 删除一条记录
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static bool Delete<T>(T entity) where T : class
        {
            return ExcuateByTransaction((conn, trans) => conn.Delete(entity, trans) > 0);
        }

        /// <summary>
        /// 根据实体集合批量删除记录
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entities">要删除的实体集合</param>
        /// <returns></returns>
        public static bool DeleteBatch<T>(IEnumerable<T> entities) where T : class
        {
            return ExcuateByTransaction((conn, trans) =>
            {
                int rows = 0;
                var enumerable = entities as T[] ?? entities.ToArray();
                foreach (var entity in enumerable)
                {
                    conn.Delete<T>(entity, trans);
                    rows += 1;
                }
                return rows == enumerable.Length;
            });
        }

        /// <summary>
        /// 根据对象条件批量删除记录，example：new { Age = 10 }
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="whereConditions"></param>
        /// <returns></returns>
        public static bool DeleteBatch<T>(object whereConditions) where T : class
        {
            return ExcuateByTransaction((conn, trans) => conn.DeleteList<T>(whereConditions, trans) > 0);
        }

        /// <summary>
        /// 根据条件批量删除记录，example："where Age > 20"
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="conditions"></param>
        /// <param name="parms"></param>
        /// <returns></returns>
        public static bool DeleteBatch<T>(string conditions, object parms = null) where T : class
        {
            return ExcuateByTransaction((conn, trans) => conn.DeleteList<T>(conditions, parms, trans) > 0);
        }

        /// <summary>
        /// 根据条件获取记录总数，example："where Age > 20"
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="conditions"></param>
        /// <param name="parms"></param>
        /// <returns></returns>
        public static int Count<T>(string conditions, object parms = null) where T : class
        {
            try
            {
                using (var conn = GetConnection())
                {
                    return conn.RecordCount<T>(conditions, parms);
                }
            }
            catch (Exception e)
            {
                var c = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error:" + e.Message);
                LogHelper.Error(e.Message, e);
                Console.ForegroundColor = c;
                throw e;
            }
        }

        /// <summary>
        /// 根据对象条件获取记录总数，example：new { Age = 10 }
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="whereConditions"></param>
        /// <returns></returns>
        public static int Count<T>(object whereConditions) where T : class
        {
            try
            {
                using (var conn = GetConnection())
                {
                    return conn.RecordCount<T>(whereConditions);
                }
            }
            catch (Exception e)
            {
                var c = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error:" + e.Message);
                LogHelper.Error(e.Message, e);
                Console.ForegroundColor = c;
                throw e;
            }
        }

        /// <summary>
        /// 单语句查询
        /// </summary>
        /// <typeparam name="T">返回值类型</typeparam>
        /// <param name="sql">查询语句</param>
        /// <param name="parms">参数</param>
        /// <returns></returns>
        public static List<T> Query<T>(string sql, object parms = null)
        {
            try
            {
                using (var conn = GetConnection())
                {
                    return conn.Query<T>(sql, parms).AsList();
                }
            }
            catch (Exception e)
            {
                var c = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error:" + e.Message);
                LogHelper.Error(e.Message, e);
                Console.ForegroundColor = c;
                throw e;
            }
        }

        /// <summary>
        /// 两个表连接查询
        /// </summary>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecond"></typeparam>
        /// <typeparam name="TReturn"></typeparam>
        /// <param name="sql"></param>
        /// <param name="map"></param>
        /// <param name="splitOn">用来指定列为分隔列，之前的列为前一对象，之后的列为后一对象。 </param>
        /// <param name="parms"></param>
        /// <returns></returns>
        public static List<TReturn> Query<TFirst, TSecond, TReturn>(string sql, Func<TFirst, TSecond, TReturn> map, object parms = null, string splitOn = "Id")
        {
            try
            {
                using (var conn = GetConnection())
                {
                    return conn.Query(sql, map, parms, splitOn: splitOn).AsList();
                }
            }
            catch (Exception e)
            {
                var c = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error:" + e.Message);
                LogHelper.Error(e.Message, e);
                Console.ForegroundColor = c;
                throw e;
            }
        }

        /// <summary>
        /// 三个表连接查询
        /// </summary>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecond"></typeparam>
        /// <typeparam name="TThird"></typeparam>
        /// <typeparam name="TReturn"></typeparam>
        /// <param name="sql"></param>
        /// <param name="map"></param>
        /// <param name="splitOn">用来指定列为分隔列，之前的列为前一对象，之后的列为后一对象。 </param>
        /// <param name="parms"></param>
        /// <returns></returns>
        public static List<TReturn> Query<TFirst, TSecond, TThird, TReturn>(string sql, Func<TFirst, TSecond, TThird, TReturn> map, object parms = null, string splitOn = "Id")
        {
            try
            {
                using (var conn = GetConnection())
                {
                    return conn.Query(sql, map, parms, splitOn: splitOn).AsList();
                }
            }
            catch (Exception e)
            {
                var c = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error:" + e.Message);
                LogHelper.Error(e.Message, e);
                Console.ForegroundColor = c;
                throw e;
            }
        }

        /// <summary>
        /// 四个表连接查询
        /// </summary>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecond"></typeparam>
        /// <typeparam name="TThird"></typeparam>
        /// <typeparam name="TFourth"></typeparam>
        /// <typeparam name="TReturn"></typeparam>
        /// <param name="sql"></param>
        /// <param name="map"></param>
        /// <param name="splitOn">用来指定列为分隔列，之前的列为前一对象，之后的列为后一对象。 </param>
        /// <param name="parms"></param>
        /// <returns></returns>
        public static List<TReturn> Query<TFirst, TSecond, TThird, TFourth, TReturn>(string sql, Func<TFirst, TSecond, TThird, TFourth, TReturn> map, object parms = null, string splitOn = "Id")
        {
            try
            {
                using (var conn = GetConnection())
                {
                    return conn.Query(sql, map, parms, splitOn: splitOn).AsList();
                }
            }
            catch (Exception e)
            {
                var c = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error:" + e.Message);
                LogHelper.Error(e.Message, e);
                Console.ForegroundColor = c;
                throw e;
            }
        }

        /// <summary>
        /// 五个表连接查询
        /// </summary>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecond"></typeparam>
        /// <typeparam name="TThird"></typeparam>
        /// <typeparam name="TFourth"></typeparam>
        /// <typeparam name="TFifth"></typeparam>
        /// <typeparam name="TReturn"></typeparam>
        /// <param name="sql"></param>
        /// <param name="map"></param>
        /// <param name="splitOn">用来指定列为分隔列，之前的列为前一对象，之后的列为后一对象。 </param>
        /// <param name="parms"></param>
        /// <returns></returns>
        public static List<TReturn> Query<TFirst, TSecond, TThird, TFourth, TFifth, TReturn>(string sql, Func<TFirst, TSecond, TThird, TFourth, TFifth, TReturn> map, object parms = null, string splitOn = "Id")
        {
            try
            {
                using (var conn = GetConnection())
                {
                    return conn.Query(sql, map, parms, splitOn: splitOn).AsList();
                }
            }
            catch (Exception e)
            {
                var c = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error:" + e.Message);
                LogHelper.Error(e.Message, e);
                Console.ForegroundColor = c;
                throw e;
            }
        }

        /// <summary>
        /// 六个表连接查询
        /// </summary>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecond"></typeparam>
        /// <typeparam name="TThird"></typeparam>
        /// <typeparam name="TFourth"></typeparam>
        /// <typeparam name="TFifth"></typeparam>
        /// <typeparam name="TSixth"></typeparam>
        /// <typeparam name="TReturn"></typeparam>
        /// <param name="sql"></param>
        /// <param name="map"></param>
        /// <param name="splitOn">用来指定列为分隔列，之前的列为前一对象，之后的列为后一对象。 </param>
        /// <param name="parms"></param>
        /// <returns></returns>
        public static List<TReturn> Query<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TReturn>(string sql, Func<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TReturn> map, object parms = null, string splitOn = "Id")
        {
            try
            {
                using (var conn = GetConnection())
                {
                    return conn.Query(sql, map, parms, splitOn: splitOn).AsList();
                }
            }
            catch (Exception e)
            {
                var c = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error:" + e.Message);
                LogHelper.Error(e.Message, e);
                Console.ForegroundColor = c;
                throw e;
            }
        }

        /// <summary>
        /// 七个表连接查询
        /// </summary>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecond"></typeparam>
        /// <typeparam name="TThird"></typeparam>
        /// <typeparam name="TFourth"></typeparam>
        /// <typeparam name="TFifth"></typeparam>
        /// <typeparam name="TSixth"></typeparam>
        /// <typeparam name="TSeventh"></typeparam>
        /// <typeparam name="TReturn"></typeparam>
        /// <param name="sql"></param>
        /// <param name="map"></param>
        /// <param name="splitOn">用来指定列为分隔列，之前的列为前一对象，之后的列为后一对象。 </param>
        /// <param name="parms"></param>
        /// <returns></returns>
        public static List<TReturn> Query<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TReturn>(string sql, Func<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TReturn> map, object parms = null, string splitOn = "Id")
        {
            try
            {
                using (var conn = GetConnection())
                {
                    return conn.Query(sql, map, parms, splitOn: splitOn).AsList();
                }
            }
            catch (Exception e)
            {
                var c = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error:" + e.Message);
                LogHelper.Error(e.Message, e);
                Console.ForegroundColor = c;
                throw e;
            }
        }

        /// <summary>
        /// 查询2条SQL语句并返回值，example: sql = "select * from A;select * from B"，
        /// </summary>
        /// <typeparam name="T1">第一条语句返回集合类型</typeparam>
        /// <typeparam name="T2">第二条语句返回集合类型</typeparam>
        /// <param name="sql">sql语句</param>
        /// <param name="param">参数化值</param>
        /// <returns></returns>
        public static Tuple<IEnumerable<T1>, IEnumerable<T2>> QueryMultiple<T1, T2>(string sql, object param)
        {
            IEnumerable<T1> item1 = null; IEnumerable<T2> item2 = null;
            if (string.IsNullOrEmpty(sql))
                return Tuple.Create<IEnumerable<T1>, IEnumerable<T2>>(new List<T1>(), new List<T2>());

            ExcuateByTransaction((conn, trans) =>
            {
                using (var multi = conn.QueryMultiple(sql, param, trans))
                {
                    item1 = multi.Read<T1>() ?? new List<T1>();
                    item2 = multi.Read<T2>() ?? new List<T2>();
                }
                return null;
            });
            return Tuple.Create(item1, item2);
        }

        /// <summary>
        /// 查询3条SQL语句并返回值，example: sql = "select * from A;select * from B;select * from B;select * from B;select * from C"，
        /// </summary>
        /// <typeparam name="T1">第一条语句返回集合类型</typeparam>
        /// <typeparam name="T2">第二条语句返回集合类型</typeparam>
        /// <typeparam name="T3">第三条语句返回集合类型</typeparam>
        /// <param name="sql">sql语句</param>
        /// <param name="param">参数化值</param>
        /// <returns></returns>
        public static Tuple<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>> QueryMultiple<T1, T2, T3>(string sql, object param)
        {
            IEnumerable<T1> item1 = null; IEnumerable<T2> item2 = null; IEnumerable<T3> item3 = null;
            if (string.IsNullOrEmpty(sql))
                return Tuple.Create<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>>(new List<T1>(), new List<T2>(), new List<T3>());

            ExcuateByTransaction((conn, trans) =>
            {
                using (var multi = conn.QueryMultiple(sql, param, trans))
                {
                    item1 = multi.Read<T1>() ?? new List<T1>();
                    item2 = multi.Read<T2>() ?? new List<T2>();
                    item3 = multi.Read<T3>() ?? new List<T3>();
                }
                return null;
            });
            return Tuple.Create(item1, item2, item3);
        }

        /// <summary>
        /// 查询4条SQL语句并返回值，example: sql = "select * from A;select * from B;select * from B;select * from B;select * from C;select * from B;select * from D"，
        /// </summary>
        /// <typeparam name="T1">第一条语句返回集合类型</typeparam>
        /// <typeparam name="T2">第二条语句返回集合类型</typeparam>
        /// <typeparam name="T3">第三条语句返回集合类型</typeparam>
        /// <typeparam name="T4">第四条语句返回集合类型</typeparam>
        /// <param name="sql">sql语句</param>
        /// <param name="param">参数化值</param>
        /// <returns></returns>
        public static Tuple<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>, IEnumerable<T4>> QueryMultiple<T1, T2, T3, T4>(string sql, object param)
        {
            IEnumerable<T1> item1 = null; IEnumerable<T2> item2 = null; IEnumerable<T3> item3 = null; IEnumerable<T4> item4 = null;
            if (string.IsNullOrEmpty(sql))
                return Tuple.Create<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>, IEnumerable<T4>>(new List<T1>(), new List<T2>(), new List<T3>(), new List<T4>());

            ExcuateByTransaction((conn, trans) =>
            {
                using (var multi = conn.QueryMultiple(sql, param, trans))
                {
                    item1 = multi.Read<T1>() ?? new List<T1>();
                    item2 = multi.Read<T2>() ?? new List<T2>();
                    item3 = multi.Read<T3>() ?? new List<T3>();
                    item4 = multi.Read<T4>() ?? new List<T4>();
                }
                return null;
            });
            return Tuple.Create(item1, item2, item3, item4);
        }

        /// <summary>
        /// 查询5条SQL语句并返回值，example: sql = "select * from A;select * from B;select * from B;select * from B;select * from C;select * from B;select * from D"，
        /// </summary>
        /// <typeparam name="T1">第一条语句返回集合类型</typeparam>
        /// <typeparam name="T2">第二条语句返回集合类型</typeparam>
        /// <typeparam name="T3">第三条语句返回集合类型</typeparam>
        /// <typeparam name="T4">第四条语句返回集合类型</typeparam>
        /// <typeparam name="T5">第五条语句返回集合类型</typeparam>
        /// <param name="sql">sql语句</param>
        /// <param name="param">参数化值</param>
        /// <returns></returns>
        public static Tuple<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>, IEnumerable<T4>, IEnumerable<T5>> QueryMultiple<T1, T2, T3, T4, T5>(string sql, object param)
        {
            IEnumerable<T1> item1 = null; IEnumerable<T2> item2 = null; IEnumerable<T3> item3 = null; IEnumerable<T4> item4 = null; IEnumerable<T5> item5 = null;
            if (string.IsNullOrEmpty(sql))
                return Tuple.Create<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>, IEnumerable<T4>, IEnumerable<T5>>(new List<T1>(), new List<T2>(), new List<T3>(), new List<T4>(), new List<T5>());

            ExcuateByTransaction((conn, trans) =>
            {
                using (var multi = conn.QueryMultiple(sql, param, trans))
                {
                    item1 = multi.Read<T1>() ?? new List<T1>();
                    item2 = multi.Read<T2>() ?? new List<T2>();
                    item3 = multi.Read<T3>() ?? new List<T3>();
                    item4 = multi.Read<T4>() ?? new List<T4>();
                    item5 = multi.Read<T5>() ?? new List<T5>();
                }
                return null;
            });
            return Tuple.Create(item1, item2, item3, item4, item5);
        }

        /// <summary>
        /// 执行存储过程查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="procName"></param>
        /// <param name="parms"></param>
        /// <returns></returns>
        public static List<T> QueryByProc<T>(string procName, object parms = null)
        {
            try
            {
                using (var conn = GetConnection())
                {
                    return conn.Query<T>(procName, parms, commandType: CommandType.StoredProcedure).AsList();
                }
            }
            catch (Exception e)
            {
                var c = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error:" + e.Message);
                LogHelper.Error(e.Message, e);
                Console.ForegroundColor = c;
                throw e;
            }
        }

        /// <summary>
        /// 单语句查询分页
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <param name="parms"></param>
        /// <returns></returns>
        public static List<T> QueryByPage<T>(string sql, int pageIndex, int pageSize, out int totalCount, object parms = null)
        {
            try
            {
                using (var conn = GetConnection())
                {
                    var all = conn.Query<T>(sql, parms).AsList();
                    totalCount = all.Count;
                    return all.Skip((pageIndex - 1) * pageSize).Take(pageSize).AsList();
                }
            }
            catch (Exception e)
            {
                var c = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error:" + e.Message);
                LogHelper.Error(e.Message, e);
                Console.ForegroundColor = c;
                throw e;
            }
        }

        /// <summary>
        /// 根据条件返回表记录，example：new User { Name = "Jack" }
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="whereConditions">实体条件</param>
        /// <returns></returns>
        public static List<T> GetList<T>(object whereConditions) where T : class
        {
            try
            {
                using (var conn = GetConnection())
                {
                    return conn.GetList<T>(whereConditions).AsList();
                }
            }
            catch (Exception e)
            {
                var c = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error:" + e.Message);
                LogHelper.Error(e.Message, e);
                Console.ForegroundColor = c;
                throw e;
            }
        }

        /// <summary>
        /// 获取表所有记录
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static List<T> GetAll<T>() where T : class
        {
            try
            {
                using (var conn = GetConnection())
                {
                    return conn.GetList<T>().AsList();
                }
            }
            catch (Exception e)
            {
                var c = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error:" + e.Message);
                LogHelper.Error(e.Message, e);
                Console.ForegroundColor = c;
                throw e;
            }
        }

        /// <summary>
        /// 分页查询，example：GetListPaged&lt;User&gt;(1,10,"where age = @Age","Name desc", new {Age = 10}); 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="allRowsCount"></param>
        /// <param name="conditions"></param>
        /// <param name="orderby"></param>
        /// <param name="parms"></param>
        /// <returns></returns>
        public static List<T> GetPageList<T>(int pageIndex, int pageSize, out int allRowsCount, string conditions, string orderby, object parms = null) where T : class
        {
            try
            {
                using (var conn = GetConnection())
                {
                    allRowsCount = conn.RecordCount<T>(conditions, parms);
                    return conn.GetListPaged<T>(pageIndex, pageSize, conditions, orderby, parms).AsList();
                }
            }
            catch (Exception e)
            {
                var c = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error:" + e.Message);
                LogHelper.Error(e.Message, e);
                Console.ForegroundColor = c;
                throw e;
            }
        }

        /// <summary>
        /// 单表查询分页
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public static List<T> GetAllByPage<T>(int pageIndex, int pageSize, out int totalCount) where T : class
        {
            try
            {
                using (var conn = GetConnection())
                {
                    var all = conn.GetList<T>().AsList();
                    totalCount = all.Count;
                    return all.Skip((pageIndex - 1) * pageSize).Take(pageSize).AsList();
                }
            }
            catch (Exception e)
            {
                var c = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error:" + e.Message);
                LogHelper.Error(e.Message, e);
                Console.ForegroundColor = c;
                throw e;
            }
        }

        /// <summary>
        /// 根据主键获取实体，注意参数类型是object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="primaryKey">主键</param>
        /// <returns></returns>
        public static T Get<T>(object primaryKey) where T : class
        {
            try
            {
                using (var conn = GetConnection())
                {
                    return conn.Get<T>(primaryKey);
                }
            }
            catch (Exception e)
            {
                var c = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error:" + e.Message);
                LogHelper.Error(e.Message, e);
                Console.ForegroundColor = c;
                throw e;
            }

        }

        /// <summary>
        /// 根据对象条件获取单个实体，example：new User { Name = "Jack" }
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="whereConditions"></param>
        /// <returns></returns>
        public static T First<T>(object whereConditions) where T : class
        {
            try
            {
                using (var conn = GetConnection())
                {
                    return conn.GetList<T>(whereConditions).FirstOrDefault();
                }
            }
            catch (Exception e)
            {
                var c = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error:" + e.Message);
                LogHelper.Error(e.Message, e);
                Console.ForegroundColor = c;
                throw e;
            }
        }

        /// <summary>
        /// 根据条件获取一条记录，example："where Age > 20"
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="conditions"></param>
        /// <param name="parms"></param>
        /// <returns></returns>
        public static T First<T>(string conditions, object parms = null) where T : class
        {
            try
            {
                using (var conn = GetConnection())
                {
                    return conn.GetList<T>(conditions, parms).FirstOrDefault();
                }
            }
            catch (Exception e)
            {
                var c = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error:" + e.Message);
                LogHelper.Error(e.Message, e);
                Console.ForegroundColor = c;
                throw e;
            }
        }

        /// <summary>
        /// 执行sql返回单个实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="parms"></param>
        /// <returns></returns>
        public static T Get<T>(string sql, object parms = null)
        {
            try
            {
                using (var conn = GetConnection())
                {
                    return conn.QueryFirstOrDefault<T>(sql, parms);
                }
            }
            catch (Exception e)
            {
                var c = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error:" + e.Message);
                LogHelper.Error(e.Message, e);
                Console.ForegroundColor = c;
                throw e;
            }
        }

        /// <summary>
        /// 执行sql判断记录是否存在
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parms"></param>
        /// <returns></returns>
        public static bool Exists(string sql, object parms = null)
        {
            try
            {
                using (var conn = GetConnection())
                {
                    return conn.QueryFirstOrDefault(sql, parms) != null;
                }
            }
            catch (Exception e)
            {
                var c = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error:" + e.Message);
                LogHelper.Error(e.Message, e);
                Console.ForegroundColor = c;
                throw e;
            }
        }

        /// <summary>
        /// 使用事务执行sql语句
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parms"></param>
        /// <returns></returns>
        public static int Execute(string sql, object parms = null)
        {
            return ExcuateByTransaction((conn, trans) => conn.Execute(sql, parms, trans));
        }

        /// <summary>
        /// 使用事务执行
        /// </summary>
        /// <param name="action">要执行的操作</param>
        /// <returns></returns>
        public static dynamic ExcuateByTransaction(Func<IDbConnection, IDbTransaction, dynamic> action)
        {
            try
            {
                using (var conn = GetConnection())
                {
                    Console.WriteLine("DataBase：" + conn.Database);
                    Console.WriteLine("Action：" + action.Method.Name);

                    var trans = conn.BeginTransaction();
                    var result = action(conn, trans); ;
                    trans.Commit();
                    return result;
                }
            }
            catch (Exception e)
            {
                var c = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error:" + e.Message);
                LogHelper.Error(e.Message, e);
                Console.ForegroundColor = c;
                throw e;
            }
        }


    }

    /// <summary>
    /// 数据库类型
    /// </summary>
    public enum DataBase
    {
        /// <summary>
        /// ms sqlserver 数据库
        /// </summary>
        Mssql,
        /// <summary>
        /// mysql数据库
        /// </summary>
        Mysql,
        /// <summary>
        /// oracle数据库
        /// </summary>
        Oracle
    }
}
