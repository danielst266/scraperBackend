using System.Text.Json;
using System.Text.RegularExpressions;
using HtmlAgilityPack;
namespace gasStation {

    public class PreemScraper : StationBase, Istation {

        private static readonly string LINK = "https://www.preem.se/privat/drivmedel/drivmedelspriser/";
        private static readonly string FILENAME = "Preem.txt";
        private static readonly string STATION_NAME = "Preem";
        //private List<IFuel>? fuelList;
        private IFuelParser _fuelParser;

        public PreemScraper() {
            _fuelParser = new PreemFuelParser(LINK, FILENAME);
            
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