using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace NECaptcha.Sdk
{
    /// <summary>
    /// 易盾验证码二次校验接口简单封装demo
    /// </summary>
    public class NECaptchaVerifier
    {
        public static string VERIFY_API = "http://c.dun.163yun.com/api/v2/verify"; // verify接口地址
        public static string REQ_VALIDATE = "NECaptchaValidate"; // 二次验证带过来的validate

        private string captchaId; // 验证码id
        private NESecretPair secretPair; // 密钥对
        private readonly string VERSION = "v2";
        private readonly HttpClient client = Utils.makeHttpClient();

        public NECaptchaVerifier(string captchaId, NESecretPair secretPair)
        {
            this.captchaId = captchaId;
            this.secretPair = secretPair;
        }
        
        /// <summary>
        /// 向易盾验证码后台发起二次校验请求
        /// </summary>
        /// <param name="validate">二次校验请求字符串</param>
        /// <param name="user">当前用户信息，可以为空字符串</param>
        /// <returns></returns>
        public bool verify(string validate, string user)
        {
            if (String.IsNullOrEmpty(validate))
            {
                return false;
            }
            user = (user == null) ? "" : user;
            Dictionary<String, String> parameters = new Dictionary<String, String>();
            long curr = (long)(DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds;
            String time = curr.ToString();

            parameters.Add("captchaId", captchaId);
            parameters.Add("validate", validate);
            parameters.Add("user", user);

            // 1.设置公共参数
            parameters.Add("secretId", secretPair.secretId);
            parameters.Add("version", VERSION);
            parameters.Add("timestamp", time);
            parameters.Add("nonce", new Random().Next().ToString());

            // 2.生成签名信息
            String signature = Utils.genSignature(secretPair.secretKey, parameters);
            parameters.Add("signature", signature);

            // 3.发送HTTP请求
            String response = Utils.doPost(client, VERIFY_API, parameters, 5000);
            return verifyRet(response);
        }

        /// <summary>
        /// 解析二次校验接口返回的结果
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        private bool verifyRet(string response)
        {
            if (String.IsNullOrEmpty(response))
            {
                return false;
            }
            try
            {
                JObject j = JObject.Parse(response);
                return j.GetValue("result").ToObject<Boolean>();
            }
            catch (Exception e)
            {
                if (e.Source != null)
                {
                    Console.WriteLine("IOException source: {0}", e.Source);
                }

            }
            return false;
        }
    }

    /// <summary>
    /// 易盾验证码密钥对
    /// </summary>
    public class NESecretPair
    {
        public string secretId; // 密钥对id
        public string secretKey; // 密钥对key

        public NESecretPair(string secretId, string secretKey)
        {
            this.secretId = secretId;
            this.secretKey = secretKey;
        }
    }
}