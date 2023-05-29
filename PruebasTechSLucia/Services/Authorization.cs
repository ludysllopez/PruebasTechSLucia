using Newtonsoft.Json;
using PruebasTechSLucia.Models;
using System.Net.Http.Headers;
using System.Text.Json;

namespace PruebasTechSLucia.Services
{
	public class Authorization : IAuthorization
	{
		public Authorization() {
		} 

		public async Task<string> CreateAuthorization(autorizacion datos)
		{
			autorizacionModel am = mapeoAutorization(datos);			
			
			JsonSerializerOptions? jsonSerializeOption = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
			HttpClient? httpClient = new HttpClient();

			httpClient.BaseAddress = new Uri("https://api-uat.kushkipagos.com/");
			httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			httpClient.DefaultRequestHeaders.Add("Private-Merchant-Id", "fb362956c13b4a6fbe2bd55a1dd73baf");

			HttpResponseMessage salida = await httpClient.PostAsJsonAsync("https://api-uat.kushkipagos.com/card/v1/preAuthorization", am);
			Task<string>? y = salida.Content.ReadAsStringAsync();
			var x = JsonConvert.DeserializeObject<Object>(y.Result);

			return x.ToString(); 
		}

		public autorizacionModel mapeoAutorization(autorizacion datos)
		{
			datos.currency = "USD";
			datos.subtotalIva = 0;
			datos.subtotalIva0 = 10;
			datos.iva = 0;
			datos.ice = 0;
			datos.agenciaDeViaje = 0;
			datos.iac = 0;
			datos.tasaAeroportuaria = 0;
			datos.fullResponse = "v2";

			return  new autorizacionModel
			{
				token= datos.token,
				amount = new AmountA
				{
					currency = datos.currency,
					extraTaxes = new ExtraTaxes
					{
						agenciaDeViaje = datos.agenciaDeViaje,
						iac = datos.iac,
						tasaAeroportuaria = datos.tasaAeroportuaria
					},
					ice= datos.ice,
					iva = datos.iva,
					subtotalIva = datos.subtotalIva,
					subtotalIva0 = datos.subtotalIva0 
				},
				ordenDetails = new OrderDetails
				{
					siteDomain = datos.siteDomain,
					shippingDetails = new ShippingDetails
					{
						address1 = datos.address1,
						name = datos.name,
						city= datos.city,
						country= datos.country,
						phone = datos.phone,
						region = datos.region
					},
					billingDetails = new ShippingDetails
					{
						address1 = datos.address2,
						name = datos.name2,
						city = datos.city2,
						country = datos.country2,
						phone = datos.phone2,
						region = datos.region2
					}				
				},
				fullResponse = datos.fullResponse,
				threeDomainSecure = new ThreeDomainSecure
				{
					cavv = "AAABBoVBaZKAR3BkdkFpELpWIiE=",
					eci = "07",
					xid = "NEpab1F1MEdtaWJ2bEY3ckYxQzE=",
					specificationVersion = "2.2.0",
					acceptRisk = true
					
				}
			};
		}

		public async Task<string> getAuthorization(getAutorizacion datos)
		{
			getAutorizacionModel ga = new getAutorizacionModel
			{
				ticketNumber = datos.ticketNumber,
				amount = new Amount
				{
					currency = datos.currency,
					subtotalIva = datos.subtotalIva,
					iva = datos.iva,
					subtotalIva0 = datos.subtotalIva0,
					ice = datos.ice
				},
				fullResponse = "v2"
			};

			JsonSerializerOptions? jsonSerializeOption = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
			HttpClient? httpClient = new HttpClient();

			httpClient.BaseAddress = new Uri("https://api-uat.kushkipagos.com/");
			httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			httpClient.DefaultRequestHeaders.Add("Private-Merchant-Id", "fb362956c13b4a6fbe2bd55a1dd73baf");

			HttpResponseMessage salida = await httpClient.PostAsJsonAsync("https://api-uat.kushkipagos.com/card/v1/capture", ga);
			Task<string>? y = salida.Content.ReadAsStringAsync();
			var x = JsonConvert.DeserializeObject<Object>(y.Result);

			return x.ToString();
		}
	}
}
