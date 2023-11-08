# TesteKBr
## **processo seletivo Analista Desenvolvedor Back-end Jr**

### Backend do projeto

Para o desenvolvimento da API foram desenvolvidos microserviços que trabalham separadamente para servir o cliente, aqui estão eles listados:
TorneioJJ-Usuarios -> fornece funcionalidades para gerenciar os usuarios, como o CRUD, login, troca e reset de senha.
Esse serviço possui um Controller, onde chegam as requisições e conta com Services que separam as responsabilidades do sistema, como acesso ao
banco de dados, serviço de e-mail e geração de Token.
Foram criados dois serviços: 
- TorneioJJ-Usuarios que controla toda a parte de CRUD de usuarios do sistema; 
- TorneioJJ-Campeonatos, que cuida de toda a parte de cadastro de campeonatos;
  Devem rodar ao mesmo tempo para fornecer todas as funcionalidades desenvolvidas.

# **Importante:**:
Para o envio do link de redefinicao de email, colocar a URL correta em appsettings, na seção:
"PathResetarSenha": {
    "Path": "seu-caminho\\Teste KBR\\TesteKBR-Front\\painel\\painel\\"
  }

# **Informações**:

- Para a base de dados foi utilizado o SQLserver.
- o Banco de dados é gerado através de Migrations usando o conceito de code-first.
- Para gerar a base de dados para rodar o projeto execute Update-Database no "console de gerenciador de pacotes".
- Necessita ter o SQLserver instalado.
- Necessario mudar a string de configuração em appsettings.json de acordo como está configurado o seu SGBD.

