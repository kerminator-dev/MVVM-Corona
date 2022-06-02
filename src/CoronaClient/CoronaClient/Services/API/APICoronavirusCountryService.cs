using CoronaClient.Models;
using CoronaClient.Services.API.Models;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace CoronaClient.Services.API
{
    public class APICoronavirusCountryService : ICoronavirusCountryService
    {
        private const string REQUEST_URI = "https://corona.lmao.ninja/v3/covid-19/countries?sort=cases";
        public async Task<IEnumerable<CoronavirusCountry>> GetTopCases(int amountOfCountries)
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage apiResponse = await client.GetAsync(REQUEST_URI);

                string jsonResponce = await apiResponse.Content.ReadAsStringAsync();

                var jsonSerializeOptions = new JsonSerializerOptions()
                {
                    PropertyNameCaseInsensitive = true
                };

                List<APICoronavirusCountry> apiCountries = JsonSerializer.Deserialize<List<APICoronavirusCountry>>(jsonResponce, jsonSerializeOptions);

                if (apiCountries == null)
                    return new List<CoronavirusCountry>();

                return apiCountries.Take(amountOfCountries).Select(apiCountry => new CoronavirusCountry()
                {
                    CountryName = apiCountry.Country,
                    CaseCount = apiCountry.Cases,
                    FlagUri = apiCountry.CountryInfo.Flag
                });
            }
        }
    }
}
