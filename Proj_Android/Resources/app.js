// Incluir o ficheiro da pagina inicial e para a sessao do utilizador
Titanium.include("menu/main_screen.js");
Titanium.include("facebook/init_facebook.js");
Titanium.include("user/session_user.js");
Titanium.include("components/lateral_bar.js");
Titanium.include("components/events_lateral_bar.js");

var main_screen = new MainScreen();
var url_ip = "192.168.1.67";

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
	main_screen.constructorScreen();
			
	main_screen.putEventListenersMainScreen();
	main_screen.showWindow();
}

function GetAdsInit()
{
    var string_verify = "no_connection";
    var method = 'POST';
    var url = "http://" + url_ip + ":52144/odata/Ads/GetActiveAds";
    
    // Argumentos para irem no pedido
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
            
            main_screen.constructorScreen(array_ads);
            
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
