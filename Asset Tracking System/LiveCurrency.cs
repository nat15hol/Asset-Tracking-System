using System;
using System.Net;
using System.Xml;
using System.Globalization;
using System.Xml.Serialization;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Reflection.Metadata;
using System.Data.SqlTypes;

namespace WeeklyMiniProject3
{
    public class LiveCurrency // Class that handles fetching the exchange rates and converting currencies
    {
        private static List<CurrencyObj> currencyList = new List<CurrencyObj>();

        public static void FetchRates()
        {
            string url = "https://www.ecb.europa.eu/stats/eurofxref/eurofxref-daily.xml"; // Exchange rate XML document

            XmlTextReader reader = new XmlTextReader(url);
            while (reader.Read()) // Goes through the XML document and saves the currency exchange rates to the local list
            {
                if (reader.NodeType == XmlNodeType.Element)
                {
                    while (reader.MoveToNextAttribute()) 
                    {
                        if (reader.Name == "currency") // Identifies each currency attribute and saves the currency code and rate as an object
                        {
                            string currencyCode = reader.Value;

                            reader.MoveToNextAttribute();
                            double rate = double.Parse(reader.Value);
                            currencyList.Add(new CurrencyObj(currencyCode, rate));
                        }
                    }
                }
            }
        }

        public static double Convert(double input, string fromCurrency, string toCurrency) // Method that uses the fetched rates to convert between the given rates via Euro
        {
            double value = 0;
            
            if (fromCurrency == "EUR")
            {
                value = input * currencyList.Find(c => c.CurrencyCode == toCurrency).ExchangeRateFromEUR;
            }
            else if (toCurrency == "EUR")
            {
                value = input / currencyList.Find(c => c.CurrencyCode == fromCurrency).ExchangeRateFromEUR;
            }
            else
            {
                value = input / currencyList.Find(c => c.CurrencyCode == fromCurrency).ExchangeRateFromEUR;
                value *= currencyList.Find(c => c.CurrencyCode == toCurrency).ExchangeRateFromEUR;
            }

            return value;
        }
    }
}