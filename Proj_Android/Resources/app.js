// Incluir variavel que dá o ip
Titanium.include("url_ip.js");

// Incluir o ficheiro da pagina inicial e para a sessao do utilizador
Titanium.include("menu/main_screen.js");
Titanium.include("facebook/init_facebook.js");
Titanium.include("user/session_user.js");
Titanium.include("components/lateral_bar.js");
Titanium.include("components/header_bar.js");
Titanium.include("components/events_lateral_bar.js");
Titanium.include("components/back_window.js");
Titanium.include("search/result_search.js");

var main_screen = new MainScreen();
var type_back = [];

if(!Titanium.Network.online)
{
    var intent = Ti.Android.createIntent({
        action : Ti.Android.ACTION_MAIN,
        className : 'com.stellio.healthplus.HealthActivity',
        url : 'app.js',
        flags : Ti.Android.FLAG_ACTIVITY_RESET_TASK_IF_NEEDED | Ti.Android.FLAG_ACTIVITY_SINGLE_TOP
    });
   
    intent.addCategory(Titanium.Android.CATEGORY_LAUNCHER);
    
    var pending = Ti.Android.createPendingIntent({
        activity : activity,
        intent : intent,
        type : Ti.Android.PENDING_INTENT_FOR_ACTIVITY,
        flags : Ti.Android.FLAG_ACTIVITY_NO_HISTORY
    });
    
    
    var notification = Ti.Android.createNotification({
        contentIntent : pending,
        contentTitle : 'Falta de conexão à Internet',
        contentText : 'Por favor verifique se o seu dispositivo tem acesso à Internet.',
        when : new Date().getTime(),
        icon : Ti.App.Android.R.drawable.appicon,
        flags : Titanium.Android.ACTION_DEFAULT | Titanium.Android.FLAG_AUTO_CANCEL | Titanium.Android.FLAG_SHOW_LIGHTS,
        audioStreamType: Titanium.Android.STREAM_VOICE_CALL
    });
    
    // Send the notification.
    Titanium.Android.NotificationManager.notify(1, notification);
    
    var activity = Titanium.Android.currentActivity;
    activity.finish();
}
else
{
    /** Caso o utilizador ja esteja logado **/
    if(fb.loggedIn)
    {
    	fb.requestWithGraphPath('me', {}, 'GET', function(e) 
    	{
    		if (e.success) 
    		{
        		var JSONdata = JSON.parse(e.result);
    			
    			// Dados globais
    			user_name = JSONdata.name;
    			user_facebook_id = parseFloat(JSONdata.id);
    			user_email = JSONdata.email;
    			
    			//Dados apenas para registo
    			var location = JSONdata.location;
    			var locationJSON = JSON.stringify(location);
    			user_city = JSON.parse(locationJSON).name;
    			
    			// Se o user estiver logado tenta ver se existe esse utilizador
    			loginUser();
    			GetAdsInit();
    		} 
    		else if (e.error) 
    		{
        		alert('Erro!');
    		} 
    		else 
    		{
        		alert('Houve um problema na autenticação!');
    		}
    	});
    }
    else
    {
    	GetAdsInit();
    }
    
    function GetAdsInit()
    {
        var string_verify = "no_connection";
        var method = 'POST';
        var url = "http://" + url_ip + ":52144/odata/Ads/GetActiveAds";
        
        // Argumentos para irem no pedido
        // Argumento zero para retornar todas as instituições
        var args = {};
        var int_institution_id = 0;
        args.institution_id = int_institution_id.toString();
        
        // Buscar dados a API
        var connection_api = Titanium.Network.createHTTPClient({
            onload: function()
            {
                while(string_verify == "no_connection")
                {
                    string_verify = JSON.parse(this.responseText);
                }
                
                var array_ads = JSON.parse(string_verify.value);
                
                main_screen.constructorScreen(array_ads, type_back);
                
                main_screen.putEventListenersMainScreen();
                main_screen.showWindow();
            },
            onerror: function()
            {
                alert("Erro na obtenção de anúncios!");
            },
            timeout: 10000 // Tempo para fazer pedido
        });
        
        connection_api.open(method, url, false);
        connection_api.setRequestHeader("Content-Type", "application/json; charset=utf-8"); // Adaptar o tipo de header para o que é pedido
        
        connection_api.send(JSON.stringify(args));
    }
}
