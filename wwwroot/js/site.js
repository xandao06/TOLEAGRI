
//Configuração do Modal "SaidaEstoque" para que puxe os dados da peça quando é digitado o código e pressionado "Enter" e se necessário editar as informações dessa peça
 $('#CodigoSistema').on('keypress', function (e) {
    if (e.which === 13) { // Enter key pressed
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
        })
    }
 });

 //Pesquisa de pecas pela tecla "enter" e pelo icone da lupa
 async function searchPecas() {
    const query = document.getElementById('search-input-peca').value;
    const startDate = document.getElementById('start-date').value;
    const endDate = document.getElementById('end-date').value;
    const url = new URL(`/Estoque/Search`, window.location.origin);
    if (query) url.searchParams.append('query', query);
    if (startDate) url.searchParams.append('startDate', startDate);
    if (endDate) url.searchParams.append('endDate', endDate);
    const response = await fetch(url);
    const pecas = await response.json();
    displayPecaResults(pecas);
};

 //"Enter"
function handleKeyDownPeca(event) {
    if (event.key === 'Enter') {
        searchPecas();
    }
};

//"lupa"
function searchIconPecas () {
    document.getElementById('search-input-icon-pecas').addEventListener('click', function () {
    searchPecas();
})
};

//Função pesquisa
function displayPecaResults(pecas) {
    const resultslist = document.getElementById('pecas-list');
    resultslist.innerhtml = '';
    pecas.forEach(peca => {
        const tr = document.createElement('tr');
        tr.classname = 'peca-row';
        tr.innerhtml = `
                <td>${peca.codigoSistema}</td>
                <td>${peca.locacao}</td>
                <td>${peca.marca}</td>
                <td>${peca.modelo}</td>
                <td>${peca.quantidade}</td>
                <td>${peca.notaOuPedido}</td>
                <td>${peca.observacao}</td>
                <td>${peca.usuario}</td>
                <td>${new Date(peca.data).toLocaleDateString()}</td>  \\ajeitar isso aqui
                <td style="width:1px">
                    <a onClick="modalDeletarEstoque(${peca.id})">
                        <i class="bi bi-trash3-fill"></i>
                    </a>
                </td>
            `;
        resultslist.appendChild(tr);
        })
    };

//Pesquisa de registros pelo "enter" e pelo icone da lupa
async function searchRegistros() {
    const query = document.getElementById('search-input-registro').value;
    const startDate = document.getElementById('start-date').value;
    const endDate = document.getElementById('end-date').value;
    const url = new URL(`/Registro/Search`, window.location.origin);
    if (query) url.searchParams.append('query', query);
    if (startDate) url.searchParams.append('startDate', startDate);
    if (endDate) url.searchParams.append('endDate', endDate);
    const response = await fetch(url);
    const registros = await response.json();
    displayRegistroResults(registros);
};

//"enter"
function handleKeyDownRegistro(event) {
    if (event.key === 'Enter') {
        searchRegistros();
    }
};

//"lupa"
function searchIconRegistros () {
    document.getElementById('search-input-icon-registros').addEventListener( 'click', function () {
    searchRegistros();
})
};

//Função Pesquisa
function displayRegistroResults(registros) {
    const resultsList = document.getElementById('registros-list');
    resultsList.innerHTML = '';
    registros.forEach(registro => {
        const tr = document.createElement('tr');
        tr.className = 'registro-row';
        tr.innerHTML = `
                <td>${registro.codigoSistema}</td>
                <td>${registro.locacao}</td>
                <td>${registro.marca}</td>
                <td>${registro.modelo}</td>
                <td>${registro.quantidade}</td>
                <td>${registro.notaOuPedido}</td>
                <td>${registro.observacao}</td>
                <td>${registro.usuario}</td>
                <td>${new Date(registro.data).toLocaleDateString()}</td>  \\ajeitar isso aqui
                <td>${registro.entradaOuSaida}</td>
                <td style="width:1px">
                    <a onClick="ModalDeletarRegistro(${registro.id})">
                        <i class="bi bi-trash3-fill"></i>
                    </a>
                    </td>
            `;
        resultsList.appendChild(tr);
        })
    };

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
    })
};

//Modal para deletar todo o estoque
    function ModalDeletarAllEstoque() {
        $.get("/Estoque/ModalDeletarAllEstoque", function (data) {
            $("#modalTOLEAGRI").html(data);
            $("#modalDeletarAllEstoque").modal("show");
        })
    };

//Modal para deletar um registro    
function ModalDeletarRegistro(idPeca) {
    $.get("/Registro/ModalDeletarRegistro?id=" + idPeca, function (data) {
        $("#modalTOLEAGRI").html(data);
        $("#modalDeletarRegistro").modal("show");
    })
};

//Modal para deletar todos os registros
function ModalDeletarAllRegistro() {
    $.get("/Registro/ModalDeletarAllRegistro", function (data) {
        $("#modalTOLEAGRI").html(data);
        $("#modalDeletarAllRegistro").modal("show");
    })
};