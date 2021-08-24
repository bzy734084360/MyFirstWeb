using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Xml;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.Pkcs;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Pkcs;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.X509;

namespace Bzy.Utilities
{
    /// <summary>
    /// 加解密公共类
    /// </summary>
    public class SecretHelper
    {
        #region public static bool CheckRegister() 检查注册码是否正确
        /// <summary>
        /// 检查注册码是否正确
        /// </summary>
        /// <returns>是否进行了注册</returns>
        public static bool CheckRegister()
        {
            return false;
            //return !(SystemInfo.NeedRegister && DateTime.Now.Year > 0x7E4 && DateTime.Now.Month > 12);
        }
        #endregion

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

            byte[] keyArray = Encoding.UTF8.GetBytes("2020122419941228199301215201314X");
            byte[] toEncryptArray = Encoding.UTF8.GetBytes(toEncrypt);

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

            byte[] keyArray = Encoding.UTF8.GetBytes("2020122419941228199301215201314X");
            byte[] toEncryptArray = Convert.FromBase64String(toDecrypt);

            var rDel = new RijndaelManaged
            {
                Key = keyArray,
                Mode = CipherMode.ECB,
                Padding = PaddingMode.PKCS7
            };
            ICryptoTransform cTransform = rDel.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
            return Encoding.UTF8.GetString(resultArray);
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


        public static void XMLConvertToPEM()//XML格式密钥转PEM
        {
            var rsa2 = new RSACryptoServiceProvider();
            rsa2.FromXmlString(privateKeyFileName);
            //using (var sr = new StreamReader("e:\\PrivateKey.xml"))
            //{

            //}
            var p = rsa2.ExportParameters(true);

            var key = new RsaPrivateCrtKeyParameters(
                new BigInteger(1, p.Modulus), new BigInteger(1, p.Exponent), new BigInteger(1, p.D),
                new BigInteger(1, p.P), new BigInteger(1, p.Q), new BigInteger(1, p.DP), new BigInteger(1, p.DQ),
                new BigInteger(1, p.InverseQ));

            using (var sw = new StreamWriter("e:\\PrivateKey.pem"))
            {
                var pemWriter = new Org.BouncyCastle.OpenSsl.PemWriter(sw);
                pemWriter.WriteObject(key);
            }
        }

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

        #region Java To DotNet

