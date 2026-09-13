using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using GestaoFinanceira.Interfaces;
using GestaoFinanceira.Models;

namespace GestaoFinanceira.Services
{
    public class ArmazenamentoService : IArmazenamentoService
    {
        private readonly string _caminhoArquivo;
        private readonly JsonSerializerOptions _opcoesJson;

        public ArmazenamentoService(string caminhoArquivo = "transacoes.json")
        {
            _caminhoArquivo = caminhoArquivo;
            _opcoesJson = new JsonSerializerOptions
            {
                WriteIndented = true
            };
        }

        public bool SalvarTransacoes(List<Transacao> transacoes)
        {
            try
            {
                string jsonString = JsonSerializer.Serialize(transacoes, _opcoesJson);
                File.WriteAllText(_caminhoArquivo, jsonString);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao salvar os dados: {ex.Message}");
                return false;
            }
        }

        public List<Transacao> CarregarTransacoes()
        {
            try
            {
                if (!File.Exists(_caminhoArquivo))
                {
                    return new List<Transacao>();
                }

                string jsonString = File.ReadAllText(_caminhoArquivo);

                if (string.IsNullOrWhiteSpace(jsonString))
                {
                    return new List<Transacao>();
                }

                var transacoes = JsonSerializer.Deserialize<List<Transacao>>(jsonString, _opcoesJson);
                return transacoes ?? new List<Transacao>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao carregar os dados: {ex.Message}. Iniciando com lista vazia.");
                return new List<Transacao>();
            }
        }
    }
}