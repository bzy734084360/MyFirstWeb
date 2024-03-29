﻿using System;
using System.Collections.Generic;
using System.Web;
using System.Text;

namespace NewStudy.WebUtilities
{
    /// <summary>
    /// <para>　Session操作类</para>
    /// <para>　----------------------------------------------------------</para>
    /// <para>　AddSession：添加Session,有效期为默认</para>
    /// <para>　AddSession：添加Session，并调整有效期为分钟或几年</para>
    /// <para>　GetSession：读取某个Session对象值</para>
    /// <para>　DelSession：删除某个Session对象</para>
    /// </summary>
    public class SessionHelper
    {
        #region 添加Session,有效期为默认
        /// <summary>
        /// 添加Session,有效期为默认
        /// </summary>
        /// <param name="strSessionName">Session对象名称</param>
        /// <param name="strValue">Session值</param>
        public static void Add(string strSessionName, object objValue)
        {
            HttpContext.Current.Session[strSessionName] = objValue;
        }
        #endregion

        #region 添加Session，并调整有效期为分钟或几年
        /// <summary>
        /// 添加Session，并调整有效期为分钟或几年
        /// </summary>
        /// <param name="strSessionName">Session对象名称</param>
        /// <param name="strValue">Session值</param>
        /// <param name="iExpires">分钟数：大于０则以分钟数为有效期，等于０则以后面的年为有效期</param>
        /// <param name="iYear">年数：当分钟数为０时按年数为有效期，当分钟数大于０时此参数随意设置</param>
        public static void Set(string strSessionName, object objValue, int iExpires, int iYear)
        {
            HttpContext.Current.Session[strSessionName] = objValue;
            if (iExpires > 0)
            {
                HttpContext.Current.Session.Timeout = iExpires;
            }
            else if (iYear > 0)
            {
                HttpContext.Current.Session.Timeout = 60 * 24 * 365 * iYear;
            }
        }
        #endregion

        #region 读取某个Session对象值
        /// <summary>
        /// 读取某个Session对象值
        /// </summary>
        /// <param name="strSessionName">Session对象名称</param>
        /// <returns>Session对象值</returns>
        public static object Get(string strSessionName)
        {
            return HttpContext.Current.Session[strSessionName];
        }
        #endregion

        #region 删除某个Session对象
        /// <summary>
        /// 删除某个Session对象
        /// </summary>
        /// <param name="strSessionName">Session对象名称</param>
        public static void Remove(string strSessionName)
        {
            HttpContext.Current.Session.Remove(strSessionName);
        }
        #endregion

        #region Session操作
        /// <summary>
        /// 写Session
        /// </summary>
        /// <typeparam name="T">Session键值的类型</typeparam>
        /// <param name="key">Session的键名</param>
        /// <param name="value">Session的键值</param>
        public static void WriteSession<T>(string key, T value)
        {
            if (string.IsNullOrWhiteSpace(key))
                return;
            HttpContext.Current.Session[key] = value;
        }

        /// <summary>
        /// 写Session
        /// </summary>
        /// <param name="key">Session的键名</param>
        /// <param name="value">Session的键值</param>
        public static void WriteSession(string key, string value)
        {
            WriteSession<string>(key, value);
        }

        /// <summary>
        /// 读取Session的值
        /// </summary>
        /// <param name="key">Session的键名</param>        
        public static string GetSession(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
                return string.Empty;
            return HttpContext.Current.Session[key] as string;
        }
        /// <summary>
        /// 删除指定Session
        /// </summary>
        /// <param name="key">Session的键名</param>
        public static void RemoveSession(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
                return;
            HttpContext.Current.Session.Contents.Remove(key);
        }

        #endregion
    }
}
