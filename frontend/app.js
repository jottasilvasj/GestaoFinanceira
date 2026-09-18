// TROQUE a porta abaixo pela porta HTTPS que sua API usa
const API_BASE_URL = "https://localhost:7207/api/transacoes";

const form = document.getElementById("form-transacao");
const mensagemErro = document.getElementById("mensagem-erro");
const saldoEl = document.getElementById("saldo");
const tabela = document.getElementById("tabela-transacoes");
const filtroCategoria = document.getElementById("filtro-categoria");

document.getElementById("data").valueAsDate = new Date();

function formatarMoeda(valor) {
    return valor.toLocaleString("pt-BR", { style: "currency", currency: "BRL" });
}

async function carregarSaldo() {
    const resposta = await fetch(`${API_BASE_URL}/saldo`);
    const dados = await resposta.json();
    saldoEl.textContent = formatarMoeda(dados.saldo);
}

async function carregarTransacoes() {
    const categoria = filtroCategoria.value;
    const url = categoria
        ? `${API_BASE_URL}/categoria/${categoria}`
        : API_BASE_URL;

    const resposta = await fetch(url);
    const transacoes = await resposta.json();

    tabela.innerHTML = "";
    transacoes.forEach((t) => {
        const tipo = t.$type === "receita" ? "Receita" : "Despesa";
        const classe = t.$type === "receita" ? "receita" : "despesa";
        const sinal = t.$type === "receita" ? "+" : "-";

        const linha = document.createElement("tr");
        linha.innerHTML = `
      <td>${new Date(t.data).toLocaleDateString("pt-BR")}</td>
      <td>${t.descricao}</td>
      <td>${t.categoria}</td>
      <td>${tipo}</td>
      <td class="${classe}">${sinal} ${formatarMoeda(t.valor)}</td>
    `;
        tabela.appendChild(linha);
    });
}

form.addEventListener("submit", async (evento) => {
    evento.preventDefault();
    mensagemErro.textContent = "";

    const corpo = {
        tipo: document.getElementById("tipo").value,
        descricao: document.getElementById("descricao").value,
        valor: parseFloat(document.getElementById("valor").value),
        data: document.getElementById("data").value,
        categoria: document.getElementById("categoria").value,
    };

    const resposta = await fetch(API_BASE_URL, {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(corpo),
    });

    if (!resposta.ok) {
        const erro = await resposta.json();
        mensagemErro.textContent = erro.erro || (erro.erros ? erro.erros.join(", ") : "Erro ao salvar.");
        return;
    }

    form.reset();
    document.getElementById("data").valueAsDate = new Date();
    await carregarSaldo();
    await carregarTransacoes();
});

filtroCategoria.addEventListener("change", carregarTransacoes);

carregarSaldo();
carregarTransacoes();