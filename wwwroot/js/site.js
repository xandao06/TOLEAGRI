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
                } else {
                    // Limpar os campos do formulário se o código não existir
                    $('#Locacao').val(data.locacao);
                    $('#Marca').val(data.marca);
                    $('#Modelo').val(data.modelo);
                    $('#Quantidade').val(data.quantidade);
                    $('#NotaOuPedido').val(data.notaoupedido);
                }
            }
        });
    }
 });

 // Busca o método de filtragem de pecas e retorna para a View Index
//function handleKeyDown(event) {
//    if (event.key === 'Enter') {
//        searchPecas();
//    }
//}

//async function searchPecas() {
//    const query = document.getElementById('search-input-pecas').value;
//    const response = await fetch(`/search?query=${query}`);
//    const pecas = await response.json();
//    displayResults(pecas);
//}

//function displayResults(pecas) {
//    const resultsList = document.getElementById('pecas-list');
//    resultsList.innerHTML = '';
//    pecas.forEach(peca => {
//        const tr = document.createElement('tr');
//        tr.className = 'peca-row';
//        tr.innerHTML = `
//                <td>${peca.codigoSistema}</td>
//                <td>${peca.locacao}</td>
//                <td>${peca.marca}</td>
//                <td>${peca.modelo}</td>
//                <td>${peca.quantidade}</td>
//                <td>${peca.notaOuPedido}</td>
//                <td>${peca.observacao}</td>
//                <td>${peca.usuario}</td>
//                <td>${peca.entrada}</td>
//                <td>${peca.saida}</td>
//                <td>${new Date(peca.data).toLocaleDateString()}</td>
//                <td style="width:1px">
//                    <a onClick="ModalDeletarEstoque(${peca.id})">
//                        <i class="bi bi-trash3-fill"></i>
//                    </a>
//                </td>
//            `;
//        resultsList.appendChild(tr);
//    });
//}

//// Adiciona a função de busca de peças ao clicar em cima do icone de "lupa"
//document.getElementById('search-input-icon-pecas').addEventListener('click', function () {
//    searchPecas();
//});

//// Busca o método de filtragem de registros e retorna para a View Registro
//function handleKeyDownRegistro(event) {
//    if (event.key === 'Enter') {
//        searchRegistros();
//    }
//}

//async function searchRegistros() {
//    const query = document.getElementById('search-input-registro').value;
//    const response = await fetch(`/search?query=${query}`);
//    const registros = await response.json();
//    displayRegistroResults(registros);
//}

//function displayRegistroResults(registros) {
//    const resultsList = document.getElementById('registros-list');
//    resultsList.innerHTML = '';
//    registros.forEach(registro => {
//        const tr = document.createElement('tr');
//        tr.className = 'registro-row';
//        tr.innerHTML = `
//                <td>${registro.codigoSistema}</td>
//                <td>${registro.locacao}</td>
//                <td>${registro.marca}</td>
//                <td>${registro.modelo}</td>
//                <td>${registro.quantidade}</td>
//                <td>${registro.notaOuPedido}</td>
//                <td>${registro.observacao}</td>
//                <td>${new Date(registro.data).toLocaleDateString()}</td>
//                <td>${registro.usuario}</td>
//                <td>${registro.entrada}</td>
//                <td>${registro.saida}</td>
//                <td>${registro.acao}</td>
//            `;
//        resultsList.appendChild(tr);
//    });
//}

//// Adiciona a função de busca de registros ao clicar em cima do icone de "lupa"
//document.getElementById('search-input-icon-registros').addEventListener('click', function () {
//    searchRegistros();
//});

//Modal para criar uma entrada/saida no estoque
function ModalEntradaSaidaEstoque() {
        $.get("/Estoque/ModalEntradaSaidaEstoque", function (data) {
            $("#modalTOLEAGRI").html(data);
            $("#modalEntradaSaidaEstoque").modal("show");
        });
};

//Modal para deletar uma peça do estoque
function ModalDeletarEstoque(idPeca) {
    $.get("/Estoque/ModalDeletarEstoque?id=" + idPeca, function (data) {
        $("#modalTOLEAGRI").html(data);
        $("#modalDeletarEstoque").modal("show");


    });
};

document.getElementById('option1').addEventListener('change', function () {
    if (this.checked) {
        console.log(this.value + " is checked");
    } else {
        console.log(this.value + " is unchecked");
    }
});

document.getElementById('option2').addEventListener('change', function () {
    if (this.checked) {
        console.log(this.value + " is checked");
    } else {
        console.log(this.value + " is unchecked");
    }
});

document.addEventListener('keydown', function (event) {
    if (event.key === 'Enter') {
        event.preventDefault(); // Prevent the default form submit
        const entradaChecked = document.getElementById('option1').checked;
        const saidaChecked = document.getElementById('option2').checked;
        console.log("Entrada is " + (entradaChecked ? "checked" : "unchecked"));
        console.log("Saida is " + (saidaChecked ? "checked" : "unchecked"));

        // You can send these values via AJAX or handle them as needed
    }
});

//function handleFormSubmit(event) {
//    event.preventDefault();

//    // Get form data
//    const data = {
//        codigoSistema: document.getElementById('CodigoSistema').value,
//        locacao: document.getElementById('Locacao').value,
//        marca: document.getElementById('Marca').value,
//        modelo: document.getElementById('Modelo').value,
//        quantidade: document.getElementById('Quantidade').value,
//        notaOuPedido: document.getElementById('NotaOuPedido').value,
//        observacao: document.getElementById('Observacao').value,
//        usuario: document.getElementById('Usuario').value,
//        data: document.getElementById('Data').value,
//        entrada: document.querySelector('input[name="options-base"]:checked').value === 'Entrada' ? 'Entrada' : 'Saída'
//    };

//    // Add new row to the table
//    const registrosList = document.getElementById('registros-list');
//    const newRow = document.createElement('tr');
//    newRow.className = 'registro-row';
//    newRow.innerHTML = `
//        <td>${data.codigoSistema}</td>
//        <td>${data.locacao}</td>
//        <td>${data.marca}</td>
//        <td>${data.modelo}</td>
//        <td>${data.quantidade}</td>
//        <td>${data.notaOuPedido}</td>
//        <td>${data.observacao}</td>
//        <td>${new Date(data.data).toLocaleDateString()}</td>
//        <td>${data.usuario}</td>
//        <td>${data.entrada}</td>
//    `;
//    registrosList.appendChild(newRow);

//    // Close the modal
//    const modal = bootstrap.Modal.getInstance(document.getElementById('modalEntradaSaidaEstoque'));
//    modal.hide();

//    // Reset the form
//    document.getElementById('formEntradaEstoque').reset();
//}