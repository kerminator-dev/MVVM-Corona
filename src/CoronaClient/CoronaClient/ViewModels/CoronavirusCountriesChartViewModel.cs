using CoronaClient.Models;
using CoronaClient.Services;
using LiveCharts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace CoronaClient.ViewModels
{
    public class CoronavirusCountriesChartViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Ограничитель - кол-во стран
        /// </summary>
        private const int AMOUNT_OF_COUNTRIES = 10;
        private readonly ICoronavirusCountryService _coronavirusCountryService;
        private ChartValues<int> _coronavirusCountryCaseCounts;
        private string[] _coronavirusCountryNames;

        public event PropertyChangedEventHandler? PropertyChanged;

        // Используются две параллельные коллекции (CoronavirusCountryCaseCounts и массив CoronavirusCountryNames),
        // т. к. отдельные коллекции нужны для работы графика
        public ChartValues<int> CoronavirusCountryCaseCounts
        {
            get => _coronavirusCountryCaseCounts;
            private set
            {
                _coronavirusCountryCaseCounts = value;

                OnPropertyChanged(nameof(CoronavirusCountryCaseCounts));
            }
        }

        public string[] CoronavirusCountryNames
        {
            get => _coronavirusCountryNames;
            private set
            {
                _coronavirusCountryNames = value;

                OnPropertyChanged(nameof(CoronavirusCountryNames));
            }
        }

        public CoronavirusCountriesChartViewModel(ICoronavirusCountryService coronavirusCountryService)
        {
            _coronavirusCountryService = coronavirusCountryService;
        }

        public static CoronavirusCountriesChartViewModel LoadViewModel(ICoronavirusCountryService coronavirusCountryService, Action<Task> onLoaded = null)
        {
            CoronavirusCountriesChartViewModel viewModel = new CoronavirusCountriesChartViewModel(coronavirusCountryService);

            viewModel.Load().ContinueWith(t => onLoaded?.Invoke(t));

            return viewModel;
        }

        public async Task Load()
        {
            IEnumerable<CoronavirusCountry> countries = await _coronavirusCountryService.GetTopCases(AMOUNT_OF_COUNTRIES);

            CoronavirusCountryCaseCounts = new ChartValues<int>
            (
                countries.Select(c => c.CaseCount)
            );

            CoronavirusCountryNames = countries.Select(c => c.CountryName).ToArray();
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
