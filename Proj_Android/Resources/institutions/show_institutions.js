// Incluir o ficheiro da pagina onde a barra e feita
Titanium.include("/components/lateral_bar.js");

// Incluir o ficheiro onde o header e a barra de pesquisa sao feitas
Titanium.include("/components/header_bar.js");


function InstitutionsScreen()
{
	var institution_window = null;
	var institution_scroll_view = null;
	
	// Objecto barra lateral
	var lateral_bar_object = new LateralBarWithoutSearch();
	
	var header_view_object = new HeaderViewText();
	var search_bar_object = new SearchBar();
	
	this.constructorScreen = function ()
	{
		// Construir barra lateral
		lateral_bar_object.constructorLateralBar();
		var lateral_bar = lateral_bar_object.lateral_bar;
		
		// Construir header
		header_view_object.constructorHeaderView("INSTITUIÇÕES");
		var header_view = header_view_object.header_view;
		
		// Construir barra de pesquisa
		search_bar_object.constructorSearchBar();
		var search_bar = search_bar_object.search_bar;
		
		institution_window = Titanium.UI.createWindow({
			navBarHidden: true
		});
		
		var background_view = Titanium.UI.createView({
			backgroundColor: "#f6f2e2"
		});
		
		institution_window.add(background_view);
		
		/** Colocar as varias instituições em forma de scroll **/
		var number_institutions = 6;
		
		institution_scroll_view = Titanium.UI.createScrollView({
			top: '11.79%',
			overScrollMode: Titanium.UI.Android.OVER_SCROLL_ALWAYS,
			scrollType: "vertical",
			contentHeight: 'auto',
			width: '100%'
		});
		
		for(var i = 0; i < number_institutions; i++)
		{
			
			var top_value = (i * 72.41) + 35.97;
			
			var institution_one_view = Titanium.UI.createView({
				backgroundColor: "#f1e9ce",
				borderRadius: 10,
				top: top_value + 'dp',
				height: '52.34dp',
				left: '7.04%',
				width: '85.21%'
			});
			
			var image_institution = Titanium.UI.createImageView({
				top: '22.03%',
				left: '4.48%',
				width: '11.11%',
				height: '55.16%',
				image: "/healthplus/healthplus_institution.png"
			});
			
			var text_institution = Titanium.UI.createLabel({
				text: "HOSPITAL PRIVADO\nPorto",
				left: '19.03%',
				top: '20.03%',
				color: 'black',
				font:
				{
					fontSize: '10%'
				}
			});
			
			var click_institution = Titanium.UI.createImageView({
				image: '/healthplus/healthplus_positive.png',
				top: '22.03%',
				left: '82.93%',
				width: '11.08%',
				height: '55.66%'
			});
			
			institution_one_view.add(image_institution);
			institution_one_view.add(text_institution);
			institution_one_view.add(click_institution);
			institution_scroll_view.add(institution_one_view);
		}
		
		
		institution_window.add(lateral_bar);
		institution_window.add(institution_scroll_view);
		institution_window.add(search_bar);
		institution_window.add(header_view);
		
		// Fazer com que no inicio a searchBar não fique visivel
		search_bar.setVisible(false);
		search_bar_object.putEventListenerToShowSearchBar(header_view_object.search_header_view);
		
		// Fazer com que a barra lateral desapareca
		lateral_bar.setVisible(false);
		
		// Colocar events listeners barra lateral
		var eventListernerLateralBar = new EventsLateralBar();
		eventListernerLateralBar.putListenersEventsLateralBar(lateral_bar_object);
	};
	
	this.putEventListenersInstitutionsScreen = function ()
	{
		var lateral_bar_listener = lateral_bar_object.lateral_bar;
		var institution_scroll = institution_scroll_view;
		
		institution_window.addEventListener('swipe', function(e)
		{
			// Fazer swipe para a esquerda para desaparecer window
			if(e.direction == 'left')
			{
				if(lateral_bar_listener.getVisible() == true)
				{
					lateral_bar_listener.setVisible(false);
					institution_scroll.left = '0%';
				}
			}
			else if(e.direction == 'right') // Fazer swipe para a direita para desaparecer window
			{
				if(lateral_bar_listener.getVisible() == false)
				{
					institution_scroll.left = '76.44%';
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
				institution_scroll.left = '0%';
			}
			else
			{
				institution_scroll.left = '76.44%';
				lateral_bar_listener.setVisible(true);
			}
		});
	};
	
	this.showWindow = function()
	{
		institution_window.open();
	};
}
