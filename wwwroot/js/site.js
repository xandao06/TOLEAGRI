// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.




//Configuração do Modal "SaidaEstoque" para que puxe os dados da peça quando é digitado o código e pressionado "Enter" e se necessário editar as informações dessa peça
 $('#CodigoSistema').on('keypress', function (e) {
    if (e.which === 13) { // Enter key pressed
        e.preventDefault();
        var codigoSistema = $(this).val();

        $.ajax({
            url: '/Estoque/GetByCodigoSistema',
            type: 'GET',
            data: { codigoSistema: codigoSistema },
            success: function (data) {
                if (data) {
                    // Preencher os campos do formulário com os detalhes da peça
                    $('#Locacao').val(data.locacao);
                    $('#Marca').val(data.marca);
                    $('#Modelo').val(data.modelo);
                    $('#Quantidade').val(data.quantidade);
                    $('#NotaOuPedido').val(data.notaoupedido);
                    $('#Observacao').val(data.observacao);
                } else {
                    // Limpar os campos do formulário se o código não existir
                    $('#Locacao').val(data.locacao);
                    $('#Marca').val(data.marca);
                    $('#Modelo').val(data.modelo);
                    $('#Quantidade').val(data.quantidade);
                    $('#NotaOuPedido').val(data.notaoupedido);
                    $('#Observacao').val(data.observacao);
                }
            }
        });
    }
});

//Modal para registrar uma entrada no estoque
function ModalEntradaSaidaEstoque() {
        $.get("/Estoque/ModalEntradaSaidaEstoque", function (data) {
            $("#modalTOLEAGRI").html(data);
            $("#modalEntradaSaidaEstoque").modal("show");
        });
};


$(".search").keypress(function (event) {
    var searchString = $(this).val();
    console.log(searchString);
    console.log("blah");
    if (event.which == 13) {
        var searchUrl = '/Estoque/GetByCodigoSistema'
        searchUrl += "/" + searchString;
        window.location.href = searchUrl;
    } // end if
});

////Modal para registrar uma saída no estoque
//    function ModalSaidaEstoque() {
//        $.get("/Estoque/ModalSaidaEstoque", function (data) {
//            $("#modalTOLEAGRI").html(data);
//            $("#modalSaidaEstoque").modal("show");
//        });
//};

//Modal para deletar uma peça do estoque
function ModalDeletarEstoque(idPeca) {
    $.get("/Estoque/ModalDeletarEstoque?id=" + idPeca, function (data) {
        $("#modalTOLEAGRI").html(data);
        $("#modalDeletarEstoque").modal("show");


    });
};


    //$('#modalTOLEAGRI').on('hidden.bs.modal', function () {
    //    $(this).removeData('bs.modal').find('.modal-content').html('');
    //});



