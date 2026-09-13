using System;
using System.Linq;
using GestaoFinanceira.Models;
using GestaoFinanceira.Interfaces;
using System.Collections.Generic;
using GestaoFinanceira.Exceptions;

namespace GestaoFinanceira.Services
{
    public class GerenciadorFinanceiro : IGerenciadorFinanceiro
    {
        private readonly List<Transacao> _transacoes = new List<Transacao>();

        // Método já existente (com implementação completa)
        public void AdicionarTransacao(Transacao transacao)
        {
            if (transacao == null)
            {
                throw new ArgumentNullException(nameof(transacao), "A transação é inválida.");
            }

            decimal impacto = transacao.CalcularImpactoSaldo();

            if (impacto < 0)
            {
                decimal saldoAtual = ObterSaldoTotal();
                if (saldoAtual + impacto < 0)
                {
                    throw new InvalidOperationException("Saldo insuficiente para realizar a transação.");
                }
            }

            _transacoes.Add(transacao);
        }

        // Implementação exigida pela interface; delega para AdicionarTransacao
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
            return new List<Transacao>(_transacoes);
        }

        public List<Transacao> FiltrarPorCategoria(CategoriaEnum categoria)
        {
            return _transacoes.Where(t => t.categoria == categoria).ToList();
        }

        public List<Transacao> BuscarPorPeriodo(DateTime dataInicio, DateTime dataFim)
        {
            if (dataInicio > dataFim)
            {
                throw new ArgumentException(nameof(dataInicio), "dataInicio deve ser menor ou igual a dataFim.");
            }

            DateTime inicio = dataInicio.Date;
            DateTime fim = dataFim.Date;

            return _transacoes
                .Where(t => t.Data.Date >= inicio && t.Data.Date <= fim)
                .ToList();
        }
    }
}
