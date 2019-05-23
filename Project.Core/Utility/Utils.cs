using Microsoft.International.Converters.PinYinConverter;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;

namespace Project.Core.Utility
{
    public static class Utils
    {
        private const string PublicKey = @"MIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQCpGwbSWMy64zcCZo/c53NxEZEw
3JEEa3iDJd54bu3ettZdx9WiBUBAZzGEjz/sn7ADpBHCL1Zqj3xW+CsuNyorMi2o
hXJDHiHDHfID+0WbOQiZJGigc6zxcNejI2oJEIzSxMFmLghtZFbg7WqKMDsAI30l
3fbiQRk07o/K9JrxJwIDAQAB";

        /// <summary>
        /// Copy fields of source object to the target object, if match.
        /// </summary>
        /// <param name="dest"></param>
        /// <param name="src"></param>
        public static void ShallowCopy(object dest, object src)
        {
            var flags = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;
            var dt = dest.GetType();
            var st = src.GetType();
            var destFields = dt.GetFields(flags).Concat(dt.BaseType.GetFields(flags)).ToArray();
            var srcFields = st.GetFields(flags).Concat(st.BaseType.GetFields(flags)).ToArray();

            foreach (var srcField in srcFields)
            {
                var destField = destFields.FirstOrDefault(field => field.Name == srcField.Name);

                if (destField != null && !destField.IsLiteral)
                {
                    if (srcField.FieldType == destField.FieldType)
                    {
                        destField.SetValue(dest, srcField.GetValue(src));
                    }
                }
            }
        }


        public static List<int> SplitInteger(string text, char splitter)
        {
            if (string.IsNullOrEmpty(text)) return null;
            var list = new List<int>();

            var arr = text.Split(splitter);
            for (var i = 0; i < arr.Length; i++)
            {
                int a;
                if (int.TryParse(arr[i].Trim(), out a))
                {
                    list.Add(a);
                }
            }
            return list;
        }


        public static List<string> SplitString(string text, char splitter)
        {
            if (string.IsNullOrEmpty(text)) return null;
            var list = new List<string>();

            var arr = text.Split(splitter);
            for (var i = 0; i < arr.Length; i++)
            {
                var a = arr[i].Trim();
                if (!string.IsNullOrEmpty(a))
                {
                    list.Add(a);
                }
            }
            return list;
        }

        public static List<int> SplitCommaInteger(string text)
        {
            return SplitInteger(text, ',');
        }

        public static List<string> SplitCommaString(string text)
        {
            return SplitString(text, ',');
        }

        public static HashSet<string> SplitCommaHash(string text)
        {
            if (string.IsNullOrEmpty(text)) return null;
            var hash = new HashSet<string>();

            var arr = text.Split(',');
            for (var i = 0; i < arr.Length; i++)
            {
                var a = arr[i].Trim();
                if (!string.IsNullOrEmpty(a))
                {
                    hash.Add(a);
                }
            }
            return hash;
        }

        public static List<NameValuePair> GetNameValuePairs(Type enumType, List<int> outValues = null)
        {
            var pairs = new List<NameValuePair>();
            var values = Enum.GetValues(enumType);
            foreach (var value in values)
            {
                if (outValues != null && !outValues.Contains((int)value))
                {
                    continue;
                }
                var name = Enum.GetName(enumType, value);
                var field = enumType.GetField(name);
                var attribute = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;
                pairs.Add(new NameValuePair()
                {
                    Name = name,
                    Value = Convert.ToInt32(value),
                    Description = attribute?.Description
                });
            }

            return pairs;
        }

        /// <summary>
        /// 获取枚举的键值对
        /// </summary>
        /// <returns>返回枚举简直对</returns>
        public static string GetEnumDesc<T>(int value)
        {
            var @enum = typeof(T);
            var list = GetNameValuePairs(@enum);
            return list.FirstOrDefault(o => o.Value == value)?.Description;
        }

        public static string GetPinyin(string str)
        {
            string r = string.Empty;
            foreach (char obj in str)
            {
                try
                {
                    ChineseChar chineseChar = new ChineseChar(obj);
                    string t = chineseChar.Pinyins[0].ToString();
                    r += t.Substring(0, t.Length - 1);
                }
                catch
                {
                    r += obj.ToString();
                }
            }
            return r;
        }
        public static string GetFirstPinyin(string str)
        {
            string r = string.Empty;
            foreach (char obj in str)
            {
                try
                {
                    ChineseChar chineseChar = new ChineseChar(obj);
                    string t = chineseChar.Pinyins[0].ToString();
                    r += t.Substring(0, 1);
                }
                catch
                {
                    r += obj.ToString();
                }
            }
            return r;
        }


        #region rsaWithSHA1验签
        /// <summary>
        /// 验签
        /// </summary>
        /// <param name="content">待验签字符串(在map中去除sign，将map中剩下的参数进行url_decode,然后进行字典排序,组成字符串,得到待签名字符串。)</param>
        /// <param name="signedString">签名</param>
        /// <param name="publicKey">公钥</param>
        /// <param name="input_charset">编码格式</param>
        /// <returns>true(通过)，false(不通过)</returns>
        public static bool Verify(string content, string signedString, string input_charset)
        {
            bool result;
            byte[] Data = System.Text.Encoding.GetEncoding(input_charset).GetBytes(content);
            byte[] data = Convert.FromBase64String(signedString);
            RSAParameters paraPub = ConvertFromPublicKey(PublicKey);
            RSACryptoServiceProvider rsaPub = new RSACryptoServiceProvider();
            rsaPub.ImportParameters(paraPub);
            SHA1 sh = new SHA1CryptoServiceProvider();
            result = rsaPub.VerifyData(Data, sh, data);
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pemFileConent"></param>
        /// <returns></returns>
        private static RSAParameters ConvertFromPublicKey(string pemFileConent)
        {
            byte[] keyData = Convert.FromBase64String(pemFileConent);
            if (keyData.Length < 162)
            {
                throw new ArgumentException("pem file content is incorrect.");
            }
            byte[] pemModulus = new byte[128];
            byte[] pemPublicExponent = new byte[3];
            Array.Copy(keyData, 29, pemModulus, 0, 128);
            Array.Copy(keyData, 159, pemPublicExponent, 0, 3);
            RSAParameters para = new RSAParameters();
            para.Modulus = pemModulus;
            para.Exponent = pemPublicExponent;
            return para;
        }

        #endregion
    }

    public class NameValuePair
    {
        public string Name { get; set; }

        public int Value { get; set; }

        public string Description { get; set; }
    }
}
