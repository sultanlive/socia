using System.Threading.Tasks;

namespace SocioLiftPay.Services
{
    public interface IPayService
    {
        Task<string> Pay(string first, string last, string iin, float sum, string fio, string school, string backUrl, string requestUrl);
    }
}
