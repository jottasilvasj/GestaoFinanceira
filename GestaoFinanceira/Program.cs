using System;
using System.Collections.Generic;
using GestaoFinanceira.Interfaces;
using GestaoFinanceira.Models;
using GestaoFinanceira.Services;

namespace GestaoFinanceira
{
    public class Program
    {
        private static readonly IGerenciadorFinanceiro _gerenciador = new GerenciadorFinanceiro();
        private static readonly IArmazenamentoService _armazenamento = new ArmazenamentoService();

        public static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            CarregarDadosSalvos();

            bool continuar = true;

            while (continuar)
            {
                ExibirMenu();
                string opcao = Console.ReadLine();

                switch (opcao)
                {
                    case "1":
                        AdicionarReceita();
                        break;
                    case "2":
                        AdicionarDespesa();
                        break;
                    case "3":
                        ExibirSaldo();
                        break;
                    case "4":
                        ListarTransacoes(_gerenciador.ListarTodas());
                        break;
                    case "5":
                        FiltrarPorCategoria();
                        break;
                    case "6":
                        BuscarPorPeriodo();
                        break;
                    case "7":
                        SalvarDados();
                        break;
                    case "0":
                        continuar = false;
                        SalvarDados();
                        Console.WriteLine("\nAté logo!");
                        break;
                    default:
                        Console.WriteLine("\nOpção inválida. Tente novamente.");
                        break;
                }

                if (continuar)
                {
                    Console.WriteLine("\nPressione ENTER para continuar...");
                    Console.ReadLine();
                }
            }
        }

        private static void ExibirMenu()
        {
            Console.Clear();
            Console.WriteLine("=========================================");
            Console.WriteLine("     GESTÃO FINANCEIRA - MENU PRINCIPAL   ");
            Console.WriteLine("=========================================");
            Console.WriteLine("1 - Adicionar Receita");
            Console.WriteLine("2 - Adicionar Despesa");
            Console.WriteLine("3 - Exibir Saldo Total");
            Console.WriteLine("4 - Listar Todas as Transações");
            Console.WriteLine("5 - Filtrar por Categoria");
            Console.WriteLine("6 - Buscar por Período");
            Console.WriteLine("7 - Salvar Dados");
            Console.WriteLine("0 - Sair");
            Console.WriteLine("=========================================");
            Console.Write("Escolha uma opção: ");
        }

        private static void AdicionarReceita()
        {
            Console.WriteLine("\n--- Nova Receita ---");
            var transacao = LerDadosTransacao();

            if (transacao == null) return;

            try
            {
                var receita = new Receita(transacao.Value.descricao, transacao.Value.valor, transacao.Value.data, transacao.Value.categoria);
                _gerenciador.AdicionarReceita(receita);
                Console.WriteLine("\nReceita adicionada com sucesso!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nErro ao adicionar receita: {ex.Message}");
            }
        }

        private static void AdicionarDespesa()
        {
            Console.WriteLine("\n--- Nova Despesa ---");
            var transacao = LerDadosTransacao();

            if (transacao == null) return;

            try
            {
                var despesa = new Despesas(transacao.Value.descricao, transacao.Value.valor, transacao.Value.data, transacao.Value.categoria);

                if (_gerenciador is GerenciadorFinanceiro gerenciadorConcreto)
                {
                    gerenciadorConcreto.AdicionarTransacao(despesa);
                }

                Console.WriteLine("\nDespesa adicionada com sucesso!");
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"\nOperação negada: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nErro ao adicionar despesa: {ex.Message}");
            }
        }

        private static (string descricao, decimal valor, DateTime data, CategoriaEnum categoria)? LerDadosTransacao()
        {
            Console.Write("Descrição: ");
            string descricao = Console.ReadLine();

            Console.Write("Valor (R$): ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal valor) || valor <= 0)
            {
                Console.WriteLine("\nValor inválido.");
                return null;
            }

            Console.Write("Data (dd/MM/yyyy) [ENTER para hoje]: ");
            string dataTexto = Console.ReadLine();
            DateTime data = string.IsNullOrWhiteSpace(dataTexto) ? DateTime.Now : DateTime.Parse(dataTexto);

            CategoriaEnum categoria = LerCategoria();

            return (descricao, valor, data, categoria);
        }

        private static CategoriaEnum LerCategoria()
        {
            Console.WriteLine("\nCategorias disponíveis:");
            var categorias = Enum.GetValues(typeof(CategoriaEnum));
            int i = 1;
            foreach (var cat in categorias)
            {
                Console.WriteLine($"{i} - {cat}");
                i++;
            }

            Console.Write("Escolha a categoria (número): ");
            if (int.TryParse(Console.ReadLine(), out int escolha) && escolha >= 1 && escolha <= categorias.Length)
            {
                return (CategoriaEnum)(escolha - 1);
            }

            Console.WriteLine("Opção inválida, categoria padrão 'Lazer' será usada.");
            return CategoriaEnum.Lazer;
        }

        private static void ExibirSaldo()
        {
            decimal saldo = _gerenciador.ObterSaldoTotal();
            Console.WriteLine($"\nSaldo atual: R$ {saldo:F2}");
        }

        private static void ListarTransacoes(List<Transacao> transacoes)
        {
            Console.WriteLine("\n--- Transações ---");

            if (transacoes.Count == 0)
            {
                Console.WriteLine("Nenhuma transação encontrada.");
                return;
            }

            foreach (var t in transacoes)
            {
                string tipo = t is Receita ? "Receita" : "Despesa";
                Console.WriteLine($"[{tipo}] {t.Data:dd/MM/yyyy} | {t.Descricao} | {t.Categoria} | R$ {t.Valor:F2}");
            }
        }

        private static void FiltrarPorCategoria()
        {
            CategoriaEnum categoria = LerCategoria();
            var resultado = _gerenciador.FiltrarPorCategoria(categoria);
            ListarTransacoes(resultado);
        }

        private static void BuscarPorPeriodo()
        {
            Console.Write("\nData inicial (dd/MM/yyyy): ");
            DateTime inicio = DateTime.Parse(Console.ReadLine());

            Console.Write("Data final (dd/MM/yyyy): ");
            DateTime fim = DateTime.Parse(Console.ReadLine());

            try
            {
                var resultado = _gerenciador.BuscarPorPeriodo(inicio, fim);
                ListarTransacoes(resultado);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"\nErro: {ex.Message}");
            }
        }

        private static void SalvarDados()
        {
            bool sucesso = _armazenamento.SalvarTransacoes(_gerenciador.ListarTodas());
            Console.WriteLine(sucesso ? "\nDados salvos com sucesso!" : "\nFalha ao salvar os dados.");
        }

        private static void CarregarDadosSalvos()
        {
            var transacoesSalvas = _armazenamento.CarregarTransacoes();

            if (transacoesSalvas == null || transacoesSalvas.Count == 0)
                return;

            foreach (var t in transacoesSalvas)
            {
                if (_gerenciador is GerenciadorFinanceiro gerenciadorConcreto)
                {
                    gerenciadorConcreto.AdicionarTransacao(t);
                }
            }
        }
    }
}
