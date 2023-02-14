using System.Text.Json.Serialization;

namespace gasStation {


    public interface IFuel {
        bool Manned {get; set;}
        double Price {get;set;}
        [JsonConverter(typeof(JsonStringEnumConverter))]
        FuelType FuelType {get;set;}
        [JsonConverter(typeof(JsonStringEnumConverter))]
        UnitType UnitType {get;set;}
        public DateTime Date {get;set;}
        //public IRetailInfo RetailInfo {get;set;}
        //public abstract string GetRetailInfo();
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Retailer Retailer {get;set;}
        public string Address {get;set;}

        public abstract string ToString(); 

        public abstract string jsonString(); 
        
    }



}