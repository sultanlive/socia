using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;
using Wooppay;

namespace SocioLiftPay.Services
{
    public class PayService : IPayService
    {
        private readonly XmlControllerPortTypeClient _client;
        private readonly IConfiguration _configuration;

        public PayService(IConfiguration configuration)
        {
            _client = new XmlControllerPortTypeClient();
            _configuration = configuration;
        }

        public async Task<string> Pay(string first, string last, string iin, float sum, string fio, string school, string backUrl, string requestUrl)
        {
            string username = _configuration["Wooppay:Username"];
            string password = _configuration["Wooppay:Password"];

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                throw new ArgumentException("Нет данных о платеже.");
            }

            if (string.IsNullOrEmpty(first) || string.IsNullOrEmpty(last) || string.IsNullOrEmpty(iin))
            {
                throw new ArgumentException("Нет данных о пользователе.");
            }

            if (sum <= 0)
            {
                throw new ArgumentException("Сумма оплаты должна быть больше нуля.");
            }

            var description = $"Имя: {first}. Фамилия: {last}. ИИН: {iin}. ФИО Ученика: {fio}. Школа: {school}";
            var creditination = await Login(username, password);

            var result = await _client.cash_createInvoiceAsync(new CashCreateInvoiceRequest()
            {
                referenceId = Guid.NewGuid().ToString(),
                amount = sum,
                backUrl = backUrl,
                requestUrl = requestUrl,
                deathDate = DateTime.UtcNow.AddHours(6).AddMinutes(20).ToString("yyyy-MM-dd HH:mm:ss"),
                description = description,
                addInfo = description
            });

            return result.response.operationUrl;
        }

        private Task<CoreLoginResponse> Login(string login, string password)
        {
            return _client.core_loginAsync(new CoreLoginRequest
            {
                username = login,
                password = password
            });
        }
    }
}
