

//Pesquisa de registros pelo "enter" e pelo icone da lupa
async function searchRegistros() {
    const query = document.getElementById('searchBarRegistro').value;
    const startDate = document.getElementById('start-date').value;
    const endDate = document.getElementById('end-date').value;
    const url = new URL(`/Registro/SearchReg`, window.location.origin);
    if (query) url.searchParams.append('query', query);
    if (startDate) url.searchParams.append('startDate', startDate);
    if (endDate) url.searchParams.append('endDate', endDate);
    const response = await fetch(url);
    const registros = await response.json();
    displayRegistroResults(registros);
};

//"enter"
function EnterRegistro(event) {
    if (event.key === 'Enter') {
        searchRegistros();
    }
};

//"lupa"
function searchIconRegistros () {
    document.getElementById('searchIconRegistros').addEventListener( 'click', function () {
    searchRegistros();
})
};

//Função Pesquisa que mostra os resultados
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
                <td>${new Date(registro.data).toLocaleDateString()}</td>
                <td>${registro.entradaOuSaida}</td>
            `;
        resultsList.appendChild(tr);
        })
    };

 ////////////////////////////


//Modal para deletar um registro    
function ModalDeletarRegistro(idPeca) {
    $.get("/Registro/ModalDeletarRegistro?id=" + idPeca, function (data) {
        $("#modalTOLEAGRI").html(data);
        $("#modalDeletarRegistro").modal("show");
    })
};

///////////////////////////


//Modal para deletar todos os registros
function ModalDeletarAllRegistro() {
    $.get("/Registro/ModalDeletarAllRegistro", function (data) {
        $("#modalTOLEAGRI").html(data);
        $("#modalDeletarAllRegistro").modal("show");
    })
};