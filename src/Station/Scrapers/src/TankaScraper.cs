using System.Text.RegularExpressions;
using HtmlAgilityPack;
namespace gasStation {

    public class TankaScraper : StationBase, Istation {

        private static readonly string LINK = "https://tanka.se/api/prices/single";
        private static readonly string FILENAME = "Tanka.txt";
        private static readonly string STATION_NAME = "Tanka";
        //private List<IFuel>? fuelList;
        private IFuelParser _fuelParser;


        public TankaScraper() {
            _fuelParser = new TankaFuelParser(LINK, FILENAME);
    
        }

        
        public List<IFuel> fetchData() {
        
            fuelList = _fuelParser.ParseFuelData() ?? new List<IFuel>();
        
            return fuelList;

        }

        public void printCurrentFuels() {
            FuelPrinter.PrintFuels(fuelList, STATION_NAME);
        }

    }
    
}