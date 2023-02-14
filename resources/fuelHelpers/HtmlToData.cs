using System.Linq;
using System.Text.RegularExpressions;
using System.Globalization;

namespace gasStation {

    public static class HtmlToData {

        private static Regex _priceMatcher = new Regex(@"(\d+[,.]\d+)");
        
        
        public static FuelType ToFuelType(string parse) {
            
            var smth = FuelPatterns.PATTERNS.Where( x => x.Key.Any( s => s.IsMatch(parse) )).First().Value;
            return smth;
        }   

        public static UnitType ToUnitType(FuelType type) {

            var smth = FuelPatterns.FUEL2UNIT[type];
            return smth;
        }
        public static double ToDouble(string parse) {
            
            return Double.Parse(_priceMatcher.Match(parse).Value.Replace(',', '.'), System.Globalization.CultureInfo.InvariantCulture);

        }
        public static DateTime ToDate(string parse, string format) {
        
            return DateTime.ParseExact(parse, format, CultureInfo.InvariantCulture, DateTimeStyles.AllowWhiteSpaces);

        }
        
    }
    

}