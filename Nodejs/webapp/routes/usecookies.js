var express = require('express');
var router = express();

router.get('/', function(req, res) {
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
	
	res.render('usecookies:',{title:'使用cookies示例'});
});

module.exports = router;