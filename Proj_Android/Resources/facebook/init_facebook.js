// Incluir variavel que dá o ip
Titanium.include("/url_ip.js");

// Incluir ficheiro para a sessao do utilizador
Titanium.include("/user/session_user.js"); 

var fb = require('facebook');

function InitFacebook()
{
    /** Inicializar a ligacao com o facebook **/
    fb = require('facebook');
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
	        alert('Erro!');
	    } 
	    else if (e.cancelled)
		{
	        alert('Autenticação cancelada!');
	    }
	});
	fb.authorize();
}

function existUserInDatabase()
{
	var string_verify = "no_connection";
	var method = 'POST';
	var url = "http://" + url_ip + ":52144/odata/Accounts/AccountExistDatabase";
	
	// Argumentos para irem no pedido
	var args = {};
	args.facebook_id = user_facebook_id.toString();
	
	// Buscar dados a API
    var connection_api = Titanium.Network.createHTTPClient({
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
				alert('Erro!');
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
			alert('Erro!');
		},
		timeout: 10000 // Tempo para fazer pedido
	});
	
	connection_api.open(method, url, false);
	connection_api.setRequestHeader("Content-Type", "application/json; charset=utf-8"); // Adaptar o tipo de header para o que é pedido
	
	connection_api.send(JSON.stringify(args));
}

function registerAccount()
{
	var string_verify = "no_connection";
	var method = 'POST';
	var url = "http://" + url_ip + ":52144/odata/Accounts";
	//var url = "http://172.30.57.248:52144/odata/Accounts";
	
	// Argumentos para irem no pedido
	var args = {};
	args.fb_id = user_facebook_id.toString();
	
	// Buscar dados a API
    var connection_api = Titanium.Network.createHTTPClient({
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
			alert('Erro!');
		},
		timeout: 10000 // Tempo para fazer pedido
	});
	
	connection_api.open(method, url, false);
	connection_api.setRequestHeader("Content-Type", "application/json; charset=utf-8"); // Adaptar o tipo de header para o que é pedido
	
	connection_api.send(JSON.stringify(args));
}

function registerClient()
{
	var string_verify = "no_connection";
	var method = 'POST';
	var url = "http://" + url_ip + ":52144/odata/Clients";
	//var url = "http://172.30.57.248:52144/odata/Clients";
	
	// Argumentos para irem no pedido
	var args = {};
	args.id = user_id.toString();
	args.name = user_name;
	args.city = user_city;
	args.email = user_email;
	
	// Buscar dados a API
    var connection_api = Titanium.Network.createHTTPClient({
		onload: function()
		{
			while(string_verify == "no_connection")
			{
				string_verify = JSON.parse(this.responseText).id;
			}
			
			if(user_id == parseInt(string_verify))
			{
				// Não faz nada, deixa correr
			}
			else
			{
				alert('Houve um problema na autenticação!');
			}
		},
		onerror: function()
		{
			alert('Erro!');
		},
		timeout: 10000 // Tempo para fazer pedido
	});
	
	connection_api.open(method, url, false);
	connection_api.setRequestHeader("Content-Type", "application/json; charset=utf-8"); // Adaptar o tipo de header para o que é pedido
	
	connection_api.send(JSON.stringify(args));
}

function loginUser()
{
	var string_verify = "no_connection";
	var method = 'GET';
	var url = "http://" + url_ip + ":52144/odata/Accounts(" + user_facebook_id.toString() + "L)";
	//var url = "http://172.30.57.248:52144/odata/Accounts(" + user_facebook_id.toString() + "L)";
	
	// Buscar dados a API
    var connection_api = Titanium.Network.createHTTPClient({
		onload: function()
		{
			while(string_verify == "no_connection")
			{
				string_verify = JSON.parse(this.responseText).id;
			}
			
			if(parseInt(string_verify) == user_id || user_id == null)
			{
                user_id = parseInt(string_verify);
			}
			else
			{
				alert('Erro!');
			}
		},
		onerror: function()
		{
			alert('Erro!');
		},
		timeout: 10000 // Tempo para fazer pedido
	});
	
	connection_api.open(method, url, false);
	connection_api.send();
}
