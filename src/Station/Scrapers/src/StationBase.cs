
using System.Text.Json;

namespace gasStation {
        public abstract class StationBase {

        protected List<IFuel>? fuelList;

        protected IFuel extractFuelData(List<String> fuelData) {
            
            return new FuelBase();
        }

        protected IFuel ExtractFuelData(HtmlFuelInfo info ) {
            
            var fuel = new FuelBase();

            var tuple = info._toParseOrder.Zip(info._toParse);

            foreach (var items in tuple) {
                
                switch(items.First) {
                    case HtmlPattern.FUELTYPE:
                        var fuelType = HtmlToData.ToFuelType(items.Second);
                        fuel.FuelType = fuelType;
                        fuel.UnitType = HtmlToData.ToUnitType(fuelType);
                        break;
                    case HtmlPattern.PRICE:
                        fuel.Price = HtmlToData.ToDouble(items.Second);
                        break;
                    case HtmlPattern.MANNED:
                        break;
                    case HtmlPattern.SKIP:
                        break;
                    case HtmlPattern.DATE:
                        fuel.Date = HtmlToData.ToDate(items.Second, items.Second);
                        break;
                }

            }

            return fuel;
        }

        public string jsonData() {
            return JsonSerializer.Serialize(fuelList);
        }


    }

}


