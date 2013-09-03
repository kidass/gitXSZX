//translate keys: 
//  return -> tab when control is enalbed, 
//  backspace -> end when control is disabled or readonly
function TranslateKeys()
{
	try {
		var vReadOnly;
		var vDisabled;
		if (!event) 
			return;
		if (event.altKey   == true)
			return;
		if (event.ctrlKey  == true)
			return;
		if (event.shiftKey == true)
			return;
		try {
			vDisabled = event.srcElement.disabled;
			vReadOnly = event.srcElement.readOnly;
		}
		catch (e) {
			vReadOnly = true;
			vDisabled = true;
		}
		if ((event.keyCode == 13) && (vDisabled == false))
		{
			event.keyCode = 9; //return -> tab
			event.cancelBubble = true; 
			event.returnValue = true;
			return;
		}   
		if ((event.keyCode == 8) && ((vDisabled == true) || (vReadOnly == true)))
		{
			event.keyCode = 0; //backspace -> end
			event.cancelBubble = true; 
			event.returnValue = true;
			return;
		}   
	} catch (e) {}
    return;
}

//get the frame object by frame name
function getFrame(objFrames,strFrameName) 
{
	try 
	{
		var i = 0;
		for (i=0; i<objFrames.length; i++) 
			if (objFrames(i).name.toUpperCase() == strFrameName.toUpperCase())
				return objFrames(i);
	}
	catch (e)
	{
		return null;
	}
	return null;
}

//Safe get element value : string
function getElementValueSafe_String(strElemId)
{
	var objControl = document.getElementById(strElemId);
	var strValue = "";
	if (objControl)
		strValue = objControl.value;
	return strValue;
}

//Safe get element value : boolean
//value = "1": true; else false
function getElementValueSafe_Bool(strElemId)
{
	var objControl = document.getElementById(strElemId);
	var strValue = "";
	if (objControl)
		strValue = objControl.value;
	if (strValue == "1")
		return true;
	else
		return false;
}