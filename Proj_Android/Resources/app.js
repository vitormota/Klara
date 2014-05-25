// Incluir o ficheiro da pagina inicial e para a sessao do utilizador
Titanium.include("menu/main_screen.js");
Titanium.include("user/session_user.js");
Titanium.include("components/lateral_bar.js");
Titanium.include("components/events_lateral_bar.js");

var main_screen = new MainScreen();
main_screen.constructorScreen();


/** Inicializar a ligacao com o facebook **/
var fb = require('facebook');
fb.appid = 234060146782596;
fb.permissions = ['public_profile', 'email', 'user_location', 'user_about_me', 'user_photos']; // Permissions your app needs
fb.forceDialogAuth = true;
fb.addEventListener('login', function(e) 
{
    if (e.success) 
    {
        var JSONdata = JSON.parse(e.data);
        
        // Dados globais
		user_name = JSONdata.name;
		user_facebook_id = parseFloat(JSONdata.id);
		user_email = JSONdata.email;
		
		//Dados apenas para registo
		var location = JSONdata.location;
		var locationJSON = JSON.stringify(location);
		user_city = JSON.parse(locationJSON).name;
		
		existUserInDatabase();
    } 
    else if (e.error) 
    {
        var current_activity = Titanium.Android.currentActivity;
     	current_activity.finish();  
    } 
    else if (e.cancelled)
	{
        var current_activity = Titanium.Android.currentActivity;
     	current_activity.finish(); 
    }
});
fb.authorize();

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
		} 
		else if (e.error) 
		{
    		var current_activity = Titanium.Android.currentActivity;
     		current_activity.finish(); 
		} 
		else 
		{
    		var current_activity = Titanium.Android.currentActivity;
     		current_activity.finish(); 
		}
	});
}

function existUserInDatabase()
{
	var string_verify = "no_connection";
	var method = 'POST';
	var url = "http://192.168.1.67:52144/odata/Accounts/AccountExistDatabase";
	//var url = "http://172.30.57.248:52144/odata/Accounts/AccountExistDatabase";
	
	// Argumentos para irem no pedido
	var args = {};
	args.facebook_id = user_facebook_id.toString();
	
	// Criar clientes para ir buscar dados à api
	var client = Titanium.Network.createHTTPClient({
		onload: function()
		{
			// Ciclo while que
			while(string_verify == "no_connection")
			{
				string_verify = JSON.parse(this.responseText).value;
			}
			
			if(string_verify == "false")
			{
				// Se nao existir vai fazer o registo
				registerAccount();
			}
			else if(string_verify == "error")
			{
				var current_activity = Titanium.Android.currentActivity;
     			current_activity.finish(); 
			}
			else
			{
				// Vai fazer o login do utilizador
				user_id = parseInt(string_verify);
				loginUser();
			}
		},
		onerror: function()
		{
			var current_activity = Titanium.Android.currentActivity;
     		current_activity.finish(); 
		},
		timeout: 10000 // Tempo para fazer pedido
	});
	
	client.open(method, url, false);
	client.setRequestHeader("Content-Type", "application/json; charset=utf-8"); // Adaptar o tipo de header para o que é pedido
	
	client.send(JSON.stringify(args));
}

function registerAccount()
{
	var string_verify = "no_connection";
	var method = 'POST';
	var url = "http://192.168.1.67:52144/odata/Accounts";
	//var url = "http://172.30.57.248:52144/odata/Accounts";
	
	// Argumentos para irem no pedido
	var args = {};
	args.fb_id = user_facebook_id.toString();
	
	// Criar clientes para ir buscar dados à api
	var client = Titanium.Network.createHTTPClient({
		onload: function()
		{
			while(string_verify == "no_connection")
			{
				string_verify = JSON.parse(this.responseText).id;
			}
			
			user_id = parseInt(string_verify);
			registerClient();
		},
		onerror: function()
		{
			var current_activity = Titanium.Android.currentActivity;
     		current_activity.finish(); 
		},
		timeout: 10000 // Tempo para fazer pedido
	});
	
	client.open(method, url, false);
	client.setRequestHeader("Content-Type", "application/json; charset=utf-8"); // Adaptar o tipo de header para o que é pedido
	
	client.send(JSON.stringify(args));
}

function registerClient()
{
	var string_verify = "no_connection";
	var method = 'POST';
	var url = "http://192.168.1.67:52144/odata/Clients";
	//var url = "http://172.30.57.248:52144/odata/Clients";
	
	// Argumentos para irem no pedido
	var args = {};
	args.id = user_id.toString();
	args.name = user_name;
	args.city = user_city;
	args.email = user_email;
	
	// Criar clientes para ir buscar dados à api
	var client = Titanium.Network.createHTTPClient({
		onload: function()
		{
			while(string_verify == "no_connection")
			{
				string_verify = JSON.parse(this.responseText).id;
			}
			
			if(user_id == parseInt(string_verify))
			{
				// Como deve de estar correto, passa para o ecra inicial
				main_screen.putEventListenersMainScreen();
				main_screen.showWindow();
			}
			else
			{
				var current_activity = Titanium.Android.currentActivity;
     			current_activity.finish(); 
			}
		},
		onerror: function()
		{
			var current_activity = Titanium.Android.currentActivity;
     		current_activity.finish(); 
		},
		timeout: 10000 // Tempo para fazer pedido
	});
	
	client.open(method, url, false);
	client.setRequestHeader("Content-Type", "application/json; charset=utf-8"); // Adaptar o tipo de header para o que é pedido
	
	client.send(JSON.stringify(args));
}

function loginUser()
{
	var string_verify = "no_connection";
	var method = 'GET';
	var url = "http://192.168.1.67:52144/odata/Accounts(" + user_facebook_id.toString() + "L)";
	//var url = "http://172.30.57.248:52144/odata/Accounts(" + user_facebook_id.toString() + "L)";
	
	// Criar clientes para ir buscar dados à api
	var client = Titanium.Network.createHTTPClient({
		onload: function()
		{
			while(string_verify == "no_connection")
			{
				string_verify = JSON.parse(this.responseText).id;
			}
			
			if(parseInt(string_verify) == user_id || user_id == null)
			{
				user_id = parseInt(string_verify);
				main_screen.putEventListenersMainScreen();
				main_screen.showWindow();
			}
			else
			{
				var current_activity = Titanium.Android.currentActivity;
     			current_activity.finish(); 
			}
		},
		onerror: function()
		{
			var current_activity = Titanium.Android.currentActivity;
     		current_activity.finish(); 
		},
		timeout: 10000 // Tempo para fazer pedido
	});
	
	client.open(method, url, false);
	client.send();
}


