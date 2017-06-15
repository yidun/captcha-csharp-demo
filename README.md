# 演示说明

* 开发工具 visual studio 2015 社区版，.Net 版本 4.5.2
* 打开captcha-csharp-demo.sln
* 右键解决方案，还原NuGet包
* 调整 Views\Login\Index.cshtml 文件
```
initNECaptcha({
  captchaId: 'YOUR_CAPTCHA_ID', // <-- 这里填入在易盾官网申请的验证码id
  element: '#captcha_div',
  mode: 'float',
  width: '320px',
  onVerify: function(err, ret){
    if(!err){
        // ret['validate'] 获取二次校验数据
    }
  }
}, function (instance) {
  // 初始化成功后得到验证实例instance，可以调用实例的方法
}, function (err) {
  // 初始化失败后触发该函数，err对象描述当前错误信息
})
```

* 调整 Controllers\LoginController.cs 文件，填入从易盾官网申请到的配置信息
```
	private static string captchaId = "YOUR_CAPTCHA_ID"; // 验证码id
	private static string secretId = "YOUR_SECRET_ID"; // 密钥对id
	private static string secretKey = "YOUR_SECRET_KEY"; // 密钥对key
```

* 运行演示程序
* 欢迎fork & pull request帮助改进Demo