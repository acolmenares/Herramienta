(function (document) {
	'use strict';

	if (!navigator.getUserMedia) {
		navigator.getUserMedia = navigator.webkitGetUserMedia || navigator.mozGetUserMedia ||
			navigator.msGetUserMedia;
	}

	if (!window.URL) {
		window.URL =  window.webkitURL || window.msURL || window.oURL;
	}

	Date.prototype.toDateInputValue = function () {
		var local = new Date(this);
		local.setMinutes(this.getMinutes() - this.getTimezoneOffset());
		return local.toJSON().slice(0, 10);
	};

	Date.prototype.toTimeInputValue = function () {
		var local = new Date(this);
		return local.toTimeString().slice(0, 8);
	};

	Date.prototype.toMsFormat = function () {
		var local = new Date(this);
		return '\/Date('+ local.getTime()+')\/';
	};

	Date.prototype.toFilename = function () {
		return this.toDateInputValue().replace(/-/g, '') +
			'_' + this.toTimeInputValue().replace(/:/g, '');
	};

	if (typeof String.prototype.startsWith !== 'function') {
		String.prototype.startsWith = function (str, nocase){
			return (!nocase? this.slice(0, str.length) === str:
					this.slice(0, str.length).toLowerCase() === str.toLowerCase());
		};
	}
	if (typeof String.prototype.endsWith !== 'function') {
		String.prototype.endsWith = function (str, nocase){
			return (!nocase? this.slice(-str.length) === str:
					this.slice(-str.length).toLowerCase()=== str.toLowerCase() );
		};
	}
	String.prototype.contains = function(text, nocase) {
		return ( !nocase? this.indexOf(text) !== -1 : this.toLowerCase().indexOf(text.toLowerCase()) !== -1 );
	};

	Object.clone = function (src) {
		var newObj = (src instanceof Array) ? [] : {};
		if ((src === null || src instanceof Date || !(src instanceof Array ||  typeof (src) === 'object'))) {
			return src;
		}
		for (var i in src) {
			if (src[i] && typeof src[i] === 'object') {
				newObj[i] = Object.clone(src[i]);
			}
			else
			{
				newObj[i] = src[i];
			}
		}
		return newObj;
	};

	Object.compare = function (o1, o2) {

		if(!o1){
			return !o2;
		}

		if (o1===null) {
			return o2===null?true:false;
		}

		if ( o1 instanceof Date ) {
			return (o2 instanceof Date)? o1.toString()===o2.toString():false;
		}

		if (!(o1 instanceof Array || typeof (o1) === 'object')) {
			return  (!(o1 instanceof Array || typeof (o1) === 'object'))? o1===o2:false;
		}

		if (Object.getOwnPropertyNames(o1).length!==Object.getOwnPropertyNames(o2||{}).length) {
			return false;
		}
		for( var i in  o1){
			if ( typeof o1[i] === 'object') {
				if (!Object.compare(o1[i], o2[i])) {
					return false;
				}
			}
			else{
				if(o1[i]!== o2[i]) {
					return false;
				}
			}
		}
		return true;
	};

	Object.isEmptyOrNull = function (o) {
		return  typeof o === "undefined" || o===null || o==='' || ( JSON.stringify(o)===JSON.stringify({}));
	}

	window.aicl={};

	/**
	* @return {Date}
	* @param {String "/Date(1480514533028)/"}
	*/
	window.aicl.convertToJsDate= function (v){
		if (!v) {
			return null;
		}
		if (typeof v !== 'string'){
			return v;
		}
		try{
		  return new Date(parseFloat(/Date\(([^)]+)\)/.exec(v)[1])); // thanks demis bellot!
	  }
		catch(err){
			return window.aicl.parseDate(v);
		}

	};

	/**
	* @return {String yyyy-mm-dd }
	* @param {String "/Date(1480514533028)/"}
	*/
	window.aicl.formatDate= function(value) {
		if(!value) {
			return '';
		}
		var v = window.aicl.convertToJsDate(value);
		return v? v.toDateInputValue():'';
	};

	/**
	* @return {Date}
	* @param {String yyyy-mm-dd}
	*/
	window.aicl.parseDate= function(text) {
        var parts = text.split('-');
		if (parts.length === 3) {
			var date = new Date(0, 0);
			date.setFullYear(parseInt(parts[0]));
			date.setMonth(parseInt(parts[1]) - 1);
			date.setDate(parseInt(parts[2]));
			return date;
		}
	}


	/**
	* @return {Object}
	* @param {IronRequestElement}
	*/
	window.aicl.xhrToResponse=function(ironRequest){

		var xhr= ironRequest.xhr;
		var rs = {
			Status:xhr.status,
			StatusText:xhr.statusText||"Error !",
			ResponseType:xhr.responseType || xhr._responseType,
			ResponseUrl: xhr.responseURL || ironRequest.url,
			Succeeded: ironRequest.succeeded
		};

		var responseTmp= xhr.response;
		if( xhr._responseType==='json' && typeof(xhr.response)==='string'){
			try{
				responseTmp=JSON.parse(xhr.response);
			}
			catch(ex){
			}
		}
		rs.Response = (responseTmp && !responseTmp.ResponseStatus)? responseTmp:{};
/*
		if (typeof(rs.Response)==='string'){
			try {
				rs.Response= JSON.parse(rs.Response);
			}
			catch(ex){
			}
		}
*/
		rs.ResponseStatus=  (responseTmp && responseTmp.ResponseStatus)? responseTmp.ResponseStatus:{};
		var apn = Object.getOwnPropertyNames(rs.ResponseStatus);
		for(var pn in apn ){
			if(apn[pn]==="Message"){
				var msg = rs.ResponseStatus.Message ||'';
				rs.ResponseStatus.Message= msg.substring(0,msg.indexOf(': '));
			}
			if(apn[pn]==="StackTrace"){
				var st = rs.ResponseStatus.StackTrace ||'';
				rs.ResponseStatus.StackTrace= st.substring(0,st.indexOf(':\n'));
			}
     }
		return rs;
	}

	/**
	* @return {Object}
	* @param {XMLHttpRequest}
	*/
	window.aicl.parseResponseStatus=function(xhr){
		var a=[];
		var rs= window.aicl.xhrToResponseStatus(xhr);
		return a;
	}

	window.aicl.validatePasswordStrength=function(inputtxt){
		var lowletters = /[a-z]/;
		var upletters = /[A-Z]/;
		var numbers = /[0-9]/;
		return (inputtxt.match(lowletters) && inputtxt.match(upletters) && inputtxt.match(numbers) )
  }

	window.aicl.isDeviceNarrow= function() {
		return (window.innerWidth <= 800 && window.innerHeight <= 600) ;
	}

// wrap document so it plays nice with other libraries
// http://www.polymer-project.org/platform/shadow-dom.html#wrappers
})(document);
/*

ResponseStatus:Object
ErrorCode:"ValidationException"
Errors:Array[2] Object ErrorCode:"" FieldName:"NuevaContrasena"Message:"Debe Indicar la nueva contraseña"
Message:"Validation failed: ↵ -- Debe Indicar la nueva contraseña↵ -- 12 <= Longitud Contraseña <= 32 "
StackTrace:"[ActualizarContrasena: 20/04/2016 02:20:19 p.m.]:↵[REQUEST: {Usuario:Angel,ContrasenaActual:coasdfsa}]↵ServiceStack.FluentValidation.ValidationException: Validation failed:
↵ -- Debe Indicar la nueva contraseña
↵ -- 12 <= Longitud Contraseña <= 32
↵   en ServiceStack.FluentValidation.DefaultValidatorExtensions.ValidateAndThrow[T](IValidator`1 validator, T instance)
↵   en GestionClaves.BL.Gestores.GestorUsuarios.ActualizarContrasena(ActualizarContrasena request)
↵   en lambda_method(Closure , Object , Object )
↵   en ServiceStack.Host.ServiceRunner`1.Execute(IRequest request, Object instance, TRequest requestDto)"
Status:400
StatusText:
"ValidationException"
*/
