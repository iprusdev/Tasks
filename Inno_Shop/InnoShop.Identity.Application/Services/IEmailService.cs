using System.Threading.Tasks;

namespace InnoShop.Identity.Application.Services
{
    public interface IEmailService
    {
        Task SendEmailAsync(string toEmail, string subject, string messageBody);
    }
}