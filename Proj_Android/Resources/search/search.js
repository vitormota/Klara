// Incluir o ficheiro da pagina onde a barra e feita
Titanium.include("/components/lateral_bar.js");

// Incluir o ficheiro onde o header e a barra de pesquisa sao feitas
Titanium.include("/components/header_bar.js");

// Incluir facebook
Titanium.include("/facebook/init_facebook.js");

function SearchScreen()
{
	var header_view_object = new HeaderViewImage();
	
	var lateral_bar_object = new LateralBarWithoutSearch();
	var lateral_bar = null;
	
	var search_window = null;
	var search_scroll = null;
	
	var run_constructor_fb_bool = null;
	
	this.constructorScreen = function()
	{
		// Verificar se existe login
		run_constructor_fb_bool = fb.loggedIn;
		
		// Construir barra lateral
		lateral_bar_object.constructorLateralBar();
		lateral_bar = lateral_bar_object.lateral_bar;
		
		// Construir header (botao pesquisa escondido)
		header_view_object.constructorHeaderView();
		var header_view = header_view_object.header_view;
		header_view_object.search_header_view.setVisible(false);
		
		// Abrir nova janela
		search_window = Titanium.UI.createWindow({
			navBarHidden: true
		}); 
		
		var background_view = Titanium.UI.createView({
			backgroundColor: "#f6f2e2"
		});
		
		search_window.add(background_view);
		
		// Criar scroll view ---> se necessário
		search_scroll = Titanium.UI.createScrollView({
			overScrollMode: Titanium.UI.Android.OVER_SCROLL_ALWAYS,
			scrollType: "vertical",
			top: '11.79%',
			width: '100%'
		});
		
		search_window.add(search_scroll);
		search_window.add(lateral_bar);
		search_window.add(header_view);
		
		// Fazer com que a barra lateral desapareca
		lateral_bar.setVisible(false);
		
		// Colocar events listeners barra lateral
		var eventListernerLateralBar = new EventsLateralBar();
		eventListernerLateralBar.putListenersEventsLateralBar(lateral_bar_object);
	};
	
	this.showWindow = function()
	{
		search_window.open();
	};
}
