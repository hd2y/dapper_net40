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
            DbProviderFactory dbProvider = Type.GetType("System.Data.OracleClient.OracleClientFactory, System.Data.OracleClient, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089").GetField("Instance", BindingFlags.Public | BindingFlags.Static).GetValue(null) as DbProviderFactory;
            CommandDefinition.OnConstruction += (c) =>
            {
                string text = c.CommandText;
                System.Console.WriteLine(text);
            };
            using (IDbConnection connection = dbProvider.CreateConnection())
            {
                connection.ConnectionString = "***";
                connection.GetAll<X_CANSHUDENGJI>();
            }
            System.Console.ReadKey();
        }
    }

    [Table("X_CANSHUDENGJI")]
    public class X_CANSHUDENGJI
    {
        [Key]
        public string CANSHUDENGJIID { get; set; }
    }
}
