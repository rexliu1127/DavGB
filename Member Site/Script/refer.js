function CallMiniGame(status){

    if (SiteMode == "1")
        return;
        
      var sbhost=document.location.host;
      var intstartpos=sbhost.indexOf(".",0);
      var headname=sbhost.substr(0,intstartpos);
      var minidomain;

      if (MiniGameServerMode)
      {
          minidomain=sbhost.replace(headname,"ca88"); 	      
	  }
	  else
	  {
	  	  minidomain=sbhost.replace(headname,"casinoqa");
	      
	      intstartpos=minidomain.indexOf(":",0);
	      if (intstartpos>=0)
	      {
	         headname=minidomain.substr(intstartpos+1,minidomain.length-intstartpos);
	         minidomain=minidomain.replace(headname,"8889");
	      }
	     else
	     {
	        minidomain = minidomain + ":8889";
	     } 
	  }	   

      var head= document.getElementsByTagName('head')[0];
      var script= document.createElement('script');
      script.type= 'text/javascript';
      script.src= '';
      head.appendChild(script);
}


function OpenMiniGame() { 
  
} 

function CloseMiniGame() { 

} 