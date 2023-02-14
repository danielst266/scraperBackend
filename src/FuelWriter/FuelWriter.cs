namespace gasStation
{

    public static class FuelPrinter
    {
        public static void PrintFuels(List<IFuel>? fuelList)
        {
            if (fuelList is null)
            {
                System.Console.WriteLine("Fuel not yet processed");
            }
            else
            {

                foreach (var fuel in fuelList)
                {
                    System.Console.WriteLine(fuel.ToString());
                }
            }
        }

        public static void PrintFuels(List<IFuel>? fuelList, string station)
        {
            if (fuelList is null)
            {
                System.Console.WriteLine("Fuel not yet processed");
            }
            else
            {

                foreach (var fuel in fuelList)
                {
                    System.Console.WriteLine(fuel.ToString() + "station: " + station);
                }
            }
        }


        public static void printJson(List<IFuel>? fuelList) {

        }

    }

}