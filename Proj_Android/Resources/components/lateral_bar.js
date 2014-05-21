function LateralBarWithoutSearch()
{
	// Variavel global
	this.lateral_bar = null;
	
	// Variaveis dos botoes
	this.lateral_home_view = null;
	this.lateral_search_view = null;
	this.lateral_subscription_view = null;
	this.lateral_definition_view = null;
	this.lateral_institution_view = null;
	this.lateral_register_view = null;
	this.lateral_perfil_view = null;
	this.lateral_qr_code_view = null;

	this.constructorLateralBar = function()
	{
		this.lateral_bar = Titanium.UI.createView({
			left: '0%',
			top: '11.78%',
			width: '76.58%'
		});
		
		/*** Botão Home***/
		this.lateral_home_view = Titanium.UI.createView({
			top: '4.54%',
			left: '17.07%',
			backgroundColor: '#f1e9cE',
			width: '71.68%',
			height:'9.24%',
			borderRadius: 10,
			zIndex: 1
		});
		
		var lateral_home_image = Titanium.UI.createImageView({
			image: "/healthplus/healthplus_home.png",
			width: '15.56%',
			left: '10.38%',
			height: '59.99%',
			top: '19.25%',
			zIndex: 1
		});
		
		var lateral_home_text = Titanium.UI.createLabel({
			text: "HOME",
			verticalAlign: Titanium.UI.TEXT_VERTICAL_ALIGNMENT_CENTER,
			width: '53.21%',
			left: '32.44%',
			color: "#d96b63",
			font:
			{
				fontSize: '13%'
			},
			zIndex: 1
		});
		
		this.lateral_home_view.add(lateral_home_image);
		this.lateral_home_view.add(lateral_home_text);
		this.lateral_bar.add(this.lateral_home_view);
		
		
		/*** Botão Pesquisa***/
		this.lateral_search_view = Titanium.UI.createView({
			top: '16.17%',
			left: '17.07%',
			backgroundColor: '#f1e9cE',
			width: '71.68%',
			height:'9.24%',
			borderRadius: 10,
			zIndex: 1
		});
		
		var lateral_search_image = Titanium.UI.createImageView({
			image: "/healthplus/healthplus_search.png",
			width: '15.56%',
			left: '10.38%',
			height: '59.99%',
			top: '19.25%',
			zIndex: 1
		});
		
		var lateral_search_text = Titanium.UI.createLabel({
			text: "PESQUISA",
			verticalAlign: Titanium.UI.TEXT_VERTICAL_ALIGNMENT_CENTER,
			width: '53.21%',
			left: '32.44%',
			color: "#d96b63",
			font:
			{
				fontSize: '13%'
			},
			zIndex: 1
		});
		
		this.lateral_search_view.add(lateral_search_image);
		this.lateral_search_view.add(lateral_search_text);
		this.lateral_bar.add(this.lateral_search_view);
		
		
		/*** Botão Subscricoes***/
		this.lateral_subscription_view = Titanium.UI.createView({
			top: '27.8%',
			left: '17.07%',
			backgroundColor: '#f1e9cE',
			width: '71.68%',
			height:'9.24%',
			borderRadius: 10,
			zIndex: 1
		});
		
		var lateral_subscription_image = Titanium.UI.createImageView({
			image: "/healthplus/healthplus_subscription.png",
			width: '15.56%',
			left: '10.38%',
			height: '59.99%',
			top: '19.25%',
			zIndex: 1
		});
		
		var lateral_subscription_text = Titanium.UI.createLabel({
			text: "SUBSCRIÇÕES",
			verticalAlign: Titanium.UI.TEXT_VERTICAL_ALIGNMENT_CENTER,
			width: '53.21%',
			left: '32.44%',
			color: "#d96b63",
			font:
			{
				fontSize: '13%'
			},
			zIndex: 1
		});
		
		this.lateral_subscription_view.add(lateral_subscription_image);
		this.lateral_subscription_view.add(lateral_subscription_text);
		this.lateral_bar.add(this.lateral_subscription_view);
		
		
		/*** Botão Definicoes***/
		this.lateral_definition_view = Titanium.UI.createView({
			top: '39.43%',
			left: '17.07%',
			backgroundColor: '#f1e9cE',
			width: '71.68%',
			height:'9.24%',
			borderRadius: 10,
			zIndex: 1
		});
		
		var lateral_definition_image = Titanium.UI.createImageView({
			image: "/healthplus/healthplus_definition.png",
			width: '15.56%',
			left: '10.38%',
			height: '59.99%',
			top: '19.25%',
			zIndex: 1
		});
		
		var lateral_definition_text = Titanium.UI.createLabel({
			text: "DEFINIÇÕES",
			verticalAlign: Titanium.UI.TEXT_VERTICAL_ALIGNMENT_CENTER,
			width: '53.21%',
			left: '32.44%',
			color: "#d96b63",
			font:
			{
				fontSize: '13%'
			},
			zIndex: 1
		});
		
		this.lateral_definition_view.add(lateral_definition_image);
		this.lateral_definition_view.add(lateral_definition_text);
		this.lateral_bar.add(this.lateral_definition_view);
		
		
		/*** Botão Instituicoes***/
		this.lateral_institution_view = Titanium.UI.createView({
			top: '51.06%',
			left: '17.07%',
			backgroundColor: '#f1e9cE',
			width: '71.68%',
			height:'9.24%',
			borderRadius: 10,
			zIndex: 1
		});
		
		var lateral_institution_image = Titanium.UI.createImageView({
			image: "/healthplus/healthplus_institution.png",
			width: '15.56%',
			left: '10.38%',
			height: '59.99%',
			top: '19.25%',
			zIndex: 1
		});
		
		var lateral_institution_text = Titanium.UI.createLabel({
			text: "INSTITUIÇÕES",
			verticalAlign: Titanium.UI.TEXT_VERTICAL_ALIGNMENT_CENTER,
			width: '53.21%',
			left: '32.44%',
			color: "#d96b63",
			font:
			{
				fontSize: '13%'
			},
			zIndex: 1
		});
		
		this.lateral_institution_view.add(lateral_institution_image);
		this.lateral_institution_view.add(lateral_institution_text);
		this.lateral_bar.add(this.lateral_institution_view);
		
		
		/*** Botão Registo***/
		this.lateral_register_view = Titanium.UI.createView({
			top: '62.69%',
			left: '17.07%',
			backgroundColor: '#f1e9cE',
			width: '71.68%',
			height:'9.24%',
			borderRadius: 10,
			zIndex: 1
		});
		
		var lateral_register_image = Titanium.UI.createImageView({
			image: "/healthplus/healthplus_register.png",
			width: '15.56%',
			left: '10.38%',
			height: '59.99%',
			top: '19.25%',
			zIndex: 1
		});
		
		var lateral_register_text = Titanium.UI.createLabel({
			text: "REGISTO",
			verticalAlign: Titanium.UI.TEXT_VERTICAL_ALIGNMENT_CENTER,
			width: '53.21%',
			left: '32.44%',
			color: "#d96b63",
			font:
			{
				fontSize: '13%'
			},
			zIndex: 1
		});
		
		this.lateral_register_view.add(lateral_register_image);
		this.lateral_register_view.add(lateral_register_text);
		this.lateral_bar.add(this.lateral_register_view);
		
		
		/*** Botão Perfil***/
		this.lateral_perfil_view = Titanium.UI.createView({
			top: '74.32%',
			left: '17.07%',
			backgroundColor: '#f1e9cE',
			width: '71.68%',
			height:'9.24%',
			borderRadius: 10,
			zIndex: 1
		});
		
		var lateral_perfil_image = Titanium.UI.createImageView({
			image: "/healthplus/healthplus_perfil.png",
			width: '15.56%',
			left: '10.38%',
			height: '59.99%',
			top: '19.25%',
			zIndex: 1
		});
		
		var lateral_perfil_text = Titanium.UI.createLabel({
			text: "PERFIL",
			verticalAlign: Titanium.UI.TEXT_VERTICAL_ALIGNMENT_CENTER,
			width: '53.21%',
			left: '32.44%',
			color: "#d96b63",
			font:
			{
				fontSize: '13%'
			},
			zIndex: 1
		});
		
		this.lateral_perfil_view.add(lateral_perfil_image);
		this.lateral_perfil_view.add(lateral_perfil_text);
		this.lateral_bar.add(this.lateral_perfil_view);
		
		
		
		/*** Botão Qr-Code***/
		this.lateral_qr_code_view = Titanium.UI.createView({
			top: '85.95%',
			left: '17.07%',
			backgroundColor: '#f1e9cE',
			width: '71.68%',
			height:'9.24%',
			borderRadius: 10,
			zIndex: 1
		});
		
		var lateral_qr_code_image = Titanium.UI.createImageView({
			image: "/healthplus/healthplus_qr_code.png",
			width: '15.56%',
			left: '10.38%',
			height: '59.99%',
			top: '19.25%',
			zIndex: 1
		});
		
		var lateral_qr_code_text = Titanium.UI.createLabel({
			text: "LER QR CODE",
			verticalAlign: Titanium.UI.TEXT_VERTICAL_ALIGNMENT_CENTER,
			width: '53.21%',
			left: '32.44%',
			color: "#d96b63",
			font:
			{
				fontSize: '13%'
			},
			zIndex: 1
		});
		
		this.lateral_qr_code_view.add(lateral_qr_code_image);
		this.lateral_qr_code_view.add(lateral_qr_code_text);
		this.lateral_bar.add(this.lateral_qr_code_view);
		
		
		/*** Opacidades ao nivel certo ***/
		var create_lateral_opacity_view = Titanium.UI.createView({
			height: '100%',
			width: '100%',
			backgroundColor: "#a39795",
			opacity: 0.7
		});
		
		this.lateral_bar.add(create_lateral_opacity_view);
	};
}


