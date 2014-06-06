// Incluir o ficheiro da pagina onde a barra e feita
Titanium.include("/components/lateral_bar.js");

// Incluir o ficheiro onde o header e a barra de pesquisa sao feitas
Titanium.include("/components/header_bar.js");

// Incluir ficheiro com variaveis de sessao
Titanium.include("/user/session_user.js");

// Incluir facebook
Titanium.include("/facebook/init_facebook.js");

// Incluir ficheiro para pag. principal
Titanium.include("/menu/main_screen.js");

function ProfileScreen(infoDatabase)
{
	var header_view_object = new HeaderViewText();
	
	var lateral_bar_object = new LateralBarWithoutSearch();
	var lateral_bar = null;
	
	var search_bar_object = new SearchBar();
	
	var window_perfil = null;
	var scroll_view = null;
	
	// "Tabs" onde se tem que clicar
	var view_dados_pessoais = null;
	var view_cupoes_subscritos = null;
	var view_cupoes_adquiridos = null;
	
	// Butões
	var button_edit_dados_pessoais = null;

	// Linhas que separam
	var line2 = null;
	var line3 = null;
	var line4 = null;
	
	var edit = false;
	
	var run_constructor_fb_bool = null;
	
	// Info sobre os clientes
	if(infoDatabase.phone_number == null)
	{
		var client_phone_number = 'n/d';
	}
	else
	{
		var client_phone_number = infoDatabase.phone_number;
	}
	if(infoDatabase.nif == null)
	{
		var client_nif = 'n/d';
	}
	else
	{
		var client_nif = infoDatabase.nif;
	}
	if(infoDatabase.address == null)
	{
		var client_address = 'n/d';
	}
	else
	{
		var client_address = infoDatabase.address;
	}
	if(infoDatabase.city == null)
	{
		var client_city = 'n/d';
	}
	else
	{
		var client_city = infoDatabase.city;
	}
	
	// Valores a serem usados 
	var value_top_line2 = 0;
	var value_top_cupoes_subscritos = 0;
	var value_top_line3 = 0;
	var value_top_cupoes_adquiridos = 0;
	var value_top_line4 = 0;
	
	// Variaveis para alterar o tipo de como aparece
	var see_dados_pessoais = null;
	
	// Partes a editar
	var see_phone_number = null;
	var see_address = null;
	var see_city = null;
	var see_nif = null;
	
	// Botoes para editar
	var button_save_dados_pessoais = null;
	var button_not_save_dados_pessoais = null;
	
	this.constructorScreen = function()
	{
		// Verificar se existe login
		run_constructor_fb_bool = fb.loggedIn;
		
		// Construir barra lateral
		lateral_bar_object.constructorLateralBar();
		lateral_bar = lateral_bar_object.lateral_bar;
		
		// Construir header
		header_view_object.constructorHeaderView("PERFIL");
		var header_view = header_view_object.header_view;
		
		// Construir barra de pesquisa
		search_bar_object.constructorSearchBar();
		var search_bar = search_bar_object.search_bar;
		
		// Abrir nova janela
		window_perfil = Titanium.UI.createWindow({
			navBarHidden: true
		}); 
		
		var background_view = Titanium.UI.createView({
			backgroundColor: "#f6f2e2"
		});
		
		window_perfil.add(background_view);
		
		// Criar scroll view
		scroll_view = Titanium.UI.createScrollView({
			overScrollMode: Titanium.UI.Android.OVER_SCROLL_ALWAYS,
			scrollType: "vertical",
			top: '11.79%',
			width: '100%'
		});
		
		// Views relacionadas com a parte de mostrar a imagem do user e o nome
		var view_name_image_perfil = Titanium.UI.createView({
			width: '78.64%',
			height: '45.98dp',
			top: '41.29dp',
			left: '6.59%'
		});
		
		var image_view_perfil = Titanium.UI.createImageView({
			image: "https://graph.facebook.com/" + user_facebook_id + "/picture",
			width: '18.99%',
			height: '100%',
			left: '0%',
			top: '0%'
		});
		
		view_name_image_perfil.add(image_view_perfil);
		
		var text_view_perfil = Titanium.UI.createLabel({
			text: user_name + "\n" + user_email,
			top: '0%',
			left: '24.93%',
			color: 'black',
			font:
			{
				fontSize: '11%'
			}
		});
		
		view_name_image_perfil.add(text_view_perfil);
		
		
		var line_view = Titanium.UI.createView({
			backgroundColor: '#d96b63',
			height: '1.82%',
			bottom: '0%',
			left: '24.93%',
			zIndex: 1
		});
		
		view_name_image_perfil.add(line_view);
		scroll_view.add(view_name_image_perfil);
		
		// Criar primeiro separador
		line1 = Titanium.UI.createView({
			backgroundColor: "#f1e9ce",
			width: '100%',
			height: '2.58dp',
			top: '144.75dp'
		});
		
		scroll_view.add(line1);
		
		// Criar view para mostrar dados pessoais
		var value_top_dados_pessoais = 151.26;
		var value_height_dados_pessoais = 50.43;
		
		view_dados_pessoais = Titanium.UI.createView({
			top: value_top_dados_pessoais + 'dp',
			height: value_height_dados_pessoais + 'dp'
		});
		
		var image_view_dados_pessoais = Titanium.UI.createImageView({
			image: "/healthplus/healthplus_perfil.png",
			left: '7.20%',
			width: '9.66%',
			height: '58.27%',
			top: '20.01%'
		});
		
		view_dados_pessoais.add(image_view_dados_pessoais);
		
		var text_view_dados_pessoais = Titanium.UI.createLabel({
			text:"DADOS PESSOAIS",
			color: "#d96b63",
			left: '28.89%',
			font:
			{
				fontSize: '13%'
			},
			top: '30.01%'
		});
		
		view_dados_pessoais.add(text_view_dados_pessoais);
		scroll_view.add(view_dados_pessoais);
		
		button_edit_dados_pessoais = Titanium.UI.createImageView({
			height: '17.13dp',
			top: '167.86dp',
			left: '73.98%',
			width: '5.81%',
			image: "/healthplus/healthplus_edit.png"
		});
		
		button_edit_dados_pessoais.setVisible(false);
		scroll_view.add(button_edit_dados_pessoais);
		
		// Colocar info pessoal
		var value_top_see_dados_pessoais = 199.66;
		var value_height_see_dados_pessoais = 242.32;
		
		see_dados_pessoais = Titanium.UI.createView({
			top: value_top_see_dados_pessoais + 'dp',
			height: value_height_see_dados_pessoais + 'dp'
		});
		
		/* ---------------------- Componentes a aparecer nos dados pessoais -----------------------------*/
		// Info sobre numero telemovel
		var see_title_phone_number = Titanium.UI.createLabel({
			left: '28.89%',
			top: '0%',
			color: 'black',
			text: 'TELEMÓVEL',
			font:
			{
				fontSize: '11%'
			}
		});
		
		see_dados_pessoais.add(see_title_phone_number);
		
		see_phone_number = Titanium.UI.createLabel({
			left: '28.89%',
			top: '9.5%',
			color: 'black',
			text: client_phone_number,
			font:
			{
				fontSize: '11%'
			}
		});
		
		see_dados_pessoais.add(see_phone_number);
		
		// Info sobre a morada
		var see_title_address = Titanium.UI.createLabel({
			left: '28.89%',
			top: '25%',
			color: 'black',
			text: 'MORADA',
			font:
			{
				fontSize: '11%'
			}
		});
		
		see_dados_pessoais.add(see_title_address);
		
		see_address = Titanium.UI.createLabel({
			left: '28.89%',
			top: '34.5%',
			color: 'black',
			text: client_address,
			font:
			{
				fontSize: '11%'
			}
		});
		
		see_dados_pessoais.add(see_address);
		
		// Info sobre a cidade
		var see_title_city = Titanium.UI.createLabel({
			left: '28.89%',
			top: '50%',
			color: 'black',
			text: 'CIDADE',
			font:
			{
				fontSize: '11%'
			}
		});
		
		see_dados_pessoais.add(see_title_city);
		
		see_city = Titanium.UI.createLabel({
			left: '28.89%',
			top: '59.5%',
			color: 'black',
			text: client_city,
			font:
			{
				fontSize: '11%'
			}
		});
		
		see_dados_pessoais.add(see_city);
		
		// Info sobre o nif do utilizador
		var see_title_nif = Titanium.UI.createLabel({
			left: '28.89%',
			top: '75%',
			color: 'black',
			text: 'NIF',
			font:
			{
				fontSize: '11%'
			}
		});
		
		see_dados_pessoais.add(see_title_nif);
		
		see_nif = Titanium.UI.createLabel({
			left: '28.89%',
			top: '84.5%',
			color: 'black',
			text: client_nif,
			font:
			{
				fontSize: '11%'
			}
		});
		
		see_dados_pessoais.add(see_nif);
		
		see_dados_pessoais.setVisible(false);
		scroll_view.add(see_dados_pessoais);
		
		// Colocacao da linha nº 2 (segundo separador)
		value_top_line2 = value_top_dados_pessoais + value_height_dados_pessoais;
		
		line2 = Titanium.UI.createView({
			backgroundColor: "#f1e9ce",
			width: '100%',
			height: '2.58dp',
			top: value_top_line2 +'dp' 
		});
		
		scroll_view.add(line2);
		
		// View para mostrar cupoes subscritos
		value_top_cupoes_subscritos = value_top_line2 + 2.58;
		var value_height_cupoes_subscritos = 50.43;
		
		view_cupoes_subscritos = Titanium.UI.createView({
			top: value_top_cupoes_subscritos + 'dp',
			height: value_height_cupoes_subscritos + 'dp'
		});
		
		var image_view_cupoes_subscritos = Titanium.UI.createImageView({
			image: "/healthplus/healthplus_subscription.png",
			left: '7.20%',
			width: '9.66%',
			height: '58.27%',
			top: '23.01%'
		});
		
		view_cupoes_subscritos.add(image_view_cupoes_subscritos);
		
		var text_view_cupoes_subscritos = Titanium.UI.createLabel({
			text:"CUPÕES SUBSCRITOS",
			color: "#d96b63",
			left: '28.89%',
			font:
			{
				fontSize: '13%'
			},
			top: '30.01%'
		});
		
		view_cupoes_subscritos.add(text_view_cupoes_subscritos);
		scroll_view.add(view_cupoes_subscritos);
		
		// Fazer o separador numero 3
		value_top_line3 = value_height_cupoes_subscritos + value_top_cupoes_subscritos;
		
		line3 = Titanium.UI.createView({
			backgroundColor: "#f1e9ce",
			width: '100%',
			height: '2.58dp',
			top: value_top_line3 +'dp' 
		});
		
		scroll_view.add(line3);
		
		// View para mostrar cupoes adquiridos
		value_top_cupoes_adquiridos = value_top_line3 + 2.58;
		var value_height_cupoes_adquiridos = 50.43;
		
		view_cupoes_adquiridos = Titanium.UI.createView({
			top: value_top_cupoes_adquiridos + 'dp',
			height: value_height_cupoes_adquiridos + 'dp'
		});
		
		var image_view_cupoes_adquiridos = Titanium.UI.createImageView({
			image: "/healthplus/healthplus_positive.png",
			left: '7.20%',
			width: '9.66%',
			height: '58.27%',
			top: '23.01%'
		});
		
		view_cupoes_adquiridos.add(image_view_cupoes_adquiridos);
		
		var text_view_cupoes_adquiridos = Titanium.UI.createLabel({
			text:"CUPÕES ADQUIRIDOS",
			color: "#d96b63",
			left: '28.89%',
			font:
			{
				fontSize: '13%'
			},
			top: '30.01%'
		});
		
		view_cupoes_adquiridos.add(text_view_cupoes_adquiridos);
		scroll_view.add(view_cupoes_adquiridos);
		
		// Fazer o ultimo separador
		value_top_line4 = value_height_cupoes_adquiridos + value_top_cupoes_adquiridos;
		
		line4 = Titanium.UI.createView({
			backgroundColor: "#f1e9ce",
			width: '100%',
			height: '2.58dp',
			top: value_top_line4 +'dp' 
		});
		
		scroll_view.add(line4);
		
		window_perfil.add(scroll_view);
		window_perfil.add(lateral_bar);
		window_perfil.add(search_bar);
		window_perfil.add(header_view);
		
		// Fazer com que no inicio a searchBar não fique visivel
		search_bar.setVisible(false);
		search_bar_object.putEventListenerToShowSearchBar(header_view_object.search_header_view);
		
		// Fazer com que a barra lateral desapareca
		lateral_bar.setVisible(false);
		
		// Colocar events listeners barra lateral
		var eventListernerLateralBar = new EventsLateralBar();
		eventListernerLateralBar.putListenersEventsLateralBar(lateral_bar_object);
		
		// Verificar se esta logado
		changeLateralBar();
	};
	
	this.putEventListenersProfileScreen = function ()
	{
		var scroll_profile = scroll_view;
		
		window_perfil.addEventListener('swipe', function(e)
		{
			// Fazer swipe para a esquerda para desaparecer window
			if(e.direction == 'left')
			{
				if(lateral_bar.getVisible() == true)
				{
					lateral_bar.setVisible(false);
					scroll_profile.left = '0%';
				}
			}
			else if(e.direction == 'right') // Fazer swipe para a direita para desaparecer window
			{
				if(lateral_bar.getVisible() == false)
				{
					scroll_profile.left = '76.44%';
					lateral_bar.setVisible(true);
				}
			}
		});
		
		// Carregar no botao de menu
		header_view_object.menu_header_view.addEventListener('click', function()
		{
			if(lateral_bar.getVisible() == true)
			{
				lateral_bar.setVisible(false);
				scroll_profile.left = '0%';
			}
			else
			{
				scroll_profile.left = '76.44%';
				lateral_bar.setVisible(true);
			}
		});
		
		// Alterar alturas
		view_dados_pessoais.addEventListener('click', function()
		{	
			if(see_dados_pessoais.getVisible() == false)
			{
				value_top_line2 = value_top_line2 + 250;
				line2.top = value_top_line2 + 'dp';
				
				value_top_cupoes_subscritos = value_top_cupoes_subscritos + 250;
				view_cupoes_subscritos.top = value_top_cupoes_subscritos + 'dp';
				
				value_top_line3 = value_top_line3 + 250;
				line3.top = value_top_line3 + 'dp';
				
				value_top_cupoes_adquiridos = value_top_cupoes_adquiridos + 250;
				view_cupoes_adquiridos.top = value_top_cupoes_adquiridos + 'dp';
				
				value_top_line4 = value_top_line4 + 250;
				line4.top = value_top_line4 + 'dp';
				
				if(edit == true)
				{
					button_not_save_dados_pessoais.setVisible(true);
					button_save_dados_pessoais.setVisible(true);
				}
				else
				{
					button_edit_dados_pessoais.setVisible(true);
				}
				
				see_dados_pessoais.setVisible(true);
			}
			else if(see_dados_pessoais.getVisible() == true)
			{
				see_dados_pessoais.setVisible(false);
				button_edit_dados_pessoais.setVisible(false);
				
				if(edit == true)
				{
					button_not_save_dados_pessoais.setVisible(false);
					button_save_dados_pessoais.setVisible(false);
				}
				
				value_top_line2 = value_top_line2 - 250;
				line2.top = value_top_line2 + 'dp';
				
				value_top_cupoes_subscritos = value_top_cupoes_subscritos - 250;
				view_cupoes_subscritos.top = value_top_cupoes_subscritos + 'dp';
				
				value_top_line3 = value_top_line3 - 250;
				line3.top = value_top_line3 + 'dp';
				
				value_top_cupoes_adquiridos = value_top_cupoes_adquiridos - 250;
				view_cupoes_adquiridos.top = value_top_cupoes_adquiridos + 'dp';
				
				value_top_line4 = value_top_line4 - 250;
				line4.top = value_top_line4 + 'dp';
			}
		});
		
		button_edit_dados_pessoais.addEventListener('click', function()
		{
			if(edit == false)
			{
				edit = true;
			}
			
			// Telefone
			see_phone_number.setVisible(false);
			
			var see_field_phone_number = Titanium.UI.createTextField({
				left: '28.89%',
				backgroundColor: "white",
				autocorrect: false,
				top: '9.5%',
				color: 'black',
				value: see_phone_number.text,
				width: '45%',
				font:
				{
					fontSize: '11%'
				}
			});
			
			see_dados_pessoais.add(see_field_phone_number);
			
			// Morada
			see_address.setVisible(false);
			
			var see_field_address = Titanium.UI.createTextField({
				left: '28.89%',
				backgroundColor: "white",
				autocorrect: false,
				top: '34.5%',
				color: 'black',
				value: see_address.text,
				width: '45%',
				font:
				{
					fontSize: '11%'
				}
			});
			
			see_dados_pessoais.add(see_field_address);
			
			// Cidade
			see_city.setVisible(false);
			
			var see_field_city = Titanium.UI.createTextField({
				left: '28.89%',
				backgroundColor: "white",
				autocorrect: false,
				top: '59.5%',
				color: 'black',
				value: see_city.text,
				width: '45%',
				font:
				{
					fontSize: '11%'
				}
			});
			
			see_dados_pessoais.add(see_field_city);
			
			// NIF
			see_nif.setVisible(false);
			
			var see_field_nif = Titanium.UI.createTextField({
				left: '28.89%',
				backgroundColor: "white",
				autocorrect: false,
				top: '84.5%',
				color: 'black',
				value: see_nif.text,
				width: '45%',
				font:
				{
					fontSize: '11%'
				}
			});
			
			see_dados_pessoais.add(see_field_nif);
			
			// Botao para guardar
			button_edit_dados_pessoais.setVisible(false);
			
			button_save_dados_pessoais = Titanium.UI.createImageView({
				height: '17.13dp',
				top: '167.86dp',
				left: '73.98%',
				width: '5.81%',
				image: "/healthplus/healthplus_positive.png"
			});
			
			scroll_view.add(button_save_dados_pessoais);
			
			// Botao para não guardar
			button_not_save_dados_pessoais = Titanium.UI.createImageView({
				height: '17.13dp',
				top: '167.86dp',
				left: '80.98%',
				width: '5.81%',
				image: "/healthplus/healthplus_negative.png"
			});
			
			scroll_view.add(button_not_save_dados_pessoais);
			
			button_save_dados_pessoais.addEventListener('click', function()
			{
				// Telemovel
				see_field_phone_number.setVisible(false);
				see_phone_number.text = see_field_phone_number.value;
				see_phone_number.setVisible(true);
				
				// Morada
				see_field_address.setVisible(false);
				see_address.text = see_field_address.value;
				see_address.setVisible(true);
				
				// Cidade
				see_field_city.setVisible(false);
				see_city.text = see_field_city.value;
				see_city.setVisible(true);
				
				// NIF
				see_field_nif.setVisible(false);
				see_nif.text = see_field_nif.value;
				see_nif.setVisible(true);
				
				button_not_save_dados_pessoais.setVisible(false);
				button_save_dados_pessoais.setVisible(false);
				button_edit_dados_pessoais.setVisible(true);
				
				edit = false;
			});
			
			button_not_save_dados_pessoais.addEventListener('click', function()
			{
				// Telemovel
				see_field_phone_number.setVisible(false);
				see_phone_number.setVisible(true);
				
				// Morada
				see_field_address.setVisible(false);
				see_address.setVisible(true);
				
				// Cidade
				see_field_city.setVisible(false);
				see_city.setVisible(true);
				
				// NIF
				see_field_nif.setVisible(false);
				see_nif.setVisible(true);
				
				button_not_save_dados_pessoais.setVisible(false);
				button_save_dados_pessoais.setVisible(false);
				button_edit_dados_pessoais.setVisible(true);
				
				edit = false;
			});
		});
	};
	
	function changeLateralBar()
	{
		setInterval(function()
		{
			if(run_constructor_fb_bool != fb.loggedIn)
			{
				// Voltar à pag. principal
				var main_screen = new MainScreen();
				main_screen.constructorScreen();
			
				main_screen.putEventListenersMainScreen();
				main_screen.showWindow();
				
				run_constructor_fb_bool = fb.loggedIn;				
			}
		}, 50);
	};
	
	this.showWindow = function()
	{
		window_perfil.open();
	};
}