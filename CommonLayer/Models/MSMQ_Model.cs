namespace CommonLayer.Models
{
    using Experimental.System.Messaging;
    using System;
    using System.IdentityModel.Tokens.Jwt;
    using System.Linq;
    using System.Net;
    using System.Net.Mail;

    public class MSMQ_Model
    {
        MessageQueue messageQueue = new MessageQueue();
        public void MSMQSender(string token)
        {
            messageQueue.Path = @".\private$\Token";//for windows path

            if (!MessageQueue.Exists(messageQueue.Path))
            {

                MessageQueue.Create(messageQueue.Path);

            }
            messageQueue.Formatter = new XmlMessageFormatter(new Type[] { typeof(string) });
            messageQueue.ReceiveCompleted += MessageQueue_ReceiveCompleted;
            messageQueue.Send(token);
            messageQueue.BeginReceive();
            messageQueue.Close();
        }

        private void MessageQueue_ReceiveCompleted(object sender, ReceiveCompletedEventArgs e)
        {
            var message = messageQueue.EndReceive(e.AsyncResult);
            string token = message.Body.ToString();
            string Subject = "Fundoo Notes Reset Link";
            string Body = "Dear Sir/mam! Please copy the token provided for Resetting your Password:" + token;
            string JWT = DecodeJWT(token);
            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential("mohsinzahoor386@gmail.com", "Mohsin@123"),
                EnableSsl = true,
            };
            smtpClient.Send("mohsinzahoor386@gmail.com", JWT, Subject, Body);
            messageQueue.BeginReceive();
        }
        public string DecodeJWT(string token)
        {
            try
            {
                var DecodeToken = token;
                var handler = new JwtSecurityTokenHandler();
                var jsonToken = handler.ReadJwtToken((DecodeToken));
                var result = jsonToken.Claims.FirstOrDefault().Value;
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}