        public static string RSAPrivateKeyDotNet2Java()
        {
            string privateKey = @"<RSAKeyValue><Modulus>7QNarOX2I7zr/MKAJuw/Xp0ZWP32Bxj3EhhL8V7Xh9rezbjCwRm3JIzDBe71gkLmRp0FXjorJ7MPBWglzR7Mew/fDy9/jJ8bOrdfO98b6xmHjI44Vtl8JHnrSLdOmJmfWyDAPHPYGd7xJoPD9gL6WRAdqh11fbRWOi784tSnfsU=</Modulus><Exponent>AQAB</Exponent><P>+mEQ/HV9qYpJu57R7Tn+ktMulDhmy+uDBRzJKRiI0vrhFuBe+qrIBRNoPMer5FvwTyeu1cKpaIp8NVLx5Q4Erw==</P><Q>8lV5C/1vzwlP3U8PAmz3A6VF1nRnuskGnyNLTPD+T71Y0ZX7LpwANC/mrq5PSB/k0FCY3n9Ez3ZlPwtPtpS4yw==</Q><DP>N2lUAQtfjC32s3cqrn2vQX9LR7JTzb2JeZAtVNMBNPAg8JcnbgJt0fHBl/H4sMlIHbyCjPxP0bsUUhjRQAgiMQ==</DP><DQ>7TGyGIVJMhnIPTML2vDy2nOjpuQrP81SGOO/6aCdG0mXLRWjVRzGqk4UefgNi+gD2853wphJrtMRCKM2s9xf/w==</DQ><InverseQ>vno0TwTBG+H0b8ou3T+pIKI8Qhw3Tlx21BND0XcpVfNQIdHdOglgOC03wkFJK6pqVlL9Fkw2Cg6Znlg0Dc/8AA==</InverseQ><D>kzG7FZe3lphUwuQUbJfZ/yt3u4H1UpEZmD5Io71wA2pVZtPw97W9vNBCqiOui1h65K3N0Kg1dvFISeZVW59eA3lnR9Jpj0mBf/mFEqGWNIdRT397v40Rc8JxEgHbIC6E+meX0xbWSyum3Dc1bqvZZJ2GSKjnIZxo8LLto1ji8B0=</D></RSAKeyValue>";
            XmlDocument doc = new XmlDocument();

            doc.LoadXml(privateKey);

            BigInteger m = new BigInteger(1, Convert.FromBase64String(doc.DocumentElement.GetElementsByTagName("Modulus")[0].InnerText));

            BigInteger exp = new BigInteger(1, Convert.FromBase64String(doc.DocumentElement.GetElementsByTagName("Exponent")[0].InnerText));

            BigInteger d = new BigInteger(1, Convert.FromBase64String(doc.DocumentElement.GetElementsByTagName("D")[0].InnerText));

            BigInteger p = new BigInteger(1, Convert.FromBase64String(doc.DocumentElement.GetElementsByTagName("P")[0].InnerText));

            BigInteger q = new BigInteger(1, Convert.FromBase64String(doc.DocumentElement.GetElementsByTagName("Q")[0].InnerText));

            BigInteger dp = new BigInteger(1, Convert.FromBase64String(doc.DocumentElement.GetElementsByTagName("DP")[0].InnerText));

            BigInteger dq = new BigInteger(1, Convert.FromBase64String(doc.DocumentElement.GetElementsByTagName("DQ")[0].InnerText));

            BigInteger qinv = new BigInteger(1, Convert.FromBase64String(doc.DocumentElement.GetElementsByTagName("InverseQ")[0].InnerText));

            RsaPrivateCrtKeyParameters privateKeyParam = new RsaPrivateCrtKeyParameters(m, exp, d, p, q, dp, dq, qinv);

            PrivateKeyInfo privateKeyInfo = PrivateKeyInfoFactory.CreatePrivateKeyInfo(privateKeyParam);

            byte[] serializedPrivateBytes = privateKeyInfo.ToAsn1Object().GetEncoded();

            return Convert.ToBase64String(serializedPrivateBytes);

        }
        /// <summary>    
        /// RSA公钥格式转换，.net->java    
        /// </summary>    
        /// <param name="publicKey">.net生成的公钥</param>    
        /// <returns></returns>   
        public static string RSAPublicKeyDotNet2Java()
        {
            string publicKey = publicKeyFileName;
            XmlDocument doc = new XmlDocument(); doc.LoadXml(publicKey);
            BigInteger m = new BigInteger(1, Convert.FromBase64String(doc.DocumentElement.GetElementsByTagName("Modulus")[0].InnerText));
            BigInteger p = new BigInteger(1, Convert.FromBase64String(doc.DocumentElement.GetElementsByTagName("Exponent")[0].InnerText));
            RsaKeyParameters pub = new RsaKeyParameters(false, m, p);
            SubjectPublicKeyInfo publicKeyInfo = SubjectPublicKeyInfoFactory.CreateSubjectPublicKeyInfo(pub);
            byte[] serializedPublicBytes = publicKeyInfo.ToAsn1Object().GetDerEncoded();
            return Convert.ToBase64String(serializedPublicBytes);
        }

        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string RsaToJava(string data)
        {
            //加密
            Asn1Object pubKeyObj = Asn1Object.FromByteArray(Convert.FromBase64String(publicKeyJava));//这里也可以从流中读取，从本地导入
            AsymmetricKeyParameter pubKey = PublicKeyFactory.CreateKey(SubjectPublicKeyInfo.GetInstance(pubKeyObj));
            IAsymmetricBlockCipher cipher = new RsaEngine();
            cipher.Init(true, pubKey);
            byte[] encryptData = cipher.ProcessBlock(Encoding.Default.GetBytes(data), 0, Encoding.Default.GetBytes(data).Length);
            return Convert.ToBase64String(encryptData);
        }

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string UnRsaToJava(string data)
        {
            //解密
            AsymmetricKeyParameter priKey = PrivateKeyFactory.CreateKey(Convert.FromBase64String(privateKeyJava));
            IAsymmetricBlockCipher cipher = new RsaEngine();
            cipher.Init(false, priKey);//false表示解密
            string decryptData = Encoding.Default.GetString(cipher.ProcessBlock(Convert.FromBase64String(data), 0, Convert.FromBase64String(data).Length));
            return decryptData;
        }

        #region 签名JAVA版本

        /// <summary>
        /// 签名数据
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string SignToJava(string data)
        {
            byte[] Data = Encoding.GetEncoding("utf-8").GetBytes(data);
            RSACryptoServiceProvider rsa = DecodePemPrivateKey(privateKeyJava);
            SHA256 sh = new SHA256CryptoServiceProvider();
            //SHA1 sh = new SHA1CryptoServiceProvider();
            byte[] signData = rsa.SignData(Data, sh);
            return Convert.ToBase64String(signData);
        }

