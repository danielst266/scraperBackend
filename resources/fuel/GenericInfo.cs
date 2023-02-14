using System.Text.Json.Serialization;

namespace gasStation {
    
    

    public class GenericInfo : IRetailInfo {
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Retailer Retailer {get;}

        public GenericInfo(Retailer retailer) {
            this.Retailer = retailer;
        }

        public override string ToString() {
            return Retailer.ToString();
        }

    }

}