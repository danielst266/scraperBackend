using System.Text.RegularExpressions;
using HtmlAgilityPack;
namespace gasStation
{

    public class CircleKScraper: StationBase, Istation
    {

        private static readonly string LINK = "https://www.circlek.se/drivmedel/drivmedelspriser";
        private static readonly string FILENAME = "CircleK.txt";
        private static readonly string STATION_NAME = "CircleK";
        //private List<IFuel>? fuelList;
        private IFuelParser _fuelParser;


        public CircleKScraper()
        {
            _fuelParser = new CircleKFuelParser(LINK, FILENAME);
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