using System.Threading.Tasks;

namespace SocioLiftPay.Services
{
    public interface IPayService
    {
        Task<string> Pay(string desc, float sum, string backUrl, string requestUrl);
    }
}
