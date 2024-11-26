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

#### 2. Configuração do Banco de Dados no Visual Studio

- Vá até **Ferramentas** > **Gerenciador de Pacotes NuGet** > **Console do Gerenciador de Pacotes**.
- Com o **projeto API** como o projeto padrão, execute os seguintes comandos no console:

   ```bash
   add-migration migTeste
   update-database
   ```
