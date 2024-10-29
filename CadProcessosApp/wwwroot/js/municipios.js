document.addEventListener("DOMContentLoaded", function () {
    const ufSelect = document.getElementById("uf");
    const municipioSelect = document.getElementById("municipio");

    ufSelect.addEventListener("change", function () {
        const uf = ufSelect.value;
        
        if (uf) {
            fetch(`https://servicodados.ibge.gov.br/api/v1/localidades/estados/${uf}/municipios`)
                .then(response => response.json())
                .then(data => {
                    // Limpa o seletor de municípios
                    municipioSelect.innerHTML = '<option value="">Selecione um município</option>';

                    // Adiciona as opções de município ao seletor
                    data.forEach(municipio => {
                        const option = document.createElement("option");
                        option.value = municipio.nome;
                        option.textContent = municipio.nome;
                        municipioSelect.appendChild(option);
                    });
                })
                .catch(error => {
                    console.error("Erro ao buscar municípios:", error);
                });
        } else {
            // Limpa o seletor de municípios caso nenhuma UF esteja selecionada
            municipioSelect.innerHTML = '<option value="">Selecione um município</option>';
        }
    });
});