# Documentação do Projeto Gestão Financeira

## Visão geral

O projeto Gestão Financeira é uma aplicação para controle básico de finanças pessoais, com foco em receitas, despesas, saldo total, filtros por categoria e busca por período. O repositório contém duas formas de execução:

- `GestaoFinanceira`: aplicação de console em C# para uso local e didático
- `GestaoFinanceira.Api`: API REST em ASP.NET Core para acesso via web ou integrações
- `frontend`: interface web em HTML, CSS e JavaScript para uso em navegador

O objetivo principal é demonstrar organização por camadas, manipulação de transações financeiras e persistência em arquivo local.

## Estrutura do repositório

```text
GestaoFinanceira/
├── GestaoFinanceira/          # Projeto principal em console/C#
│   ├── Exceptions/
│   ├── Interfaces/
│   ├── Models/
│   ├── Services/
│   ├── Program.cs
│   └── GestaoFinanceira.csproj
├── GestaoFinanceira.Api/      # API REST ASP.NET Core
│   ├── Dtos/
│   ├── Properties/
│   ├── Program.cs
│   ├── appsettings.json
│   ├── appsettings.Development.json
│   └── GestaoFinanceira.Api.csproj
├── frontend/                  # Front-end web
│   ├── index.html
│   ├── style.css
│   ├── app.js
├── README.md                  # Documentação inicial do projeto
├── GestaoFinanceira.slnx      # Solução do projeto
└── .gitignore
```

## Arquitetura e componentes

### 1. Camada de domínio

Localizada em `GestaoFinanceira` e responsável pela regra de negócio principal.

Principais responsabilidades:

- cadastro de receitas e despesas
- cálculo do saldo total
- listagem de transações
- filtros por categoria
- buscas por período
- persistência de dados em arquivo JSON

### 2. API REST

Localizada em `GestaoFinanceira.Api`, a API expõe endpoints para consulta e inserção de transações.

Funcionalidades disponíveis:

- listar todas as transações
- consultar saldo
- buscar transações por categoria
- buscar transações por período
- criar nova transação
- documentação automática via Swagger

### 3. Front-end

Localizada em `frontend`, a interface web utiliza HTML, CSS e JavaScript para consumir a API REST do backend.

## Requisitos do sistema

Antes de executar o projeto, verifique se os itens abaixo estão instalados:

- .NET SDK (compatível com o projeto)
- Git
- Editor de código: VS Code, Visual Studio ou qualquer IDE compatível com .NET
- Navegador para acessar a interface web

## Como executar a aplicação em console

Na raiz do repositório:

```bash
dotnet restore
cd GestaoFinanceira

dotnet run
```

Ou diretamente no projeto:

```bash
dotnet run --project ./GestaoFinanceira/GestaoFinanceira.csproj
```

### Menu disponível

A aplicação apresenta um menu com as seguintes opções:

1. Adicionar receita
2. Adicionar despesa
3. Exibir saldo total
4. Listar transações
5. Filtrar por categoria
6. Buscar por período
7. Salvar dados
0. Sair

## Como executar a API

A partir da raiz do projeto:

```bash
dotnet run --project ./GestaoFinanceira.Api/GestaoFinanceira.Api.csproj
```

A API será iniciada localmente e pode ser acessada em:

```text
https://localhost:5001
http://localhost:5000
```

A documentação Swagger fica disponível em:

```text
https://localhost:5001/swagger
```

## Endpoints da API

### Listar todas as transações

```http
GET /api/transacoes/
```

### Obter saldo total

```http
GET /api/transacoes/saldo
```

### Filtrar por categoria

```http
GET /api/transacoes/categoria/{categoria}
```

Exemplo:

```http
GET /api/transacoes/categoria/Alimentacao
```

### Buscar por período

```http
GET /api/transacoes/periodo?inicio=2024-01-01&fim=2024-12-31
```

### Criar transação

```http
POST /api/transacoes/
```

Payload esperado:

```json
{
  "descricao": "Salário",
  "valor": 3500.00,
  "data": "2024-08-15",
  "categoria": "Salario",
  "tipo": "Receita"
}
```

> Os nomes dos campos e valores devem seguir o modelo da aplicação, especialmente o `TipoTransacao` e o `CategoriaEnum` definidos no projeto.

## Como executar o front-end

Abra o arquivo `frontend/index.html` em um navegador, ou utilize um servidor local simples, por exemplo:

```bash
cd frontend
python -m http.server 8000
```

Depois acesse:

```text
http://localhost:8000
```

## Persistência de dados

A aplicação de console salva e carrega transações em um arquivo local chamado:

```text
transacoes.json
```

Esse arquivo é gerado no diretório de execução do projeto. Em ambiente local, normalmente ele fica na pasta do projeto `GestaoFinanceira`.

## Boas práticas observadas no projeto

- separação de responsabilidades por camadas
- uso de interfaces para abstração de serviços
- uso de modelos para representar transações
- uso de serviços para regras de negócio e persistência
- API com documentação automática via Swagger
- arquitetura simples e didática para estudo e evolução

## Fluxo de uso típico

1. Usuário cadastra receitas e despesas.
2. O sistema calcula o saldo total.
3. As transações são armazenadas em arquivo JSON.
4. A API expõe dados para consulta e integração.
5. O front-end exibe os dados em uma interface amigável.

## Possíveis melhorias futuras

- autenticação e autorização
- banco de dados persistente (SQL Server, PostgreSQL, SQLite)
- exportação para CSV/PDF
- relatórios mensais e gráficos
- testes automatizados (unitários e de integração)
- melhorias na validação de entrada
- paginação e filtros avançados

## Contribuição

Para colaborar com o projeto:

1. faça um fork do repositório
2. crie uma branch com nome descritivo
3. implemente a mudança
4. abra um pull request com descrição clara

## Observação final

Este projeto é bem adequado para fins de estudo, demonstração de boas práticas de desenvolvimento em C# e construções de API REST com ASP.NET Core. Ele pode evoluir para um sistema financeiro mais completo sem perder a simplicidade inicial.

---

Se quiser, posso também criar uma versão mais "profissional" da documentação em inglês, ou preparar uma documentação específica para:

- README principal do GitHub
- documentação técnica para desenvolvedores
- documentação para usuários finais
- documentação de API com exemplos completos
