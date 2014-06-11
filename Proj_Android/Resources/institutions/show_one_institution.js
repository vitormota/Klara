// Incluir o ficheiro da pagina onde a barra e feita
Titanium.include("/components/lateral_bar.js");

// Incluir o ficheiro onde o header e a barra de pesquisa sao feitas
Titanium.include("/components/header_bar.js");

// Incluir ficheiro para fazer back
Titanium.include("/components/back_window.js");

// Include facebook
Titanium.include("/facebook/init_facebook.js");

var MapModule = require('ti.map');

function InstitutionOneScreen()
{
    var one_institution_window = null;
    var institution_scroll_view = null;
    
    // Objecto barra lateral
    var lateral_bar_object = new LateralBarWithoutSearch();
    var lateral_bar = null;
    
    var header_view_object = new HeaderViewImage();
    var search_bar_object = new SearchBar();
    
    var run_constructor_fb_bool = null;
    
    // Por causa de colocar o botao a aparecer ou a não aparecer
    var institution_image_subscribe;
    
    this.constructorScreen = function(institution, type_back)
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
        
        one_institution_window = Titanium.UI.createWindow({
            navBarHidden: true
        });
        
        one_institution_window.addEventListener('android:back', function(e)
        {
            Ti.API.info('Volta atrás!');
            BackWindow(type_back, one_institution_window);
        });
        
        var background_view = Titanium.UI.createView({
            backgroundColor: "#f6f2e2"
        });
        
        one_institution_window.add(background_view);
        
        /** View para aparecer a info sobre o cupao **/
        institution_scroll_view = Titanium.UI.createScrollView({
            top: '86.96dp',
            overScrollMode: Titanium.UI.Android.OVER_SCROLL_ALWAYS,
            scrollType: "vertical",
            contentHeight: 'auto',
            width: '100%'
        });
        
        var institution_image;
        
        if(institution.img_url == null)
        {
            institution_image = Titanium.UI.createImageView({
                image: "/healthplus/healthplus_test.jpg",
                top: '0%',
                height: '177.73dp',
                width: '84.68%',
                left: '7.49%',
                borderRadius: 10
            });
        }
        else
        {
            institution_image = Titanium.UI.createImageView({
                image: institution.img_url,
                top: '0%',
                height: '177.73dp',
                width: '84.68%',
                left: '7.49%',
                borderRadius: 10
            });
        }
        
        institution_scroll_view.add(institution_image);
        
        var institution_information = Titanium.UI.createView({
            left: '7.49%',
            width: '84.68%',
            top: '213.63dp',
            backgroundColor: '#f1e9ce',
            height: '621.67dp'
        });
        
        var institution_info_text = Titanium.UI.createView({
            left: '10.28%',
            width: '80.55%'
        });
        
        var institution_title = Titanium.UI.createLabel({
            text: (institution.name).toUpperCase(),
            color: '#a39795',
            top: '2.32%',
            left: '0%',
            font:
            {
                fontSize: '12.94dp'
            }
        });
        
        institution_info_text.add(institution_title);
        
        var institution_line = Titanium.UI.createView({
            height: '0.27%',
            backgroundColor: '#d96b63',
            top: '5.94%'
        });
        
        institution_info_text.add(institution_line);
        
        var institution_address_title = Titanium.UI.createLabel({
            text: "Morada",
            top: '8.87%',
            color: '#d96b63',
            left: '0%',
            font:
            {
                fontSize: '11dp'
            }
        });

        institution_info_text.add(institution_address_title);
        
        var institution_address_text = Titanium.UI.createLabel({
            text: institution.address,
            top: '10.71%',
            color: '#d96b63',
            left: '0%',
            font:
            {
                fontSize: '11dp'
            }
        });
        
        institution_info_text.add(institution_address_text);
        
        var institution_e_mail_title = Titanium.UI.createLabel({
            text: "E-mail",
            top: "19%",
            left: '0%',
            color: '#d96b63',
            font: 
            {
                fontSize: '11dp'
            }
        });
        
        institution_info_text.add(institution_e_mail_title);
        
        var institution_e_mail_text = Titanium.UI.createLabel({
            text: institution.email,
            top: "20.54%",
            left: '0%',
            color: '#d96b63',
            font: 
            {
                fontSize: '11dp'
            }
        });
        
        institution_info_text.add(institution_e_mail_text);
        
        var institution_city_title = Titanium.UI.createLabel({
            text: "Cidade",
            top: "24.47%",
            left: '0%',
            color: '#d96b63',
            font: 
            {
                fontSize: '11dp'
            }
        });
        
        institution_info_text.add(institution_city_title);
        
        var institution_city_text = Titanium.UI.createLabel({
            text: institution.city,
            top: "26.01%",
            left: '0%',
            color: '#d96b63',
            font: 
            {
                fontSize: '11dp'
            }
        });
        
        institution_info_text.add(institution_city_text);
        
        var institution_phone_number_title = Titanium.UI.createLabel({
            text: "Telefone",
            top: "29.94%",
            left: '0%',
            color: '#d96b63',
            font: 
            {
                fontSize: '11dp'
            }
        });
        
        institution_info_text.add(institution_phone_number_title);
        
        var institution_phone_number_text = Titanium.UI.createLabel({
            text: institution.phone_number,
            top: "31.48%",
            left: '0%',
            color: '#d96b63',
            font: 
            {
                fontSize: '11dp'
            }
        });
        
        institution_info_text.add(institution_phone_number_text);
        
        var institution_website_title = Titanium.UI.createLabel({
            text: "Website",
            top: "35.41%",
            left: '0%',
            color: '#d96b63',
            font: 
            {
                fontSize: '11dp'
            }
        });
        
        institution_info_text.add(institution_website_title);
        
        var institution_website_text = Titanium.UI.createLabel({
            text: institution.website,
            top: "36.95%",
            left: '0%',
            color: '#d96b63',
            font: 
            {
                fontSize: '11dp'
            }
        });
        
        institution_info_text.add(institution_website_text);
        
        var institution_group_title = Titanium.UI.createLabel({
            text: "Grupo",
            top: "40.88%",
            left: '0%',
            color: '#d96b63',
            font: 
            {
                fontSize: '11dp'
            }
        });
        
        institution_info_text.add(institution_group_title);
        
        var institution_group_text;
        
        if(institution.group == null)
        {
            institution_group_text = Titanium.UI.createLabel({
                text: "n/d",
                top: "42.42%",
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
            FindNameGroup(institution_info_text, institution.group_id);
        }
        
        institution_info_text.add(institution_group_text);
        
        
        var institution_fax_title = Titanium.UI.createLabel({
            text: "Fax",
            top: "46.35%",
            left: '0%',
            color: '#d96b63',
            font: 
            {
                fontSize: '11dp'
            }
        });
        
        institution_info_text.add(institution_fax_title);
        
        var institution_fax_text;
        
        if(institution.fax == null)
        {
            institution_fax_text = Titanium.UI.createLabel({
                text: "n/d",
                top: "47.89%",
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
            institution_fax_text = Titanium.UI.createLabel({
                text: institution.fax,
                top: "47.89%",
                left: '0%',
                color: '#d96b63',
                font: 
                {
                    fontSize: '11dp'
                }
            });
        }
        
        institution_info_text.add(institution_fax_text);
        
        var institution_localization_title = Titanium.UI.createLabel({
            text: "Localização",
            top: "51.82%",
            left: '0%',
            color: '#d96b63',
            font: 
            {
                fontSize: '11dp'
            }
        });
        
        institution_info_text.add(institution_localization_title);
        
        /*var hospital = MapModule.createAnnotation({
            latitude: institution.latitude,
            longitude: institution.longitude,
            draggable: false,
            pincolor: MapModule.ANNOTATION_AZURE,   
            title: institution.name,
            subtitle: institution.address
        });
        
        var institution_localization_image = MapModule.createView({
            userLocation: false,
            mapType: MapModule.NORMAL_TYPE,
            animate: false,
            region: {latitude: institution.latitude, longitude: institution.longitude, latitudeDelta: 0.5, longitudeDelta: 0.5 },
            top: "53.36%",
            left: '0%',
            height: '30.7%'
        });
        
        institution_localization_image.addAnnotation(hospital);
        institution_info_text.add(institution_localization_image);*/
        
        var institution_localization_image = Titanium.UI.createImageView({
            image: "/healthplus/healthplus_google_maps.jpg",
            top: "53.36%",
            left: '0%',
            height: '30.7%'
        });
        
        institution_info_text.add(institution_localization_image);
        
        if(run_constructor_fb_bool == true)
        {
            var args = {};
            args.client_id = user_id.toString();
            args.subscribable_id = institution.id.toString();
            
            InstitutionUserSubscribe(institution_info_text, args);
        }
        
        institution_information.add(institution_info_text);
        institution_scroll_view.add(institution_information);
        
        one_institution_window.add(institution_scroll_view);
        one_institution_window.add(header_view);
        one_institution_window.add(search_bar);
        one_institution_window.add(lateral_bar);
        
        // Fazer com que no inicio a searchBar não fique visivel
        search_bar.setVisible(false);
        search_bar_object.putEventListenerToShowSearchBar(header_view_object.search_header_view);
        
        // Fazer com que a barra lateral desapareca
        lateral_bar.setVisible(false);
        
        // Colocar events listeners barra lateral
        var eventListernerLateralBar = new EventsLateralBar();
        eventListernerLateralBar.putListenersEventsLateralBar(lateral_bar_object, type_back, "instituicao_" + institution.id, one_institution_window);
        
        // Verificar se esta logado
        changeLateralBar(type_back, institution);
    };
    
    this.putEventListenersInstitutionOneScreen = function ()
    {
        //var lateral_bar_listener = lateral_bar_object.lateral_bar;
        var institution_scroll = institution_scroll_view;
        
        one_institution_window.addEventListener('swipe', function(e)
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
    
    function changeLateralBar(type_back, institution)
    {
        setInterval(function()
        {
            if(run_constructor_fb_bool != fb.loggedIn)
            {
                lateral_bar.setVisible(false);
                one_institution_window.remove(lateral_bar);
                institution_scroll_view.left = '0%';
                
                lateral_bar_object = new LateralBarWithoutSearch();
                lateral_bar_object.constructorLateralBar();
                lateral_bar = lateral_bar_object.lateral_bar;
                
                lateral_bar.setVisible(false);
                one_institution_window.add(lateral_bar);
        
                eventListernerLateralBar = new EventsLateralBar();
                eventListernerLateralBar.putListenersEventsLateralBar(lateral_bar_object, type_back, "instituicao_" + institution.id, one_institution_window);
                
                if(run_constructor_fb_bool == true)
                {
                    institution_image_subscribe.setVisible(false);
                }
                
                run_constructor_fb_bool = fb.loggedIn;              
            }
        }, 100);
    };
    
    this.showWindow = function()
    {
        one_institution_window.open();
    };
    
    function InstitutionUserSubscribe(institution_info_text, args)
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
                    institution_image_subscribe = Titanium.UI.createImageView({
                        image: "/healthplus/healthplus_positive.png",
                        bottom: '5.77%',
                        height: '6.66%',
                        width: '19.44%',
                        type_obj: "button_subscribe"
                    });
                    
                    institution_info_text.add(institution_image_subscribe);
                }
                else if(string_verify.value == "true")
                {
                    institution_image_subscribe = Titanium.UI.createImageView({
                        image: "/healthplus/healthplus_negative.png",
                        bottom: '5.77%',
                        height: '6.66%',
                        width: '19.44%',
                        type_obj: "button_unsubscribe"
                    });
                    
                    institution_info_text.add(institution_image_subscribe);
                }
                else
                {
                    alert("Algo se passou!");
                }
                
                institution_image_subscribe.addEventListener('click', function(e)
                {
                    if(e.source.type_obj != null)
                    {
                        if(e.source.type_obj == "button_subscribe")
                        {
                            SubscribeInstitution(true, args.subscribable_id, e.source);
                        }
                        else if(e.source.type_obj == "button_unsubscribe")
                        {
                            SubscribeInstitution(false, args.subscribable_id, e.source);
                        }
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
        args.subscribable_id = institution_id.toString();
                        
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
    
    function FindNameGroup(institution_info_text, group_id)
    {
        var string_verify = "no_connection";
        var method = 'GET';
        var url = "http://" + url_ip + ":52144/odata/InstsGroup(" + group_id.toString() + ")";
                
        // Buscar dados a API
        var connection_api= Titanium.Network.createHTTPClient(
        {
            onload: function()
            {
                while(string_verify == "no_connection")
                {
                    string_verify = JSON.parse(this.responseText);
                }
                
                var institution_group_text = Titanium.UI.createLabel({
                    text: string_verify.name,
                    top: "42.42%",
                    left: '0%',
                    color: '#d96b63',
                    font: 
                    {
                        fontSize: '11dp'
                    }
                });
                
                institution_info_text.add(institution_group_text);
                
            },
            onerror: function()
            {
                alert("Erro na obtenção do nome do grupo no qual a instituição pertence!!");
            },
            timeout: 10000 // Tempo para fazer pedido
        });
        
        connection_api.open(method, url, false);
        connection_api.send();
    }
}



