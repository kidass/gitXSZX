//
//get variable name by field value
//
function getDocVarNameFromFieldValue(strFieldValue)
{
	if (strFieldValue == undefined)
		return "";
	if (strFieldValue == "")
		return "";
		
	var strName = "";
	var intPos = 0;
	intPos = strFieldValue.indexOf("DOCVARIABLE ",0);
	strName = strFieldValue.substr(intPos + 12);
	strName = doTrim(strName);

	return strName;
}

//
//get the field index by variable name
//
function getDocFieldByVarName(strVarName,objWord)
{
	if (strVarName == undefined)
		return -1;
	if (strVarName == "")
		return -1;
	if (objWord == null)
		return -1;
	if (objWord == undefined)
		return -1;
	
	var objVariable = objWord.Variables(strVarName);
	if (objVariable == null) 
		return -1;
		
	var strNewVarName = "";
	var intCount = 0;
	var i = 0;
	intCount = objWord.Fields.Count;
	for (i=1; i<=intCount; i++)
	{
		if (objWord.Fields(i).Type == 64) //wdFieldDocVariable
		{
			strNewVarName = getDocVarNameFromFieldValue(objWord.Fields(i).Code.Text);
			if (strVarName.toUpperCase() == strNewVarName.toUpperCase())
			{
				return i;
			}
		}
	}
	
	return -1;
}
