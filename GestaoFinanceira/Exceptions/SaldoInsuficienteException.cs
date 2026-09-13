using GestaoFinanceira.Exceptions;
using GestaoFinanceira.Interfaces;
using GestaoFinanceira.Models;
using System;
using System.Collections.Generic;
using System.
    
    
    
    
    
    
    .Linq;

namespace GestaoFinanceira.Services
{
    public class GerenciadorFinanceiro : IGerenciadorFinanceiro
    {
        // Lista privada que armazena os dados em memória RAM
        private readonly List<Transacao> _transacoes;

        public GerenciadorFinanceiro()
        {
            _transacoes = new List<Transacao>();
        }

        // Construtor alternativo para quando carregar dados salvos em arquivo
        public GerenciadorFinanceiro(List<Transacao> transacoesIniciais)
        {
            _transacoes = transacoesIniciais ?? new List<Transacao>();
        }

        public void AdicionarTransacao(Transacao transacao)
        {
            if (transacao == null)
                throw new ArgumentNullException(nameof(transacao), "A transação não pode ser nula.");

            // Regra de negócio: Se for despesa, verifica se há saldo suficiente
            if (transacao is Despesa)
            {
                decimal saldoAtual = ObterSaldoTotal();

                // O valor da despesa supera o saldo disponível?
                if (transacao.Valor > saldoAtual)
                {
                    throw new SaldoInsuficienteException(
                        $"Operação negada! Saldo atual (R$ {saldoAtual:F2}) é insuficiente para a despesa de R$ {transacao.Valor:F2}."
                    );
                }
            }

            _transacoes.Add(transacao);
        }

        public decimal ObterSaldoTotal()
        {
            // LINQ: Soma o impacto individual de cada transação (Receita soma, Despesa subtrai)
            return _transacoes.Sum(t => t.ObterValorImpactoSaldo());
        }

        public IEnumerable<Transacao> ListarTodas()
        {
            // LINQ: Retorna a lista ordenada da transação mais recente para a mais antiga
            return _transacoes.OrderByDescending(t => t.Data);
        }

        public IEnumerable<Transacao> ObterPorCategoria(CategoriaEnum categoria)
        {
            // LINQ: Filtra a lista apenas pelas transações da categoria informada
            return _transacoes
                .Where(t => t.Categoria == categoria)
                .OrderByDescending(t => t.Data);
        }

        public IEnumerable<Transacao> ObterPorIntervaloDeData(DateTime dataInicio, DateTime dataFim)
        {
            // LINQ: Filtra transações que ocorreram entre as duas datas
            return _transacoes
                .Where(t => t.Data.Date >= dataInicio.Date && t.Data.Date <= dataFim.Date)
                .OrderByDescending(t => t.Data);
        }
    }
}