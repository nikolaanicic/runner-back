using Contracts.Dtos.Email;
using System.Threading.Tasks;

namespace Contracts.Services
{
    public interface IEmailService
    {
        Task SendEmail(Message message);
    }
}
