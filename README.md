# TesteKBr
## **processo seletivo Analista Desenvolvedor Back-end Jr**

### Backend do projeto

Para o desenvolvimento da API foram desenvolvidos microserviços que trabalham separadamente para servir o cliente, aqui estão eles listados:
TorneioJJ-Usuarios -> fornece funcionalidades para gerenciar os usuarios, como o CRUD, login, troca e reset de senha.
Esse serviço possui um Controller, onde chegam as requisições e conta com Services que separam as responsabilidades do sistema, como acesso ao
banco de dados, serviço de e-mail e geração de Token;

- Para a base de dados foi utilizado o SQLserver.
- o Banco de dados é gerado através de Migrations usando o conceito de code-first.
- Para gerar a base de dados para rodar o projeto execute Update-Database no "console de gerenciador de pacotes".
- Necessita ter o SQLserver instalado.
- Necessario mudar a string de configuração em appsettings.json de acordo como está configurado o seu SGBD.

