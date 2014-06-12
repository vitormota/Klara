// Include facebook
Titanium.include("/facebook/init_facebook.js");

function ResultSearchScreen()
{
    var header_view_object = new HeaderViewText();
    
    var lateral_bar_object = new LateralBarWithoutSearch();
    var lateral_bar = null;
    
    var search_bar_object = new SearchBar();
    
    var results_search_window = null;
    var scroll_view = null;
    
    // "Tabs" onde se tem que clicar
    var view_results_institutions = null;
    var view_results_ads = null;

    // Linhas que separam
    var line2 = null;
    var line3 = null;
    
    var run_constructor_fb_bool = null;
    
    // Valores a serem usados 
    var value_top_line2 = 0;
    var value_top_ads_title = 0;
    var value_top_line3 = 0;
    
    var see_institutions = null;
    var value_height_institutions = null;
    var value_top_institutions = null;
    var length_institutions = 0;
    
    var see_ads = null;
    var value_height_ads = null;
    var value_top_ads = null;
    var length_ads = 0;
    
    var array_clicks = [];
    var textSearch = null;
    
    this.constructorScreen = function(type_back, array_ads, array_institutions, text)
    {
        length_ads = array_ads.length;
        Ti.API.info(length_ads);
        length_institutions = array_institutions.length;
        textSearch = text;
        
        // Verificar se existe login
        run_constructor_fb_bool = fb.loggedIn;
        
        // Construir barra lateral
        lateral_bar_object.constructorLateralBar();
        lateral_bar = lateral_bar_object.lateral_bar;
        
        // Construir header
        header_view_object.constructorHeaderView("RESULTADOS");
        var header_view = header_view_object.header_view;
        
        // Construir barra de pesquisa
        search_bar_object.constructorSearchBar();
        var search_bar = search_bar_object.search_bar;
        
        // Abrir nova janela
        results_search_window = Titanium.UI.createWindow({
            navBarHidden: true
        }); 
        
        results_search_window.addEventListener('android:back', function(e)
        {
            Ti.API.info('Volta atrás!');
            BackWindow(type_back, results_search_window);
        });
        
        var background_view = Titanium.UI.createView({
            backgroundColor: "#f6f2e2"
        });
        
        results_search_window.add(background_view);
        
        // Criar scroll view
        scroll_view = Titanium.UI.createScrollView({
            overScrollMode: Titanium.UI.Android.OVER_SCROLL_ALWAYS,
            scrollType: "vertical",
            top: '11.79%',
            width: '100%'
        });
        
        // Criar primeiro separador
        line1 = Titanium.UI.createView({
            backgroundColor: "#f1e9ce",
            width: '100%',
            height: '2.58dp',
            top: '0dp'
        });
        
        scroll_view.add(line1);
        
        // Criar view para mostrar dados pessoais
        var value_top_institutions_title = 2.58;
        var value_height_institutions_title = 50.43;
        
        view_results_institutions = Titanium.UI.createView({
            top: value_top_institutions_title + 'dp',
            height: value_height_institutions_title + 'dp'
        });
        
        var image_view_results_institutions = Titanium.UI.createImageView({
            image: "/healthplus/healthplus_institution.png",
            left: '7.20%',
            width: '9.66%',
            height: '58.27%',
            top: '20.01%'
        });
        
        view_results_institutions.add(image_view_results_institutions);
        
        var text_view_results_institutions = Titanium.UI.createLabel({
            text:"INSTITUIÇÕES",
            color: "#d96b63",
            left: '28.89%',
            font:
            {
                fontSize: '13%'
            },
            top: '30.01%'
        });
        
        view_results_institutions.add(text_view_results_institutions);
        scroll_view.add(view_results_institutions);
        
        if(array_institutions.length > 0)
        {
            // Ver cupões subscritos
            value_height_institutions = (array_institutions.length - 1)*65.14 + 52.03;
            value_top_institutions = value_top_institutions_title + value_height_institutions_title;
            
            see_institutions = Titanium.UI.createView({
                width: '85.4%',
                height: value_height_institutions + 'dp', 
                top: value_top_institutions + 'dp'
            });
            
            var top_value_for = 0;
            
            for(var i = 0; i < array_institutions.length; i++)
            {
                top_value_for = (i * 65.14);
                
                var institution_one_view = Titanium.UI.createView({
                    backgroundColor: "#f1e9ce",
                    borderRadius: 10,
                    top: top_value_for + 'dp',
                    height: '52.03dp',
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
                    requestInstitution.put(institution_one_view, scroll_view, top_value, args, array_institutions[i].id);
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
                
                see_institutions.add(institution_one_view);
            }
            
            see_institutions.setVisible(false);
            scroll_view.add(see_institutions);
        }
        
        // Colocacao da linha nº 2 (segundo separador)
        value_top_line2 = value_top_institutions_title + value_height_institutions_title;
        
        line2 = Titanium.UI.createView({
            backgroundColor: "#f1e9ce",
            width: '100%',
            height: '2.58dp',
            top: value_top_line2 +'dp' 
        });
        
        scroll_view.add(line2);
        
        // View para mostrar cupoes subscritos
        value_top_ads_title = value_top_line2 + 2.58;
        var value_height_ads_title = 50.43;
        
        view_results_ads = Titanium.UI.createView({
            top: value_top_ads_title + 'dp',
            height: value_height_ads_title + 'dp'
        });
        
        var image_view_results_ads = Titanium.UI.createImageView({
            image: "/healthplus/healthplus_subscription.png",
            left: '7.20%',
            width: '9.66%',
            height: '58.27%',
            top: '23.01%'
        });
        
        view_results_ads.add(image_view_results_ads);
        
        var text_view_results_ads = Titanium.UI.createLabel({
            text: "ANÚNCIOS",
            color: "#d96b63",
            left: '28.89%',
            font:
            {
                fontSize: '13%'
            },
            top: '30.01%'
        });
        
        view_results_ads.add(text_view_results_ads);
        scroll_view.add(view_results_ads);
        
        if(array_ads.length > 0)
        {
            // Ver cupões subscritos
            value_height_ads = (array_ads.length - 1)*65.14 + 52.03;
            value_top_ads = value_top_ads_title + value_height_ads_title + 5;
            
            see_ads = Titanium.UI.createView({
                width: '85.4%',
                height: value_height_ads + 'dp', 
                top: value_top_ads + 'dp'
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
                see_ads.add(see_ad);
                
                see_ad.addEventListener('click', function(e)
                {
                    if(run_constructor_fb_bool == false)
                    {
                        InitFacebook();
                    }
                    else if(run_constructor_fb_bool == true)
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
                    } 
                });
            }
            
            see_ads.setVisible(false);
            scroll_view.add(see_ads);
        }
        
        // Fazer o separador numero 3
        value_top_line3 = value_top_ads_title + value_height_ads_title;
        
        line3 = Titanium.UI.createView({
            backgroundColor: "#f1e9ce",
            width: '100%',
            height: '2.58dp',
            top: value_top_line3 +'dp' 
        });
        
        scroll_view.add(line3);
        
        
        results_search_window.add(scroll_view);
        results_search_window.add(lateral_bar);
        results_search_window.add(search_bar);
        results_search_window.add(header_view);
        
        // Fazer com que no inicio a searchBar não fique visivel
        search_bar.setVisible(false);
        search_bar_object.putEventListenerToShowSearchBar(header_view_object.search_header_view);
        
        // Fazer com que a barra lateral desapareca
        lateral_bar.setVisible(false);
        
        // Colocar events listeners barra lateral
        var eventListernerLateralBar = new EventsLateralBar();
        eventListernerLateralBar.putListenersEventsLateralBar(lateral_bar_object, type_back, "result_" + textSearch, results_search_window);
        
        // Verificar se esta logado
        changeLateralBar(type_back);
    };
    
    this.putEventListenersProfileScreen = function ()
    {
        var scroll_profile = scroll_view;
        
        results_search_window.addEventListener('swipe', function(e)
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
        view_results_institutions.addEventListener('click', function()
        {   
            if(length_institutions > 0)
            {
                if(see_institutions.getVisible() == false)
                {
                    value_top_line2 = value_top_line2 + value_height_institutions + 15;
                    line2.top = value_top_line2 + 'dp';
                    
                    value_top_ads_title = value_top_ads_title + value_height_institutions + 15;
                    view_results_ads.top = value_top_ads_title + 'dp';
                    
                    if(length_ads > 0)
                    {
                        value_top_ads = value_top_ads + value_height_institutions + 15;
                        see_ads.top = value_top_ads + 'dp';
                    }
                    
                    value_top_line3 = value_top_line3 + value_height_institutions + 15;
                    line3.top = value_top_line3 + 'dp';
    
                    see_institutions.setVisible(true);
                }
                else if(see_institutions.getVisible() == true)
                {
                    see_institutions.setVisible(false);
                    
                    value_top_line2 = value_top_line2 - value_height_institutions - 15;
                    line2.top = value_top_line2 + 'dp';
                    
                    value_top_ads_title = value_top_ads_title - value_height_institutions - 15;
                    view_results_ads.top = value_top_ads_title + 'dp';
                    
                    if(length_ads > 0)
                    {
                        value_top_ads = value_top_ads - value_height_institutions - 15;
                        see_ads.top = value_top_ads + 'dp';
                    }
                    
                    value_top_line3 = value_top_line3 - value_height_institutions - 15;
                    line3.top = value_top_line3 + 'dp';
                }
            }
        });
        
        view_results_ads.addEventListener('click', function()
        {   
            if(length_ads > 0)
            {
                if(see_ads.getVisible() == false)
                {
                    value_top_line3 = value_top_line3 + value_height_ads + 15;
                    line3.top = value_top_line3 + 'dp';
                    
                    see_ads.setVisible(true);
                }
                else if(see_ads.getVisible() == true)
                {
                    see_ads.setVisible(false);
                    
                    value_top_line3 = value_top_line3 - value_height_ads - 15;
                    line3.top = value_top_line3 + 'dp';
                  
                }
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
                SearchAd(type_back, search_bar_object.search_bar_textbox.value, results_search_window);
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
                type_back.push("result_" + textSearch);
                
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
                results_search_window.remove(lateral_bar);
                scroll_view.left = '0%';
                
                lateral_bar_object = new LateralBarWithoutSearch();
                lateral_bar_object.constructorLateralBar();
                lateral_bar = lateral_bar_object.lateral_bar;
                
                lateral_bar.setVisible(false);
                results_search_window.add(lateral_bar);
        
                eventListernerLateralBar = new EventsLateralBar();
                eventListernerLateralBar.putListenersEventsLateralBar(lateral_bar_object, type_back, "result_" + textSearch, results_search_window);
                
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
        results_search_window.open();
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
                type_back.push("result_" + textSearch);
                cupon.constructorScreen(ad, institution, type_back);
                    
                cupon.putEventListenersCuponScreen();
                cupon.showWindow();
                
                results_search_window.close();
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
                type_back.push("result_" + textSearch);
                institution.constructorScreen(string_verify, type_back);
                
                institution.putEventListenersInstitutionOneScreen();
                institution.showWindow();
                
                results_search_window.close();
                
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