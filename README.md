# 🛒 Outsider Server — Plataforma de E-commerce com Microsserviços

Back-end completo de uma plataforma de e-commerce construído com **arquitetura de microsserviços** em **ASP.NET Core (.NET)**. Cada domínio do negócio é isolado em sua própria API independente, comunicando-se por meio de um **API Gateway** e um **Message Bus** assíncrono.

---

## 🏗️ Arquitetura

O sistema é composto por múltiplos serviços independentes, cada um responsável por um contexto de negócio distinto:

```
                        ┌─────────────────────┐
                        │   outsider.web       │  ← Interface Web (Front-end)
                        └──────────┬──────────┘
                                   │
                        ┌──────────▼──────────┐
                        │  Outsider.APIGateway │  ← Ponto de entrada único (Ocelot/YARP)
                        └──────────┬──────────┘
                                   │
        ┌──────────────────────────┼───────────────────────────┐
        │                          │                           │
┌───────▼──────┐    ┌──────────────▼───┐    ┌─────────────────▼──┐
│ ProdutoAPI   │    │  CarrinhoAPI      │    │  PedidoAPI          │
└──────────────┘    └──────────────────┘    └────────────────────┘
        │                          │                           │
┌───────▼──────┐    ┌──────────────▼───┐    ┌─────────────────▼──┐
│ AvaliacaoAPI │    │  CupomAPI         │    │  PagamentoAPI       │
└──────────────┘    └──────────────────┘    └────────────────────┘
        │
┌───────▼──────┐    ┌──────────────────┐    ┌────────────────────┐
│ EnderecoAPI  │    │  IdentityServer   │    │  MessageBus        │
└──────────────┘    └──────────────────┘    └────────────────────┘
```

---

## 📦 Microsserviços

| Serviço                    | Responsabilidade                                               |
|----------------------------|----------------------------------------------------------------|
| `Outsider.APIGateway`      | Roteamento e ponto de entrada centralizado para todos os serviços |
| `Outsider.IdentityServer`  | Autenticação e autorização (gerenciamento de identidade/JWT)   |
| `Outsider.ProdutoAPI`      | Cadastro, consulta e gerenciamento de produtos                 |
| `Outsider.CarrinhoAPI`     | Gerenciamento do carrinho de compras por usuário               |
| `Outsider.PedidoAPI`       | Criação e acompanhamento de pedidos                            |
| `Outsider.PagamentoAPI`    | Processamento de pagamentos                                    |
| `Outsider.CupomAPI`        | Gerenciamento e validação de cupons de desconto                |
| `Outsider.AvaliacaoAPI`    | Avaliações e reviews de produtos                               |
| `Outsider.EnderecoAPI`     | Cadastro e consulta de endereços de entrega                    |
| `Outsider.MessageBus`      | Comunicação assíncrona entre os serviços (mensageria)          |
| `Outsider.Email`           | Envio de e-mails transacionais                                 |
| `Outsider.DTO`             | Objetos de transferência de dados compartilhados entre serviços|
| `DataNoSQL`                | Camada de acesso a dados não-relacionais                       |
| `outsider.web`             | Interface web (front-end da plataforma)                        |

---

## 🛠️ Tecnologias Utilizadas

| Tecnologia              | Uso                                               |
|-------------------------|---------------------------------------------------|
| C# / ASP.NET Core       | Desenvolvimento de todos os microsserviços        |
| API Gateway             | Centralização e roteamento de requisições         |
| Identity Server / JWT   | Autenticação e autorização                        |
| Message Bus             | Comunicação assíncrona entre serviços             |
| NoSQL                   | Persistência de dados não-relacionais             |
| SQL Server / T-SQL      | Persistência relacional                           |
| Vue.js                  | Componentes de interface (front-end)              |
| CSS / Less / HTML       | Estilização da interface web                      |
| GitHub Actions          | Pipeline de CI/CD                                 |

---

## 📋 Pré-requisitos

- [.NET SDK](https://dotnet.microsoft.com/download) (versão compatível com o projeto)
- [SQL Server](https://www.microsoft.com/pt-br/sql-server)
- Instância de banco NoSQL configurada (ex: MongoDB ou Redis)
- [Visual Studio](https://visualstudio.microsoft.com/) ou [VS Code](https://code.visualstudio.com/)

---

## 🚀 Como Executar

1. **Clone o repositório:**
   ```bash
   git clone https://github.com/Dravvos/Outsider-Server.git
   cd Outsider-Server
   ```

2. **Configure as strings de conexão:**
   - Abra cada projeto de API e atualize o `appsettings.json` com as conexões do seu banco de dados SQL Server e NoSQL.

3. **Configure o Identity Server:**
   - Defina as variáveis de ambiente ou `appsettings` do `Outsider.IdentityServer` com as chaves de autenticação.

4. **Execute os serviços:**
   - Abra a solution `Outsider.sln` no Visual Studio
   - Configure o **Multiple Startup Projects** para subir todos os serviços simultaneamente
   - Pressione `F5`

   Ou via CLI, execute cada serviço individualmente:
   ```bash
   cd Outsider.ProdutoAPI && dotnet run
   cd Outsider.CarrinhoAPI && dotnet run
   # ... repita para cada serviço
   ```

5. **Acesse o API Gateway:**
   - O gateway centraliza as rotas; consulte `Outsider.APIGateway/ocelot.json` (ou configuração equivalente) para ver os endpoints disponíveis.

---

## 📁 Estrutura do Projeto

```
Outsider-Server/
├── .github/workflows/          # Pipeline CI/CD (GitHub Actions)
├── Outsider.APIGateway/        # API Gateway (roteamento centralizado)
├── Outsider.IdentityServer/    # Servidor de identidade e autenticação
├── Outsider.ProdutoAPI/        # Microsserviço de produtos
├── Outsider.CarrinhoAPI/       # Microsserviço de carrinho
├── Outsider.PedidoAPI/         # Microsserviço de pedidos
├── Outsider.PagamentoAPI/      # Microsserviço de pagamentos
├── Outsider.Pagamentos/        # Lógica de integração de pagamentos
├── Outsider.CupomAPI/          # Microsserviço de cupons
├── Outsider.AvaliacaoAPI/      # Microsserviço de avaliações
├── Outsider.EnderecoAPI/       # Microsserviço de endereços
├── Outsider.MessageBus/        # Barramento de mensagens assíncronas
├── Outsider.Email/             # Serviço de envio de e-mails
├── Outsider.DTO/               # DTOs compartilhados
├── DataNoSQL/                  # Acesso a dados NoSQL
├── outsider.web/               # Front-end da aplicação
├── wwwroot/                    # Arquivos estáticos
├── Outsider.sln                # Solution file
└── LICENSE                     # Licença MIT
```

---

## 🤝 Contribuições

Contribuições são bem-vindas! Para contribuir:

1. Faça um **fork** do repositório
2. Crie uma branch: `git checkout -b feature/minha-feature`
3. Commit: `git commit -m 'feat: descrição da melhoria'`
4. Push: `git push origin feature/minha-feature`
5. Abra um **Pull Request**

---

## 📄 Licença

Este projeto está licenciado sob a [MIT License](LICENSE).

---

Desenvolvido por [Daniel Oliveira Dias (Dravvos)](https://github.com/Dravvos)
