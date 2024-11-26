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

