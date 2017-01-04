using NECaptcha.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NECaptcha.Controllers
{
    public class LoginController : Controller
    {
        private static string captchaId = "YOUR_CAPTCHA_ID"; // 验证码id
        private static string secretId = "YOUR_SECRET_ID"; // 密钥对id
        private static string secretKey = "YOUR_SECRET_KEY"; // 密钥对key
        private NECaptchaVerifier verifier = new NECaptchaVerifier(captchaId, new NESecretPair(secretId, secretKey));

        // GET: Login
        // 显示演示用的登录页面，这个是默认的首页，配置在 RouteConfig.cs 里
        public ActionResult Index()
        {
            return View();
        }

        // POST: Submit
        /// <summary>
        /// 演示用的提交登录请求接口
        /// </summary>
        /// <param name="username">表单用户名</param>
        /// <param name="password">表单密码</param>
        /// <param name="NECaptchaValidate">验证码组件生成的二次校验数据</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Submit(string username, string password, string NECaptchaValidate)
        {
            bool verifyResult = verifier.verify(NECaptchaValidate, username);
            string msg = String.Format("验证 {0}! <a href=\"/\">返回首页</a>", verifyResult ? "成功" : "失败");
            return Content(msg);// 这里简单的在页面上显示一下结果即可
        }
    }
}