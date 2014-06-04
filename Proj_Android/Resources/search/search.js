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
	
	var logo_plus_search = null;
	var logo_hospital_search = null;
	
	var plus_click = false;
	var institution_click = false;
	var price_click = false;
	var institutions_view = null;
	var institutions_scroll_view = null;
	var price_view = null;
	var price_slide_view = null;
	var init_institutions_filter = null;
	var init_price_filter = null;
	
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
		
		var view_init_search = Titanium.UI.createView({
			left: '20.43%',
			top: '42.64dp',
			height: '131.98dp',
			width: '58.23%'
		});
		
		var logo_search = Titanium.UI.createImageView({
			left: '7.77%',
			top: '0%',
			height: '21.86%',
			width: '16.15%',
			image: '/healthplus/healthplus_search.png'
		});
		
		view_init_search.add(logo_search);
		
		var text_search = Titanium.UI.createLabel({
			text: 'PESQUISA',
			left: '28.66%',
			color: '#d96b63',
			top: '3.08%',
			font: 
			{
				fontSize: '13dp'
			}
		});
		
		view_init_search.add(text_search);
		
		var line_search = Titanium.UI.createView({
			backgroundColor: '#d96b63',
			top: '40.04%',
			width: '100%',
			height: '0.68%'
		});
		
		view_init_search.add(line_search);
		
		logo_plus_search = Titanium.UI.createImageView({
			left: '7.77%',
			bottom: '0%',
			width: '28.14%',
			height: '37.88%',
			id: 'search_cupon',
			image: '/healthplus/healthplus_plus.png'
		});
		
		view_init_search.add(logo_plus_search);
		
		logo_hospital_search = Titanium.UI.createImageView({
			width: '28.14%',
			height: '37.88%',
			bottom: '0%',
			left: '61.89%',
			image: '/healthplus/healthplus_institution.png' 
		});
		
		view_init_search.add(logo_hospital_search);
		search_scroll.add(view_init_search);
		
		institutions_view = Titanium.UI.createView({
			top: '199.09dp',
			left: '24.77%',
			width: '48.08%',
			height: '24.85dp',
			backgroundColor: '#da8673'
		});
		
		init_institutions_filter = 199.09 + 24.85;
		
		var institutions_view_text = Titanium.UI.createLabel({
			top: '0%',
			left: '5%',
			text: "INSTITUIÇÕES",
			width: '76.9%',
			height: '100%',
			color: 'white',
			font: 
			{
				fontSize: '10dp'
			}
		});
		
		institutions_view.add(institutions_view_text);
		institutions_view.setVisible(false);
		search_scroll.add(institutions_view);
		
		institutions_scroll_view = Titanium.UI.createScrollView({
			overScrollMode: Titanium.UI.Android.OVER_SCROLL_ALWAYS,
			scrollType: "vertical",
			backgroundColor: '#a39795',
			width: '48.08%',
			height: '109.94dp',
			left: '24.77%',
			top: '230.70dp'
		});
		
		institutions_scroll_view.setVisible(false);
		search_scroll.add(institutions_scroll_view);
		
		init_price_filter = init_institutions_filter + 21.61;
		
		price_view = Titanium.UI.createView({
			top: init_price_filter + 'dp',
			left: '24.77%',
			width: '48.08%',
			height: '24.85dp',
			backgroundColor: '#da8673',
			id: 'price_view'
		});
		
		var price_view_text = Titanium.UI.createLabel({
			top: '0%',
			left: '5%',
			text: "PREÇO",
			width: '76.9%',
			height: '100%',
			color: 'white',
			font: 
			{
				fontSize: '10dp'
			}
		});
		
		price_view.add(price_view_text);
		price_view.setVisible(false);
		search_scroll.add(price_view);
		
		price_slide_view = Titanium.UI.createView({
			left: '24.77%',
			width: '48.08%',
			height: '40.46dp',
			backgroundColor: '#a39795'
		});
		
		var price_slide = Titanium.UI.createSlider({
			width: '90%',
			min: 0,
			max: 500,
			top: '0%',
			value: 250
		});
		
		price_slide_view.add(price_slide);
		
		var price_slide_min = Titanium.UI.createLabel({
			bottom: '0%',
			left: '5%',
			text: '0€',
			color: 'white',
			font:
			{
				fontSize: '10dp'
			}
		});
		
		price_slide_view.add(price_slide_min);
		
		var price_slide_max = Titanium.UI.createLabel({
			bottom: '0%',
			right: '5%',
			text: '500€',
			color: 'white',
			font:
			{
				fontSize: '10dp'
			}
		});
		
		price_slide_view.add(price_slide_max);
		
		price_slide_view.setVisible(false);
		search_scroll.add(price_slide_view);
		
		search_window.add(search_scroll);
		search_window.add(lateral_bar);
		search_window.add(header_view);
		
		// Fazer com que a barra lateral desapareca
		lateral_bar.setVisible(false);
		
		// Colocar events listeners barra lateral
		var eventListernerLateralBar = new EventsLateralBar();
		eventListernerLateralBar.putListenersEventsLateralBar(lateral_bar_object);
		
		changeLateralBar();
	};
	
	this.putEventListenersSearchScreen = function ()
	{
		var scroll_search = search_scroll;
		
		search_window.addEventListener('swipe', function(e)
		{
			if(e.source.id != 'price_view')
			{
				// Fazer swipe para a esquerda para desaparecer window
				if(e.direction == 'left')
				{
					if(lateral_bar.getVisible() == true)
					{
						lateral_bar.setVisible(false);
						scroll_search.left = '0%';
					}
				}
				else if(e.direction == 'right') // Fazer swipe para a direita para desaparecer window
				{
					if(lateral_bar.getVisible() == false)
					{
						scroll_search.left = '76.44%';
						lateral_bar.setVisible(true);
					}
				}
			}
		});
		
		// Carregar no botao de menu
		header_view_object.menu_header_view.addEventListener('click', function()
		{
			if(lateral_bar.getVisible() == true)
			{
				lateral_bar.setVisible(false);
				scroll_search.left = '0%';
			}
			else
			{
				scroll_search.left = '76.44%';
				lateral_bar.setVisible(true);
			}
		});
		
		// Carregar no botao Mais
		logo_plus_search.addEventListener('click', function()
		{
			if(plus_click == false)
			{
				logo_hospital_search.opacity = 0.7;
				institutions_view.setVisible(true);
				price_view.setVisible(true);
				
				plus_click = true;
			}
			else if(plus_click == true)
			{
				logo_hospital_search.opacity = 1;
				institutions_view.setVisible(false);
				price_view.setVisible(false);
				
				if(institution_click == true)
				{
					institutions_scroll_view.setVisible(false);
					init_price_filter = init_price_filter - 115.98;
					price_view.top = init_price_filter + 'dp';
				}
				if(price_click == true)
				{
					price_slide_view.setVisible(false);
				}
				
				institution_click = false;
				price_click = false;
				plus_click = false;
			}
		});
		
		institutions_view.addEventListener('click', function()
		{
			if(institution_click == false)
			{
				institutions_scroll_view.setVisible(true);
				init_price_filter = init_price_filter + 115.98;
				price_view.top = init_price_filter + 'dp';
				
				if(price_click == true)
				{
					var slide_top = init_price_filter + 24.85 + 5;
					price_slide_view.top = slide_top + 'dp';
				}
				
				institution_click = true;
			}
			else if(institution_click == true)
			{
				institutions_scroll_view.setVisible(false);
				init_price_filter = init_price_filter - 115.98;
				price_view.top = init_price_filter + 'dp';
				
				if(price_click == true)
				{
					var slide_top = init_price_filter + 24.85 + 5;
					price_slide_view.top = slide_top + 'dp';
				}
				
				institution_click = false;
			}
		});
		
		price_view.addEventListener('click', function()
		{
			if(price_click == false)
			{
				var slide_top = init_price_filter + 24.85 + 5;
				price_slide_view.top = slide_top + 'dp';
				
				price_slide_view.setVisible(true);
				price_click = true;
			}
			else if(price_click == true)
			{	
				price_slide_view.setVisible(false);
				price_click = false;
			}
		});
	};
	
	function changeLateralBar()
	{
		setInterval(function()
		{
			if(run_constructor_fb_bool != fb.loggedIn)
			{
				lateral_bar.setVisible(false);
				search_window.remove(lateral_bar);
				search_scroll.left = '7.43%';
				
				lateral_bar_object = new LateralBarWithoutSearch();
				lateral_bar_object.constructorLateralBar();
				lateral_bar = lateral_bar_object.lateral_bar;
				
				lateral_bar.setVisible(false);
				search_window.add(lateral_bar);
		
				eventListernerLateralBar = new EventsLateralBar();
				eventListernerLateralBar.putListenersEventsLateralBar(lateral_bar_object);
				
				run_constructor_fb_bool = fb.loggedIn;				
			}
		}, 150);
	};
	
	this.showWindow = function()
	{
		search_window.open();
	};
}
