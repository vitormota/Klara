function BackWindow(type_back, current_window)
{   
    if(type_back.length > 0)
    {
        var last_id = type_back.length - 1;
        
        if(type_back[last_id] == "home")
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
                    
                    current_window.close();
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
        else if(type_back[last_id] == "pesquisa")
        {
            var search_screen = new SearchScreen();
            type_back.pop();
            search_screen.constructorScreen(type_back);
            
            search_screen.putEventListenersSearchScreen();
            search_screen.showWindow();
            
            current_window.close();
        }
        else if((type_back[last_id] == "instituicoes") || (type_back[last_id] == "subscricoes" && fb.loggedIn == false))
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
                    type_back.pop();
                    institutions_screen.constructorScreen(array_institutions, type_back);
                    
                    institutions_screen.showWindow();
                    institutions_screen.putEventListenersInstitutionsScreen();
                    
                    current_window.close();
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
        else if(type_back[last_id] == "subscricoes")
        {
            var string_verify = "no_connection";
            var method = 'POST';
            var url = "http://" + url_ip + ":52144/odata/Subscriptions/InstitutionsSubscribe";
            
            var args = {};
            args.client_id = user_id.toString(); 
            
            // Buscar dados a API
            var connection_api= Titanium.Network.createHTTPClient({
                onload: function()
                {
                    while(string_verify == "no_connection")
                    {
                        string_verify = JSON.parse(this.responseText);
                    }
                    
                    var array_institutions;
                    
                    if(string_verify.value == "no subscriptions")
                    {
                        array_institutions = null;
                    }
                    else
                    {
                        array_institutions = JSON.parse(string_verify.value);
                    }
                       
                    var subscriptions_screen = new SubscriptionsInstitutionsScreen();
                    type_back.pop();
                    subscriptions_screen.constructorScreen(array_institutions, type_back);
                    
                    subscriptions_screen.showWindow();
                    subscriptions_screen.putEventListenersSubscriptionsInstitutionsScreen();
                    
                    current_window.close();
                },
                onerror: function()
                {
                    alert("Erro na obtenção das subscrições!");
                },
                timeout: 10000 // Tempo para fazer pedido
            });
            
            connection_api.open(method, url, false);
            connection_api.setRequestHeader("Content-Type", "application/json; charset=utf-8");
      
            connection_api.send(JSON.stringify(args));
        }
        else if(type_back[last_id] == "perfil" && fb.loggedIn == false)
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
                    
                    current_window.close();
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
        else if(type_back[last_id] == "perfil")
        {
            var string_verify = "no_connection";
            var method = 'GET';
            var url = "http://" + url_ip + ":52144/odata/Clients(" + user_id.toString() + ")";
            
            // Buscar dados a API
            var connection_api = Titanium.Network.createHTTPClient({
                onload: function()
                {
                    while(string_verify == "no_connection")
                    {
                        string_verify = JSON.parse(this.responseText);
                    }
                    
                    type_back.pop();
                    GetSubscribeAds(type_back, current_window, string_verify);
                },
                onerror: function()
                {
                    alert('Erro na obtenção dos dados do utilizador!');
                },
                timeout: 10000 // Tempo para fazer pedido
            });
            
            connection_api.open(method, url, false);
            connection_api.send();
        }
        else if(type_back[last_id] == "qr-code")
        {
            var qrcode = new SeeQrCode();
            type_back.pop();
            qrcode.initializeScanner(type_back);
            
            current_window.close();
        }
        else
        {
            var result_split = [];
            result_split = type_back[last_id].split('_');
            
            if(result_split[0] == "result")
            {
                type_back.pop();
                SearchAd(type_back, result_split[1], current_window);
            }
            else if(result_split[0] == "cupao")
            {
                var cupon = new CuponScreen();
                type_back.pop();
                cupon.constructorScreen(result_split[1], type_back);
                
                cupon.putEventListenersCuponScreen();
                cupon.showWindow();
                
                current_window.close();
            }
            else if(result_split[0] == "instituicao")
            {
                var institution_id = result_split[1];
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
                        type_back.pop();
                        institution.constructorScreen(string_verify, type_back);
                        
                        institution.putEventListenersInstitutionOneScreen();
                        institution.showWindow();
                        
                        current_window.close();
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
        }
    }
    else
    {
       current_window.close();    
    }
}

function GetSubscribeAds(type_back, last_window, string_verify_init_profile)
{
    var string_verify = "no_connection";
    var method = 'POST';
    var url = "http://" + url_ip + ":52144/odata/Subscriptions/AdsSubscribe";
    
    var args = {};
    args.client_id = user_id.toString(); 
    
    // Buscar dados a API
    var connection_api= Titanium.Network.createHTTPClient({
        onload: function()
        {
            while(string_verify == "no_connection")
            {
                string_verify = JSON.parse(this.responseText);
            }
            
            var array_ads;
            
            if(string_verify.value == "no subscriptions")
            {
                array_ads = null;
            }
            else
            {
                array_ads = JSON.parse(string_verify.value);
            }
               
            GetPurchaseCupons(type_back, string_verify_init_profile, last_window, array_ads);
        },
        onerror: function()
        {
            alert("Erro na obtenção das subscrições!");
        },
        timeout: 10000 // Tempo para fazer pedido
    });
    
    connection_api.open(method, url, false);
    connection_api.setRequestHeader("Content-Type", "application/json; charset=utf-8");
  
    connection_api.send(JSON.stringify(args));
}

function GetPurchaseCupons(type_back, string_verify_init_profile, last_window, array_ads)
{
    var string_verify = "no_connection";
    var method = 'GET';
    var url = "http://" + url_ip + ":52144/odata/Cupon(" + user_id.toString() + ")/Purchases";
    
    // Buscar dados a API
    var connection_api= Titanium.Network.createHTTPClient({
        onload: function()
        {
            while(string_verify == "no_connection")
            {
                string_verify = JSON.parse(this.responseText);
            }
            
            var array_cupons = string_verify;
               
            var profile_screen = new ProfileScreen(string_verify_init_profile);
            profile_screen.constructorScreen(type_back, array_ads, array_cupons);
            
            profile_screen.showWindow();
            profile_screen.putEventListenersProfileScreen();
            
            last_window.close();
        },
        onerror: function()
        {
            alert("Erro na obtenção das subscrições!");
        },
        timeout: 10000 // Tempo para fazer pedido
    });
    
    connection_api.open(method, url, false);
    connection_api.send();
}

function SearchAd (type_back, textSearch, current_window)
{
    var string_verify = "no_connection";
    var method = 'POST';
    var url = "http://" + url_ip + ":52144/odata/Ads/SearchAd";
    
    var args = {};
    var last_id = 0;
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
    var last_id = 0;
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

