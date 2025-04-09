mergeInto(LibraryManager.library, {
	
	JavascriptSyncFiles : function ()
	{
		FS.syncfs(false, function (err) {});
	},
	
	JavascriptGetHostName : function ()
	{
		var value = window.location.hostname;
		if ( value==null || value=="" )
			value = window.top.location.hostname;
		if ( value==null || value=="" )
			value = document.domain;
		if ( value==null || value=="" )
			value = document.referrer;
		if ( value==null )
			value = "";
		var size = lengthBytesUTF8(value) + 1;
		var buffer = _malloc(size);
		stringToUTF8(value, buffer, size);
		return buffer;
	},

	JavascriptOpenUrl : function (url)
	{
		window.open(Pointer_stringify(url));
	},

});
