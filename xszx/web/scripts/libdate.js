//
//convert yyyy-MM-dd to mm-dd-yyyy
//
function doConvertDateFormat(strDate)
{
	if (strDate == undefined)
		return "";
	if (strDate == "")
		return "";
		
	var strArray;
	strArray = strDate.split("-");
	strDate = strArray[1] + "-" + strArray[2] + "-" + strArray[0];
	
	return strDate;
}

// 
//get the chinese year expression
//
function getChineseYear(objDate)
{
	if (objDate == null)
		return "";
		
	var intYear = objDate.getYear();
	var strYear = intYear.toString();
	var intLen = strYear.length;
	var strResult = "";
	var i = 0;
	for (i=0; i<intLen; i++)
	{
		switch (parseInt(strYear.substr(i,1), 10))
		{
			case 0:
				strResult += decodeURI("%E2%97%8B");
				break;
			case 1:
				strResult += decodeURI("%E4%B8%80");
				break;
			case 2:
				strResult += decodeURI("%E4%BA%8C");
				break;
			case 3:
				strResult += decodeURI("%E4%B8%89");
				break;
			case 4:
				strResult += decodeURI("%E5%9B%9B");
				break;
			case 5:
				strResult += decodeURI("%E4%BA%94");
				break;
			case 6:
				strResult += decodeURI("%E5%85%AD");
				break;
			case 7:
				strResult += decodeURI("%E4%B8%83");
				break;
			case 8:
				strResult += decodeURI("%E5%85%AB");
				break;
			default:
				strResult += decodeURI("%E4%B9%9D");
				break;
		}
	}
	
	return strResult;
}

// 
//get the chinese month expression
//
function getChineseMonth(objDate)
{
	if (objDate == null)
		return "";

	var strResult = "";
	var intMonth = objDate.getMonth();
	
	switch (intMonth)
	{
		case 0:
			strResult += decodeURI("%E4%B8%80");
			break;
		case 1:
			strResult += decodeURI("%E4%BA%8C");
			break;
		case 2:
			strResult += decodeURI("%E4%B8%89");
			break;
		case 3:
			strResult += decodeURI("%E5%9B%9B");
			break;
		case 4:
			strResult += decodeURI("%E4%BA%94");
			break;
		case 5:
			strResult += decodeURI("%E5%85%AD");
			break;
		case 6:
			strResult += decodeURI("%E4%B8%83");
			break;
		case 7:
			strResult += decodeURI("%E5%85%AB");
			break;
		case 8:
			strResult += decodeURI("%E4%B9%9D");
			break;
		case 9:
			strResult += decodeURI("%E5%8D%81");
			break;
		case 10:
			strResult += decodeURI("%E5%8D%81%E4%B8%80");
			break;
		default:
			strResult += decodeURI("%E5%8D%81%E4%BA%8C");
			break;
	}
	
	return strResult;
}

// 
//get the chinese day expression
//
function getChineseDay(objDate)
{
	if (objDate == null)
		return "";

	var intDay = objDate.getDate();
	var strResult = "";
	switch (intDay)
	{
		case 1:
			strResult += decodeURI("%E4%B8%80");
			break;
		case 2:
			strResult += decodeURI("%E4%BA%8C");
			break;
		case 3:
			strResult += decodeURI("%E4%B8%89");
			break;
		case 4:
			strResult += decodeURI("%E5%9B%9B");
			break;
		case 5:
			strResult += decodeURI("%E4%BA%94");
			break;
		case 6:
			strResult += decodeURI("%E5%85%AD");
			break;
		case 7:
			strResult += decodeURI("%E4%B8%83");
			break;
		case 8:
			strResult += decodeURI("%E5%85%AB");
			break;
		case 9:
			strResult += decodeURI("%E4%B9%9D");
			break;
		case 10:
			strResult += decodeURI("%E5%8D%81");
			break;
		case 11:
			strResult += decodeURI("%E5%8D%81%E4%B8%80");
			break;
		case 12:
			strResult += decodeURI("%E5%8D%81%E4%BA%8C");
			break;
		case 13:
			strResult += decodeURI("%E5%8D%81%E4%B8%89");
			break;
		case 14:
			strResult += decodeURI("%E5%8D%81%E5%9B%9B");
			break;
		case 15:
			strResult += decodeURI("%E5%8D%81%E4%BA%94");
			break;
		case 16:
			strResult += decodeURI("%E5%8D%81%E5%85%AD");
			break;
		case 17:
			strResult += decodeURI("%E5%8D%81%E4%B8%83");
			break;
		case 18:
			strResult += decodeURI("%E5%8D%81%E5%85%AB");
			break;
		case 19:
			strResult += decodeURI("%E5%8D%81%E4%B9%9D");
			break;
		case 20:
			strResult += decodeURI("%E4%BA%8C%E5%8D%81");
			break;
		case 21:
			strResult += decodeURI("%E4%BA%8C%E5%8D%81%E4%B8%80");
			break;
		case 22:
			strResult += decodeURI("%E4%BA%8C%E5%8D%81%E4%BA%8C");
			break;
		case 23:
			strResult += decodeURI("%E4%BA%8C%E5%8D%81%E4%B8%89");
			break;
		case 24:
			strResult += decodeURI("%E4%BA%8C%E5%8D%81%E5%9B%9B");
			break;
		case 25:
			strResult += decodeURI("%E4%BA%8C%E5%8D%81%E4%BA%94");
			break;
		case 26:
			strResult += decodeURI("%E4%BA%8C%E5%8D%81%E5%85%AD");
			break;
		case 27:
			strResult += decodeURI("%E4%BA%8C%E5%8D%81%E4%B8%83");
			break;
		case 28:
			strResult += decodeURI("%E4%BA%8C%E5%8D%81%E5%85%AB");
			break;
		case 29:
			strResult += decodeURI("%E4%BA%8C%E5%8D%81%E4%B9%9D");
			break;
		case 30:
			strResult += decodeURI("%E4%B8%89%E5%8D%81");
			break;
		default:
			strResult += decodeURI("%E4%B8%89%E5%8D%81%E4%B8%80");
			break;
	}
	
	return strResult;
}

//
//get the chinese date format
//input format: yyyy-MM-dd
//
function getChineseDate(strDate)
{
	//blank
	if (strDate == undefined)
		return "";
	if (strDate == "")
		return "";
		
	//format conversion
	var strNewDate = "";
	strNewDate = doConvertDateFormat(strDate);
	
	//get the date object
	var objDate = new Date(strNewDate);
	if (objDate == null)
		return strDate;
		
	//get the chinese date
	var strCYear = getChineseYear(objDate);
	var strCMonth = getChineseMonth(objDate);
	var strCDay = getChineseDay(objDate);
	
	return strCYear + decodeURI("%E5%B9%B4") + strCMonth + decodeURI("%E6%9C%88") + strCDay + decodeURI("%E6%97%A5");
	
}
