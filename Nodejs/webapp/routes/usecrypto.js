var express = require('express');
var router = express();
var crypto = require('crypto');

router.get('/', function(req, res) {
	res.render('usecrypto', {
		title: '加密字符串示例'
	});
});

router.post('/', function(req, res) {
	var username = req.body.username;
	var userpwd = req.body.userpwd;

	var md5 = crypto.createHash('md5');
	var en_upwd = md5.update(userpwd).digest('hex');

	console.log('加密后的密码：' + en_upwd);

	res.render('usecrypto', {
		title: '加密字符串示例'
	});

});
module.exports = router;