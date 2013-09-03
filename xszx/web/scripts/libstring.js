//
//string is empty,"" or null
//
function isEmptyString(str)
{
	var result = false;
	if(str == "" || str == null)
	{
		result = true;
	}
	return result;
}

//
//trim left space
//
function doTrimLeft(strValue)
{
	if (strValue == undefined)
		return "";
	if (strValue == "")
		return "";

	var strNewValue = strValue;
	var strCH = "";
	var i = 0;
	for (i=0; i<strValue.length; i++)
	{
		strCH = strValue.substr(i,1);
		if (strCH != " ")
		{
			strNewValue = strValue.substr(i);
			break;						
		}
	}

	return strNewValue;
}

//
//trim right space
//
function doTrimRight(strValue)
{
	if (strValue == undefined)
		return "";
	if (strValue == "")
		return "";

	var strNewValue = strValue;
	var strCH = "";
	var i = 0;
	for (i=strValue.length-1; i>=0; i--)
	{
		strCH = strValue.substr(i,1);
		if (strCH != " ")
		{
			strNewValue = strValue.substr(0, i+1);
			break;						
		}
	}
	
	return strNewValue;
}

//
//trim left and right space
//
function doTrim(strValue)
{
	if (strValue == undefined)
		return "";
	if (strValue == "")
		return "";

	var strNewValue = strValue;
	strNewValue = doTrimLeft(strNewValue);
	strNewValue = doTrimRight(strNewValue);
	
	return strNewValue;
}

//
//replace strFrom to strTo in strValue
//
function doReplace(strValue,strFrom,strTo)
{
	if (strValue == undefined)
		return "";
	if (strValue == "")
		return "";

	while (strValue.indexOf(strFrom, 0) >= 0)
		strValue = strValue.replace(strFrom, strTo);

	return strValue;
}
