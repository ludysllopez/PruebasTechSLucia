using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PruebasTechSLucia.Models;
using PruebasTechSLucia.Services;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Text.Json;


namespace PruebasTechSLucia.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
		private readonly ISubcription _subcriptionServicio;
		private readonly IAuthorization _authorizationServicio;
		private readonly ITransaction _transactionServicio;

		public HomeController(ILogger<HomeController> logger, ISubcription subs, IAuthorization aut, ITransaction tr)
        {
            _logger = logger;
            _subcriptionServicio = subs;
            _authorizationServicio = aut;
			_transactionServicio = tr;
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
        public async Task<IActionResult> crearSubcripcion(subscription datos)
        {
			try
			{
				var resultado = await _subcriptionServicio.createSubscription(datos);
				return Ok(resultado);
			}
			catch (Exception e)
			{
				throw;
			}			
		}

        [HttpPost]
        public async Task<ActionResult> preAutorizarPago(autorizacion datos)
        {
			try
			{
				var resultado = await _authorizationServicio.CreateAuthorization(datos);
				return Ok(resultado);
			}
			catch (Exception e)
			{
				throw;
			}

			JsonSerializerOptions? jsonSerializeOption = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
            HttpClient? httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://api-uat.kushkipagos.com/card/v1/preAuthorization");
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.DefaultRequestHeaders.Add("Private-Merchant-Id", "037fc0bfe30e411d8c382674c5e7762f");
            HttpResponseMessage salida = await httpClient.PostAsJsonAsync("https://api-uat.kushkipagos.com/card/v1/preAuthorization", datos);
            Task<string>? y = salida.Content.ReadAsStringAsync();
            respuesta? x = JsonConvert.DeserializeObject<respuesta>(y.Result);

            return Json(x);
        }

		[HttpPost]
		public async Task<ActionResult> GetAutorizacion(getAutorizacion datos)
		{
			try
			{                
				var resultado = await _authorizationServicio.getAuthorization(datos);
				return Ok(resultado);
			}
			catch (Exception e)
			{
				throw;
			}			
		}

		

        [HttpPost]
        public async Task<ActionResult> GetSubscripcion(getSubscription subcription)
        {
			JsonSerializerOptions? jsonSerializeOption = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
			HttpClient? httpClient = new HttpClient();

			httpClient.BaseAddress = new Uri("https://api-uat.kushkipagos.com/");
			httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			httpClient.DefaultRequestHeaders.Add("Private-Merchant-Id", "fb362956c13b4a6fbe2bd55a1dd73baf");

			HttpResponseMessage salida = await httpClient.GetAsync("https://api-uat.kushkipagos.com/subscriptions/v1/card/search/"+subcription.subscriptionId);
			Task<string>? y = salida.Content.ReadAsStringAsync();
			var x = JsonConvert.DeserializeObject<Object>(y.Result);

			return Json(x.ToString());
        }

		[HttpPost]
		public async Task<ActionResult> VoidTransaction(voidTransaction tr)
		{
			JsonSerializerOptions? jsonSerializeOption = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
			HttpClient? httpClient = new HttpClient();

			httpClient.BaseAddress = new Uri("https://api-uat.kushkipagos.com/");
			httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			httpClient.DefaultRequestHeaders.Add("Private-Merchant-Id", "fb362956c13b4a6fbe2bd55a1dd73baf");
			//httpClient.DefaultRequestHeaders.Add("ticketNumber", tr.ticketNumber);

			HttpResponseMessage salida = await httpClient.DeleteAsync("https://api-uat.kushkipagos.com/v1/charges/"+ tr.ticketNumber);
			Task<string>? y = salida.Content.ReadAsStringAsync();
			var x = JsonConvert.DeserializeObject<Object>(y.Result);

			return Json(y.Result);
		}

		[HttpPost]
		public async Task<ActionResult> ListTransaction(listTransactionModel lt)
		{
			try
			{
				var resultado = await _transactionServicio.ObtenerListadoTransacciones(lt);
				return Json(resultado);
			}
			catch (Exception e)
			{
				throw;
			}
		}

		

		[HttpPost]
		public ActionResult ConfirmPayment()
		{
			List<respuesta> respuesta = new List<respuesta>();
			foreach (KeyValuePair<string, Microsoft.Extensions.Primitives.StringValues> i in Request.Form)
			{
				respuesta.Add(new respuesta()
				{
					code = i.Key,
					message = i.Value
				});
			}
			return Json(respuesta);
		}


		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}