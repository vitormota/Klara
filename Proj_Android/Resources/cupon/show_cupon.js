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
			top: '86.96dp',
			overScrollMode: Titanium.UI.Android.OVER_SCROLL_ALWAYS,
			scrollType: "vertical",
			contentHeight: 'auto',
			width: '100%'
		});
		
		var cupon_image = Titanium.UI.createImageView({
			image: "/healthplus/healthplus_test.jpg",
			top: '0%',
			height: '177.73dp',
			width: '84.68%',
			left: '7.49%',
			borderRadius: 10
		});
		
		cupon_scroll_view.add(cupon_image);
		
		var cupon_information = Titanium.UI.createView({
			left: '7.49%',
			width: '84.68%',
			top: '213.63dp',
			backgroundColor: '#f1e9ce',
			height: '621.67dp'
		});
		
		var cupon_info_text = Titanium.UI.createView({
			left: '10.28%',
			width: '80.55%'
		});
		
		var cupon_title = Titanium.UI.createLabel({
			text: "CUPÃO XYZ",
			color: '#a39795',
			top: '2.32%',
			left: '0%',
			font:
			{
				fontSize: '12.94dp'
			}
		});
		
		cupon_info_text.add(cupon_title);
		
		var cupon_line = Titanium.UI.createView({
			height: '0.27%',
			backgroundColor: '#d96b63',
			top: '5.94%'
		});
		
		cupon_info_text.add(cupon_line);
		
		var cupon_description_title = Titanium.UI.createLabel({
			text: "Descrição",
			top: '8.87%',
			color: '#d96b63',
			left: '0%',
			font:
			{
				fontSize: '11dp'
			}
		});

		cupon_info_text.add(cupon_description_title);
		
		var cupon_description_text = Titanium.UI.createLabel({
			text: "Et audaectes quamusam int voluptiorum alit dis rehent rerum nul¬pa volor aut qui am sandeligent alis sitatia tiumquunt, qui berione cerum, sit as volut odi deles reiur aut molorit alisqui rest, omnis do-lorias elent moditati cum fuga. Nam, voluptatur, sediore sectur?",
			top: '10.71%',
			color: '#d96b63',
			left: '0%',
			font:
			{
				fontSize: '11dp'
			}
		});
		
		cupon_info_text.add(cupon_description_text);
		
		var cupon_data_title = Titanium.UI.createLabel({
			text: "Data",
			top: "25.35%",
			left: '0%',
			color: '#d96b63',
			font: 
			{
				fontSize: '11dp'
			}
		});
		
		cupon_info_text.add(cupon_data_title);
		
		var cupon_data_text = Titanium.UI.createLabel({
			text: "02.11.2014",
			top: "27.08%",
			left: '0%',
			color: '#d96b63',
			font: 
			{
				fontSize: '11dp'
			}
		});
		
		cupon_info_text.add(cupon_data_text);
		
		var cupon_hora_title = Titanium.UI.createLabel({
			text: "Hora",
			top: "30.82%",
			left: '0%',
			color: '#d96b63',
			font: 
			{
				fontSize: '11dp'
			}
		});
		
		cupon_info_text.add(cupon_hora_title);
		
		var cupon_hora_text = Titanium.UI.createLabel({
			text: "17h45",
			top: "32.64%",
			left: '0%',
			color: '#d96b63',
			font: 
			{
				fontSize: '11dp'
			}
		});
		
		cupon_info_text.add(cupon_hora_text);
		
		var cupon_institutions_title = Titanium.UI.createLabel({
			text: "Instituição",
			top: "36.38%",
			left: '0%',
			color: '#d96b63',
			font: 
			{
				fontSize: '11dp'
			}
		});
		
		cupon_info_text.add(cupon_institutions_title);
		
		var cupon_institutions_text = Titanium.UI.createLabel({
			text: "Hospital S.João",
			top: "38.2%",
			left: '0%',
			color: '#d96b63',
			font: 
			{
				fontSize: '11dp'
			}
		});
		
		cupon_info_text.add(cupon_institutions_text);
		
		var cupon_discount_title = Titanium.UI.createLabel({
			text: "Desconto",
			top: "41.94%",
			left: '0%',
			color: '#d96b63',
			font: 
			{
				fontSize: '11dp'
			}
		});
		
		cupon_info_text.add(cupon_discount_title);
		
		var cupon_discount_text = Titanium.UI.createLabel({
			text: "50%",
			top: "43.76%",
			left: '0%',
			color: '#d96b63',
			font: 
			{
				fontSize: '11dp'
			}
		});
		
		cupon_info_text.add(cupon_discount_text);
		
		var cupon_localization_title = Titanium.UI.createLabel({
			text: "Localização",
			top: "47.5%",
			left: '0%',
			color: '#d96b63',
			font: 
			{
				fontSize: '11dp'
			}
		});
		
		cupon_info_text.add(cupon_localization_title);
		
		var cupon_localization_image = Titanium.UI.createImageView({
			image: "/healthplus/healthplus_test.jpg",
			top: "53.61%",
			left: '0%',
			height: '27.45%'
		});
		
		cupon_info_text.add(cupon_localization_image);
		
		var cupon_image_buy = Titanium.UI.createImageView({
			image: "/healthplus/healthplus_buy.png",
			bottom: '5.77%',
			height: '6.66%',
			width: '19.44%'
		});
		
		cupon_info_text.add(cupon_image_buy);
		
		cupon_information.add(cupon_info_text);
		cupon_scroll_view.add(cupon_information);
		
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
