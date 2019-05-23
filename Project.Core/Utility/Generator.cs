using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;

namespace Project.Core.Utility
{

    /// <summary>
    /// 用于生成一些值的类
    /// </summary>
    public class Generator
    {
        #region 私有静态变量

        /// <summary>
        /// 加密密钥
        /// </summary>
        private const string EncryptKey = "6ReJRIXP";

        /// <summary>
        /// 加密向量
        /// </summary>
        private const string EncryptIV = ".uu1.com";

        #endregion

        #region 内部调用的私有方法

        /// <summary>
        /// 使用CSPRNG生成强随机数
        /// </summary>
        /// <param name="max">随机数的最大值</param>
        /// <returns>返回一个随机数</returns>
        private static int StrongRandom(int max)
        {
            var randomByte = new byte[4];
            var gen = new RNGCryptoServiceProvider();
            gen.GetBytes(randomByte);
            var value = BitConverter.ToInt32(randomByte, 0);
            value = value % (max + 1);
            if (value < 0) value = -value;
            return value;
        }

        #endregion

        /// <summary>
        /// MD5加密方法
        /// </summary>
        /// <param name="value">需要加密的值</param>
        /// <param name="salt">加密盐值，默认不加盐</param>
        /// <returns>返回MD5加密值</returns>
        public static string Md5(string value, string salt = "")
        {
            var result = Encoding.Default.GetBytes(value + salt);
            var md5 = new MD5CryptoServiceProvider();
            var output = md5.ComputeHash(result);
            return BitConverter.ToString(output).Replace("-", "").ToUpper();
        }

        /// <summary>
        /// 字符串加密
        /// </summary>
        public static string EncryptString(string str)
        {
            var bytes = Encoding.UTF8.GetBytes(str);
            var des = new DESCryptoServiceProvider
            {
                Key = Encoding.ASCII.GetBytes(EncryptKey),
                IV = Encoding.ASCII.GetBytes(EncryptIV)
            };
            var encrypt = des.CreateEncryptor();
            var result = encrypt.TransformFinalBlock(bytes, 0, bytes.Length);
            return Convert.ToBase64String(result);
        }

        /// <summary>
        /// 字符串解密
        /// </summary>
        public static string DecryptString(string str)
        {
            var bytes = Convert.FromBase64String(str);
            var des = new DESCryptoServiceProvider
            {
                Key = Encoding.ASCII.GetBytes(EncryptKey),
                IV = Encoding.ASCII.GetBytes(EncryptIV)
            };
            var encrypt = des.CreateDecryptor();
            var result = encrypt.TransformFinalBlock(bytes, 0, bytes.Length);
            return Encoding.UTF8.GetString(result);
        }

        /// <summary>
        /// 生成一个随机字符串
        /// </summary>
        /// <param name="len">字符串长度</param>
        /// <returns>返回随机字符串</returns>
        public static string RandomChar(int len)
        {
            var builder = new StringBuilder();
            var chars = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ~!@#$%^&*()".ToArray();
            for (var i = 0; i < len; i++)
            {
                builder.Append(chars[StrongRandom(chars.Length - 1)]);
            }
            return builder.ToString();
        }

        /// <summary>
        /// 获取枚举的键值对
        /// </summary>
        /// <param name="enum">枚举类型</param>
        /// <returns>返回枚举简直对</returns>
        public static List<object> GetNameValuePair(Type @enum)
        {
            var pairs = new List<object>();
            var values = Enum.GetValues(@enum);
            foreach (var value in values)
            {
                var name = Enum.GetName(@enum, value);
                pairs.Add(new
                {
                    Value = Convert.ToInt32(value),
                    Name = name,
                    Description = GetDescription(@enum, name)
                });
            }
            return pairs;
        }

        /// <summary>
        /// 获取枚举的描述
        /// </summary>
        /// <param name="enum">枚举</param>
        /// <param name="name">枚举名</param>
        /// <returns>返回枚举的描述</returns>
        public static string GetDescription(Type @enum, string name)
        {
            var field = @enum.GetField(name);
            if (field == null) return string.Empty;
            var attribute = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;
            return attribute?.Description;
        }

        public static object GetDefaultValue(Type @enum, string name)
        {
            var field = @enum.GetField(name);
            var attribute = DefaultValueAttribute.GetCustomAttribute(field, typeof(DefaultValueAttribute)) as DefaultValueAttribute;
            return attribute?.Value;
        }

        public static string GetDiff<T>(T OldItem, T NewItem)
        {
            string str = "";

            if (OldItem != null && NewItem != null)
            {
                Type type = typeof(T);
                PropertyInfo[] props = type.GetProperties();
                foreach (var prop in props)
                {
                    var desc = Attribute.GetCustomAttribute(prop, typeof(DescriptionAttribute)) as DescriptionAttribute;
                    if (desc == null) { continue; };
                    var newValue = prop.GetValue(NewItem);
                    var oldValue = prop.GetValue(OldItem);
                    var propType = prop.PropertyType.Name;
                    if (propType.ToLower() == "decimal")
                    {
                        var newStr = newValue != null ? (decimal)newValue : default(decimal);
                        var oldStr = oldValue != null ? (decimal)oldValue : default(decimal);

                        if (newStr != oldStr)
                        {
                            str += desc.Description + $":由【{oldStr}】-->【{newStr}】;";
                        }
                    }
                    else
                    {
                        string newStr = newValue != null ? newValue.ToString() : "";
                        string oldStr = oldValue != null ? oldValue.ToString() : "";

                        if (newStr != oldStr)
                        {
                            str += desc.Description + $":由【{oldStr}】-->【{newStr}】;";
                        }
                    }

                }
            }

            return str;
        }

        public static string SerializeEntity<T>(T entity)
        {
            string str = "";
            if (entity != null)
            {
                PropertyInfo[] propertyInfo = typeof(T).GetProperties(BindingFlags.Instance | BindingFlags.Public); ;
                foreach (var item in propertyInfo)
                {
                    string name = item.Name; //名称
                    object value = item.GetValue(entity, null); //value
                    var proptype = item.PropertyType; //type

                    string des = ((DescriptionAttribute)Attribute.GetCustomAttribute(item, typeof(DescriptionAttribute)))?.Description;
                    if (value != null && !string.IsNullOrEmpty(des))
                    {
                        str += string.Format("{0}:【{1}】,", des, value);
                    }
                }
            }
            return str;

        }

        public static string SerializeListEntity<T>(List<T> list)
        {
            StringBuilder builder = new StringBuilder();
            if (list != null && list.Count > 0)
            {
                foreach (var item in list)
                {
                    builder.Append(SerializeEntity(item));
                }
            }
            return builder.ToString();
        }
    }
}
