using System.Text.RegularExpressions;

namespace gasStation {
    
    
    public class FuelPatterns {
        public static readonly Dictionary<List<Regex>, FuelType> PATTERNS = new Dictionary<List<Regex>, FuelType>{
            [new List<Regex>{ new Regex("95")}] = FuelType.GAS95,
            [new List<Regex>{ new Regex("98")}] = FuelType.GAS98,
            [new List<Regex>{ new Regex("HVO100"), new Regex("HVO")}] = FuelType.HVO100,
            [new List<Regex>{ new Regex("[Dd]iesel")}] = FuelType.DIESEL,
            [new List<Regex>{ new Regex("Ethanol"), new Regex("Etanol"), new Regex("[eE]85")}] = FuelType.ETHANOL,
            [new List<Regex>{ new Regex("AdBlue")}] = FuelType.ADBLUE,
            [new List<Regex>{ new Regex("Biogas"), new Regex("[Ff]ordongsas")}] = FuelType.BIOGAS,
            [new List<Regex>{ new Regex("150 kW")}] = FuelType.ELECTRIC150,
            [new List<Regex>{ new Regex("50 kW")}] = FuelType.ELECTRIC50,
            [new List<Regex>{ new Regex("Fordonsgas")}] = FuelType.CNG,
        };

        public static readonly Dictionary<FuelType, UnitType> FUEL2UNIT = new Dictionary<FuelType, UnitType> {
            [FuelType.ADBLUE] = UnitType.LITER,
            [FuelType.GAS95]  = UnitType.LITER,
            [FuelType.GAS98]  = UnitType.LITER,
            [FuelType.DIESEL] = UnitType.LITER,
            [FuelType.BIOGAS] = UnitType.KG,
            [FuelType.ELECTRIC150] = UnitType.KWH,
            [FuelType.ELECTRIC50] = UnitType.KWH,
            [FuelType.ETHANOL] = UnitType.LITER,
            [FuelType.HVO100] = UnitType.LITER,
            [FuelType.CNG] = UnitType.KG,
        };
    }
    
    


}