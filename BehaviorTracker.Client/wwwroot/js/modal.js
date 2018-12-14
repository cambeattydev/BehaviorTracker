window.modal = {
    init : function (elementId){
      $("#"+elementId).modal({
          keyboard: false
      })  
    },    
    show : function (elementId) {
        $("#"+elementId).modal('show');
    },
    hide : function (elementId){
        $("#"+elementId).modal('hide');
    }
};