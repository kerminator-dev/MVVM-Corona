using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoronaClient.Models
{
    public class CoronavirusCountry
    {
        /// <summary>
        /// Название страны
        /// </summary>
        public string CountryName { get; set; }

        /// <summary>
        /// Кол-во случаев
        /// </summary>
        public int CaseCount { get; set; }

        /// <summary>
        /// Ссылка на флаг страны
        /// </summary>
        public string FlagUri { get; set; }
    }
}
