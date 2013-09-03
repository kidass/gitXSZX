//
//set default cookie
//
function setDefaultCookie(cookieName,cookieValue)
{
	setCookie(cookieName, cookieValue, "", "", "/");
}

//
//set cookie
//
function setCookie(cookieName,cookieValue,expiresDate,domainName,path)
{
	var result = "";
	
	if (cookieName == "" || cookieName == null)
		return ;
	result = cookieName + "=" + escape(cookieValue) + ";";

	if (expiresDate == "" || expiresDate == null)
	{
		var tmpDate = new Date(2100,1,1);
		expiresDate = tmpDate.toGMTString();
	}
	result += "expires=" + expiresDate + ";";
	
	if(! ((domainName == "" || domainName == null)))
	{
		result += "domain=" + domainName + ";";
	}
	
	if(! ((path == "" || path == null)))
	{
		result += "path=" + escape(path) + ";";
	}

	document.cookie = result;
}

//
//get cookie
//
function getCookie(sName)
{
	var aCookie = document.cookie.split("; ");
	var result = "";

	for (var i=0; i < aCookie.length; i++)
	{
		var aCrumb = aCookie[i].split("=");
		if (sName == aCrumb[0]) 
		{
			result = unescape(aCrumb[1]);
			break;
		}
	}
	return result;
}
