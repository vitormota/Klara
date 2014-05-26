function testeDatas()
{
	var arrays_list = [];
	
	var arrays_today = [];
	var arrays_yesterday = [];
	var arrays_last_week = [];
	var arrays_last_month = [];
	var arrays_other_dates = [];
	
	var currentTime = new Date();
	
	var hours = 0;
	var minutes = 0;
	var month = currentTime.getMonth();
	var day = currentTime.getDate();
	var year = currentTime.getFullYear();
	
	var element1 = {};
	element1['nome'] = "Elemento1";
	element1['data'] = new Date(year, month, day, hours, minutes, 0, 0);
	arrays_list.push(element1);
	
	var element2 = {};
	element2['nome'] = "Elemento2";
	element2['data'] = new Date(year, month, 25, hours, minutes, 0, 0);
	arrays_list.push(element2);
	
	var element3 = {};
	element3['nome'] = "Elemento3";
	element3['data'] = new Date(year, 3, 29, hours, minutes, 0, 0);
	arrays_list.push(element3);
	
	var element4 = {};
	element4['nome'] = "Elemento4";
	element4['data'] = new Date(year, month, 23, hours, minutes, 0, 0);
	arrays_list.push(element4);
	
	var element5 = {};
	element5['nome'] = "Elemento5";
	element5['data'] = new Date(2012, 11, 22, hours, minutes, 0, 0);
	arrays_list.push(element5);
	
	var ref_date = new Date(year, month, day, 0, 0, 0, 0);
	
	for(var i = 0; i < arrays_list.length; i++)
	{
		var ref_for_date = new Date(year, month, day, 0, 0, 0, 0);
		
		if(arrays_list[i]['data'] >= ref_for_date)
		{
			arrays_today.push(arrays_list[i]);		
		}
		else if(arrays_list[i]['data'] >= ref_for_date.setDate((ref_date.getDate() - 1)))
		{
			arrays_yesterday.push(arrays_list[i]);		
		}
		else if(arrays_list[i]['data'] >= ref_for_date.setDate((ref_date.getDate() - 7)))
		{
			arrays_last_week.push(arrays_list[i]);		
		}
		else if(arrays_list[i]['data'] >= ref_for_date.setDate((ref_date.getDate() - 30)))
		{
			arrays_last_month.push(arrays_list[i]);		
		}
		else if(arrays_list[i]['data'] < ref_for_date.setDate((ref_date.getDate() - 30)))
		{
			arrays_other_dates.push(arrays_list[i]);		
		}
	}
	
	alert(JSON.stringify(arrays_today));
	alert(JSON.stringify(arrays_yesterday));
	alert(JSON.stringify(arrays_last_week));
	alert(JSON.stringify(arrays_last_month));
	alert(JSON.stringify(arrays_other_dates));
	
	
	// Ordenar um array pelas datas
	arrays_list.sort(function (a,b)
	{
		return new Date(a['data']) - new Date(b['data']);
	});
	
	// Verificar condições para colocação nos arrays
	
	alert(JSON.stringify(arrays_list));
}
