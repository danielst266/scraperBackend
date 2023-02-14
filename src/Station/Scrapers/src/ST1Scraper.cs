namespace gasStation {
    
    public class ST1Scraper : StationBase, Istation {

        private static readonly string LINK = "https://www.st1.se/fuel/stationPrices/pricesPerStation/";
        private static readonly string FILENAME = "ST1.txt";
        private static readonly string STATION_NAME = "ST1";
        //private List<IFuel>? fuelList;
        private IFuelParser _fuelParser;

        public ST1Scraper () {
            _fuelParser = new ST1FuelParser(LINK,FILENAME);
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