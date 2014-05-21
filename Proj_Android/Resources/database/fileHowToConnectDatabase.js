function testConnectionDatabase()
{
	var string_verify = "no_connection";
	var method = 'POST';
	var url = "http://192.168.1.67:52144/odata/Subscriptions/InstitutionsSubscribe";
	
	// Argumentos para irem no pedido
	var client_id_int = 36;
	var args = {};
	args.client_id = client_id_int.toString();
	
	// Abrir nova janela
	var pag_inicial_window = Titanium.UI.createWindow();
	pag_inicial_window.navBarHidden = true;
	pag_inicial_window.open();
	
	var background_view = Titanium.UI.createView({
		backgroundColor: "#f6f2e2"
	});
	
	pag_inicial_window.add(background_view);
	
	
	// Criar clientes para ir buscar dados à api
	var client = Titanium.Network.createHTTPClient({
		onload: function()
		{
			while(string_verify == "no_connection")
			{
				alert("A conectar...");
				string_verify = JSON.parse(this.responseText).value;
			}
			
			var view_solution = Titanium.UI.createLabel({
				text: string_verify,
				color: 'black',
				top: '0%'
			});
			
			// Para se obter dados tem que se pegar no json que esta no STRING_VERIFY e fazer um novo parse e depois ai pode-se dividir
			// por instituição e ir buscar informações detalhadas
			alert(JSON.parse(string_verify)[0].name);
			
			pag_inicial_window.add(view_solution);
		},
		onerror: function()
		{
			alert('Erro!');
			string_verify = JSON.parse(this.responseText).value;
			
			var view_solution = Titanium.UI.createLabel({
				text: string_verify,
				color: 'black',
				top: '0%'
			});
			
			pag_inicial_window.add(view_solution);
		},
		timeout: 10000 // Tempo para fazer pedido
	});
	
	client.open(method, url, false);
	client.setRequestHeader("Content-Type", "application/json; charset=utf-8"); // Adaptar o tipo de header para o que é pedido
	
	client.send(JSON.stringify(args));
}
