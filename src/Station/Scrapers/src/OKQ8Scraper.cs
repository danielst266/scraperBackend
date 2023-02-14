using System.Text.RegularExpressions;
using HtmlAgilityPack;
using System.Text.Json;
namespace gasStation
{

    public class OKQ8Scraper : StationBase, Istation
    {

        private static readonly string LINK = "https://www.okq8.se/pa-stationen/drivmedel/";
        private static readonly string FILENAME = "OKQ8.txt";
        private static readonly string STATION_NAME = "OKQ8";
        //private List<IFuel>? fuelList;
        private IFuelParser _fuelParser;

        
        public OKQ8Scraper()
        {
            _fuelParser = new OKQ8FuelParser(LINK, FILENAME);
        }


        public List<IFuel> fetchData()
        {
            fuelList = _fuelParser.ParseFuelData() ?? new List<IFuel>();
            return fuelList;

        }

        public void printCurrentFuels()
        {
            FuelPrinter.PrintFuels(fuelList, STATION_NAME);
        }

        

    }

}