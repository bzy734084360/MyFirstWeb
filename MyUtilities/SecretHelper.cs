using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Xml;

namespace Bzy.Utilities
{
    /// <summary>
    /// 加解密公共类
    /// </summary>
    public class SecretHelper
    {

        #region 256位AES加解密
        /// <summary>
        /// 256位AES加密
        /// </summary>
        /// <param name="toEncrypt">待加密文本</param>
        /// <returns>加密后的文本</returns>
        public static string AESEncrypt(string toEncrypt)
        {
            if (string.IsNullOrEmpty(toEncrypt))
            {
                return string.Empty;
            }

            byte[] keyArray = UTF8Encoding.UTF8.GetBytes("12345678901234567890123456789012");//12345678901234567890123456789012 注意，此处不能乱改动
            byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(toEncrypt);

            var rDel = new RijndaelManaged
            {
                Key = keyArray,
                Mode = CipherMode.ECB,
                Padding = PaddingMode.PKCS7
            };
            ICryptoTransform cTransform = rDel.CreateEncryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }

        /// <summary>
        /// 256位AES解密
        /// </summary>
        /// <param name="toDecrypt">待解密文本</param>
        /// <returns>解密后的文本</returns>
        public static string AESDecrypt(string toDecrypt)
        {
            if (string.IsNullOrEmpty(toDecrypt))
            {
                return string.Empty;
            }

            byte[] keyArray = UTF8Encoding.UTF8.GetBytes("12345678901234567890123456789012");
            byte[] toEncryptArray = Convert.FromBase64String(toDecrypt);

            var rDel = new RijndaelManaged
            {
                Key = keyArray,
                Mode = CipherMode.ECB,
                Padding = PaddingMode.PKCS7
            };
            ICryptoTransform cTransform = rDel.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
            return UTF8Encoding.UTF8.GetString(resultArray);
        }
        #endregion

        #region AES

        public const string aeaDeckey = "taoSocialContactOPP0MIViV0HuaWei";
        //public const string aeaEnckey = "taoSocialContactMIViV0HuaWeiOPP0";
        public const string aeaEnckey = "taoSocialContact";


        /// <summary>
        /// AES加密
        /// </summary>
        /// <param name="data">待加密数据</param>
        /// <returns></returns>
        public static string AESEncryptByData(string data, string aesKey = "")
        {
            string result;
            try
            {
                if (string.IsNullOrEmpty(data))
                {
                    result = null;
                }
                else
                {
                    string key = string.IsNullOrEmpty(aesKey) ? aeaEnckey : aesKey;
                    byte[] bytes = Encoding.UTF8.GetBytes(data);
                    RijndaelManaged rijndaelManaged = new RijndaelManaged
                    {
                        Key = Encoding.UTF8.GetBytes(key),
                        Mode = CipherMode.ECB,
                        Padding = PaddingMode.PKCS7
                    };
                    ICryptoTransform cryptoTransform = rijndaelManaged.CreateEncryptor();
                    byte[] array = cryptoTransform.TransformFinalBlock(bytes, 0, bytes.Length);
                    result = Convert.ToBase64String(array, 0, array.Length);
                }
            }
            catch (Exception ex)
            {
                result = null;
            }
            return result;
        }

        /// <summary>
        /// AES解密
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string AESDecryptByData(string data, string aesKey = "")
        {
            if (string.IsNullOrEmpty(data))
            {
                return null;
            }
            string result;
            try
            {
                string key = string.IsNullOrEmpty(aesKey) ? aeaEnckey : aesKey;
                byte[] array = Convert.FromBase64String(data);
                RijndaelManaged rijndaelManaged = new RijndaelManaged
                {
                    Key = Encoding.UTF8.GetBytes(key),
                    Mode = CipherMode.ECB,
                    Padding = PaddingMode.PKCS7
                };
                ICryptoTransform cryptoTransform = rijndaelManaged.CreateDecryptor();
                byte[] bytes = cryptoTransform.TransformFinalBlock(array, 0, array.Length);
                result = Encoding.UTF8.GetString(bytes);
            }
            catch (Exception ex)
            {
                result = null;
            }
            return result;
        }

        #endregion

        #region RSA

