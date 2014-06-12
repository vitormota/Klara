// load the Scandit SDK module
var scanditsdk = require("com.mirasense.scanditsdk");

// Incluir ficheiro para fazer back
Titanium.include("/components/back_window.js");

function SeeQrCode()
{
    this.initializeScanner = function (type_back)
    {
        var picker;
        
        // Create a window to add the picker to and display it. 
        var window = Titanium.UI.createWindow({
                navBarHidden: true
        });
        
        window.addEventListener('android:back', function(e)
        {
            Ti.API.info('Volta atrás!');
            BackWindow(type_back, window);
        });
        
        // Sets up the scanner and starts it in a new window.
        var openScanner = function() {
            
            // Instantiate the Scandit SDK Barcode Picker view
            picker = scanditsdk.createView({
                width:"100%",
                height:"100%"
            });
            
            // Initialize the barcode picker, remember to paste your own app key here.
            picker.init("/DzhqupCEeODJjYzwukAfgO+Ja3xGdQNKibV98pyNW8", 0);
            //picker.showSearchBar(true);
            
            // add a tool bar at the bottom of the scan view with a cancel button (iphone/ipad only)
            //picker.showToolBar(true);
            
            // Set callback functions for when scanning succeedes and for when the 
            // scanning is canceled.
            picker.setSuccessCallback(function(e) 
            {
                if(e.barcode.indexOf("cupon") > -1)
                {
                    var result_split = [];
                    result_split = e.barcode.split('_');
                    
                    SpecsCupon(result_split[1], type_back);
                }
                else if(e.barcode.indexOf("institution") > -1)
                {
                    var result_split = [];
                    result_split = e.barcode.split('_');
                    
                    SpecsInstitution(result_split[1], type_back);
                }
                else
                {
                    alert("QR-CODE inválido!");
                }
            });
            
            picker.setCancelCallback(function(e) {
                closeScanner();
            });
            
            window.add(picker);
            window.addEventListener('open', function(e) {
                // Adjust to the current orientation.
                // since window.orientation returns 'undefined' on ios devices 
                // we are using Ti.UI.orientation (which is deprecated and no longer 
                // working on Android devices.)
                if(Ti.Platform.osname == 'iphone' || Ti.Platform.osname == 'ipad'){
                    picker.setOrientation(Ti.UI.orientation);
                }   
                else {
                    picker.setOrientation(window.orientation);
                }
                
                picker.setSize(Ti.Platform.displayCaps.platformWidth, 
                               Ti.Platform.displayCaps.platformHeight);
                picker.startScanning();     // startScanning() has to be called after the window is opened. 
            });
            window.open();
        };
        
        // Stops the scanner, removes it from the window and closes the latter.
        var closeScanner = function() {
            if (picker != null) {
                picker.stopScanning();
                window.remove(picker);
            }
            window.close();
        };
        
        // Changes the picker dimensions and the video feed orientation when the
        // orientation of the device changes.
        Ti.Gesture.addEventListener('orientationchange', function(e) {
            window.orientationModes = [Titanium.UI.PORTRAIT, Titanium.UI.UPSIDE_PORTRAIT, 
                           Titanium.UI.LANDSCAPE_LEFT, Titanium.UI.LANDSCAPE_RIGHT];
            if (picker != null) {
                picker.setOrientation(e.orientation);
                picker.setSize(Ti.Platform.displayCaps.platformWidth, 
                        Ti.Platform.displayCaps.platformHeight);
                // You can also adjust the interface here if landscape should look
                // different than portrait.
            }
        });
        
        // Mal dê o clique para logo para a visualizacao
        openScanner();
    };
}

function SpecsInstitution(institution_id, type_back)
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
            type_back.push("qr-code");
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

function SpecsCupon(cupon_id, type_back)
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
            type_back.push("qr-code");
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
