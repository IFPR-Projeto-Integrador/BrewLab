# Iniciando o projeto

### 1 - Baixe o Visual Studio (Windows)

Baixe e installe o [Visual Studio](https://visualstudio.microsoft.com/pt-br/vs/) caso não possua (Community Edition).

### 2 - Clone o projeto pelo Visual Studio

Clone o repositório através do Visual Studio: </br>
<img src="https://github.com/user-attachments/assets/ed382657-847d-490d-8b05-b13a78c40be4" width="300" height="200">

### 3 - Crie a .env

Crie um arquivo .env com os seguintes campos, removendo os comentários em #:
```txt
DatabaseName=""
User=""
Host=""
Password=""

SymmetricSecurityKey="" # Chave de segurança para criação de tokens JWT
JWTTokenDuration=0 # Duração em minutos dos tokens JWT
JWTIssuer="" # Claim do issuer do Token JWT
JWTAudience="" # Claim da audience do Token JWT

ApiKeyPublic="" # Chave API pública do MailJet (Serviço de email usado pela aplicação)
ApiKeyPrivate="" # Chave API privada do MailJet (Serviço de email usado pela aplicação)
Email="" # Email que será usado para enviar as confirmações de recuperação de senha
NomeEmail="" # Nome do email
```

O arquivo .env deve estar localizado dentro da pasta principal da solução, onde estão as pastas que contem os .csproj:
<img src="https://github.com/user-attachments/assets/8c87522e-33c8-4ba4-87eb-3c338e23c59a"  width="300" height="200">

### 4 - Rode as migrations

Abre o menu tools > NuGet Package Manager > Package Manager Console: </br>
<img src="https://github.com/user-attachments/assets/a8c0f4e9-742d-488b-85f9-d4705083e4dd" width="300" height="200">

Após aberto o console, certifique que tanto o console quanto a solução estejam com o projeto "BrewLab.Models":
<img src="https://github.com/user-attachments/assets/fdd972ed-b302-460b-8d9d-5f201bcdc940" width="300" height="200">

Com isso, rode o comando "Update-Database". O banco de dados deve ser automaticamente atualizado com todos os modelos.
Certifique-se de que o banco Postgresql já exista, e que os dados para esse banco estejam presentes no arquivo .env.

### 5 - Inicie o projeto

Após isso, o projeto pode ser rodado através da IDE ao clicar no botão verde no topo da tela, a direita:
<img src="https://github.com/user-attachments/assets/cd2b90e3-7afc-464e-bf4a-731017826382" width="300" height="200">



