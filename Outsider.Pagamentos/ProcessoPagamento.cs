using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Outsider.Pagamentos
{
    public class ProcessoPagamento : IProcessoPagamento
    {
        public string CriarPagamento(float valor)
        {
            StripeConfiguration.ApiKey = Environment.GetEnvironmentVariable("SK_Stripe");
            try
            {
                var service = new PaymentIntentService();
                var options = new PaymentIntentCreateOptions
                {
                    Amount = Convert.ToInt32(valor * 100),
                    Currency = "brl",
                    PaymentMethodTypes = new List<string>
                    {
                        "card",
                    },
                    Description = "Compra na Outsider",
                };
                var payment = service.Create(options);               
                return payment.ClientSecret;
            }
            catch (StripeException e)
            {
                Console.WriteLine($"Erro do Stripe: {e.StripeError.Message}");
                return "";
            }
            catch (Exception e)
            {
                Console.WriteLine($"Erro geral: {e.Message}");
                return "";
            }

        }
    }
}
