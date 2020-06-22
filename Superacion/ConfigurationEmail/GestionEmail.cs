using Microsoft.EntityFrameworkCore;
using MimeKit;
using Superacion.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ItlaTwitter.Action
{
    public class GestionEmail
    {
        private ItlaTwitter4Context _context = new ItlaTwitter4Context();

        public void sendEmail(string Correo, string NameUsuario, string Clave)
        {

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("itlaprueba2@gmail.com"));
            message.To.Add(new MailboxAddress(Correo));
            message.Subject = "Nueva Contraseña";
            message.Body = new TextPart("html")
            {

                Text = "Saludos <strong>" + NameUsuario + "</strong> hemos recibido una solicitud de parte suya para restablecer la contraseña.<br><br> Hemos generado la siguiente clave para usted <strong>" + Clave + "</strong>"

            };

            using (var client = new MailKit.Net.Smtp.SmtpClient())
            {
                client.Connect("smtp.gmail.com", 465, true);
                client.Authenticate("itlaprueba2@gmail.com", "#wakandaForever1");
                client.Send(message);
                client.Disconnect(false);


            }

            _context.Database.ExecuteSqlCommand("UPDATE Usuario SET Clave = '" + Clave + "' WHERE Usuario = '" + NameUsuario + "'");

        }

        public void sendEmailAccount(string Correo,  string NameUsuario, int? Id)
        {

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("itlaprueba2@gmail.com"));
            message.To.Add(new MailboxAddress(Correo));
            message.Subject = "Nueva Contraseña";
            message.Body = new TextPart("html")
            {

                Text = "Bienevido a ItlaTwitter <strong>" + NameUsuario + "</strong>. Debes hacer click en este link para activar tu cuenta:<br><br><strong>http://localhost:52525/Principal/ConfirmAccount/"+Id+"</strong>"

            };

            using (var client = new MailKit.Net.Smtp.SmtpClient())
            {
                client.Connect("smtp.gmail.com", 465, true);
                client.Authenticate("itlaprueba2@gmail.com", "#wakandaForever1");
                client.Send(message);
                client.Disconnect(false);


            }

     }


        public string randomClave(int Countcaracteres) {


            //Caracteres opcionales: abcdefghijklmnopqrstuvwxyz
            string Caracteres = "ABCDEFGHJKLMNOPQRSTUVWXYZ0123456789_";
            Random random = new Random();

            char[] chars = new char[Countcaracteres];

            for (int i = 0; i < Countcaracteres; i++)
            {
                chars[i] = Caracteres[random.Next(0, Caracteres.Length)];
            }

            return new string(chars);

        
        }


    }
}