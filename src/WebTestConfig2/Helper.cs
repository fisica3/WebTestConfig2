using Microsoft.Extensions.Configuration;
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebTestConfig2
{
    public enum ConnPrefix
    {
        SqlDatabase,
        SqlServer,
        MySql,
        Custom,
        Empty
    }

    public static class EnumUtil
    {
        public static IEnumerable<T> GetValues<T>()
        {
            return Enum.GetValues(typeof(T)).Cast<T>();
        }
    }
    static public class HelperConfig
    {
        static string GetConnPrefixDescritpion(ConnPrefix prefix)
        {
            switch (prefix)
            {
                case ConnPrefix.SqlDatabase:
                    return "SQLAZURECONNSTR_";                  
                case ConnPrefix.SqlServer:
                    return "SQLCONNSTR_";
                case ConnPrefix.MySql:
                    return "MYSQLCONNSTR_";
                case ConnPrefix.Custom:
                    return "CUSTOMCONNSTR_";
                case ConnPrefix.Empty:
                    return string.Empty;  
            }
            return string.Empty;
        }
        public static string GetAppSettings(IConfigurationRoot config, string key)
        {
            var localKey = "AppSettings:" + key;
            var envKey = "APPSETTING_" +key;
            return config[envKey] ?? config[localKey];
        }


        public static string GetConnectionString(IConfigurationRoot config, string key, ConnPrefix prefix = ConnPrefix.Empty)
        {
            string localKey = "Data:" + key + ":ConnectionString";
            if (prefix == ConnPrefix.Empty)
            {
                var values = EnumUtil.GetValues<ConnPrefix>();

                foreach (var item in values)
                {
                   string tryEnvKey = GetConnPrefixDescritpion(item) + key;
                    if (config[tryEnvKey] != null)
                        return config[tryEnvKey];
                }
                return config[localKey];
            }
            else
            {
                string envKey = GetConnPrefixDescritpion(prefix) + key;
                return config[envKey] ?? config[localKey];
            }
        }
    }


    /*
    Connection String Type  Environment Variable Prefix
SQL Database  SQLAZURECONNSTR_
SQL Server  SQLCONNSTR_
MySQL  MYSQLCONNSTR_
Custom  CUSTOMCONNSTR
    */

}
