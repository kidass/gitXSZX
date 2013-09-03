// show current date, time, weekday in the DIV timerId like:
// xxxx year xx month xx day week xx HH:mm:ss
// 
function DisplayDateAndWeekday()
{
	try {
		//get now date information
		var oDate = new Date();
		var sDate = "";
		
		//get year
		var sYear = oDate.getFullYear();
		sDate += sYear;
		sDate += decodeURI("%E5%B9%B4");
		
		//get month
		var sMonth = oDate.getMonth() + 1;
		if (sMonth >= 10)
			sDate += sMonth;
		else
			sDate += "0" + sMonth;
		sDate += decodeURI("%E6%9C%88");
		
		//get day
		var sDay = oDate.getDate();
		if (sDay >= 10)
			sDate += sDay;
		else
			sDate += "0" + sDay;
		sDate += decodeURI("%E6%97%A5");
		sDate += " ";
		
		//get weekday
		var sWeekDay = oDate.getDay();
		switch (sWeekDay) {
			case 0:
				sDate += decodeURI("%E6%98%9F%E6%9C%9F%E6%97%A5");
				break;
			case 1:
				sDate += decodeURI("%E6%98%9F%E6%9C%9F%E4%B8%80");
				break;
			case 2:
				sDate += decodeURI("%E6%98%9F%E6%9C%9F%E4%BA%8C");
				break;
			case 3:
				sDate += decodeURI("%E6%98%9F%E6%9C%9F%E4%B8%89");
				break;
			case 4:
				sDate += decodeURI("%E6%98%9F%E6%9C%9F%E5%9B%9B");
				break;
			case 5:
				sDate += decodeURI("%E6%98%9F%E6%9C%9F%E4%BA%94");
				break;
			default:
				sDate += decodeURI("%E6%98%9F%E6%9C%9F%E5%85%AD");
				break;
		}
		
		//get hours
		//var sTime = "";
		//var sHours = oDate.getHours();
		//if (sHours >= 10)
		//	sTime += sHours;
		//else
		//	sTime += "0" + sHours;
		//sTime += ":";
		
		//get minute
		//var sMinutes = oDate.getMinutes();
		//if (sMinutes >= 10)
		//	sTime += sMinutes;
		//else
		//	sTime += "0" + sMinutes;
		//sTime += ":";
		
		//get seconds
		//var sSeconds = oDate.getSeconds();
		//if (sSeconds >= 10)
		//	sTime += /sSeconds;
		//else
		//	sTime += "0" + sSeconds;
		sTime = "";
		
		//display date, time, and weekday
		timerId.innerHTML = sDate + " " + sTime;
		
		//set timeout "DisplayDateAndWeekday"
		try { if (m_dateWeekTimerId > 0) window.clearTimeout(m_dateWeekTimerId);m_dateWeekTimerId = -1;} catch (e) {}
		m_dateWeekTimerId = window.setTimeout("DisplayDateAndWeekday()", 200);
	} catch (e) {}
}

//show user login time
function ShowUserLoginTime()
{
	try {
		//get the user enter time
		var objControl = null;
		var strEnter = "";
		objControl = document.getElementById("mainmenu_htxtUserEnterTime");
		if (objControl)
			strEnter = objControl.value;
		if (strEnter == "")
			return;
			
		//get now date information
		var oDate1 = new Date(strEnter);
		var oDate2 = new Date();
		var intMinutes = oDate2.getTimezoneOffset();
		var oDate  = new Date(oDate2 - oDate1 + intMinutes*60*1000);

		//get hours
		var sTime = "";
		var sHours = oDate.getHours();
		if (sHours >= 10)
			sTime += sHours;
		else
			sTime += "0" + sHours;
		sTime += ":";
		
		//get minute
		var sMinutes = oDate.getMinutes();
		if (sMinutes >= 10)
			sTime += sMinutes;
		else
			sTime += "0" + sMinutes;
		sTime += ":";
		
		//get seconds
		var sSeconds = oDate.getSeconds();
		if (sSeconds >= 10)
			sTime += sSeconds;
		else
			sTime += "0" + sSeconds;
		
		//show login time
		mainmenu_lblUserEnterTime.innerHTML = sTime;
		
		//set timeout for "ShowUserLoginTime"
		try { if (m_loginTimeTimerId > 0) window.clearTimeout(m_loginTimeTimerId); m_loginTimeTimerId = -1;} catch (e) {}
		m_loginTimeTimerId = window.setTimeout("ShowUserLoginTime()", 200);
	} catch (e) {}
}

function doAutoRefresh()
{
	try {
		if (document.readyState.toLowerCase() != "complete")
			window.setTimeout("doAutoRefresh()", 500); 

		var objAutoRefreshEnabled = null;
		var objAutoRefreshTime = null;
		var objControl = null;
		var strEnter = "";
		
		//if not login, then check once per 15 seconds
		objControl = document.getElementById("mainmenu_lblUserEnterTime");
		if (objControl)
			strEnter = objControl.innerHTML;
		if (((strEnter == "") || (strEnter == undefined)))
		{
			try { if (m_autoLoadTimerId > 0) window.clearTimeout(m_autoLoadTimerId);m_autoLoadTimerId = -1;} catch (e) {}
			m_autoLoadTimerId = window.setTimeout("document.location.reload(true)", 15000); 
			return;
		}

		//if login, then check according to configured parameters (default 1800 seconds)
		try { if (m_autoRefreshTimerId > 0) window.clearTimeout(m_autoRefreshTimerId);m_autoRefreshTimerId = -1;} catch (e) {}
		objAutoRefreshEnabled = document.getElementById("mainmenu_htxtAutoRefreshEnabled");
		objAutoRefreshTime = document.getElementById("mainmenu_htxtAutoRefreshTime");
		if (objAutoRefreshEnabled)
		{
			if (objAutoRefreshEnabled.value == "1")
			{
				if (objAutoRefreshTime)
					m_autoRefreshTimerId = window.setTimeout("document.location.reload(true)", parseInt(objAutoRefreshTime.value,10)*1000); 
				else
					m_autoRefreshTimerId = window.setTimeout("document.location.reload(true)", 1800000); 
			}
		}
		else
			m_autoRefreshTimerId = window.setTimeout("document.location.reload(true)", 1800000); 
	} catch (e) {}
}
