// Incluir variavel que dá o ip
Titanium.include("/url_ip.js");

// Include para passar as variaveis cujos listeners vão ser adicionados
Titanium.include("lateral_bar.js");

// Includes de outros ficheiros para que o redireccionamento possa ser feito
Titanium.include("/user/profile_screen.js");
Titanium.include("/institutions/show_institutions.js");
Titanium.include("/institutions/show_institutions_subscriptions.js");
Titanium.include("/menu/main_screen.js");
Titanium.include("/search/search.js");
Titanium.include("/facebook/init_facebook.js");
Titanium.include("/qr_code/look_qr_code.js");

// Só para teste
Titanium.include("/test_functionalities/teste.js");

function EventsLateralBar()
{
	this.putListenersEventsLateralBar = function(lateral_bar, type_back, str, last_window)
	{
		// Botao Home
		lateral_bar.lateral_home_view.addEventListener('click', function()
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
                    
                    var main_screen = new MainScreen();
                    type_back.push(str);
                    main_screen.constructorScreen(array_ads, type_back);
                    
                    main_screen.putEventListenersMainScreen();
                    main_screen.showWindow();
                    
                    last_window.close();
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
		});
		
		// Botao Pesquisa
		lateral_bar.lateral_search_view.addEventListener('click', function()
		{
			var search_screen = new SearchScreen();
			type_back.push(str);
			search_screen.constructorScreen(type_back);
			
			search_screen.putEventListenersSearchScreen();
			search_screen.showWindow();
			
			last_window.close();
		});
		
		// Botao Instituicao
		lateral_bar.lateral_institution_view.addEventListener('click', function()
		{
		    var string_verify = "no_connection";
            var method = 'GET';
            var url = "http://" + url_ip + ":52144/odata/Institutions";
            
            // Buscar dados a API
            var connection_api= Titanium.Network.createHTTPClient({
                onload: function()
                {
                    while(string_verify == "no_connection")
                    {
                        string_verify = JSON.parse(this.responseText);
                    }
                    
                    var array_institutions = JSON.parse(string_verify.value);
                       
                    var institutions_screen = new InstitutionsScreen();
                    type_back.push(str);
                    institutions_screen.constructorScreen(array_institutions, type_back);
                    
                    institutions_screen.showWindow();
                    institutions_screen.putEventListenersInstitutionsScreen();
                    
                    last_window.close();
                },
                onerror: function()
                {
                    alert("Erro na obtenção de instituições!");
                },
                timeout: 10000 // Tempo para fazer pedido
            });
            
            connection_api.open(method, url, false);
            connection_api.send();
		});
		
		if(fb.loggedIn)
		{
			// Botao Subscricao
			lateral_bar.lateral_subscription_view.addEventListener('click', function()
			{
				var string_verify = "no_connection";
                var method = 'POST';
                var url = "http://" + url_ip + ":52144/odata/Subscriptions/InstitutionsSubscribe";
                
                var args = {};
                args.client_id = user_id.toString(); 
                
                // Buscar dados a API
                var connection_api= Titanium.Network.createHTTPClient({
                    onload: function()
                    {
                        while(string_verify == "no_connection")
                        {
                            string_verify = JSON.parse(this.responseText);
                        }
                        
                        var array_institutions;
                        
                        if(string_verify.value == "no subscriptions")
                        {
                            array_institutions = null;
                        }
                        else
                        {
                            array_institutions = JSON.parse(string_verify.value);
                        }
                           
                        var subscriptions_screen = new SubscriptionsInstitutionsScreen();
                        type_back.push(str);
                        subscriptions_screen.constructorScreen(array_institutions, type_back);
                        
                        subscriptions_screen.showWindow();
                        subscriptions_screen.putEventListenersSubscriptionsInstitutionsScreen();
                        
                        last_window.close();
                    },
                    onerror: function()
                    {
                        alert("Erro na obtenção das subscrições!");
                    },
                    timeout: 10000 // Tempo para fazer pedido
                });
                
                connection_api.open(method, url, false);
                connection_api.setRequestHeader("Content-Type", "application/json; charset=utf-8");
          
                connection_api.send(JSON.stringify(args));
			});
		
			// Botao Perfil
			lateral_bar.lateral_perfil_view.addEventListener('click', function()
			{
				var string_verify = "no_connection";
				var method = 'GET';
				var url = "http://" + url_ip + ":52144/odata/Clients(" + user_id.toString() + ")";
				
				// Buscar dados a API
				var connection_api = Titanium.Network.createHTTPClient({
					onload: function()
					{
						while(string_verify == "no_connection")
						{
							string_verify = JSON.parse(this.responseText);
						}
						
						type_back.push(str);
						GetSubscribeAds(type_back, last_window, string_verify);
					},
					onerror: function()
					{
						var current_activity = Titanium.Android.currentActivity;
			     		current_activity.finish(); 
					},
					timeout: 10000 // Tempo para fazer pedido
				});
				
				connection_api.open(method, url, false);
				connection_api.send();
			});
		}
		
		// Botao Qr-code
		lateral_bar.lateral_qr_code_view.addEventListener('click', function()
		{
			var qrcode = new SeeQrCode();
			type_back.push(str);
			qrcode.initializeScanner(type_back);
			
			last_window.close();
		});
		
		// Botao Logout
		lateral_bar.lateral_logout_view.addEventListener('click', function()
		{
			if(fb.loggedIn)
			{
				fb.logout();
			}
			else
			{
				var current_activity = Titanium.Android.currentActivity;
	     		current_activity.finish(); 
	     		
	     		last_window.close();
			}
		});
	};
}

function GetSubscribeAds(type_back, last_window, string_verify_init_profile)
{
    var string_verify = "no_connection";
    var method = 'POST';
    var url = "http://" + url_ip + ":52144/odata/Subscriptions/AdsSubscribe";
    
    var args = {};
    args.client_id = user_id.toString(); 
    
    // Buscar dados a API
    var connection_api= Titanium.Network.createHTTPClient({
        onload: function()
        {
            while(string_verify == "no_connection")
            {
                string_verify = JSON.parse(this.responseText);
            }
            
            var array_ads;
            
            if(string_verify.value == "no subscriptions")
            {
                array_ads = null;
            }
            else
            {
                array_ads = JSON.parse(string_verify.value);
            }
               
            var profile_screen = new ProfileScreen(string_verify_init_profile);
            profile_screen.constructorScreen(type_back, array_ads);
            
            profile_screen.showWindow();
            profile_screen.putEventListenersProfileScreen();
            
            last_window.close();
        },
        onerror: function()
        {
            alert("Erro na obtenção das subscrições!");
        },
        timeout: 10000 // Tempo para fazer pedido
    });
    
    connection_api.open(method, url, false);
    connection_api.setRequestHeader("Content-Type", "application/json; charset=utf-8");
  
    connection_api.send(JSON.stringify(args));
}
