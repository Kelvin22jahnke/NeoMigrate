# 🛠️ **NeoMigrate**

**NeoMigrate** é uma aplicação desenvolvida em **.NET Core 8** que facilita a criação de toda a estrutura de um banco de dados baseado em **migrations** feitas com **C#**. A solução é composta por dois projetos: uma **class library** para gerenciar as migrations e um **projeto console** que executa o processo de migração.

## 📁 **Estrutura da Solução**

A solução contém dois projetos principais:

### 1. **Migrations (Class Library)** 📚
O projeto **Migrations** é responsável por definir a estrutura do banco de dados e as migrations. Ele é dividido nas seguintes pastas:

- **Data** 🗂️: Contém o `AppDbContext` para configurar e gerenciar o contexto do banco de dados.
- **Helpers** ⚙️: Contém classes para configurações de base e sistema, além de mensagens de log e console.
- **Interface** 🔌: Contém interfaces que podem ser usadas para abstração e desacoplamento de funcionalidades.
- **Migrations** 🔄: Contém as classes responsáveis por atualizar o banco de dados, gerando as migrations conforme necessário.
- **Models** 🏗️: Contém as entidades que representam as tabelas e outros objetos no banco de dados.

### 2. **Migrator (Console Application)** 💻
O projeto **Migrator** é responsável por executar o processo de migração, rodando as atualizações no banco de dados de acordo com as migrations definidas no projeto **Migrations**.

## 🚀 **Como Usar**

### Pré-requisitos

1. **.NET Core 8** instalado 💻.
2. Banco de dados configurado 🏦.

### Passos para Rodar a Aplicação

1. Clone o repositório:
    ```bash
    git clone <url-do-repositorio>
    ```

2. Navegue até a pasta do projeto **Migrator** e execute o seguinte comando para rodar a migração:
    ```bash
    dotnet run
    ```

### 🏗️ **Estrutura do Banco de Dados**

O **AppDbContext** configura a conexão e o esquema do banco de dados. As migrations geram a estrutura das tabelas de acordo com as entidades definidas na pasta **Models**.

## 💡 **Contribuições**

Sinta-se à vontade para contribuir com melhorias! Para isso, siga o fluxo abaixo:

1. Faça um fork do repositório 🍴.
2. Crie uma branch para sua modificação (`git checkout -b feature/nova-feature`).
3. Faça commit das suas mudanças (`git commit -am 'Adicionando nova feature'`).
4. Envie para o seu repositório (`git push origin feature/nova-feature`).
5. Abra um pull request 🤝.
