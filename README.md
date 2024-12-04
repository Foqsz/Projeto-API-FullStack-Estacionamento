# Projeto API de Estacionamento

Este é um projeto de API RESTful para gerenciamento de um estacionamento de veículos. A API foi desenvolvida com .NET 9, utilizando Entity Framework Core para persistência de dados em um banco MySQL, e Swagger para documentação da API.
Como DEV Jr, fiz esse projeto como parte de um [Desafio](https://github.com/fcamarasantos/backend-test-dotnet). Irei aprimorando conforme estudo e adquiro mais conhecimento.

## Tecnologias Utilizadas

- **.NET 9**: Framework de desenvolvimento de aplicações web.
- **Entity Framework Core**: ORM (Object-Relational Mapper) para acesso ao banco de dados.
- **MySQL Workbench**: Ferramenta de gerenciamento e design de banco de dados MySQL.
- **Swagger**: Ferramenta para documentação da API e visualização dos endpoints.
- **ASP.NET Core**: Framework para a criação da API.

## Funcionalidades

A API permite realizar as seguintes operações:

### Empresas
- **Cadastrar Empresa**: Registra um novo estabelecimento de estacionamento.
- **Listar Empresas**: Recupera todos os estabelecimentos cadastrados.
- **Checar Empresa**: Verifica a existência de uma empresa pelo ID.
- **Editar Empresa**: Atualiza as informações de uma empresa cadastrada.
- **Deletar Empresa**: Exclui uma empresa registrada.

### Veículos
- **Cadastrar Veículo**: Registra um novo veículo com informações como marca, modelo, cor, placa e tipo (carro ou moto).
- **Listar Veículos**: Recupera todos os veículos cadastrados.
- **Checar Veículo**: Verifica a existência de um veículo pelo ID.
- **Atualizar Veículo**: Atualiza as informações de um veículo cadastrado.
- **Deletar Veículo**: Exclui um veículo registrado.

### Movimentação
- **Listar Veículos Estacionados**: Recupera todos os veículos que estão atualmente estacionados.
- **Registrar Entrada de Veículo**: Registra a entrada de um veículo no estacionamento.
- **Registrar Saída de Veículo**: Registra a saída de um veículo, utilizando seu ID e placa.

### Relatórios
- **Relatório de Entrada e Saída**: Gera um relatório com a quantidade de veículos que entraram e saíram durante o dia.
- **Relatório por Hora**: Gera um relatório de veículos que entraram e saíram em cada hora do dia.

## Endpoints

### Empresas
- `GET /api/Empresas/ListarEmpresas`: Lista todos os estabelecimentos cadastrados.
- `GET /api/Empresas/ChecarEmpresa/{id}`: Verifica a existência de uma empresa pelo ID.
- `POST /api/Empresas/CriarEmpresa`: Cria um novo estabelecimento de estacionamento.
- `PUT /api/Empresas/EditarEmpresa/{id}`: Atualiza as informações de uma empresa existente.
- `DELETE /api/Empresas/DeletarEmpresa/{id}`: Exclui uma empresa cadastrada.

### Veículos
- `GET /api/Veiculos/ListarVeiculos`: Lista todos os veículos cadastrados.
- `GET /api/Veiculos/ChecarVeiculo/{id}`: Verifica a existência de um veículo pelo ID.
- `POST /api/Veiculos/CadastrarVeiculo`: Cadastra um novo veículo.
- `PUT /api/Veiculos/AtualizarVeiculo/{id}`: Atualiza as informações de um veículo existente.
- `DELETE /api/Veiculos/DeletarVeiculo/{id}`: Exclui um veículo cadastrado.

### Movimentação
- `GET /api/Movimentacao/Estacionados`: Recupera todos os veículos atualmente estacionados.
- `POST /api/Movimentacao/Entrada`: Registra a entrada de um veículo no estacionamento.
- `POST /api/Movimentacao/Saida/{id}/{placa}`: Registra a saída de um veículo, identificando-o pelo ID e pela placa.

## Configuração

1. **Banco de Dados**: Configure a string de conexão no arquivo `appsettings.json` com os dados do seu banco MySQL.
2. **Swagger**: Após rodar a aplicação, você pode acessar a documentação da API em `http://localhost:5000/swagger`.

## Como Rodar o Projeto

1. Clone este repositório:
    ```bash
    git clone https://github.com/Foqsz/Projeto-API-BackEnd-Estacionamento.git
    ```
2. Abra o projeto no Visual Studio ou no VS Code.
3. Configure a string de conexão no arquivo `appsettings.json` para o banco MySQL.
4. Execute o comando para aplicar as migrations:
    ```bash
    dotnet ef database update
    ```
5. Inicie o projeto:
    ```bash
    dotnet run
    ```

## Testes

A API pode ser testada via Swagger ou usando ferramentas como Postman. Todos os endpoints estão documentados na interface do Swagger.

## Contribuições

Sinta-se à vontade para abrir issues ou enviar pull requests. Toda contribuição é bem-vinda!

## Licença

Este projeto está licenciado sob a MIT License - veja o arquivo [LICENSE](LICENSE) para mais detalhes.
