using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PruebasTechSLucia.Models;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Text.Json;

namespace PruebasTechSLucia.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult>Index()
        {
           return View();
       
        }

        public IActionResult Actions()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> crearSubcripcion()
        {
            subscription sub = new subscription()
            {
                token = "gV3ox6100000sAxClU033646vnnJsT83",
                planName = "Premium",
                periodicity = "monthly",
                ContactDetails = new contactDetails()
                {
                    documentType = "CC",
                    documentNumber = "1009283738",
                    email = "test@test.com",
                    firstName = "Diego",
                    lastName = "Cadena",
                    phoneNumber = "+593988734644"
                },
                Amount = new amount()
                {
                    subtotalIva = 1,
                    subtotalIva0 = 0,
                    ice = 0,
                    iva = 0.14,
                    currency = "USD"
                },
                startDate = "2018-09-25",
                Metadata = new metadata()
                {
                    Plan = new plan()
                    {
                        Fitness = new fitness()
                        {
                            cardio = "include",
                            rumba = "include",
                            pool = "include"
                        }
                    }
                }
            };
            JsonSerializerOptions? jsonSerializeOption = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
            HttpClient? httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://api-uat.kushkipagos.com/subscriptions/v1/card");
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.DefaultRequestHeaders.Add("Private-Merchant-Id", "037fc0bfe30e411d8c382674c5e7762f");
            HttpResponseMessage salida = await httpClient.PostAsJsonAsync("https://api-uat.kushkipagos.com/subscriptions/v1/card", sub);
            Task<string>? y = salida.Content.ReadAsStringAsync();
            respuesta? x = JsonConvert.DeserializeObject<respuesta>(y.Result);

            return Json(x);
        }

        [HttpPost]
        public async Task<ActionResult> preAutorizarPago(List<string> tok)
        {
            autorizacion aut = new autorizacion()
            {
                token = "QZPnSP1000000b3MG3062555GhIrcYt5",                
                amount = new amount()
                {
                    subtotalIva = 0,
                    subtotalIva0 = 600,
                    ice = 0,
                    iva = 0,
                    currency = "PEN"
                },
                fullResponse = true
            };
            JsonSerializerOptions? jsonSerializeOption = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
            HttpClient? httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://api-uat.kushkipagos.com/card/v1/preAuthorization");
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.DefaultRequestHeaders.Add("Private-Merchant-Id", "037fc0bfe30e411d8c382674c5e7762f");
            HttpResponseMessage salida = await httpClient.PostAsJsonAsync("https://api-uat.kushkipagos.com/card/v1/preAuthorization", aut);
            Task<string>? y = salida.Content.ReadAsStringAsync();
            respuesta? x = JsonConvert.DeserializeObject<respuesta>(y.Result);

            return Json(x);
        }

        [HttpPost]
        public ActionResult ConfirmPayment()
        {
            List<respuesta> respuesta = new List<respuesta>();
            foreach(KeyValuePair<string, Microsoft.Extensions.Primitives.StringValues> i in Request.Form)
            {
                respuesta.Add(new respuesta()
                {
                    code = i.Key,
                    message = i.Value
                });
            }
            return Json(respuesta);
        }

        [HttpPost]
        public async Task<ActionResult> GetSubscription(String subcription)
        {
            using (HttpClient? httpClient = new HttpClient())
            {
                string? url = "https://api-uat.kushkipagos.com/subscriptions/v1/card/search/1574693127852000";
                JsonSerializerOptions? jsonSerializeOption = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
                httpClient.BaseAddress = new Uri("https://api-uat.kushkipagos.com/subscriptions/v1/card/search");
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                httpClient.DefaultRequestHeaders.Add("Private-Merchant-Id", "037fc0bfe30e411d8c382674c5e7762f");            
                HttpResponseMessage salida = await httpClient.GetAsync(url);
                

                Task<string>? y = salida.Content.ReadAsStringAsync();
                respuesta? x = JsonConvert.DeserializeObject<respuesta>(y.Result);

                return Json(x);
            }
           
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}