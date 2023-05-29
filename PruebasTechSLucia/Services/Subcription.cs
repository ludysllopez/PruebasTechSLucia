using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using PruebasTechSLucia.Models;
using System.Net.Http.Headers;
using System.Text.Json;

using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace PruebasTechSLucia.Services
{
	public class Subcription : ISubcription
	{

		public Subcription()
		{

		}
		public async Task<string> createSubscription(subscription datos)
		{
			subscriptionModel subs = MapeoDeSubscription(datos);

			JsonSerializerOptions? jsonSerializeOption = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
			HttpClient? httpClient = new HttpClient();

			httpClient.BaseAddress = new Uri("https://api-uat.kushkipagos.com/subscriptions/v1/card");
			httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			httpClient.DefaultRequestHeaders.Add("Private-Merchant-Id", "fb362956c13b4a6fbe2bd55a1dd73baf");

			HttpResponseMessage salida = await httpClient.PostAsJsonAsync("https://api-uat.kushkipagos.com/subscriptions/v1/card", subs);
			Task<string>? y = salida.Content.ReadAsStringAsync();
			var x = JsonConvert.DeserializeObject<Object>(y.Result);

			return x.ToString();
			//return "Ok";
		}

		public subscriptionModel MapeoDeSubscription(subscription datos)
		{
			return new subscriptionModel
			{
				token = datos.token,
				planName = datos.planName,
				periodicity = datos.periodicity,
				contactDetails = new ContactDetails
				{
					documentNumber = datos.documentNumber,
					documentType = datos.documentType,
					email = datos.email,
					firstName = datos.firstName,
					lastName = datos.lastName,
					phoneNumber = datos.phoneNumber
				},
				amount = new Amount
				{
					subtotalIva = 1,
					subtotalIva0 = 0,
					ice = 0,
					iva = 0.14,
					currency = "USD"
				},
				startDate = DateTime.Today.ToString("yyyy-MM-dd"),
				metadata = new Metadata
				{
					plan = new Plan
					{
						fitness = new Fitness
						{
							cardio = "include",
							rumba = "include",
							pool = "include"
						}
					}
				}
			};
		}
	}	
}
