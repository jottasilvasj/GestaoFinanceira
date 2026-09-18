# Documentação do Projeto Gestão Financeira

## Visão geral

O projeto Gestão Financeira é uma aplicação para gerenciamento de finanças pessoais com suporte a receitas, despesas, cálculo de saldo e organização por categoria e período. Ao longo do desenvolvimento, o projeto evoluiu para incluir também uma API REST e uma interface web, tornando a aplicação mais moderna e fácil de usar.

## Estrutura do repositório

```text
GestaoFinanceira/
├── GestaoFinanceira/              # Aplicação principal em C#
│   ├── Exceptions/
│   ├── Interfaces/
│   ├── Models/
│   ├── Services/
│   ├── Program.cs
│   └── GestaoFinanceira.csproj
├── GestaoFinanceira.Api/          # API REST em ASP.NET Core
│   ├── Dtos/
│   ├── Properties/
│   ├── Program.cs
│   ├── appsettings.json
│   ├── appsettings.Development.json
│   └── GestaoFinanceira.Api.csproj
├── frontend/                      # Aplicação web
│   ├── index.html
│   ├── style.css
│   └── app.js
├── README.md
├── DOCUMENTACAO.md
├── GestaoFinanceira.slnx
├── .gitignore
└── transacoes.json
```

## Funcionalidades atuais

### Aplicação em console
- cadastro de receitas
- cadastro de despesas
- cálculo do saldo total
- listagem de transações
- filtros por categoria
- busca por período
- persistência dos dados em arquivo JSON

### API REST
- listagem de transações
- consulta de saldo
- filtro por categoria
- busca por período
- cadastro de transações
- validação de entrada
- documentação com Swagger

### Front-end web
- visualização do saldo
- listagem das transações em tabela
- filtro por categoria
- cadastro de receitas e despesas diretamente pela página
- atualização automática da tela após inclusão

### Regras de negócio
- despesas não podem deixar o saldo final negativo
- o sistema valida valores e datas antes de registrar a transação
- transações são armazenadas em memória e também em arquivo para persistência local

## Arquitetura

O projeto foi organizado em camadas para facilitar manutenção e evolução:

- `GestaoFinanceira`: lógica principal do sistema
- `GestaoFinanceira.Api`: camada de acesso e exposição de serviços
- `frontend`: interface visual para interação com o usuário
- `Models`: entidades e enumerações
- `Services`: regras de negócio e persistência
- `Interfaces`: contratos para abstração das dependências

## Requisitos

- .NET SDK
- Git
- Visual Studio Code ou Visual Studio
- Navegador (para o frontend)

## Como executar a aplicação de console

```bash
cd GestaoFinanceira
dotnet run
```

Ou:

```bash
dotnet run --project ./GestaoFinanceira/GestaoFinanceira.csproj
```

## Como executar a API

```bash
dotnet run --project ./GestaoFinanceira.Api/GestaoFinanceira.Api.csproj
```

A API local normalmente fica em:

```text
https://localhost:5001
```

A documentação Swagger pode ser acessada em:

```text
https://localhost:5001/swagger
```

## Endpoints da API

### GET /api/transacoes/
Lista todas as transações.

### GET /api/transacoes/saldo
Retorna o saldo total atual.

### GET /api/transacoes/categoria/{categoria}
Retorna transações filtradas por categoria.

### GET /api/transacoes/periodo?inicio=yyyy-MM-dd&fim=yyyy-MM-dd
Retorna transações em um período específico.

### POST /api/transacoes/
Cria uma nova transação.

Exemplo de payload:

```json
{
  "descricao": "Salário",
  "valor": 3500.00,
  "data": "2024-08-15",
  "categoria": "Salario",
  "tipo": "Receita"
}
```

## Como executar o front-end

```bash
cd frontend
python -m http.server 8000
```

Acesse:

```text
http://localhost:8000
```

## Persistência de dados

Os dados são salvos em `transacoes.json` no diretório de execução da aplicação. Esse arquivo permite que informações sejam carregadas novamente ao iniciar o sistema.

## Novas funcionalidades implementadas

As principais melhorias recentes foram:

1. Conversão do sistema para arquitetura com API REST
2. Integração do backend com um frontend web
3. Validação de saldo insuficiente para impedir despesas inconsistentes
4. Suporte à consulta por categoria e período via endpoints
5. Documentação interativa com Swagger
6. Melhor experiência de uso para quem não trabalha diretamente no console

## Melhorias futuras sugeridas

- autenticação e autorização
- banco de dados persistente
- relatórios mensais
- gráficos e dashboards financeiros
- exportação em PDF/CSV
- testes automatizados
- paginação e filtros avançados

## Conclusão

O projeto evoluiu de uma aplicação de console para uma solução mais completa, com suporte a API e interface web. Isso amplia bastante sua utilidade, mantendo a simplicidade e a didática do desenvolvimento em C#.
