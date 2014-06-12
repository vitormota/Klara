var MapModule = require('ti.map');

function CuponScreen()
{
	var cupon_window = null;
	var cupon_scroll_view = null;
	
	// Objecto barra lateral
	var lateral_bar_object = new LateralBarWithoutSearch();
	var lateral_bar = null;
	
	var header_view_object = new HeaderViewImage();
	var search_bar_object = new SearchBar();
	
	var run_constructor_fb_bool = null;
	
	var cupon_image_subscribe = null;
	var ad_global_id = null;
	
	this.constructorScreen = function(ad, institution, type_back)
	{
	    ad_global_id = ad.id;
	    
		// Verificar se existe login
		run_constructor_fb_bool = fb.loggedIn;
		
		// Construir barra lateral
		lateral_bar_object.constructorLateralBar();
		lateral_bar = lateral_bar_object.lateral_bar;
		
		// Construir header
		header_view_object.constructorHeaderView();
		var header_view = header_view_object.header_view;
		
		// Construir barra de pesquisa
		search_bar_object.constructorSearchBar();
		var search_bar = search_bar_object.search_bar;
		
		cupon_window = Titanium.UI.createWindow({
			navBarHidden: true
		});
		
		cupon_window.addEventListener('android:back', function(e)
        {
            BackWindow(type_back, cupon_window);
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
			image: ad.img_url,
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
			height: '675.67dp'
		});
		
		var cupon_info_text = Titanium.UI.createView({
			left: '10.28%',
			width: '80.55%',
			height: '100%'
		});
		
		var cupon_title = Titanium.UI.createLabel({
			text: (ad.name).toUpperCase(),
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
			top: '7.87%',
			color: '#d96b63',
			left: '0%',
			font:
			{
				fontSize: '11dp'
			}
		});

		cupon_info_text.add(cupon_description_title);
		
		var cupon_description_text = Titanium.UI.createLabel({
			text: ad.description,
			top: '9.71%',
			color: '#d96b63',
			left: '0%',
			font:
			{
				fontSize: '11dp'
			}
		});
		
		cupon_info_text.add(cupon_description_text);
		
		
		var cupon_service_title = Titanium.UI.createLabel({
            text: "Serviço",
            top: '18.87%',
            color: '#d96b63',
            left: '0%',
            font:
            {
                fontSize: '11dp'
            }
        });

        cupon_info_text.add(cupon_service_title);
        
        var cupon_service_text = Titanium.UI.createLabel({
            text: ad.service,
            top: '20.71%',
            color: '#d96b63',
            left: '0%',
            font:
            {
                fontSize: '11dp'
            }
        });
        
        cupon_info_text.add(cupon_service_text);
        
        
        var cupon_specialty_title = Titanium.UI.createLabel({
            text: "Especialidade",
            top: '24.34%',
            color: '#d96b63',
            left: '0%',
            font:
            {
                fontSize: '11dp'
            }
        });

        cupon_info_text.add(cupon_specialty_title);
        
        var cupon_specialty_text = Titanium.UI.createLabel({
            text: ad.specialty,
            top: '26.18%',
            color: '#d96b63',
            left: '0%',
            font:
            {
                fontSize: '11dp'
            }
        });
        
        cupon_info_text.add(cupon_specialty_text);
		
		var cupon_data_init_title = Titanium.UI.createLabel({
			text: "Data Inicio",
			top: "29.81%",
			left: '0%',
			color: '#d96b63',
			font: 
			{
				fontSize: '11dp'
			}
		});
		
		cupon_info_text.add(cupon_data_init_title);
		
		var aux_start_time = ad.start_time.replace("T", "   ");
		
		var cupon_data_init_text = Titanium.UI.createLabel({
			text: aux_start_time,
			top: "31.65%",
			left: '0%',
			color: '#d96b63',
			font: 
			{
				fontSize: '11dp'
			}
		});
		
		cupon_info_text.add(cupon_data_init_text);
		
		var cupon_data_end_title = Titanium.UI.createLabel({
			text: "Data Fim",
			top: "35.28%",
			left: '0%',
			color: '#d96b63',
			font: 
			{
				fontSize: '11dp'
			}
		});
		
		cupon_info_text.add(cupon_data_end_title);
		
		var aux_end_time = ad.end_time.replace("T", "   ");
		
		var cupon_data_end_text = Titanium.UI.createLabel({
			text: aux_end_time,
			top: "37.12%",
			left: '0%',
			color: '#d96b63',
			font: 
			{
				fontSize: '11dp'
			}
		});
		
		cupon_info_text.add(cupon_data_end_text);
		
		var cupon_buyed_cupons_title = Titanium.UI.createLabel({
            text: "Número de Cupões Comprados",
            top: "40.75%",
            left: '0%',
            color: '#d96b63',
            font: 
            {
                fontSize: '11dp'
            }
        });
        
        cupon_info_text.add(cupon_buyed_cupons_title);
        
        var cupon_buyed_cupons_text = Titanium.UI.createLabel({
            text: ad.buyed_cupons,
            top: "42.59%",
            left: '0%',
            color: '#d96b63',
            font: 
            {
                fontSize: '11dp'
            }
        });
		
		cupon_info_text.add(cupon_buyed_cupons_text);
		
		var cupon_remaining_cupons_title = Titanium.UI.createLabel({
            text: "Número de Cupões que Faltam Vender",
            top: "46.22%",
            left: '0%',
            color: '#d96b63',
            font: 
            {
                fontSize: '11dp'
            }
        });
        
        cupon_info_text.add(cupon_remaining_cupons_title);
        
        var cupon_remaining_cupons_text = Titanium.UI.createLabel({
            text: ad.remaining_cupons,
            top: "48.06%",
            left: '0%',
            color: '#d96b63',
            font: 
            {
                fontSize: '11dp'
            }
        });
        
        cupon_info_text.add(cupon_remaining_cupons_text);
		
		var cupon_institutions_title = Titanium.UI.createLabel({
			text: "Instituição",
			top: "51.69%",
			left: '0%',
			color: '#d96b63',
			font: 
			{
				fontSize: '11dp'
			}
		});
		
		cupon_info_text.add(cupon_institutions_title);
		
		var cupon_institutions_text = Titanium.UI.createLabel({
			text: institution.name,
			top: "53.53%",
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
			top: "57.16%",
			left: '0%',
			color: '#d96b63',
			font: 
			{
				fontSize: '11dp'
			}
		});
		
		cupon_info_text.add(cupon_discount_title);
		
		var cupon_discount_text;
		
		if(ad.discount == null)
		{
    		cupon_discount_text = Titanium.UI.createLabel({
    			text: "n/d",
    			top: "59%",
    			left: '0%',
    			color: '#d96b63',
    			font: 
    			{
    				fontSize: '11dp'
    			}
    		});
    	}
    	else
    	{
    	    cupon_discount_text = Titanium.UI.createLabel({
                text: ad.discount + "%",
                top: "59%",
                left: '0%',
                color: '#d96b63',
                font: 
                {
                    fontSize: '11dp'
                }
            });
    	}
		
		cupon_info_text.add(cupon_discount_text);
		
		var cupon_localization_title = Titanium.UI.createLabel({
			text: "Localização",
			top: "62.63%",
			left: '0%',
			color: '#d96b63',
			font: 
			{
				fontSize: '11dp'
			}
		});
		
		cupon_info_text.add(cupon_localization_title);
		
		/*var hospital = MapModule.createAnnotation({
            latitude: institution.latitude,
            longitude: institution.longitude,
            draggable: false,
            pincolor: MapModule.ANNOTATION_AZURE,   
            title: institution.name,
            subtitle: institution.address
        });
		
		var cupon_localization_image = MapModule.createView({
			userLocation: false,
            mapType: MapModule.NORMAL_TYPE,
            animate: false,
            region: {latitude: institution.latitude, longitude: institution.longitude, latitudeDelta: 0.5, longitudeDelta: 0.5 },
			top: "63.63%",
			left: '0%',
			height: '29.45%'
		});
		
		cupon_localization_image.addAnnotation(hospital);
		cupon_info_text.add(cupon_localization_image);*/
		
		var cupon_localization_image = Titanium.UI.createImageView({
            image: "/healthplus/healthplus_google_maps.jpg",
            top: "63.63%",
            left: '0%',
            height: '29.45%'
        });
        
        cupon_info_text.add(cupon_localization_image);
		
		var cupon_image_buy = Titanium.UI.createImageView({
			image: "/healthplus/healthplus_buy.png",
			bottom: '1.15%',
			height: '6.66%',
			width: '19.44%',
			ad_obj: ad
		});
		
		cupon_image_buy.addEventListener('click', function(e)
		{
		    if(run_constructor_fb_bool == false)
		    {
		        InitFacebook();
		    }
		    else
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
		});
		
		if(run_constructor_fb_bool == true)
		{
		    VerifyCuponSubscribe(cupon_info_text, cupon_image_buy, ad.id);
        }
		
		cupon_info_text.add(cupon_image_buy);
		
		cupon_information.add(cupon_info_text);
		cupon_scroll_view.add(cupon_information);
		
		cupon_window.add(cupon_scroll_view);
		cupon_window.add(lateral_bar);
		cupon_window.add(header_view);
		cupon_window.add(search_bar);
		
		// Fazer com que no inicio a searchBar não fique visivel
		search_bar.setVisible(false);
		search_bar_object.putEventListenerToShowSearchBar(header_view_object.search_header_view);
		
		// Fazer com que a barra lateral desapareca
		lateral_bar.setVisible(false);
		
		// Colocar events listeners barra lateral
		var eventListernerLateralBar = new EventsLateralBar();
		eventListernerLateralBar.putListenersEventsLateralBar(lateral_bar_object, type_back, "cupao_" + ad.id, cupon_window);
		
		// Verificar se esta logado
		changeLateralBar(type_back, cupon_info_text, cupon_image_buy, ad.id);
	};
	
	this.putEventListenersCuponScreen = function ()
	{
		//var lateral_bar_listener = lateral_bar_object.lateral_bar;
		var cupon_scroll = cupon_scroll_view;
		
		cupon_window.addEventListener('swipe', function(e)
		{
			// Fazer swipe para a esquerda para desaparecer window
			if(e.direction == 'left')
			{
				if(lateral_bar.getVisible() == true)
				{
					lateral_bar.setVisible(false);
					cupon_scroll.left = '0%';
				}
			}
			else if(e.direction == 'right') // Fazer swipe para a direita para desaparecer window
			{
				if(lateral_bar.getVisible() == false)
				{
					cupon_scroll.left = '76.44%';
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
				cupon_scroll.left = '0%';
			}
			else
			{
				cupon_scroll.left = '76.44%';
				lateral_bar.setVisible(true);
			}
		});
		
		search_bar_object.search_bar_icon.addEventListener('click', function()
        {
            if(search_bar_object.search_bar_textbox.value == "")
            {
                // Não faz nada
            }
            else
            {
                SearchAd(type_back, search_bar_object.search_bar_textbox.value, cupon_window);
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
                type_back.push("cupao_" + ad_global_id);
                
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
	
	function changeLateralBar(type_back, cupon_info_text, cupon_image_buy, cupon_id)
	{
		setInterval(function()
		{
			if(run_constructor_fb_bool != fb.loggedIn)
			{
				lateral_bar.setVisible(false);
				cupon_window.remove(lateral_bar);
				cupon_scroll_view.left = '0%';
				
				lateral_bar_object = new LateralBarWithoutSearch();
				lateral_bar_object.constructorLateralBar();
				lateral_bar = lateral_bar_object.lateral_bar;
				
				lateral_bar.setVisible(false);
				cupon_window.add(lateral_bar);
		
				eventListernerLateralBar = new EventsLateralBar();
				eventListernerLateralBar.putListenersEventsLateralBar(lateral_bar_object, type_back, "cupao_" + cupon_id, cupon_window);
				
				if(run_constructor_fb_bool == true)
				{
				    cupon_image_subscribe.setVisible(false);
				    cupon_image_buy.left = "40.17%";
				}
				else if(run_constructor_fb_bool == false)
				{
				    VerifyCuponSubscribe(cupon_info_text, cupon_image_buy, cupon_id);
				}
				
				run_constructor_fb_bool = fb.loggedIn;				
			}
			
		}, 100);
	};
	
	this.showWindow = function()
	{
		cupon_window.open();
	};
	
	function VerifyCuponSubscribe(cupon_info_text, cupon_image_buy, cupon_id)
	{
	    var string_verify = "no_connection";
        var method = 'POST';
        var url = "http://" + url_ip + ":52144/odata/Subscriptions/IsSubscribeUser";
        
        var args = {};
        args.client_id = user_id.toString();
        args.subscribable_id = cupon_id.toString();
                
        // Buscar dados a API
        var connection_api= Titanium.Network.createHTTPClient(
        {
            onload: function()
            {
                while(string_verify == "no_connection")
                {
                    string_verify = JSON.parse(this.responseText);
                }
                
                if(string_verify.value == "false")
                {
                    cupon_image_subscribe = Titanium.UI.createImageView({
                        image: "/healthplus/healthplus_positive.png",
                        bottom: '1.15%',
                        height: '6.66%',
                        width: '19.44%',
                        left: '27.56%',
                        type_obj: 'button_subscribe'
                    });
                    
                    cupon_image_buy.left = '53%';
                    cupon_info_text.add(cupon_image_subscribe);
                }
                else if(string_verify.value == "true")
                {
                    cupon_image_subscribe = Titanium.UI.createImageView({
                        image: "/healthplus/healthplus_negative.png",
                        bottom: '1.15%',
                        height: '6.66%',
                        width: '19.44%',
                        left: '27.56%',
                        type_obj: 'button_unsubscribe'
                    });
                    
                    cupon_image_buy.left = '53%';
                    cupon_info_text.add(cupon_image_subscribe);
                }
                else
                {
                    alert("Algo se passou!");
                }
                
                cupon_image_subscribe.addEventListener('click', function(e)
                {
                    if(e.source.type_obj == "button_subscribe")
                    {
                        SubscribeAd(true, cupon_id, e.source);
                    }  
                    else if(e.source.type_obj == "button_unsubscribe")
                    {
                        SubscribeAd(false, cupon_id, e.source);
                    } 
                });
            },
            onerror: function()
            {
                alert("Erro na verificação de subscrições!!");
            },
            timeout: 10000 // Tempo para fazer pedido
        });
        
        connection_api.open(method, url, false);
        connection_api.setRequestHeader("Content-Type", "application/json; charset=utf-8");
  
        connection_api.send(JSON.stringify(args));
	}
	
	function SubscribeAd(case_subscribe, ad_id, source_click)
    {
        var string_verify = "no_connection";
        var method = 'POST';
        var url;
        
        if(case_subscribe == true)
        {
            url = "http://" + url_ip + ":52144/odata/Subscriptions/";
        }
        else if(case_subscribe == false)
        {
            url = "http://" + url_ip + ":52144/odata/Subscriptions/DeleteSubscription";
        }
        
        var args = {};
        args.client_id = user_id.toString();
        args.subscribable_id = ad_id.toString();
                        
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
                
                if(case_subscribe == true)
                {
                    source_click.image = '/healthplus/healthplus_negative.png';
                    source_click.type_obj = "button_unsubscribe";
                }
                else if(case_subscribe == false)
                {
                    source_click.image = '/healthplus/healthplus_positive.png';
                    source_click.type_obj = "button_subscribe";
                }
            },
            onerror: function()
            {
                alert("Erro na verificação de subscrições!!");
            },
            timeout: 10000 // Tempo para fazer pedido
        });
        
        connection_api.open(method, url, false);
        connection_api.setRequestHeader("Content-Type", "application/json; charset=utf-8");
  
        connection_api.send(JSON.stringify(args));
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
