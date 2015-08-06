var express = require('express');
var router = express();

router.get('/', function(req, res) {
	
	var userName = req.query.username;
	var userpwd = req.query.userpwd;
	var userName2 = req.param('username');
	var userpwd2 = req.param('userpwd');

	console.log('req.query用户名：' + userName);
	console.log('req.query密码：' + userpwd);
	console.log('req.param用户名：' + userName2);
	console.log("req.param密码：" + userpwd2);

	res.render('subform', {
		title: '提交表单及接收参数示例'
	});
});

router.post('/', function(req, res) {
	
	var userName=req.body.username;
	var userpwd=req.body.userpwd;
	var userName2 = req.param('username');
	var userpwd2 = req.param('userpwd');

	console.log('req.query用户名：' + userName);
	console.log('req.query密码：' + userpwd);
	console.log('req.param用户名：' + userName2);
	console.log("req.param密码：" + userpwd2);

	res.render('subform', {
		title: '提交表单及接收参数示例'
	});
});

module.exports = router;