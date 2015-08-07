var express = require('express');
var router = express();

router.get('/', function(req, res) {
	
	if(req.cookies.islogin)
	{
		console.log('usecookies-cookies:'+req.cookies.islogin);
		req.session.islogin=req.cookies.islogion;
		
	}
	if (req.session.islogin) {
		console.log('usecookies:' + req.session.islogin);
		res.locals.islogin = req.session.islogin;
	}
	res.render('usecookies', {
		title: '使用cookies示例'
	});
});

router.post('/',function(req,res){
	req.session.islogin='success';
	res.locals.islogin=req.session.islogin;
	
	res.cookie('islogin','sucess',{maxAge:60000});
	
	res.render('usecookies:',{title:'使用cookies示例'});
});

module.exports = router;