        private static RSACryptoServiceProvider DecodePemPrivateKey(String pemstr)
        {
            byte[] pkcs8privatekey;
            pkcs8privatekey = Convert.FromBase64String(pemstr);
            if (pkcs8privatekey != null)
            {
                RSACryptoServiceProvider rsa = DecodePrivateKeyInfo(pkcs8privatekey);
                return rsa;
            }
            else
                return null;
        }

        private static RSACryptoServiceProvider DecodePrivateKeyInfo(byte[] pkcs8)
        {
            byte[] SeqOID = { 0x30, 0x0D, 0x06, 0x09, 0x2A, 0x86, 0x48, 0x86, 0xF7, 0x0D, 0x01, 0x01, 0x01, 0x05, 0x00 };
            byte[] seq = new byte[15];

            MemoryStream mem = new MemoryStream(pkcs8);
            int lenstream = (int)mem.Length;
            BinaryReader binr = new BinaryReader(mem);    //wrap Memory Stream with BinaryReader for easy reading  
            byte bt = 0;
            ushort twobytes = 0;

            try
            {
                twobytes = binr.ReadUInt16();
                if (twobytes == 0x8130)    //data read as little endian order (actual data order for Sequence is 30 81)  
                    binr.ReadByte();    //advance 1 byte  
                else if (twobytes == 0x8230)
                    binr.ReadInt16();    //advance 2 bytes  
                else
                    return null;

                bt = binr.ReadByte();
                if (bt != 0x02)
                    return null;

                twobytes = binr.ReadUInt16();

                if (twobytes != 0x0001)
                    return null;

                seq = binr.ReadBytes(15);        //read the Sequence OID  
                if (!CompareBytearrays(seq, SeqOID))    //make sure Sequence for OID is correct  
                    return null;

                bt = binr.ReadByte();
                if (bt != 0x04)    //expect an Octet string  
                    return null;

                bt = binr.ReadByte();        //read next byte, or next 2 bytes is  0x81 or 0x82; otherwise bt is the byte count  
                if (bt == 0x81)
                    binr.ReadByte();
                else
                    if (bt == 0x82)
                    binr.ReadUInt16();
                //------ at this stage, the remaining sequence should be the RSA private key  

                byte[] rsaprivkey = binr.ReadBytes((int)(lenstream - mem.Position));
                RSACryptoServiceProvider rsacsp = DecodeRSAPrivateKey(rsaprivkey);
                return rsacsp;
            }

            catch (Exception ex)
            {
                return null;
            }

            finally { binr.Close(); }

        }

        private static bool CompareBytearrays(byte[] a, byte[] b)
        {
            if (a.Length != b.Length)
                return false;
            int i = 0;
            foreach (byte c in a)
            {
                if (c != b[i])
                    return false;
                i++;
            }
            return true;
        }

        private static RSACryptoServiceProvider DecodeRSAPrivateKey(byte[] privkey)
        {
            byte[] MODULUS, E, D, P, Q, DP, DQ, IQ;

            // ---------  Set up stream to decode the asn.1 encoded RSA private key  ------  
            MemoryStream mem = new MemoryStream(privkey);
            BinaryReader binr = new BinaryReader(mem);    //wrap Memory Stream with BinaryReader for easy reading  
            byte bt = 0;
            ushort twobytes = 0;
            int elems = 0;
            try
            {
                twobytes = binr.ReadUInt16();
                if (twobytes == 0x8130)    //data read as little endian order (actual data order for Sequence is 30 81)  
                    binr.ReadByte();    //advance 1 byte  
                else if (twobytes == 0x8230)
                    binr.ReadInt16();    //advance 2 bytes  
                else
                    return null;

                twobytes = binr.ReadUInt16();
                if (twobytes != 0x0102)    //version number  
                    return null;
                bt = binr.ReadByte();
                if (bt != 0x00)
                    return null;


                //------  all private key components are Integer sequences ----  
                elems = GetIntegerSize(binr);
                MODULUS = binr.ReadBytes(elems);

                elems = GetIntegerSize(binr);
                E = binr.ReadBytes(elems);

                elems = GetIntegerSize(binr);
                D = binr.ReadBytes(elems);

                elems = GetIntegerSize(binr);
                P = binr.ReadBytes(elems);

                elems = GetIntegerSize(binr);
                Q = binr.ReadBytes(elems);

                elems = GetIntegerSize(binr);
                DP = binr.ReadBytes(elems);

                elems = GetIntegerSize(binr);
                DQ = binr.ReadBytes(elems);

                elems = GetIntegerSize(binr);
                IQ = binr.ReadBytes(elems);

                // ------- create RSACryptoServiceProvider instance and initialize with public key -----  
                RSACryptoServiceProvider RSA = new RSACryptoServiceProvider();
                RSAParameters RSAparams = new RSAParameters();
                RSAparams.Modulus = MODULUS;
                RSAparams.Exponent = E;
                RSAparams.D = D;
                RSAparams.P = P;
                RSAparams.Q = Q;
                RSAparams.DP = DP;
                RSAparams.DQ = DQ;
                RSAparams.InverseQ = IQ;
                RSA.ImportParameters(RSAparams);
                return RSA;
            }
            catch (Exception ex)
            {
                return null;
            }
            finally { binr.Close(); }
        }

