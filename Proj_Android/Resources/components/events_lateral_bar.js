// Include para passar as variaveis cujos listeners vão ser adicionados
Titanium.include("lateral_bar.js");

// Includes de outros ficheiros para que o redireccionamento possa ser feito
Titanium.include("/user/profile_screen.js");
Titanium.include("/institutions/show_institutions.js");
Titanium.include("/menu/main_screen.js");
Titanium.include("/search/search.js");
Titanium.include("/facebook/init_facebook.js");
Titanium.include("/qr_code/look_qr_code.js");

// Só para teste
Titanium.include("/test_functionalities/teste.js");

var url_ip = "192.168.1.67";

function EventsLateralBar()
{
	this.putListenersEventsLateralBar = function(lateral_bar)
	{
		// Botao Home
		lateral_bar.lateral_home_view.addEventListener('click', function()
		{
			var main_screen = new MainScreen();
			main_screen.constructorScreen();
			
			main_screen.showWindow();
			main_screen.putEventListenersMainScreen();
		});
		
		// Botao Pesquisa
		lateral_bar.lateral_search_view.addEventListener('click', function()
		{
			var search_screen = new SearchScreen();
			search_screen.constructorScreen();
			
			search_screen.putEventListenersSearchScreen();
			search_screen.showWindow();
		});
		
		// Botao Instituicao
		lateral_bar.lateral_institution_view.addEventListener('click', function()
		{
			var institutions_screen = new InstitutionsScreen();
			institutions_screen.constructorScreen();
			
			institutions_screen.showWindow();
			institutions_screen.putEventListenersInstitutionsScreen();
		});
		
		if(fb.loggedIn)
		{
			// Botao Subscricao
			lateral_bar.lateral_subscription_view.addEventListener('click', function()
			{
				//alert("Subscrição!");	
				testeDatas();
			});
		
			// Botao Perfil
			lateral_bar.lateral_perfil_view.addEventListener('click', function()
			{
				var string_verify = "no_connection";
				var method = 'GET';
				var url = "http://" + url_ip + ":52144/odata/Clients(" + user_id.toString() + ")";
				
				// Criar clientes para ir buscar dados à api
				var client = Titanium.Network.createHTTPClient({
					onload: function()
					{
						while(string_verify == "no_connection")
						{
							string_verify = JSON.parse(this.responseText);
						}
						
						var profile_screen = new ProfileScreen(string_verify);
						profile_screen.constructorScreen();
				
						profile_screen.showWindow();
						profile_screen.putEventListenersProfileScreen();
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
			});
		}
		
		// Botao Qr-code
		lateral_bar.lateral_qr_code_view.addEventListener('click', function()
		{
			var qrcode = new SeeQrCode();
			qrcode.initializeScanner();
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
			}
		});
	};
}
