using System;
using System.Net.Http;
using System.Text;
using System.Text.Encodings;
using System.Text.Json;
using System.Security.Claims;
using System.Text.Json.Serialization;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions;
using Microsoft.Extensions.Logging;
using BlazorApp.Shared;

namespace BlazorApp.Api
{
    public static class WeatherForecastFunction
    {
        private static string GetSummary(int temp)
        {
            var summary = "Mild";

            if (temp >= 32)
            {
                summary = "Hot";
            }
            else if (temp <= 16 && temp > 0)
            {
                summary = "Cold";
            }
            else if (temp <= 0)
            {
                summary = "Freezing";
            }

            return summary;
        }

        [FunctionName("WeatherForecast")]
        public static IActionResult Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req, ILogger log)
        {

            
            // var header = req.Headers["x-ms-client-principal"];
            // var data = header.Value[0];
            // var decoded = Convert.FromBase64String(data);
            // var json = System.Text.ASCIIEncoding.ASCII.GetString(decoded);
            // var principal = JsonSerializer.Deserialize<ClientPrincipal>(json);

            //  principal.UserRoles = principal.UserRoles.Except(new string[] { "anonymous" }, StringComparer.CurrentCultureIgnoreCase);
  
            // if (!principal.UserRoles.Any())
            // {
            //     //return new ClaimsPrincipal();
            // }
    
            // var identity = new ClaimsIdentity(principal.IdentityProvider);
            // identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, principal.UserId));
            // identity.AddClaim(new Claim(ClaimTypes.Name, principal.UserDetails));
            // identity.AddClaims(principal.UserRoles.Select(r => new Claim(ClaimTypes.Role, r)));
    

            var randomNumber = new Random();
            var temp = 0;

            var result = Enumerable.Range(1, 12).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = temp = randomNumber.Next(-20, 55),
                Summary = GetSummary(temp)
            }).ToArray();

            return new OkObjectResult(result);
        }
    }
}
