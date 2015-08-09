var redis = require('redis');
var RDS_PORT = 6379; //端口号
var RDS_HOST = '127.0.1.1'; //服务器IP
var RDS_PWD = 'foobared';
var RDS_OPTS = {
	//auth_pass: RDS_PWD
}; //设置项
//var RDS_OPTS = {}; //设置项 foobared
var client = redis.createClient(RDS_PORT, RDS_HOST, RDS_OPTS);

//var client = redis.createClient();

client.auth(RDS_PWD, function() {
	console.log('通过认证');
});

/*client.on('connect',function(){
    client.set('author', 'Wilson',redis.print);
    client.get('author', redis.print);
    console.log('connect');
});*/

/*client.on('connect',function(){
    client.hmset('short', {'js':'javascript','C#':'C Sharp'}, redis.print);
    client.hmset('short', 'SQL','Structured Query Language','HTML','HyperText Mark-up Language', redis.print);

    client.hgetall("short", function(err,res){
        if(err)
        {
            console.log('Error:'+ err);
            return;
        }            
        console.dir(res);
    });
});

client.on('ready', function(err) {
	console.log('ready');
});*/

client.on('end',function(err){
    console.log('end');
});

client.on('connect',function(){
    var key = 'skills';
      client.sadd(key, 'C#','java',redis.print);
      client.sadd(key, 'nodejs');
      client.sadd(key, "MySQL");
      
      client.multi()      
      .sismember(key,'C#')
      .smembers(key)
      .exec(function (err, replies) {
            console.log("MULTI got " + replies.length + " replies");
            replies.forEach(function (reply, index) {
                console.log("Reply " + index + ": " + reply.toString());
            });
            client.quit();
    });
});