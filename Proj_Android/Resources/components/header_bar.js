// Fazer a header das paginas com a imagem Health+
function HeaderViewImage()
{
	this.header_view = null;
	
	// Botoes do header
	this.search_header_view = null;
	this.menu_header_view = null;
	
	this.constructorHeaderView = function()
	{
		this.header_view = Titanium.UI.createView({
			backgroundColor: "#cf6b64",
			height: '11.94%',
			top: '0%'
		});
		
		var logo_header_view = Titanium.UI.createImageView({
			image: "/healthplus/healthplus_logo_white.png",
			top: '37.39%',
			height: '24.76%',
			width: '27.47%'
		});
		
		this.search_header_view = Titanium.UI.createImageView({
			image: "/healthplus/healthplus_search_transperant.png",
			top: '37.39%',
			left: '85.18%',
			height: '26.74%',
			width: '5.85%'
		});
		
		this.menu_header_view = Titanium.UI.createImageView({
			image: "/healthplus/healthplus_menu_leftside.png",
			top: '37.39%',
			left: '6.79%',
			width: '8.49%',
			height: '27.07%'
		});
		
		this.header_view.add(this.search_header_view);
		this.header_view.add(this.menu_header_view);
		this.header_view.add(logo_header_view);
	};
}


// Fazer a header das paginas com texto definido posteriormente
function HeaderViewText()
{
	this.header_view = null;
	
	// Botoes do header
	this.search_header_view = null;
	this.menu_header_view = null;
	
	this.constructorHeaderView = function(textTitle)
	{
		this.header_view = Titanium.UI.createView({
			backgroundColor: "#cf6b64",
			height: '11.94%',
			top: '0%'
		});
		
		var text_header_view = Titanium.UI.createLabel({
			top: '30.39%',
			height: '38.76%',
			width: '50%',
			color: "white",
			text: textTitle,
			font:
			{
				fontSize: "20%"
			},
			textAlign: Titanium.UI.TEXT_ALIGNMENT_CENTER
		});
		
		this.search_header_view = Titanium.UI.createImageView({
			image: "/healthplus/healthplus_search_transperant.png",
			top: '37.39%',
			left: '85.18%',
			height: '26.74%',
			width: '5.85%'
		});
		
		this.menu_header_view = Titanium.UI.createImageView({
			image: "/healthplus/healthplus_menu_leftside.png",
			top: '37.39%',
			left: '6.79%',
			width: '8.49%',
			height: '27.07%'
		});
		
		this.header_view.add(this.search_header_view);
		this.header_view.add(this.menu_header_view);
		this.header_view.add(text_header_view);
	};
}

// Fazer a bar para a procura
function SearchBar()
{
	this.search_bar = null;
	
	this.constructorSearchBar = function()
	{
		this.search_bar = Titanium.UI.createView({
			height: '7.69%',
			width: '85.15%',
			top: '12.01%'
		});
		
		var search_bar_icon = Titanium.UI.createImageView({
			image: "/healthplus/healthplus_search.png",
			height: '53.27%',
			width: '8.55%',
			left: '6.07%',
			top: '22.46%',
			opacity: 1,
			zIndex: 1
		});
		
		var search_bar_textbox = Titanium.UI.createTextField({
			width: '67.90%',
			height: '52.12%',
			left: '17.61%',
			top: '23.62%',
			autocorrect: false,
			backgroundColor: "white",
			color: "black",
			hintText: "PESQUISA",
			softKeyboardOnFocus: Titanium.UI.Android.SOFT_KEYBOARD_SHOW_ON_FOCUS,
			opacity: 1,
			zIndex: 1
		});
		
		var search_bar_opacity_view = Titanium.UI.createView({
			backgroundColor: "#a39795",
			opacity: 0.6,
			width: '100%',
			height: '100%',
			zIndex: 0
		});
		
		search_bar_textbox.font = {
			fontSize: '7%'
		};
		
		this.search_bar.add(search_bar_icon);
		this.search_bar.add(search_bar_textbox);
		this.search_bar.add(search_bar_opacity_view);
	};
	
	// Evento para a pesquisa, independente de outras coisas
	// Carregar no botao de search
	this.putEventListenerToShowSearchBar = function(buttonSearch)
	{	
		var search_bar_listener = this.search_bar;
		
		buttonSearch.addEventListener('click', function()
		{
			if(search_bar_listener.getVisible() == true)
			{
				search_bar_listener.setVisible(false);
			}
			else
			{
				search_bar_listener.setVisible(true);
			}
		});
	};
}