        private static int GetIntegerSize(BinaryReader binr)
        {
            byte bt = 0;
            byte lowbyte = 0x00;
            byte highbyte = 0x00;
            int count = 0;
            bt = binr.ReadByte();
            if (bt != 0x02)        //expect integer  
                return 0;
            bt = binr.ReadByte();

            if (bt == 0x81)
                count = binr.ReadByte();    // data size in next byte  
            else
                if (bt == 0x82)
            {
                highbyte = binr.ReadByte();    // data size in next 2 bytes  
                lowbyte = binr.ReadByte();
                byte[] modint = { lowbyte, highbyte, 0x00, 0x00 };
                count = BitConverter.ToInt32(modint, 0);
            }
            else
            {
                count = bt;        // we already have the data size  
            }



            while (binr.ReadByte() == 0x00)
            {    //remove high order zeros in data  
                count -= 1;
            }
            binr.BaseStream.Seek(-1, SeekOrigin.Current);        //last ReadByte wasn't a removed zero, so back up a byte  
            return count;
        }


        /// <summary>
        /// e.g:"D:\\pri.der";
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static RSACryptoServiceProvider DecodeRsaPrivateKey(string filePath)
        {

            byte[] MODULUS, E, D, P, Q, DP, DQ, IQ;

            FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);

            BinaryReader binr = new BinaryReader(fs);    //wrap Memory Stream with BinaryReader for easy reading
            try
            {
                var twobytes = binr.ReadUInt16();
                if (twobytes == 0x8130) //data read as little endian order (actual data order for Sequence is 30 81)
                    binr.ReadByte();        //advance 1 byte
                else if (twobytes == 0x8230)
                    binr.ReadInt16();       //advance 2 bytes
                else
                    return null;


                twobytes = binr.ReadUInt16();
                if (twobytes != 0x0102) //version number
                    return null;
                var bt = binr.ReadByte();
                if (bt != 0x00)
                    return null;

                var elems = GetIntegerSize(binr);
                MODULUS = binr.ReadBytes(elems);


                elems = GetIntegerSize(binr);
                E = binr.ReadBytes(elems);


                elems = GetIntegerSize(binr);
                D = binr.ReadBytes(elems);


                elems = GetIntegerSize(binr);
                P = binr.ReadBytes(elems);


                elems = GetIntegerSize(binr);
                Q = binr.ReadBytes(elems);


                elems = GetIntegerSize(binr);
                DP = binr.ReadBytes(elems);


                elems = GetIntegerSize(binr);
                DQ = binr.ReadBytes(elems);


                elems = GetIntegerSize(binr);
                IQ = binr.ReadBytes(elems);
                // ------- create RSACryptoServiceProvider instance and initialize with public key -----
                RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
                RSAParameters rsAparams = new RSAParameters();
                rsAparams.Modulus = MODULUS;
                rsAparams.Exponent = E;
                rsAparams.D = D;
                rsAparams.P = P;
                rsAparams.Q = Q;
                rsAparams.DP = DP;
                rsAparams.DQ = DQ;
                rsAparams.InverseQ = IQ;
                rsa.ImportParameters(rsAparams);
                return rsa;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message + e.StackTrace);
                return null;
            }
            finally
            {
                binr.Close();
            }
        }

        /// <summary>
        /// 导出私钥XML解密格式
        /// </summary>
        /// <returns></returns>
        public static string PrivateKeyDecXml()
        {
            RSACryptoServiceProvider rsaProvider = DecodeRsaPrivateKey(@"D:\\pri.der");
            var privateKey = rsaProvider.ToXmlString(true);
            return privateKey;
        }


        /// <summary>
        /// RSA解密
        /// </summary>
        /// <param name="privatekey"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public static string RsaDecrypt(string privatekey, string content)
        {

            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            byte[] cipherbytes;
            rsa.FromXmlString(privatekey);
            cipherbytes = rsa.Decrypt(Convert.FromBase64String(content), false);

            return Encoding.UTF8.GetString(cipherbytes);
        }

        #endregion

        #endregion
    }
}