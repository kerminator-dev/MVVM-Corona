using CoronaClient.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoronaClient.Services
{
    public interface ICoronavirusCountryService
    {
        public Task<IEnumerable<CoronavirusCountry>> GetTopCases(int amountOfCountries);
    }
}
