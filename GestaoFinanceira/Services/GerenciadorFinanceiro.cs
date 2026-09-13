using System;
using System.Linq;
using System.Collections.Generic;
using GestaoFinanceira.Models;
using GestaoFinanceira.Interfaces;

namespace GestaoFinanceira.Services
{
    public class GerenciadorFinanceiro : IGerenciadorFinanceiro
    {
        private readonly List<Transacao> _transacoes;

        public GerenciadorFinanceiro()
        {
            _transacoes = new List<Transacao>();
        }

        public GerenciadorFinanceiro(List<Transacao> transacoesIniciais)
        {
            _transacoes = transacoesIniciais ?? new List<Transacao>();
        }

        public void AdicionarTransacao(Transacao transacao)
        {
            if (transacao == null)
                throw new ArgumentNullException(nameof(transacao), "A transação é inválida.");

            decimal impacto = transacao.CalcularImpactoSaldo();

            if (impacto < 0)
            {
                decimal saldoAtual = ObterSaldoTotal();
                if (saldoAtual + impacto < 0)
                    throw new InvalidOperationException(
                        $"Saldo insuficiente! Saldo atual: R$ {saldoAtual:F2}, valor da transação: R$ {transacao.Valor:F2}.");
            }

            _transacoes.Add(transacao);
        }

        public void AdicionarReceita(Transacao transacao)
        {
            AdicionarTransacao(transacao);
        }

        public decimal ObterSaldoTotal()
        {
            return _transacoes.Sum(t => t.CalcularImpactoSaldo());
        }

        public List<Transacao> ListarTodas()
        {
            return _transacoes
                .OrderByDescending(t => t.Data)
                .ToList();
        }

        public List<Transacao> FiltrarPorCategoria(CategoriaEnum categoria)
        {
            return _transacoes
                .Where(t => t.Categoria == categoria)
                .OrderByDescending(t => t.Data)
                .ToList();
        }

        public List<Transacao> BuscarPorPeriodo(DateTime dataInicio, DateTime dataFim)
        {
            if (dataInicio > dataFim)
                throw new ArgumentException("dataInicio deve ser menor ou igual a dataFim.", nameof(dataInicio));

            DateTime inicio = dataInicio.Date;
            DateTime fim = dataFim.Date;

            return _transacoes
                .Where(t => t.Data.Date >= inicio && t.Data.Date <= fim)
                .OrderByDescending(t => t.Data)
                .ToList();
        }
    }
}