

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
function searchIconRegistros() {
    document.getElementById('searchIconRegistros').addEventListener('click', function () {
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
                <td class="${(registro.CodigoSistema == registro.CodigoSistema ? "text-danger" : "")}">
                ${registro.codigoSistema}
                </td >
                <td class="${(registro.Locacao == registro.Locacao ? "text-warning" : "")}">
                ${registro.locacao}
                </td>
                <td>${registro.marca}</td>
                <td>${registro.modelo}</td>
                <td>${registro.quantidade}</td>
                <td>${registro.notaOuPedido}</td>
                <td>${registro.observacao}</td>
                <td>${registro.usuario}</td>
                <td>${new Date(registro.data).toLocaleDateString()}</td>
                <td class="${(registro.entradaOuSaida == "Entrada" ? "text-success" : registro.EntradaOuSaida == "Saída" ? "text-primary" : "")}">
                ${registro.entradaOuSaida}
                
            `;
        resultsList.appendChild(tr);
    })
};

//</td >
//    <td style="width:1px">
//        <a onClick="ModalDeletarRegistro(${registro.id})">
//            <i class="bi bi-trash3-fill"></i>
//        </a>
//    </td>

///////////////////////////

function sortTableRegistros(columnIndex) {
    var table, rows, switching, i, x, y, shouldSwitch, dir, switchcount = 0;
    table = document.querySelector(".table");
    switching = true;
    dir = "asc";

    while (switching) {
        switching = false;
        rows = table.querySelectorAll(".registro-row");

        for (i = 0; i < (rows.length - 1); i++) {
            shouldSwitch = false;
            x = rows[i].querySelectorAll("td")[columnIndex];
            y = rows[i + 1].querySelectorAll("td")[columnIndex];

            if (columnIndex === 8) { // Se a coluna for a de Data
                var dateX = new Date(x.textContent.split('/').reverse().join('-'));
                var dateY = new Date(y.textContent.split('/').reverse().join('-'));
                if (dir == "asc") {
                    if (dateX > dateY) {
                        shouldSwitch = true;
                        break;
                    }
                } else if (dir == "desc") {
                    if (dateX < dateY) {
                        shouldSwitch = true;
                        break;
                    }
                }
            } else {
                if (dir == "asc") {
                    if (x.textContent.toLowerCase() > y.textContent.toLowerCase()) {
                        shouldSwitch = true;
                        break;
                    }
                } else if (dir == "desc") {
                    if (x.textContent.toLowerCase() < y.textContent.toLowerCase()) {
                        shouldSwitch = true;
                        break;
                    }
                }
            }
        }

        if (shouldSwitch) {
            rows[i].parentNode.insertBefore(rows[i + 1], rows[i]);
            switching = true;
            switchcount++;
        } else {
            if (switchcount == 0 && dir == "asc") {
                dir = "desc";
                switching = true;
            }
        }
    }
}