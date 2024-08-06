

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
                } else {
                    // Limpar os campos do formulário se o código não existir
                    $('#Locacao').val(data.locacao);
                    $('#Marca').val(data.marca);
                    $('#Modelo').val(data.modelo);
                }
            }
        })
    }
 });

 //////////////////////////


//Pesquisa de pecas pela tecla "enter" e pelo icone da lupa
 async function searchPecas() {
    const query = document.getElementById('searchBarPeca').value;
    const startDate = document.getElementById('start-date').value;
    const endDate = document.getElementById('end-date').value;
    const url = new URL(`/Estoque/SearchPec`, window.location.origin);
    if (query) url.searchParams.append('query', query);
    if (startDate) url.searchParams.append('startDate', startDate);
    if (endDate) url.searchParams.append('endDate', endDate);
    const response = await fetch(url);
    const pecas = await response.json();
    displayPecaResults(pecas);
};

//"Enter"
function EnterPeca(event) {
    if (event.key === 'Enter') {
        searchPecas();
    }
};

//"lupa"
function searchIconPecas () {
    document.getElementById('searchIconPecas').addEventListener('click', function () {
    searchPecas();
})
};

//Função pesquisa que mostra os resultados
function displayPecaResults(pecas) {
    const resultsList = document.getElementById('pecas-list');
    resultsList.innerHTML = '';
    pecas.forEach(peca => {
        const tr = document.createElement('tr');
        tr.className = 'peca-row';
        tr.innerHTML = `
                <td class="${(peca.CodigoSistema == peca.CodigoSistema ? "text-danger" : "")}">
                ${peca.codigoSistema}
                </td >
                <td class="${(peca.Locacao == peca.Locacao ? "text-warning" : "")}">
                ${peca.locacao}
                </td>
                <td>${peca.marca}</td>
                <td>${peca.modelo}</td>
                <td>${peca.quantidade}</td>
                <td>${new Date(peca.data).toLocaleDateString()}</td>
            
            `;
        resultsList.appendChild(tr);
        })
    };

 ////////////////////////////

//<td ${if (isAdmin)} >
//style="width:1px">
//    <a onClick="ModalDeletarPeca(${peca.id})">
//        <i class="bi bi-trash3-fill"></i>
//    </a>
//</td>

//Modal para criar uma entrada no estoque
function ModalEntradaEstoque() {
        $.get("/Estoque/ModalEntradaEstoque", function (data) {
            $("#modalTOLEAGRI").html(data);
            $("#modalEntradaEstoque").modal("show");
            $('#CodigoSistema').trigger('focus');
        });
};


/////////////////////////

//Modal para criar uma saida no estoque
function ModalSaidaEstoque() {
        $.get("/Estoque/ModalSaidaEstoque", function (data) {
            $("#modalTOLEAGRI").html(data);
            $("#modalSaidaEstoque").modal("show");
            $('#CodigoSistema').trigger('focus');
        });
};


//////////////////////////////


// garante filtragem por titulo da tabela e também que a pagina sempre inicie organizada por data de forma decrescente
function sortTablePecas(columnIndex) {
    var table, rows, switching, i, x, y, shouldSwitch, dir, switchcount = 0;
    table = document.querySelector(".table");
    switching = true;
    dir = "asc";
    
    while (switching) {
        switching = false;
        rows = table.querySelectorAll(".peca-row");

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

//GERA RELATÓRIO EM PDF
function generateReportPDF(filterType) {
    const query = document.getElementById('searchBarPeca').value;
    const startDate = document.getElementById('start-date').value;
    const endDate = document.getElementById('end-date').value;
    const url = new URL(`/Estoque/GenerateReportPDF`, window.location.origin);

    // Adicionar parâmetros apenas se estiverem preenchidos
    if (query.trim() !== '') url.searchParams.append('query', query.trim());
    if (startDate.trim() !== '') url.searchParams.append('startDate', startDate.trim());
    if (endDate.trim() !== '') url.searchParams.append('endDate', endDate.trim());
    url.searchParams.append('filterType', filterType);

    window.location.href = url.toString();
}

//GERA RELATÓRIO EM EXCEL
function generateReportExcel(filterType) {
    const query = document.getElementById('searchBarPeca').value;
    const startDate = document.getElementById('start-date').value;
    const endDate = document.getElementById('end-date').value;
    const url = new URL(`/Estoque/GenerateReportExcel`, window.location.origin);

    if (query.trim() !== '') url.searchParams.append('query', query.trim());
    if (startDate.trim() !== '') url.searchParams.append('startDate', startDate.trim());
    if (endDate.trim() !== '') url.searchParams.append('endDate', endDate.trim());
    url.searchParams.append('filterType', filterType);

    window.location.href = url.toString();
}
    // ABERTURA DO BOTÃO DE RELATÓRIOS
$(document).ready(function () {
    $('.dropdown-submenu a.dropdown-toggle').on("click", function (e) {
        var $submenu = $(this).next('.dropdown-menu');
        var $parent = $(this).parents('.dropdown-menu').first();

        $parent.find('.dropdown-menu').not($submenu).removeClass('show');
        $submenu.toggleClass('show');

        e.stopPropagation();
        e.preventDefault();
    });
});
  