// Incluir o ficheiro da pagina onde a barra e feita
Titanium.include("/components/lateral_bar.js");

// Incluir o ficheiro onde o header e a barra de pesquisa sao feitas
Titanium.include("/components/header_bar.js");

function CuponScreen()
{
	var cupon_window = null;
	var cupon_scroll_view = null;
	
	// Objecto barra lateral
	var lateral_bar_object = new LateralBarWithoutSearch();
	
	var header_view_object = new HeaderViewImage();
	var search_bar_object = new SearchBar();
	
	
	this.constructorScreen = function(number_cupon)
	{
		// Construir barra lateral
		lateral_bar_object.constructorLateralBar();
		var lateral_bar = lateral_bar_object.lateral_bar;
		
		// Construir header
		header_view_object.constructorHeaderView();
		var header_view = header_view_object.header_view;
		
		// Construir barra de pesquisa
		search_bar_object.constructorSearchBar();
		var search_bar = search_bar_object.search_bar;
		
		cupon_window = Titanium.UI.createWindow({
			navBarHidden: true
		});
		
		var background_view = Titanium.UI.createView({
			backgroundColor: "#f6f2e2"
		});
		
		cupon_window.add(background_view);
		
		/** View para aparecer a info sobre o cupao **/
		cupon_scroll_view = Titanium.UI.createScrollView({
			top: '15.98%'
		});
		
		cupon_window.add(cupon_scroll_view);
		cupon_window.add(header_view);
		cupon_window.add(search_bar);
		cupon_window.add(lateral_bar);
		
		// Fazer com que no inicio a searchBar não fique visivel
		search_bar.setVisible(false);
		search_bar_object.putEventListenerToShowSearchBar(header_view_object.search_header_view);
		
		// Fazer com que a barra lateral desapareca
		lateral_bar.setVisible(false);
		
		// Colocar events listeners barra lateral
		var eventListernerLateralBar = new EventsLateralBar();
		eventListernerLateralBar.putListenersEventsLateralBar(lateral_bar_object);
	};
	
	this.putEventListenersCuponScreen = function ()
	{
		var lateral_bar_listener = lateral_bar_object.lateral_bar;
		var cupon_scroll = cupon_scroll_view;
		
		cupon_window.addEventListener('swipe', function(e)
		{
			// Fazer swipe para a esquerda para desaparecer window
			if(e.direction == 'left')
			{
				if(lateral_bar_listener.getVisible() == true)
				{
					lateral_bar_listener.setVisible(false);
					cupon_scroll.left = '0%';
				}
			}
			else if(e.direction == 'right') // Fazer swipe para a direita para desaparecer window
			{
				if(lateral_bar_listener.getVisible() == false)
				{
					cupon_scroll.left = '76.44%';
					lateral_bar_listener.setVisible(true);
				}
			}
		});
		
		// Carregar no botao de menu
		header_view_object.menu_header_view.addEventListener('click', function()
		{
			if(lateral_bar_listener.getVisible() == true)
			{
				lateral_bar_listener.setVisible(false);
				cupon_scroll.left = '0%';
			}
			else
			{
				cupon_scroll.left = '76.44%';
				lateral_bar_listener.setVisible(true);
			}
		});
	};
	
	this.showWindow = function()
	{
		cupon_window.open();
	};
}
