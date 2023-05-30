using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PruebasTechSLucia.Models;
using System.Dynamic;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Web.Script.Serialization;

namespace PruebasTechSLucia.Services
{
	public class Transaction : ITransaction
	{
		public Transaction() { }
		public async Task<string> ObtenerListadoTransacciones(listTransactionModel lt)
		{

			string f1 = lt.from.ToString("yyyy-MM-ddTHH:mm:ss");
			string f2 = lt.to.ToString("yyyy-MM-ddTHH:mm:ss");
			JsonSerializerOptions? jsonSerializeOption = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
			HttpClient? httpClient = new HttpClient();

			httpClient.BaseAddress = new Uri("https://api-uat.kushkipagos.com/");
			httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			httpClient.DefaultRequestHeaders.Add("Private-Merchant-Id", "fb362956c13b4a6fbe2bd55a1dd73baf");

			var uriBuilder = new UriBuilder("https://api-uat.kushkipagos.com/analytics/v1/transactions-list");
			uriBuilder.Query = "from="+f1+"&to="+f2;
			string url = uriBuilder.ToString();

			HttpResponseMessage? salida = await httpClient.GetAsync(url);
			Task<string>? y = salida.Content.ReadAsStringAsync();
			var x = JsonConvert.DeserializeObject<Object>(y.Result);

			return x.ToString();
		}
	}
}
