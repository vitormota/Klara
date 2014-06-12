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
	
	var see_ads_subscribe = null;
	var value_height_ads_subscribe = null;
	var value_top_ads_subscribe = null;
	var length_ads = 0;
	
	var see_cupons_buyed = null;
    var value_height_cupons_buyed = null;
    var value_top_cupons_buyed = null;
    var length_cupons = 0;
	
	this.constructorScreen = function(type_back, array_ads, array_cupons)
	{
	    length_ads = array_ads.length;
	    length_cupons = array_cupons.length;
	    
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
		
		window_perfil.addEventListener('android:back', function(e)
        {
            Ti.API.info('Volta atrás!');
            BackWindow(type_back, window_perfil);
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
		
		if(array_ads.length > 0)
		{
    		// Ver cupões subscritos
    		value_height_ads_subscribe = (array_ads.length - 1)*65.14 + 52.03;
    		value_top_ads_subscribe = value_top_cupoes_subscritos + value_height_cupoes_subscritos + 5;
    		
    		see_ads_subscribe = Titanium.UI.createView({
                width: '85.4%',
                height: value_height_ads_subscribe + 'dp', 
                top: value_top_ads_subscribe + 'dp'
            });
            
            var top_value = 0;
            
            for(var i = 0; i < array_ads.length; i++)
            {
                top_value = (i * 65.14);
                
                var see_ad = Titanium.UI.createView({
                   height: '52.03dp', 
                   top: top_value + 'dp',
                   backgroundColor: '#a39795',
                   borderRadius: 10,
                   ad_id: array_ads[i].id
                });
                
                var geral_image_ad = Titanium.UI.createImageView({
                    top: '22.03%',
                    left: '4.48%',
                    width: '11.11%',
                    height: '55.16%',
                    image: "/healthplus/healthplus_subscription.png",
                    ad_id: array_ads[i].id
                });
                
                var see_text_ad = Titanium.UI.createLabel({
                   color: 'white',
                   text: (array_ads[i].name).toUpperCase() + "\n" + array_ads[i].name_institution + "\nPreço: " + array_ads[i].price + "€",
                   font:
                   {
                       fontSize: '9dp'
                   },
                   verticalAlign: Titanium.UI.VERTICAL_ALIGNMENT_CENTER,
                   left: '19.03%',
                   ad_id: array_ads[i].id 
                });
                
                var see_image_ad = Titanium.UI.createImageView({
                   image: "/healthplus/healthplus_buy.png",
                   height: '56.91%',
                   width: '11.46%',
                   right: '10%',
                   ad_buy_id: array_ads[i].id,
                   ad_obj: array_ads[i] 
                });
                
                see_ad.add(geral_image_ad);
                see_ad.add(see_image_ad);
                see_ad.add(see_text_ad);
                see_ads_subscribe.add(see_ad);
                
                see_ad.addEventListener('click', function(e)
                {
                   if(e.source.ad_buy_id != null)
                   {
                       var confirmation = Titanium.UI.createAlertDialog({
                            message: "Tem a certeza que pretende comprar este cupão?",
                            buttonNames: ['Sim', 'Não']
                        });
                        
                        confirmation.addEventListener('click', function(ev)
                        {
                            if(ev.index == 0)
                            {
                                BuyCupon(e.source.ad_obj);
                            }
                            else if(ev.index == 1)
                            {
                                alert('Cancelou a compra do cupão!');
                            }
                        });
                        
                        confirmation.show();
                   }
                   else
                   {
                       SpecsCupon(e.source.ad_id);
                   } 
                });
            }
            
            see_ads_subscribe.setVisible(false);
            scroll_view.add(see_ads_subscribe);
		}
		
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
		
		
		if(array_cupons.length > 0)
        {
            // Ver cupões adquiridos
            value_height_cupons_buyed = (array_cupons.length - 1)*65.14 + 52.03;
            value_top_cupons_buyed = value_top_cupoes_adquiridos + value_height_cupoes_adquiridos + 5;
            
            see_cupons_buyed = Titanium.UI.createView({
                width: '85.4%',
                height: value_height_cupons_buyed + 'dp', 
                top: value_top_cupons_buyed + 'dp'
            });
            
            var top_value_cupon_for = 0;
            
            for(var j = 0; j < array_cupons.length; j++)
            {
                top_value_cupon_for = (j * 65.14);
                
                var see_cupon = Titanium.UI.createView({
                   height: '52.03dp', 
                   top: top_value_cupon_for + 'dp',
                   backgroundColor: '#a39795',
                   borderRadius: 10,
                   cupon_id: array_cupons[i].id
                });
                
                var geral_image_cupon = Titanium.UI.createImageView({
                    top: '22.03%',
                    left: '4.48%',
                    width: '11.11%',
                    height: '55.16%',
                    image: "/healthplus/healthplus_positive.png",
                    cupon_id: array_cupons[i].id
                });
                
                var purchase_time = array_cupons[i].purchase_time.replace("T", "   ");
                var end_time = array_cupons[i].end_time.replace("T", "   ");
                
                var see_text_cupon = Titanium.UI.createLabel({
                   color: 'white',
                   text: (array_cupons[i].name).toUpperCase() + "\nData de compra: " + purchase_time + "\nData de validade: " + end_time,
                   font:
                   {
                       fontSize: '9dp'
                   },
                   verticalAlign: Titanium.UI.VERTICAL_ALIGNMENT_CENTER,
                   left: '19.06%',
                   cupon_id: array_cupons[i].id 
                });
                
                see_cupon.add(geral_image_cupon);
                see_cupon.add(see_text_cupon);
                see_cupons_buyed.add(see_cupon);
                
                see_cupon.addEventListener('click', function(e)
                {
                    SpecsCupon(e.source.cupon_id);
                });
            }
            
            see_cupons_buyed.setVisible(false);
            scroll_view.add(see_cupons_buyed);
        }
		
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
		eventListernerLateralBar.putListenersEventsLateralBar(lateral_bar_object, type_back, "perfil", window_perfil);
		
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
				
				if(length_ads > 0)
				{
    				value_top_ads_subscribe = value_top_ads_subscribe + 250;
    				see_ads_subscribe.top = value_top_ads_subscribe + 'dp';
    			}
				
				value_top_line3 = value_top_line3 + 250;
				line3.top = value_top_line3 + 'dp';
				
				value_top_cupoes_adquiridos = value_top_cupoes_adquiridos + 250;
				view_cupoes_adquiridos.top = value_top_cupoes_adquiridos + 'dp';
				
				if(length_cupons > 0)
				{
    				value_top_cupons_buyed = value_top_cupons_buyed + 250;
                    see_cupons_buyed.top = value_top_cupons_buyed + 'dp';
                }
				
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
				
				if(length_ads > 0)
				{
				    value_top_ads_subscribe = value_top_ads_subscribe - 250;
                    see_ads_subscribe.top = value_top_ads_subscribe + 'dp';
                }
				
				value_top_line3 = value_top_line3 - 250;
				line3.top = value_top_line3 + 'dp';
				
				value_top_cupoes_adquiridos = value_top_cupoes_adquiridos - 250;
				view_cupoes_adquiridos.top = value_top_cupoes_adquiridos + 'dp';
				
				if(length_cupons > 0)
				{
    				value_top_cupons_buyed = value_top_cupons_buyed - 250;
                    see_cupons_buyed.top = value_top_cupons_buyed + 'dp';
                }
				
				value_top_line4 = value_top_line4 - 250;
				line4.top = value_top_line4 + 'dp';
			}
		});
		
		view_cupoes_subscritos.addEventListener('click', function()
        {   
            if(length_ads > 0)
            {
                if(see_ads_subscribe.getVisible() == false)
                {
                    value_top_line3 = value_top_line3 + value_height_ads_subscribe + 15;
                    line3.top = value_top_line3 + 'dp';
                    
                    value_top_cupoes_adquiridos = value_top_cupoes_adquiridos + value_height_ads_subscribe + 15;
                    view_cupoes_adquiridos.top = value_top_cupoes_adquiridos + 'dp';
                    
                    if(length_cupons > 0)
                    {
                        value_top_cupons_buyed = value_top_cupons_buyed + value_height_ads_subscribe + 15;
                        see_cupons_buyed.top = value_top_cupons_buyed + 'dp';
                    }
                    
                    value_top_line4 = value_top_line4 + value_height_ads_subscribe + 15;
                    line4.top = value_top_line4 + 'dp';
                    
                    see_ads_subscribe.setVisible(true);
                }
                else if(see_ads_subscribe.getVisible() == true)
                {
                    see_ads_subscribe.setVisible(false);
                    
                    value_top_line3 = value_top_line3 - value_height_ads_subscribe - 15;
                    line3.top = value_top_line3 + 'dp';
                    
                    value_top_cupoes_adquiridos = value_top_cupoes_adquiridos - value_height_ads_subscribe - 15;
                    view_cupoes_adquiridos.top = value_top_cupoes_adquiridos + 'dp';
                    
                    if(length_cupons > 0)
                    {
                        value_top_cupons_buyed = value_top_cupons_buyed - value_height_ads_subscribe - 15;
                        see_cupons_buyed.top = value_top_cupons_buyed + 'dp';
                    }
                    
                    value_top_line4 = value_top_line4 - value_height_ads_subscribe - 15;
                    line4.top = value_top_line4 + 'dp';
                }
            }
        });
        
        view_cupoes_adquiridos.addEventListener('click', function()
        {
            if(length_cupons > 0)
            {
                if(see_cupons_buyed.getVisible() == false)
                {
                    value_top_line4 = value_top_line4 + value_height_cupons_buyed + 15;
                    line4.top = value_top_line4 + 'dp';
                    
                    see_cupons_buyed.setVisible(true);
                }
                else if(see_cupons_buyed.getVisible() == true)
                {
                    see_cupons_buyed.setVisible(false);
                    
                    value_top_line4 = value_top_line4 - value_height_cupons_buyed - 15;
                    line4.top = value_top_line4 + 'dp';
                }
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
	
	    search_bar_object.search_bar_icon.addEventListener('click', function()
        {
            if(search_bar_object.search_bar_textbox.value == "")
            {
                // Não faz nada
            }
            else
            {
                SearchAd(type_back, search_bar_object.search_bar_textbox.value, window_perfil);
            }
        });
    };
    
    function SearchAd (type_back, textSearch, current_window)
    {
        var string_verify = "no_connection";
        var method = 'POST';
        var url = "http://" + url_ip + ":52144/odata/Ads/SearchAd";
        
        var args = {};
        var last_id = -1;
        args.last_id = last_id.toString();
        args.textSearch = textSearch.toString();
        
        // Buscar dados a API
        var connection_api= Titanium.Network.createHTTPClient(
        {
            onload: function()
            {
                while(string_verify == "no_connection")
                {
                    string_verify = JSON.parse(this.responseText);
                }
                
                var array_ads = JSON.parse(string_verify.value);
                SearchInstitution(type_back, textSearch, array_ads, current_window);
           
            },
            onerror: function()
            {
                alert("Houve um problema com a pesquisa!");
            },
            timeout: 10000 // Tempo para fazer pedido
        });
        
        connection_api.open(method, url, false);
        connection_api.setRequestHeader("Content-Type", "application/json; charset=utf-8");
  
        connection_api.send(JSON.stringify(args));
    }
    
    function SearchInstitution(type_back, textSearch, array_ads, current_window)
    {
        var string_verify = "no_connection";
        var method = 'POST';
        var url = "http://" + url_ip + ":52144/odata/Institutions/SearchInstitution";
        
        var args = {};
        var last_id = -1;
        args.last_id = last_id.toString();
        args.textSearch = textSearch.toString();
        
        // Buscar dados a API
        var connection_api= Titanium.Network.createHTTPClient(
        {
            onload: function()
            {
                while(string_verify == "no_connection")
                {
                    string_verify = JSON.parse(this.responseText);
                }
                
                var array_institutions = JSON.parse(string_verify.value);
                type_back.push("perfil");
                
                var result_screen = new ResultSearchScreen();
                result_screen.constructorScreen(type_back, array_ads, array_institutions, textSearch);
                
                result_screen.showWindow();
                result_screen.putEventListenersProfileScreen();
                
                current_window.close();
            },
            onerror: function()
            {
                alert("Houve um problema com a pesquisa!");
            },
            timeout: 10000 // Tempo para fazer pedido
        });
        
        connection_api.open(method, url, false);
        connection_api.setRequestHeader("Content-Type", "application/json; charset=utf-8");
  
        connection_api.send(JSON.stringify(args));
    }
	
	function changeLateralBar()
	{
		setInterval(function()
		{
			if(run_constructor_fb_bool != fb.loggedIn)
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
                        type_back.pop();
                        main_screen.constructorScreen(array_ads, type_back);
                        
                        main_screen.putEventListenersMainScreen();
                        main_screen.showWindow();
                        
                        window_perfil.close();
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
		}, 100);
	};
	
	this.showWindow = function()
	{
		window_perfil.open();
	};
	
	function SpecsCupon(cupon_id)
    {
        var string_verify = "no_connection";
        var method = 'GET';
        var url = "http://" + url_ip + ":52144/odata/Ads(" + cupon_id.toString() + ")";
                
        // Buscar dados a API
        var connection_api= Titanium.Network.createHTTPClient(
        {
            onload: function()
            {
                while(string_verify == "no_connection")
                {
                    string_verify = JSON.parse(this.responseText);
                }

                
                var ad = string_verify.Key;
                var institution = string_verify.Value;
                
                // Redirecionar para a pagina da instituicao
                var cupon = new CuponScreen();
                type_back.push("perfil");
                cupon.constructorScreen(ad, institution, type_back);
                    
                cupon.putEventListenersCuponScreen();
                cupon.showWindow();
                    
                window_perfil.close();
                
            },
            onerror: function()
            {
                alert("Erro na obtenção do cupão!!");
            },
            timeout: 10000 // Tempo para fazer pedido
        });
        
        connection_api.open(method, url, false);
        connection_api.send();
    }
    
    function BuyCupon(ad_buy)
    {
        var string_verify = "no_connection";
        var method = 'POST';
        var url = "http://" + url_ip + ":52144/odata/Cupon/";
        
        var args = {};
        var state = 0;
        args.state = state.toString();
        args.ad_id = ad_buy.id.toString();
        args.start_time = ad_buy.start_time.toString();
        args.end_time = ad_buy.end_time.toString();
        args.client_id = user_id.toString();
                        
        // Buscar dados a API
        var connection_api= Titanium.Network.createHTTPClient(
        {
            onload: function()
            {
                while(string_verify == "no_connection")
                {
                    string_verify = JSON.parse(this.responseText);
                }
                
                Ti.API.info('Ação efectuada com sucesso!');
                alert('A compra foi efectuada com sucesso!');
           
            },
            onerror: function()
            {
                alert("Houve um problema a registar a sua compra. Tente novamente.");
            },
            timeout: 10000 // Tempo para fazer pedido
        });
        
        connection_api.open(method, url, false);
        connection_api.setRequestHeader("Content-Type", "application/json; charset=utf-8");
  
        connection_api.send(JSON.stringify(args));
           
    }
}