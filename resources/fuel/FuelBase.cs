using System.Text.Json;
using System.Text.Json.Serialization;

namespace gasStation {
    
    
    public class FuelBase : IFuel {
        public bool Manned {get; set;} = true;
        public double Price {get;set;}
        
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public FuelType FuelType {get;set;}

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public UnitType UnitType {get;set;}
        public DateTime Date {get;set;}
        //public IRetailInfo RetailInfo {get;set;} = default!;
        public string Address {get;set;} = default!;

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Retailer Retailer {get;set;} = default!;
        //public string GetRetailInfo() {
        //    return RetailInfo.ToString();
        //}

        public override string ToString() {
            return $"Fuel: {FuelType.ToString()} Price: {Price} Manned: {Manned} ";
        }

        public string jsonString() {

            return JsonSerializer.Serialize(this);
            
        }
    }
    
}