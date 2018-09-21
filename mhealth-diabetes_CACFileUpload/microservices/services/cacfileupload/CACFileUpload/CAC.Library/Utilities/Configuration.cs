using System;
using System.Configuration;

namespace CAC.Library.Utilities
{
    public static class Configuration
    {

        public static string GetValueConf(string keyConf) => ConfigurationManager.AppSettings[keyConf];
        public static string GetValueConf(int keyConf) => ConfigurationManager.AppSettings[keyConf];
        public static string GetValueConf(Constants keyConf) => ConfigurationManager.AppSettings[keyConf.ToString()];
        public static string GetClassName<T>() => typeof(T).Name;

    }
}
