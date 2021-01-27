var count = 0;

function Verifica1(form){
	var doc = document.forms[form];
	count = 0;
	
	ValidMail(doc['mail']);
	// http://www.w3resource.com/javascript/form/email-validation.php
	// lez 9
	if(count==0){
		return true;
	}else{
		return false;
	}	
}


function Verifica2(form){
	var doc = document.forms[form];
	count = 0;
	
	Validazione(doc['name']);
	ValidMail(doc['mail']);
	Validazione(doc['messaggio']);
	if(count==0){
		return true;
	}else{
		return false;
	}	
}

function Verifica3(form){
	var doc = document.forms[form];
	count = 0;
	
	Validazione(doc['nome']);
	Validazione(doc['cognome']);
	ValidMail(doc['email']);
	Validazione(doc['messaggio']);
	ValidRadio(doc['privacy']);
	if(count==0){
		return true;
	}else{
		return false;
	}	
}

function ValidMail(mail){
	    var re = /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
		if(re.test(mail.value)){
			mail.style.borderColor = "";
			return true;
		}else{
			mail.style.borderColor = "red";
			count++;
			return false;
		}
}

function Validazione(obj){
	if(obj.value=='' || obj.value==' name' ){
		//alert("rosso!!");
		obj.style.borderColor = "red";
		count++;
		//console.log(count);
	}else{
		obj.style.borderColor = "";
	}
}

function ValidRadio(obj){
	var label = document.getElementById('priv');
	for (var i=0; i < obj.length; i++)
	{
		if(!obj[i].checked && obj[i].value=='1')
		{
			label.style.borderColor = 'red';
			count++;
		}else{
			label.style.borderColor = "";
		}
	}

/*
	if(!obj.checked && obj.value=='1'){
		obj.style.color = 'red';
		count++;
	}else{
		obj.style.color = "";
	}
	*/
}