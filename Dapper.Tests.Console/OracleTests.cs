using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Text;
using Dapper.Contrib.Extensions;

namespace Dapper.Tests.Console
{
    public class OracleTests
    {
        public IDbConnection Connection { get; set; }

        public OracleTests(string provider, string connStr)
        {
            DbProviderFactory dbProvider = Type.GetType("System.Data.OracleClient.OracleClientFactory, System.Data.OracleClient, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089").GetField("Instance", BindingFlags.Public | BindingFlags.Static).GetValue(null) as DbProviderFactory;
            Connection = dbProvider.CreateConnection();
            Connection.ConnectionString = connStr;
        }

        public bool Test(ref string msg)
        {
            if (Connection.State == ConnectionState.Closed)
                Connection.Open();
            using (IDbTransaction transaction = Connection.BeginTransaction())
            {
                try
                {
                    L_X_CANSHU canshu = new L_X_CANSHU() { CANSHUID = "0000", CANSHUMINGCHEN = "测试", DEFAULTVALUE = "0", BEIZHU = "测试" };
                    Connection.Insert(canshu, transaction);
                    Connection.Update(canshu, transaction);
                    canshu = Connection.Get<L_X_CANSHU>(canshu.CANSHUID, transaction);
                    Connection.Delete(canshu, transaction);
                    transaction?.Commit();
                    return true;
                }
                catch (Exception exc)
                {
                    transaction.Rollback();
                    msg += $"测试出现未经处理的异常：{exc.Message}\r\n{exc.StackTrace}\r\n";
                    return false;
                }
                finally
                {
                    if (Connection.State == ConnectionState.Open)
                        Connection.Close();
                }
            }
        }

        public bool TestList(ref string msg)
        {
            if (Connection.State == ConnectionState.Closed)
                Connection.Open();
            using (IDbTransaction transaction = Connection.BeginTransaction())
            {
                try
                {
                    List<L_X_CANSHU> canshus = new List<L_X_CANSHU>();
                    for (int i = 0; i < 100; i++)
                    {
                        canshus.Add(new L_X_CANSHU()
                        {
                            CANSHUID = i.ToString().PadLeft(4, '0'),
                            CANSHUMINGCHEN = $"测试{i}",
                            DEFAULTVALUE = $"{i}",
                            BEIZHU = $"测试{i}"
                        });
                    }
                    Connection.Insert(canshus, transaction);
                    Connection.Update(canshus, transaction);
                    L_X_CANSHU canshu = Connection.Get<L_X_CANSHU>(canshus[0].CANSHUID, transaction);
                    Connection.Delete(canshus, transaction);
                    transaction?.Commit();
                    return true;
                }
                catch (Exception exc)
                {
                    transaction.Rollback();
                    msg += $"测试出现未经处理的异常：{exc.Message}\r\n{exc.StackTrace}\r\n";
                    return false;
                }
                finally
                {
                    if (Connection.State == ConnectionState.Open)
                        Connection.Close();
                }
            }
        }

        public void TestPaged()
        {
            Paged<dynamic> page1 = Connection.QueryPage<dynamic>(1, 20, "select * from l_jianyanshuju where shehedate>:shehedate", new { shehedate = DateTime.Now.AddYears(-1) });
            System.Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(page1));
            Paged<dynamic> page2 = Connection.QueryPage<dynamic>(2, 20, "select * from l_jianyanshuju where shehedate>:shehedate", new { shehedate = DateTime.Now.AddYears(-1) });
            System.Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(page2));

            Paged<L_X_CANSHU> page3 = Connection.QueryPage<L_X_CANSHU>(1, 20, "select * from l_x_canshu where length(canshuid)>:length", new { length = 4 });
            System.Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(page3));
            Paged<L_X_CANSHU> page4 = Connection.QueryPage<L_X_CANSHU>(2, 20, "select * from l_x_canshu where length(canshuid)>:length", new { length = 4 });
            System.Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(page4));
        }
    }

    /// <summary>
    /// L_X_CANSHU 表实体类(LIS_系统_参数_核心_参数表)
    /// </summary>
    [Table("L_X_CANSHU")]
    public class L_X_CANSHU
    {
        /// <summary>
        /// CANSHUID
        /// </summary>
        [Key]
        public string CANSHUID { get; set; }

        /// <summary>
        /// CANSHUMINGCHEN
        /// </summary>
        public string CANSHUMINGCHEN { get; set; }

        /// <summary>
        /// DEFAULTVALUE
        /// </summary>
        public string DEFAULTVALUE { get; set; }

        /// <summary>
        /// BEIZHU
        /// </summary>
        public string BEIZHU { get; set; }

    }
}
