# 演示说明

* 打开captcha-csharp-demo.sln
* 右键解决方案，还原NuGet包
* 调整 Views\Login\Index.cshtml 文件
```
"captchaId": "YOUR_CAPTCHA_ID", // 这里填入申请到的验证码id
```

* 调整 Controllers\LoginController.cs 文件，填入从易盾官网申请到的配置信息
```
	private static string captchaId = "YOUR_CAPTCHA_ID"; // 验证码id
	private static string secretId = "YOUR_SECRET_ID"; // 密钥对id
	private static string secretKey = "YOUR_SECRET_KEY"; // 密钥对key
```

* 运行演示程序