        private const int RsaKeySize = 1024;        //要使用的密钥的大小（以位为单位）
        private const string publicKeyFileName = @"<RSAKeyValue><Modulus>7QNarOX2I7zr/MKAJuw/Xp0ZWP32Bxj3EhhL8V7Xh9rezbjCwRm3JIzDBe71gkLmRp0FXjorJ7MPBWglzR7Mew/fDy9/jJ8bOrdfO98b6xmHjI44Vtl8JHnrSLdOmJmfWyDAPHPYGd7xJoPD9gL6WRAdqh11fbRWOi784tSnfsU=</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>";
        private const string privateKeyFileName = @"<RSAKeyValue><Modulus>7QNarOX2I7zr/MKAJuw/Xp0ZWP32Bxj3EhhL8V7Xh9rezbjCwRm3JIzDBe71gkLmRp0FXjorJ7MPBWglzR7Mew/fDy9/jJ8bOrdfO98b6xmHjI44Vtl8JHnrSLdOmJmfWyDAPHPYGd7xJoPD9gL6WRAdqh11fbRWOi784tSnfsU=</Modulus><Exponent>AQAB</Exponent><P>+mEQ/HV9qYpJu57R7Tn+ktMulDhmy+uDBRzJKRiI0vrhFuBe+qrIBRNoPMer5FvwTyeu1cKpaIp8NVLx5Q4Erw==</P><Q>8lV5C/1vzwlP3U8PAmz3A6VF1nRnuskGnyNLTPD+T71Y0ZX7LpwANC/mrq5PSB/k0FCY3n9Ez3ZlPwtPtpS4yw==</Q><DP>N2lUAQtfjC32s3cqrn2vQX9LR7JTzb2JeZAtVNMBNPAg8JcnbgJt0fHBl/H4sMlIHbyCjPxP0bsUUhjRQAgiMQ==</DP><DQ>7TGyGIVJMhnIPTML2vDy2nOjpuQrP81SGOO/6aCdG0mXLRWjVRzGqk4UefgNi+gD2853wphJrtMRCKM2s9xf/w==</DQ><InverseQ>vno0TwTBG+H0b8ou3T+pIKI8Qhw3Tlx21BND0XcpVfNQIdHdOglgOC03wkFJK6pqVlL9Fkw2Cg6Znlg0Dc/8AA==</InverseQ><D>kzG7FZe3lphUwuQUbJfZ/yt3u4H1UpEZmD5Io71wA2pVZtPw97W9vNBCqiOui1h65K3N0Kg1dvFISeZVW59eA3lnR9Jpj0mBf/mFEqGWNIdRT397v40Rc8JxEgHbIC6E+meX0xbWSyum3Dc1bqvZZJ2GSKjnIZxo8LLto1ji8B0=</D></RSAKeyValue>";

        private const string publicKeyJava = @"MIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQDtA1qs5fYjvOv8woAm7D9enRlY/fYHGPcSGEvxXteH2t7NuMLBGbckjMMF7vWCQuZGnQVeOisnsw8FaCXNHsx7D98PL3+Mnxs6t1873xvrGYeMjjhW2XwkeetIt06YmZ9bIMA8c9gZ3vEmg8P2AvpZEB2qHXV9tFY6Lvzi1Kd+xQIDAQAB";
        private const string privateKeyJava = @"MIICeAIBADANBgkqhkiG9w0BAQEFAASCAmIwggJeAgEAAoGBAO0DWqzl9iO86/zCgCbsP16dGVj99gcY9xIYS/Fe14fa3s24wsEZtySMwwXu9YJC5kadBV46KyezDwVoJc0ezHsP3w8vf4yfGzq3XzvfG+sZh4yOOFbZfCR560i3TpiZn1sgwDxz2Bne8SaDw/YC+lkQHaoddX20Vjou/OLUp37FAgMBAAECgYEAkzG7FZe3lphUwuQUbJfZ/yt3u4H1UpEZmD5Io71wA2pVZtPw97W9vNBCqiOui1h65K3N0Kg1dvFISeZVW59eA3lnR9Jpj0mBf/mFEqGWNIdRT397v40Rc8JxEgHbIC6E+meX0xbWSyum3Dc1bqvZZJ2GSKjnIZxo8LLto1ji8B0CQQD6YRD8dX2pikm7ntHtOf6S0y6UOGbL64MFHMkpGIjS+uEW4F76qsgFE2g8x6vkW/BPJ67Vwqloinw1UvHlDgSvAkEA8lV5C/1vzwlP3U8PAmz3A6VF1nRnuskGnyNLTPD+T71Y0ZX7LpwANC/mrq5PSB/k0FCY3n9Ez3ZlPwtPtpS4ywJAN2lUAQtfjC32s3cqrn2vQX9LR7JTzb2JeZAtVNMBNPAg8JcnbgJt0fHBl/H4sMlIHbyCjPxP0bsUUhjRQAgiMQJBAO0xshiFSTIZyD0zC9rw8tpzo6bkKz/NUhjjv+mgnRtJly0Vo1UcxqpOFHn4DYvoA9vOd8KYSa7TEQijNrPcX/8CQQC+ejRPBMEb4fRvyi7dP6kgojxCHDdOXHbUE0PRdylV81Ah0d06CWA4LTfCQUkrqmpWUv0WTDYKDpmeWDQNz/wA";
        public static void GenerateKeys()
        {
            string publicKey, privateKey;
            using (var rsa = new RSACryptoServiceProvider(RsaKeySize))
            {
                publicKey = rsa.ToXmlString(false);
                privateKey = rsa.ToXmlString(true);
            }
        }
        /// <summary>
        /// RSA 加密
        /// </summary>
        public static string Rsa(string source)
        {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(publicKeyFileName);
            var cipherbytes = rsa.Encrypt(Encoding.UTF8.GetBytes(source), false);
            return Convert.ToBase64String(cipherbytes);
        }

