# LabsProject
 
## 🚀 Pré-requisitos

Antes de iniciar, certifique-se de que os seguintes softwares estão instalados no seu ambiente:

- **Visual Studio**
- **Visual Studio Code** (opcional)
- **XAMPP**
- **Workbench MySQL**
- **Node.js**
- **Angular CLI** (versão 18)

## 🗄️ Configuração do Banco de Dados

As credenciais do banco de dados são:

```plaintext
Server: localhost
Database: LabsDB
User ID: root
Port: 3306
Senha: (sem senha)
```

### 🚀 Instruções de Configuração

#### 1. Iniciar o MySQL no XAMPP

- Abra o **XAMPP** e inicie o **MySQL**.

#### 2. Configuração do Banco de Dados no Visual Studio ou Visual Studio Code

- Vá até **Ferramentas** > **Gerenciador de Pacotes NuGet** > **Console do Gerenciador de Pacotes**.
- Com o **projeto API** como o projeto padrão, execute os seguintes comandos no console:

   ```bash
   add-migration migTeste
   update-database
   ```

## Instruções para Configuração da chave secreta do servidor de Email

### 🔑 Configuração da Chave Secret

Para configurar a chave do **SendGrid**, execute o seguinte comando:

```bash
dotnet user-secrets set "SendGridKey" "apiKey"
```

## Execução do Projeto

### 🚀 Rodando o Projeto .NET

1. Abra o projeto no **Visual Studio**.
2. Execute o projeto normalmente.

## 🚀 Rodando o Projeto Angular

1. Antes de iniciar, certifique-se de que os pacotes estão instalados. Para isso, execute o seguinte comando:

   ```bash
   npm install
   ```
2. Inicie o servidor Angular com o comando:

   ```bash
   ng serve -o
   ```
## ⚠️ Observações Importantes

- A **confirmação de e-mail** deve ser feita **em máquina local**.
- Passarei a apiKey por email
- Durante os meus testes:
  - O **Outlook** não respondeu muito bem e o e-mail de confirmação se perdia, mesmo com a API e o **SendGrid** afirmando o contrário.
  - Em contrapartida, o **Gmail** funcionou corretamente em todas as vezes.
- O cors está configurado para permitir somente http://localhost:4200. Caso rode o front-end em outra porta, altere a origem permitida no arquivo program.cs
- Os endpoint do back-end estão setados para a porta 7042

# 📖 Escolhas de desenvolvimento

## Teste Unitário  
Optou-se por utilizar **testes unitários** devido à sua capacidade de validar a menor granularidade do sistema, garantindo o funcionamento correto de cada componente isoladamente.  

## Arquitetura/Design  
Foi adotada uma arquitetura em camadas. Considerou-se desnecessário implementar o **DDD** (com 5 camadas), já que o projeto não apresentava alta complexidade ou robustez. Isso tornaria algumas camadas redundantes, servindo apenas como intermediárias. Assim, foram definidas as seguintes camadas:

- **Controller**: Responsável por receber os dados.  
- **Application**: Realiza o mapeamento entre as entidades e os modelos de request/response.  
- **Models**: Contém os modelos de request e response.  
- **Infra**: Agrega as regras de negócio, a conexão com o banco de dados e também a entidade.

Em relação ao **Domain-Driven Design (DDD)**, a estrutura foi simplificada, unificando as camadas de infraestrutura e serviço para atender às necessidades do projeto de forma eficiente.

## Tecnologias utilizadas

1. **Microsoft.Extensions.Logging**: Foi escolhido pela facilidade de uso e pela integração direta com o .NET, permitindo adicionar logs estruturados de forma prática e eficiente.
2. **Entity Framework Core**: Selecionado devido à sua simplicidade no acesso e manipulação de dados, atendendo bem às necessidades do projeto com recursos como LINQ e mapeamento de entidades.
3. **IdentityModel**: Foi escolhido por se adequar muito bem aos requisitos da página de login. Ele oferece criptografia automática de senhas e métodos prontos para login, registro, entre outros. Com isso, foi possível utilizar os recursos existentes e realizar apenas alguns ajustes para alinhá-los às regras especificadas no documento de requisitos.
4. **SendGrid**: Escolhido por ser uma das ferramentas de envio de e-mails mais utilizadas no mercado, com documentação clara e integração simples, facilitando o envio de e-mails transacionais.
5. **Bootstrap**: Utilizado em algumas partes para proporcionar maior agilidade na construção do layout.
