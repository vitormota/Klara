// Incluir o ficheiro da pagina inicial e para a sessao do utilizador
Titanium.include("menu/main_screen.js");
Titanium.include("facebook/init_facebook.js");
Titanium.include("user/session_user.js");
Titanium.include("components/lateral_bar.js");
Titanium.include("components/events_lateral_bar.js");

var main_screen = new MainScreen();

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
			//loginUser();
			
			main_screen.constructorScreen();
			
			main_screen.putEventListenersMainScreen();
			main_screen.showWindow();
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
