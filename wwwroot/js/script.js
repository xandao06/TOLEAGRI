// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.




//Configuração do Modal "SaidaEstoque" para que puxe os dados da peça quando é digitado o código e pressionado "Enter"
// $('#CodigoSistema').on('keypress', function (e) {
//    if (e.which === 13) { // Enter key pressed
//        e.preventDefault();
//        var codigoSistema = $(this).val();

//        $.ajax({
//            url: '/Estoque/GetByCodigoSistema',
//            type: 'GET',
//            data: { codigoSistema: codigoSistema },
//            success: function (data) {
//                if (data) {
//                    // Preencher os campos do formulário com os detalhes da peça
//                    $('#Locacao').val(data.locacao);
//                    $('#Marca').val(data.marca);
//                    $('#Modelo').val(data.modelo);
//                    $('#Quantidade').val(data.quantidade);
//                    $('#NotaOuPedido').val(data.notaoupedido);
//                    $('#Observacao').val(data.observacao);
//                } else {
//                    // Limpar os campos do formulário se o código não existir
//                    $('#Locacao').val(data.locacao);
//                    $('#Marca').val(data.marca);
//                    $('#Modelo').val(data.modelo);
//                    $('#Quantidade').val(data.quantidade);
//                    $('#NotaOuPedido').val(data.notaoupedido);
//                    $('#Observacao').val(data.observacao);
//                }
//            }
//        });
//    }
//});



//$(document).ready(function ModalEntradaEstoque() {
//    $("#btn-EntradaEstoque").click(function (event) {
//        event.preventDefault();

//        $.get("/Estoque/ModalEntradaEstoque", function (data) {
//            $("#modalTOLEAGRI").html(data);
//            $("#modalEntradaEstoque").modal("show");
//        });
//    });
//});

//$(document).ready(function ModalSaidaEstoque() {
//    $('#btn-SaidaEstoque').click(function (event) {
//        event.preventDefault();
//        $.get("/Estoque/ModalSaidaEstoque", function (data) {
//            $("#modalTOLEAGRI").html(data);
//            $("#modalSaidaEstoque").modal("show");
//        });
//    });
//});

//function ModalDeletarEstoque(idPeca) {
//    $.get("/Estoque/ModalDeletarEstoque?id=" + idPeca, function (data) {
//        $("#modalTOLEAGRI").html(data);
//        $("#modalDeletarEstoque").modal("show");


//    });
//    };
//    $('#modalTOLEAGRI').on('hidden.bs.modal', function () {
//        $(this).removeData('bs.modal').find('.modal-content').html('');
//    });




