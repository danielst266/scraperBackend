using HtmlAgilityPack;
using System.Text.RegularExpressions;
using System.Text.Json.Serialization;
using System.Text.Json;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace gasStation {

    public interface IFuelParser {
        
        public abstract List<IFuel> ParseFuelData();
    }
    
    public abstract class FuelParserBase : IFuelParser {
        public abstract List<IFuel> ParseFuelData();
        protected string? _HtmlParsed;
        protected HtmlAgilityPack.HtmlDocument _htmlDoc = new HtmlDocument();
        protected FuelParserBase(string link, string fileName) {
            _HtmlParsed = HtmlLoader.loadHtmlString(link, fileName);
            _htmlDoc = new HtmlDocument();
            _htmlDoc.Load(fileName);
        }
        protected IFuel ExtractFuelData(HtmlFuelInfo info) {
            
            var fuel = new FuelBase();

            var tuple = info._toParseOrder.Zip(info._toParse);
            fuel.Manned = info._manned;
            //fuel.RetailInfo = info._retailInfo;
            fuel.Address = info._address;
            fuel.Retailer = info._retailer;
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
                        fuel.Date = HtmlToData.ToDate(items.Second, info.Format);
                        break;
                }

            }

            return fuel;
        }
    }

    public class OKQ8FuelParser : FuelParserBase {
        private string HtmlXPath = "//table/tbody/tr";
        public OKQ8FuelParser(string link, string fileName) : base(link, fileName) {
        }
        public override List<IFuel> ParseFuelData() {

            var fuels = new List<IFuel>();
            var htmlNodes = _htmlDoc!.DocumentNode!.SelectNodes(HtmlXPath);

            foreach (var tdnode in htmlNodes) {
                
                List<string> inner = new List<string>(tdnode.InnerText.Split("\n")).Where( x => !String.IsNullOrWhiteSpace(x)).ToList();
                
                var fuelInfoManned = HtmlFuelInfoBuilder.Start().IsManned(true).ParseLine(inner).DateFormat("yyyy-mm-dd").Retailer(Retailer.OKQ8).
                    ParseOrder(new List<HtmlPattern> {HtmlPattern.FUELTYPE, HtmlPattern.PRICE, HtmlPattern.SKIP, HtmlPattern.CHANGE, HtmlPattern.DATE}).
                    Build();

                var fuelInfoUnmanned = HtmlFuelInfoBuilder.Start().IsManned(false).ParseLine(inner).DateFormat("yyyy-mm-dd").Retailer(Retailer.OKQ8).
                    ParseOrder(new List<HtmlPattern> {HtmlPattern.FUELTYPE, HtmlPattern.SKIP, HtmlPattern.PRICE, HtmlPattern.CHANGE, HtmlPattern.DATE}).
                    Build();

                // fuels.Concat(ExtractFuelData(fuelInfoManned));
                fuels.Add(ExtractFuelData(fuelInfoManned));
                fuels.Add(ExtractFuelData(fuelInfoUnmanned));

            }

            var fuelList = fuels.GroupBy(fuel => (fuel.FuelType, fuel.Manned)).Select(g => g.OrderByDescending(p => p.Price).Reverse().First()).ToList();
        
            return fuelList;
        }
    }

    public class IngoFuelParser : FuelParserBase {
        private string HtmlXPath = "//table/tbody/tr";
        public IngoFuelParser(string link, string fileName) : base(link, fileName) {
        }
        public override List<IFuel> ParseFuelData() {

            var fuels = new List<IFuel>();
            var htmlNodes = _htmlDoc!.DocumentNode!.SelectNodes(HtmlXPath);

            foreach (var tdnode in htmlNodes) {
                
                List<string> inner = new List<string>(tdnode.InnerText.Split("\n")).Where( x => !String.IsNullOrWhiteSpace(x)).ToList();
                inner[2] = Regex.Match(inner[2], "([0-9]+-[0-9]+-[0-9]+)").Value;
                var fuelInfoUnmanned = HtmlFuelInfoBuilder.Start().IsManned(true).ParseLine(inner).DateFormat("yyyy-MM-dd").Retailer(Retailer.INGO).
                    ParseOrder(new List<HtmlPattern> {HtmlPattern.FUELTYPE, HtmlPattern.PRICE, HtmlPattern.DATE}).
                    Build();


                fuels.Add(ExtractFuelData(fuelInfoUnmanned));

            }

            var fuelList = fuels.GroupBy(fuel => (fuel.FuelType, fuel.Manned)).Select(g => g.OrderByDescending(p => p.Price).Reverse().First()).ToList();
        
            return fuelList;
        }
    }

    public class PreemFuelParser : FuelParserBase {

        private string HtmlXPath = "//table/tbody/tr";
        
        public PreemFuelParser(string link, string fileName) : base(link, fileName) {
        }

        public override List<IFuel> ParseFuelData() {

            var htmlNodes = _htmlDoc!.DocumentNode!.SelectNodes(HtmlXPath);

            var fuels = new List<IFuel>();

            foreach (var tdnode in htmlNodes) {
                
                List<string> inner = new List<string>(tdnode.InnerText.Split("\n")).Where( x => !String.IsNullOrWhiteSpace(x)).ToList();
                
                var fuelInfoManned = HtmlFuelInfoBuilder.Start().IsManned(true).ParseLine(inner).DateFormat("yy-MM-dd").Retailer(Retailer.PREEM).
                    ParseOrder(new List<HtmlPattern> {HtmlPattern.FUELTYPE, HtmlPattern.PRICE,HtmlPattern.DATE}).
                    Build();

                // fuels.Concat(ExtractFuelData(fuelInfoManned));
                fuels.Add(ExtractFuelData(fuelInfoManned));

            }

            var fuelList = fuels.GroupBy(fuel => (fuel.FuelType, fuel.Manned)).Select(g => g.OrderByDescending(p => p.Price).Reverse().First()).ToList();
        
            return fuelList;
        }
    }

    public class CircleKFuelParser : FuelParserBase {

        private string HtmlXPath1 = "/html/body/div[2]/main/div/div/article/div/div[2]/div/div[2]/div/div[4]/div/div/table/tbody/tr";
        private string HtmlXPath2 = "/html/body/div[2]/main/div/div/article/div/div[3]/div/div[2]/div/div[1]/div/div/table/tbody/tr";
        public CircleKFuelParser(string link, string fileName) : base(link, fileName) {
        }

        public override List<IFuel> ParseFuelData() {

            var htmlNodes1 = _htmlDoc!.DocumentNode!.SelectNodes(HtmlXPath1);

            var fuels = new List<IFuel>();

            foreach (var tdnode in htmlNodes1) {
                
                List<string> inner = new List<string>(tdnode.InnerText.Split("\n")).Where( x => !String.IsNullOrWhiteSpace(x)).ToList();
                inner[2] = Regex.Match(inner[2], "([0-9]+-[0-9]+-[0-9]+)").Value;
                var fuelInfoManned = HtmlFuelInfoBuilder.Start().IsManned(true).ParseLine(inner).DateFormat("yyyy-MM-dd").Retailer(Retailer.CIRCLEK).
                    ParseOrder(new List<HtmlPattern> {HtmlPattern.FUELTYPE, HtmlPattern.PRICE,HtmlPattern.DATE, HtmlPattern.SKIP, HtmlPattern.CHANGE}).
                    Build();

                // fuels.Concat(ExtractFuelData(fuelInfoManned));
                fuels.Add(ExtractFuelData(fuelInfoManned));

            }

            var htmlNodes2 = _htmlDoc!.DocumentNode!.SelectNodes(HtmlXPath2);

            foreach (var tdnode in htmlNodes2) {
                
                List<string> inner = new List<string>(tdnode.InnerText.Split("\n")).Where( x => !String.IsNullOrWhiteSpace(x)).ToList();
                inner[2] = Regex.Match(inner[2], "([0-9]+-[0-9]+-[0-9]+)").Value;
                var fuelInfoUnManned = HtmlFuelInfoBuilder.Start().IsManned(false).ParseLine(inner).DateFormat("yyyy-MM-dd").Retailer(Retailer.CIRCLEK).
                    ParseOrder(new List<HtmlPattern> {HtmlPattern.FUELTYPE, HtmlPattern.PRICE,HtmlPattern.DATE, HtmlPattern.SKIP, HtmlPattern.CHANGE}).
                    Build();

                // fuels.Concat(ExtractFuelData(fuelInfoManned));
                fuels.Add(ExtractFuelData(fuelInfoUnManned));

            }

            var fuelList = fuels.GroupBy(fuel => (fuel.FuelType, fuel.Manned)).Select(g => g.OrderByDescending(p => p.Price).Reverse().First()).ToList();
        
            return fuelList;
        }
    }

    public class TankaFuelParser : IFuelParser {

        protected string? _HtmlParsed;
        public TankaFuelParser(string link, string fileName) {
            _HtmlParsed = HtmlLoader.loadHtmlString(link, fileName);
        }
        public List<IFuel> ParseFuelData() {

            var fuels = new List<IFuel>();
            
            //var fuelData = JsonSerializer.Deserialize<Dictionary<string, object>>(_HtmlParsed);
            
            var strings = new List<List<string>>();

            var fuelList = fuels.GroupBy(fuel => (fuel.FuelType, fuel.Manned)).Select(g => g.OrderByDescending(p => p.Price).Reverse().First()).ToList();
        
            return fuelList;
        }
    }

    public class ST1FuelParser : FuelParserBase, IFuelParser {

        protected string jsonData = default!;
        protected HtmlAgilityPack.HtmlDocument _htmlDoc = new HtmlDocument();

        public ST1FuelParser(string link, string fileName) : base(link, fileName) {
            jsonData = HtmlLoader.loadHtmlString(link, fileName);
            _htmlDoc = new HtmlDocument();
            _htmlDoc.Load(fileName);
        }
        public override List<IFuel> ParseFuelData() {
            
            var regex = new Regex("(\\d+-\\d+-\\d+)");

            var fuels = new List<IFuel>();

            JObject jdata = JObject.Parse(jsonData);
            JToken JTdate = jdata["data"]["updated_at"];

            string date = regex.Match(JTdate.Value<string>()).Value; 

            IList<JToken> results = jdata["data"]!["station_prices"]!.Children().ToList();
            
            IList<jsonFuelData> searchResults = new List<jsonFuelData>();

            
            foreach (JToken result in results) {
                
                jsonFuelData fuelData = result.First.ToObject<jsonFuelData>()!;

                foreach (var data in fuelData.prices ) {

                    var inner = new List<string>() {data.Key , data.Value["price_with_tax"], date};
                    var fuelInfo = HtmlFuelInfoBuilder.Start().IsManned(false).ParseLine(inner).DateFormat("yyyy-MM-dd").Retailer(Retailer.ST1).Address(fuelData.name).
                    ParseOrder(new List<HtmlPattern> {HtmlPattern.FUELTYPE, HtmlPattern.PRICE, HtmlPattern.DATE}).
                    Build();

                    fuels.Add(ExtractFuelData(fuelInfo));

                }
                
            }

            //var fuelList = fuels.GroupBy(fuel => (fuel.FuelType, fuel.Manned)).Select(g => g.OrderByDescending(p => p.Price).Reverse().First()).ToList();
        
            return fuels;
        }

        private class jsonFuelData  {
            public string name { get; set; }
            public Dictionary<string,Dictionary<string,string>> prices { get; set; }
        }


            
        
    }

}