using System.Text.RegularExpressions;
using HtmlAgilityPack;
namespace gasStation
{

    public class IngoScraper: StationBase, Istation
    {

        private static readonly string LINK = "https://www.ingo.se/v%C3%A5ra-l%C3%A5ga-priser/v%C3%A5ra-l%C3%A5ga-priser/aktuella-listpriser";
        private static readonly string FILENAME = "Ingo.txt";
        private static readonly string STATION_NAME = "Ingo";
        //private List<IFuel>? fuelList;
        private IFuelParser _fuelParser;


        public IngoScraper()
        {
            _fuelParser = new IngoFuelParser(LINK, FILENAME);
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