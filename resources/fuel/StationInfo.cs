using System.Text.Json.Serialization;

namespace gasStation {

    public class StationInfo : IRetailInfo {
        public string Address {get;} = default!;
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Retailer Retailer {get;}
        public StationInfo(string Address, Retailer Retailer) {
            this.Address = Address;
            this.Retailer = Retailer;
        }
        
        public override string ToString() {
            return $"{Address}, {Retailer.ToString()}";
        }

    }


}