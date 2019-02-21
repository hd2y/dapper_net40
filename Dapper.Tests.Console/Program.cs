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
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                CommandDefinition.OnConstruction += (c) =>
                {
                    System.Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(c, Newtonsoft.Json.Formatting.Indented));
                };
                OracleTests tests = new OracleTests("System.Data.OracleClient.OracleClientFactory, System.Data.OracleClient, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089", "***");

                #region 实体CRUD测试
                //string msg = string.Empty;
                //bool result = tests.Test(ref msg);
                //if (result == true)
                //{
                //    result = tests.TestList(ref msg);
                //}
                //if (result)
                //{
                //    System.Console.WriteLine("Oracle 数据库CRUD测试成功");
                //}
                //else
                //{
                //    System.Console.WriteLine($"Oracle 数据库CRUD测试失败:{msg}");
                //} 
                #endregion

                tests.TestPaged();

            }
            catch (Exception exc)
            {
                System.Console.WriteLine($"测试出现了未经处理的异常：{exc.Message}\r\n{exc.StackTrace}");
            }
            System.Console.ReadKey();
        }
    }
}
