using Humanizer;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSiteAPI.Application.Abstractions.Service;
using WebSiteAPI.Application.Repositories;
using WebSiteAPI.Domain.Entities;
using MailKit.Net.Smtp;
using MailKit.Security;

namespace WebSiteAPI.Persistence.Services
{
    public class MailService : IMailService
    {
        private readonly IMailReadRepository _readRepository;
        private readonly IMailWriteRepository _writeRepository;

        public MailService(IMailReadRepository readRepository, IMailWriteRepository writeRepository)
        {
            _readRepository = readRepository;
            _writeRepository = writeRepository;
        }

        public async Task CreateAsync(Mail model)
        {
            await _writeRepository.AddAsync(model);
            await _writeRepository.SaveAsync();
            await SendMailAsync(model);
        }

        public Task<List<Mail>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Mail> GetByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public async Task SendMailAsync(Mail model)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Web Site İletişim'den", "website@odayfurniture.com"));
            message.To.Add(new MailboxAddress("Oğulcan Uçar", "info@odayfurniture.com"));
            message.Subject = "Website İletişim'den Mesaj! " + model.Subject ;

            message.Body = new TextPart("plain")
            {
                Text = model.Email + " Bu hesaptan Mail gelmiştir. \n " + model.Body,
            };
            using (var client = new SmtpClient())
            {
                client.ServerCertificateValidationCallback = (s, c, h, e) => true;
                client.Connect("mail.odayfurniture.com", 587, false);
                client.Authenticate("website@odayfurniture.com", "OgulcaN12.!.");
                await client.SendAsync(message);
                client.Disconnect(true);
            }
        }
    }
}








