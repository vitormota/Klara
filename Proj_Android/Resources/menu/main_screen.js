// Incluir o ficheiro da pagina onde a barra e feita
Titanium.include("/components/lateral_bar.js");

// Incluir o ficheiro onde o header e a barra de pesquisa sao feitas
Titanium.include("/components/header_bar.js");

// Incluir ficheiro para fazer back
Titanium.include("/components/back_window.js");

// Incluir o ficheiro que permite chamar os cupoes
Titanium.include("/cupon/show_cupon.js");

// Incluir facebook
Titanium.include("/facebook/init_facebook.js");

function MainScreen()
{
	// Variaveis
	var pag_inicial_window = null;
	var ad_scroll_view = null;
	
	// Objecto barra lateral
	var lateral_bar_object = new LateralBarWithoutSearch(this);
	var lateral_bar = null;
	
	var header_view_object = new HeaderViewImage();
	var search_bar_object = new SearchBar();
	
	var run_constructor_fb_bool = null;
	
	this.constructorScreen = function(array_ads, type_back)
	{
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
		
		// Abrir nova janela
		pag_inicial_window = Titanium.UI.createWindow({
			exitOnClose: true
		});
		pag_inicial_window.navBarHidden = true;
		
		pag_inicial_window.addEventListener('android:back', function(e)
        {
            Ti.API.info('Volta atrás!');
            BackWindow(type_back, pag_inicial_window);
        });
		
		var background_view = Titanium.UI.createView({
			backgroundColor: "#f6f2e2"
		});
		
		pag_inicial_window.add(background_view);
		
		
		// ---------------------------------------- Fazer scrollList com os anuncios --------------------------------------------
		var number_of_views = array_ads.length;
		
		ad_scroll_view = Titanium.UI.createScrollView({
			overScrollMode: Titanium.UI.Android.OVER_SCROLL_ALWAYS,
			scrollType: "vertical",
			top: '16.06%',
			width: '85.15%',
			left: '7.43%',
			contentHeight: 'auto'
		});
		
		for(var i = 0; i < number_of_views; i++)
		{
			var top_value = i * 199.99;
		
			var ad_view = Titanium.UI.createView({ // Criar views para anuncio
				height: '180dp',
				top: top_value + 'dp',
				cupon_id: array_ads[i].ad.id
			});
				
			// Colocar imagem do anuncio
			var ad_image_view = Titanium.UI.createImageView({
				top: '0%',
				height: '100%',
				width: '100%',
				image: array_ads[i].ad.img_url,
				borderRadius: 10,
				cupon_id: array_ads[i].ad.id
			});
			
			// Criar view para colocar texto
			var ad_text_view = Titanium.UI.createView({
				bottom: '0%',
				height: '28.99%',
				backgroundColor: "#a39795",
				cupon_id: array_ads[i].ad.id
			});
			
			// Label para texto
			var consulta_text = Titanium.UI.createLabel({
				text: array_ads[i].ad.name.toUpperCase() + "\n" + array_ads[i].institution.name + "\nPreço: " + array_ads[i].ad.price + "€",
				font:
				{
					fontSize: '7.5%'
				},
				verticalAlign: Titanium.UI.TEXT_VERTICAL_ALIGNMENT_CENTER,
				left: '14.03%',
				color: 'white'
			});
			
			// Botão comprar
			var ad_image_buy_view = Titanium.UI.createImageView({
				image: "/healthplus/healthplus_buy.png",
				id: "image_buy",
				height: '57.5%',
				width: '11.4%',
				top: '22.57%',
				left: '78.51%'
			});
			
			ad_view.addEventListener('click', function(e)
			{
				if(e.source.id == "image_buy")
				{
				    if(run_constructor_fb_bool == false)
				    {
				        InitFacebook();
				    }
				    else
				    {
				    }
				}
				else
				{
	                SpecsCupon(e.source.cupon_id);
				}
			});

			
			ad_text_view.add(consulta_text);
			ad_text_view.add(ad_image_buy_view);
			
			ad_view.add(ad_image_view);
			ad_view.add(ad_text_view);
			ad_scroll_view.add(ad_view);
		}
		
		
		pag_inicial_window.add(lateral_bar);
		pag_inicial_window.add(ad_scroll_view);
		pag_inicial_window.add(search_bar);
		pag_inicial_window.add(header_view);
		
		// Fazer com que no inicio a searchBar não fique visivel
		search_bar.setVisible(false);
		search_bar_object.putEventListenerToShowSearchBar(header_view_object.search_header_view);
		
		// Fazer com que a barra lateral desapareca
		lateral_bar.setVisible(false);
		
		// Colocar events listeners barra lateral
		var eventListernerLateralBar = new EventsLateralBar();
		eventListernerLateralBar.putListenersEventsLateralBar(lateral_bar_object, type_back, "home", pag_inicial_window);
		
		changeLateralBar(type_back);
	};
	
	this.putEventListenersMainScreen = function ()
	{
		var ad_scroll = ad_scroll_view;
		
		pag_inicial_window.addEventListener('swipe', function(e)
		{
			// Fazer swipe para a esquerda para desaparecer window
			if(e.direction == 'left')
			{
				if(lateral_bar.getVisible() == true)
				{
					lateral_bar.setVisible(false);
					ad_scroll.left = '7.43%';
				}
			}
			else if(e.direction == 'right') // Fazer swipe para a direita para desaparecer window
			{
				if(lateral_bar.getVisible() == false)
				{
					ad_scroll.left = '83.87%';
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
				ad_scroll.left = '7.43%';
			}
			else
			{
				ad_scroll.left = '83.87%';
				lateral_bar.setVisible(true);
			}
		});
	};
	
	function changeLateralBar(type_back)
	{
		setInterval(function()
		{
			if(run_constructor_fb_bool != fb.loggedIn)
			{
				lateral_bar.setVisible(false);
				pag_inicial_window.remove(lateral_bar);
				ad_scroll_view.left = '7.43%';
				
				lateral_bar_object = new LateralBarWithoutSearch();
				lateral_bar_object.constructorLateralBar();
				lateral_bar = lateral_bar_object.lateral_bar;
				
				lateral_bar.setVisible(false);
				pag_inicial_window.add(lateral_bar);
		
				eventListernerLateralBar = new EventsLateralBar();
				eventListernerLateralBar.putListenersEventsLateralBar(lateral_bar_object, type_back, "home", pag_inicial_window);
				
				run_constructor_fb_bool = fb.loggedIn;				
			}
		}, 100);
	};
	
	this.showWindow = function()
	{
		pag_inicial_window.open();
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
                type_back.push("home");
                cupon.constructorScreen(ad, institution, type_back);
                    
                cupon.putEventListenersCuponScreen();
                cupon.showWindow();
                    
                pag_inicial_window.close();
                
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
}




