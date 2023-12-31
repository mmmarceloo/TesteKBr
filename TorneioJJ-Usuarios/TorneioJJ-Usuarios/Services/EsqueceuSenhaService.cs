﻿using MimeKit;
using System.Net;
using System.Net.Mail;
using TorneioJJ_Usuarios.Context;

namespace TorneioJJ_Usuarios.Services
{
    public class EsqueceuSenhaService
    {
        private readonly GerarTokenService _gerarTokenService;
        private readonly IConfiguration _configuration;
        public EsqueceuSenhaService(GerarTokenService gerarTokenService, IConfiguration configuration)
        {
            _gerarTokenService = gerarTokenService;
            _configuration = configuration;
        }

        public bool EnviarEmailRedefinicaoSenha(string email, int id)
        {
            string token = _gerarTokenService.GenerateResetPasswordToken(id);
            bool resposta = EnviaEmail(email, token);
            if(resposta)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool EnviaEmail(string email, string resetToken)
        {
            try
            {
                var mail = new MailMessage();
                mail.From = new MailAddress(_configuration["EmailConfig:Username"]);
                mail.To.Add(email);
                mail.Subject = "Redefinição de senha";
                string resetLink = $"file:///{_configuration["PathResetarSenha:Path"].Replace("\\", "/")}/reset-senha.html?token={resetToken}";
                mail.Body = $"Clique aqui para redefinir sua senha: <a href='{resetLink}'>{resetLink}</a>";
                mail.IsBodyHtml = true;

                SmtpClient smtpClient = new SmtpClient();
                smtpClient.Host = _configuration["EmailConfig:SmtpServer"];
                smtpClient.Port = int.Parse(_configuration["EmailConfig:Port"]);
                smtpClient.EnableSsl = bool.Parse(_configuration["EmailConfig:EnableSsl"]);
                smtpClient.Timeout = int.Parse(_configuration["EmailConfig:Timeout"]);
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential(_configuration["EmailConfig:Username"], _configuration["EmailConfig:Password"]);
                smtpClient.Send(mail);
                smtpClient.Dispose();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public int RecuperaId(string token)
        {
            return _gerarTokenService.DecodeJwtToken(token);

        }
    }
}
