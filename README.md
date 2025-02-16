#🛠️ NeoMigrate
NeoMigrate é uma aplicação desenvolvida em .NET Core 8 que facilita a criação de toda a estrutura de um banco de dados baseado em migrations feitas com C#. A solução é composta por dois projetos: uma class library para gerenciar as migrations e um projeto console que executa o processo de migração.

📁 Estrutura da Solução
A solução contém dois projetos principais:

1. Migrations (Class Library) 📚
O projeto Migrations é responsável por definir a estrutura do banco de dados e as migrations. Ele é dividido nas seguintes pastas:

Data 🗂️: Contém o AppDbContext para configurar e gerenciar o contexto do banco de dados.
Helpers ⚙️: Contém classes para configurações de base e sistema, além de mensagens de log e console.
Interface 🔌: Contém interfaces que podem ser usadas para abstração e desacoplamento de funcionalidades.
Migrations 🔄: Contém as classes responsáveis por atualizar o banco de dados, gerando as migrations conforme necessário.
Models 🏗️: Contém as entidades que representam as tabelas e outros objetos no banco de dados.
2. Migrator (Console Application) 💻
O projeto Migrator é responsável por executar o processo de migração, rodando as atualizações no banco de dados de acordo com as migrations definidas no projeto Migrations.
