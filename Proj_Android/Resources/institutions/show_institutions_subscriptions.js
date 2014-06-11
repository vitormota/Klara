// Incluir o ficheiro da pagina onde a barra e feita
Titanium.include("/components/lateral_bar.js");

// Incluir o ficheiro onde o header e a barra de pesquisa sao feitas
Titanium.include("/components/header_bar.js");

// Incluir ficheiro para fazer back
Titanium.include("/components/back_window.js");

// Include facebook
Titanium.include("/facebook/init_facebook.js");

function SubscriptionsInstitutionsScreen()
{
    var institution_subscription_window = null;
    var institution_scroll_view = null;
    
    // Objecto barra lateral
    var lateral_bar_object = new LateralBarWithoutSearch();
    var lateral_bar = null;
    
    var header_view_object = new HeaderViewText();
    var search_bar_object = new SearchBar();
    
    var run_constructor_fb_bool = null;
    
    var factor_correction = 0;
    
    this.constructorScreen = function (array_institutions, type_back)
    {
        // Verificar se existe login
        run_constructor_fb_bool = fb.loggedIn;
        
        // Construir barra lateral
        lateral_bar_object.constructorLateralBar();
        lateral_bar = lateral_bar_object.lateral_bar;
        
        // Construir header
        header_view_object.constructorHeaderView("SUBSCRIÇÕES");
        var header_view = header_view_object.header_view;
        
        // Construir barra de pesquisa
        search_bar_object.constructorSearchBar();
        var search_bar = search_bar_object.search_bar;
        
        institution_subscription_window = Titanium.UI.createWindow({
            navBarHidden: true
        });
        
        institution_subscription_window.addEventListener('android:back', function(e)
        {
            Ti.API.info('Volta atrás!');
            BackWindow(type_back, institution_subscription_window);
        });
        
        var background_view = Titanium.UI.createView({
            backgroundColor: "#f6f2e2"
        });
        
        institution_subscription_window.add(background_view);
        
        /** Colocar as varias instituições em forma de scroll **/
        var number_institutions;
        
        if(array_institutions == null)
        {
            number_institutions = 0;
        }
        else
        {
            number_institutions = array_institutions.length;
        }
        
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
            
            var click_institution = Titanium.UI.createImageView({
                image: '/healthplus/healthplus_negative.png',
                top: '22.03%',
                left: '82.93%',
                width: '11.08%',
                height: '55.66%',
                institution_click_id: array_institutions[i].id.toString()
            });
            
            institution_one_view.add(image_institution);
            institution_one_view.add(text_institution);
            institution_one_view.add(click_institution);
            institution_scroll_view.add(institution_one_view);
            
            institution_one_view.addEventListener('click', function(e)
            {
                if(e.source.institution_click_id == null)
                {
                    SpecsInstitution(e.source.institution_id);
                }
                else if(e.source.institution_click_id != null)
                {
                    UnsubscribeInstitution(e.source.institution_click_id);    
                }
            });
        }
        
        
        institution_subscription_window.add(lateral_bar);
        institution_subscription_window.add(institution_scroll_view);
        institution_subscription_window.add(search_bar);
        institution_subscription_window.add(header_view);
        
        // Fazer com que no inicio a searchBar não fique visivel
        search_bar.setVisible(false);
        search_bar_object.putEventListenerToShowSearchBar(header_view_object.search_header_view);
        
        // Fazer com que a barra lateral desapareca
        lateral_bar.setVisible(false);
        
        // Colocar events listeners barra lateral
        var eventListernerLateralBar = new EventsLateralBar();
        eventListernerLateralBar.putListenersEventsLateralBar(lateral_bar_object, type_back, "subscricoes", institution_subscription_window);
        
        // Verificar se esta logado
        changeLateralBar(type_back);
    };
    
    this.putEventListenersSubscriptionsInstitutionsScreen = function ()
    {
        var institution_scroll = institution_scroll_view;
        
        institution_subscription_window.addEventListener('swipe', function(e)
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
    };
    
    function changeLateralBar(type_back)
    {
        setInterval(function()
        {
            if(institution_subscription_window.getVisible() == true && run_constructor_fb_bool != fb.loggedIn)
            {
                PassToInstitutions();          
            }
        }, 100);
    };
    
    
    function UnsubscribeInstitution(institution_id)
    {
        var string_verify = "no_connection";
        var method = 'POST';
        var url = "http://" + url_ip + ":52144/odata/Subscriptions/DeleteSubscription";
        
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
                
                var size_children = institution_scroll_view.getChildren().length;
                factor_correction++;
                    
                for(var i = 0; i < size_children; i++)
                {
                    if(institution_scroll_view.getChildren()[i].institution_id != institution_id.toString())
                    {
                        var value_top_for = ((i-factor_correction) * 72.41) + 35.97;
                        institution_scroll_view.getChildren()[i].top = value_top_for + 'dp';
                    }
                    else
                    {
                        institution_scroll_view.getChildren()[i].setVisible(false);
                    } 
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
     
    this.showWindow = function()
    {
        institution_subscription_window.open();
    };
    
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
                type_back.push("subscricoes");
                institution.constructorScreen(string_verify, type_back);
                
                institution.putEventListenersInstitutionOneScreen();
                institution.showWindow();
                
                institution_subscription_window.setVisible(false);
                institution_subscription_window.close();
                
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
    
    function PassToInstitutions()
    {
        var string_verify = "no_connection";
        var method = 'GET';
        var url = "http://" + url_ip + ":52144/odata/Institutions";
        
        // Buscar dados a API
        var connection_api= Titanium.Network.createHTTPClient({
            onload: function()
            {
                while(string_verify == "no_connection")
                {
                    string_verify = JSON.parse(this.responseText);
                }
                
                var array_institutions = JSON.parse(string_verify.value);
                   
                var institutions_screen = new InstitutionsScreen();
                institutions_screen.constructorScreen(array_institutions, type_back);
                
                institutions_screen.showWindow();
                institutions_screen.putEventListenersInstitutionsScreen();
                run_constructor_fb_bool = fb.loggedIn;
                
                institution_subscription_window.close();
            },
            onerror: function()
            {
                alert("Erro na obtenção de instituições!");
            },
            timeout: 10000 // Tempo para fazer pedido
        });
        
        connection_api.open(method, url, false);
        connection_api.send();
    }
}
