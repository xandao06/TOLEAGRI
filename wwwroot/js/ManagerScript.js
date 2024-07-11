


/////////////// REGISTRO //////////////////////


//Modal para deletar um registro    
function ModalDeletarRegistro(idRegistro) {
    $.get("/Registro/ModalDeletarRegistro?id=" + idRegistro, function (data) {
        $("#modalTOLEAGRI").html(data);
        $("#modalDeletarRegistro").modal("show");
    })
};

////


//Modal para deletar todos os registros
//function ModalDeletarAllRegistro() {
    //$.get("/Registro/ModalDeletarAllRegistro", function (data) {
        //$("#modalTOLEAGRI").html(data);
        //$("#modalDeletarAllRegistro").modal("show");
    //})
//};



/////////////////////// ESTOQUE ///////////////////////////



//Modal para deletar uma peça do estoque
function ModalDeletarPeca(idPeca) {
    $.get("/Estoque/ModalDeletarPeca?id=" + idPeca, function (data) {
        $("#modalTOLEAGRI").html(data);
        $("#modalDeletarPeca").modal("show");
    });
};

////


//Modal para deletar todo o estoque
    //function ModalDeletarAllPeca() {
        //$.get("/Estoque/ModalDeletarAllPeca", function (data) {
            //$("#modalTOLEAGRI").html(data);
            //$("#modalDeletarAllPeca").modal("show");
        //})
    //};


 ////




