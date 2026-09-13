# GestãoFinanceira

Console app em C# para gerenciamento básico de finanças pessoais (receitas e despesas). Projeto simples para estudo e demonstração de boas práticas como separação por camadas, uso de interfaces e persistência local em JSON.

## Funcionalidades
- Adicionar receitas e despesas.
- Exibir saldo total.
- Listar transações.
- Filtrar por categoria.
- Buscar por período.
- Persistência local em `transacoes.json`.

## Requisitos
- .NET 10 SDK instalado (compatível com o projeto).
- Git (para clonar o repositório).
- Visual Studio 2022/2026 ou VS Code / outro editor com suporte .NET (opcional).

## Como executar

Estas instruções servem para qualquer pessoa que clonar o repositório do GitHub.

1. Clone o repositório
- HTTPS:
  git clone https://github.com/<seu-usuario>/GestaoFinanceira.git
- SSH:
  git clone git@github.com:<seu-usuario>/GestaoFinanceira.git

2. Via linha de comando (cross‑platform — PowerShell, Bash, etc.)
- Navegue até a pasta do projeto (a pasta que contém o arquivo `.csproj`):
  cd GestaoFinanceira/GestaoFinanceira
- Restaurar dependências:
  dotnet restore
- Build (opcional):
  dotnet build --configuration Release
- Executar:
  dotnet run --project ./GestaoFinanceira.csproj
  ou (quando já estiver na pasta do projeto)
  dotnet run

Observações:
- O aplicativo grava/carrega `transacoes.json` no diretório de execução. Ao executar localmente a partir da pasta do projeto, o arquivo será criado nessa pasta.
- Para executar a partir da raiz da solução:
  dotnet run --project ./GestaoFinanceira/GestaoFinanceira.csproj

3. Via Visual Studio
- Abra a solução `GestaoFinanceira.slnx` no __Visual Studio__.
- Defina `GestaoFinanceira` como projeto de início.
- Execute com __F5__ (Debug) ou __Ctrl+F5__ (sem debug).

4. Execução em container (opcional)
- Dockerfile não incluído por padrão — exemplo rápido:
  docker build -t gestao-financeira .
  docker run --rm -it -v "$(pwd)/data:/app/data" gestao-financeira
- Se usar Docker, monte um volume para persistir `transacoes.json`.

## Arquivo de dados
- `transacoes.json` — localizado no diretório de execução. Para portabilidade, documente no README onde o arquivo ficará quando publicar o app (ex.: em um contêiner ou serviço).

## Sugestões rápidas para quem for rodar
- Verifique se o .NET 10 está instalado: dotnet --version
- Se ocorrer erro de parsing de datas, verifique as configurações regionais do terminal (o app espera dd/MM/yyyy ao inserir datas).

## Contribuição
- Fork → branch com nome descritivo → PR com descrição e testes (se aplicável).
- Adicione testes automatizados e um arquivo LICENSE antes de publicar.