        /// <summary>
        /// RSA解密
        /// </summary>
        public static string UnRsa(string source)
        {
            try
            {
                RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
                rsa.FromXmlString(privateKeyFileName);
                var cipherbytes = rsa.Decrypt(Convert.FromBase64String(source), false);
                return Encoding.UTF8.GetString(cipherbytes);
            }
            catch (Exception ex)
            {
                string str = ex.ToString();
                return string.Empty;
            }
        }


        //public static void XMLConvertToPEM()//XML格式密钥转PEM
        //{
        //    var rsa2 = new RSACryptoServiceProvider();
        //    rsa2.FromXmlString(privateKeyFileName);
        //    //using (var sr = new StreamReader("e:\\PrivateKey.xml"))
        //    //{

        //    //}
        //    var p = rsa2.ExportParameters(true);

        //    var key = new RsaPrivateCrtKeyParameters(
        //        new BigInteger(1, p.Modulus), new BigInteger(1, p.Exponent), new BigInteger(1, p.D),
        //        new BigInteger(1, p.P), new BigInteger(1, p.Q), new BigInteger(1, p.DP), new BigInteger(1, p.DQ),
        //        new BigInteger(1, p.InverseQ));

        //    using (var sw = new StreamWriter("e:\\PrivateKey.pem"))
        //    {
        //        var pemWriter = new Org.BouncyCastle.OpenSsl.PemWriter(sw);
        //        pemWriter.WriteObject(key);
        //    }
        //}

        /// <summary>
        /// 生成签名
        /// </summary>
        /// <param name="str">需签名的数据</param>
        /// <param name="privateKey">私钥</param>
        /// <param name="encoding">编码格式 默认utf-8</param>
        /// <returns>签名后的值</returns>
        public static string RsaSignature(string str)
        {
            //SHA256withRSA
            //根据需要加签时的哈希算法转化成对应的hash字符节
            byte[] bt = Encoding.GetEncoding("utf-8").GetBytes(str);
            var sha256 = new SHA256CryptoServiceProvider();
            byte[] rgbHash = sha256.ComputeHash(bt);

            RSACryptoServiceProvider key = new RSACryptoServiceProvider();
            key.FromXmlString(privateKeyFileName);
            RSAPKCS1SignatureFormatter formatter = new RSAPKCS1SignatureFormatter(key);
            formatter.SetHashAlgorithm("SHA256");//此处是你需要加签的hash算法，需要和上边你计算的hash值的算法一致，不然会报错。
            byte[] inArray = formatter.CreateSignature(rgbHash);
            return Convert.ToBase64String(inArray);
        }
        /// <summary>
        /// 签名验证
        /// </summary>
        /// <param name="str">待验证的字符串</param>
        /// <param name="sign">加签之后的字符串</param>
        /// <returns>签名是否符合</returns>
        public static bool RsaSignCheck(string str, string sign)
        {
            try
            {
                byte[] bt = Encoding.GetEncoding("utf-8").GetBytes(str);
                var sha256 = new SHA256CryptoServiceProvider();
                byte[] rgbHash = sha256.ComputeHash(bt);

                RSACryptoServiceProvider key = new RSACryptoServiceProvider();
                key.FromXmlString(publicKeyFileName);
                RSAPKCS1SignatureDeformatter deformatter = new RSAPKCS1SignatureDeformatter(key);
                deformatter.SetHashAlgorithm("SHA256");
                byte[] rgbSignature = Convert.FromBase64String(sign);
                if (deformatter.VerifySignature(rgbHash, rgbSignature))
                {
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        #endregion

    }
}