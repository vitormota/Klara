// Incluir ficheiro para a janela da instituição
Titanium.include("/institutions/show_one_institution.js");

function InstitutionsScreen()
{
	var institution_window = null;
	var institution_scroll_view = null;
	
	// Objecto barra lateral
	var lateral_bar_object = new LateralBarWithoutSearch();
	var lateral_bar = null;
	
	var header_view_object = new HeaderViewText();
	var search_bar_object = new SearchBar();
	
	var run_constructor_fb_bool = null;
	var array_clicks = [];
	
	this.constructorScreen = function (array_institutions, type_back)
	{
		// Verificar se existe login
		run_constructor_fb_bool = fb.loggedIn;
		
		// Construir barra lateral
		lateral_bar_object.constructorLateralBar();
		lateral_bar = lateral_bar_object.lateral_bar;
		
		// Construir header
		header_view_object.constructorHeaderView("INSTITUIÇÕES");
		var header_view = header_view_object.header_view;
		
		// Construir barra de pesquisa
		search_bar_object.constructorSearchBar();
		var search_bar = search_bar_object.search_bar;
		
		institution_window = Titanium.UI.createWindow({
			navBarHidden: true
		});
		
		institution_window.addEventListener('android:back', function(e)
        {
            Ti.API.info('Volta atrás!');
            BackWindow(type_back, institution_window);
        });
		
		var background_view = Titanium.UI.createView({
			backgroundColor: "#f6f2e2"
		});
		
		institution_window.add(background_view);
		
		/** Colocar as varias instituições em forma de scroll **/
		var number_institutions = array_institutions.length;
		
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
				width: '85.21%',
				institution_id: array_institutions[i].id.toString()
			});
			
			var image_institution = Titanium.UI.createImageView({
				top: '22.03%',
				left: '4.48%',
				width: '11.11%',
				height: '55.16%',
				image: "/healthplus/healthplus_institution.png",
				institution_id: array_institutions[i].id.toString()
			});
			
			var text_institution = Titanium.UI.createLabel({
				text: (array_institutions[i].name).toUpperCase() + "\n" + array_institutions[i].city,
				left: '19.03%',
				top: '20.03%',
				color: 'black',
				institution_id: array_institutions[i].id.toString(),
				font:
				{
					fontSize: '10%'
				}
			});
			
			institution_one_view.add(image_institution);
            institution_one_view.add(text_institution);
			
			if(run_constructor_fb_bool == true)
    		{
                var args = {};
                args.client_id = user_id.toString();
                args.subscribable_id = array_institutions[i].id.toString();
                
                var requestInstitution = new PutInstitution();
                requestInstitution.put(institution_one_view, institution_scroll_view, top_value, args, array_institutions[i].id);
    		}
    		else
    		{
    		    institution_scroll_view.add(institution_one_view);
    		}
    		
    		institution_one_view.addEventListener('click', function(e)
            {
                // So funciona se o utilizador estiver logado
                if (e.source.type_obj != null)
                {
                    if(e.source.type_obj == "button_subscribe")
                    {
                        SubscribeInstitution(true, e.source.institution_click_id, e.source);
                    }
                    else if(e.source.type_obj == "button_unsubscribe")
                    {
                        SubscribeInstitution(false, e.source.institution_click_id, e.source);
                    }
                }
                else
                {
                    // Vai para a pagina da instituicao
                    SpecsInstitution(e.source.institution_id);
                } 
            });
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
		eventListernerLateralBar.putListenersEventsLateralBar(lateral_bar_object, type_back, "instituicoes", institution_window);
		
		// Verificar se esta logado
		changeLateralBar(type_back);
	};
	
	this.putEventListenersInstitutionsScreen = function ()
	{
		var institution_scroll = institution_scroll_view;
		
		institution_window.addEventListener('swipe', function(e)
		{
			// Fazer swipe para a esquerda para desaparecer window
			if(e.direction == 'left')
			{
				if(lateral_bar.getVisible() == true)
				{
					lateral_bar.setVisible(false);
					institution_scroll.left = '0%';
				}
			}
			else if(e.direction == 'right') // Fazer swipe para a direita para desaparecer window
			{
				if(lateral_bar.getVisible() == false)
				{
					institution_scroll.left = '76.44%';
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
				institution_scroll.left = '0%';
			}
			else
			{
				institution_scroll.left = '76.44%';
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
                SearchAd(type_back, search_bar_object.search_bar_textbox.value, institution_window);
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
                type_back.push("instituicoes");
                
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
	
	function changeLateralBar(type_back)
	{
		setInterval(function()
		{
			if(run_constructor_fb_bool != fb.loggedIn)
			{
				lateral_bar.setVisible(false);
				institution_window.remove(lateral_bar);
				institution_scroll_view.left = '0%';
				
				lateral_bar_object = new LateralBarWithoutSearch();
				lateral_bar_object.constructorLateralBar();
				lateral_bar = lateral_bar_object.lateral_bar;
				
				lateral_bar.setVisible(false);
				institution_window.add(lateral_bar);
		
				eventListernerLateralBar = new EventsLateralBar();
				eventListernerLateralBar.putListenersEventsLateralBar(lateral_bar_object, type_back, "instituicoes", institution_window);
				
				if(run_constructor_fb_bool == true)
				{
				    for(var i = 0; i < array_clicks.length; i++)
				    {
				        array_clicks[i].setVisible(false);
				    }
				}
				else if(run_constructor_fb_bool == false)
                {
                    for(var i = 0; i < array_clicks.length; i++)
                    {
                        array_clicks[i].setVisible(true);
                    }
                }
				
				run_constructor_fb_bool = fb.loggedIn;				
			}
			
		}, 100);
	};
	
	this.showWindow = function()
	{
		institution_window.open();
	};
	
	function SubscribeInstitution(case_subscribe, institution_id, source_click)
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
        args.subscribable_id = institution_id;
                        
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
	
    function SpecsInstitution(institution_id)
    {
        var string_verify = "no_connection";
        var method = 'GET';
        var url = "http://" + url_ip + ":52144/odata/Institutions(" + institution_id.toString() + ")";
                
        // Buscar dados a API
        var connection_api= Titanium.Network.createHTTPClient(
        {
            onload: function()
            {
                while(string_verify == "no_connection")
                {
                    string_verify = JSON.parse(this.responseText);
                }
                
                // Redirecionar para a pagina da instituicao
                var institution = new InstitutionOneScreen();
                type_back.push("instituicoes");
                institution.constructorScreen(string_verify, type_back);
                
                institution.putEventListenersInstitutionOneScreen();
                institution.showWindow();
                
                institution_window.close();
                
            },
            onerror: function()
            {
                alert("Erro na obtenção da instituição!!");
            },
            timeout: 10000 // Tempo para fazer pedido
        });
        
        connection_api.open(method, url, false);
        connection_api.send();
    }
    
    function PutInstitution()
    {
        this.put = function(institution_one_view, institution_scroll_view, top_value, args, institution_id)
        {
            var string_verify = "no_connection";
            var method = 'POST';
            var url = "http://" + url_ip + ":52144/odata/Subscriptions/IsSubscribeUser";
                    
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
                        var click_institution = Titanium.UI.createImageView({
                            image: '/healthplus/healthplus_positive.png',
                            top: '22.03%',
                            left: '82.93%',
                            width: '11.08%',
                            height: '55.66%',
                            institution_click_id: institution_id.toString(),
                            type_obj: 'button_subscribe'
                        });
                        
                        array_clicks.push(click_institution);
                        
                        institution_one_view.top = top_value + 'dp';
                        institution_one_view.add(click_institution);
                        institution_scroll_view.add(institution_one_view);
                    }
                    else if(string_verify.value == "true")
                    {
                        var click_institution = Titanium.UI.createImageView({
                            image: '/healthplus/healthplus_negative.png',
                            top: '22.03%',
                            left: '82.93%',
                            width: '11.08%',
                            height: '55.66%',
                            institution_click_id: institution_id.toString(),
                            type_obj: 'button_unsubscribe'
                        });
                        
                        array_clicks.push(click_institution);
                        
                        institution_one_view.top = top_value + 'dp';
                        institution_one_view.add(click_institution);
                        institution_scroll_view.add(institution_one_view);
                    }
                    else
                    {
                        alert("Algo se passou!");
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
        };
    }
}