namespace gasStation {

    public class HtmlFuelInfo {
        public List<string> _toParse {get;set;} = default!;
        public  bool _manned {get;set;}
        public List<HtmlPattern> _toParseOrder {get;set;} = default!;
        public string Format {get;set;} = default!;
        public IRetailInfo _retailInfo {get;set;} = default!;
        public string _address {get;set;} = default!;
        public Retailer _retailer {get;set;} = default!;
        public HtmlFuelInfo () {
        }

        
    }

    public enum HtmlPattern {
        PRICE,
        DATE,
        FUELTYPE,
        SKIP,
        CHANGE,
        MANNED

    }

    public class HtmlFuelInfoBuilder {

        private HtmlFuelInfo current = new HtmlFuelInfo();
        
        public static HtmlFuelInfoBuilder Start() {
            return new HtmlFuelInfoBuilder();
        }
        public HtmlFuelInfoBuilder IsManned(bool manned) {
            current._manned = manned;
            return this;
        }

        public HtmlFuelInfoBuilder ParseLine(List<string> toParse) {
            current._toParse = toParse;
            return this;
        }

        public HtmlFuelInfoBuilder ParseOrder(List<HtmlPattern> toParseOrder) {
            current._toParseOrder = toParseOrder;
            return this;
        }

        public HtmlFuelInfoBuilder DateFormat(string dateFormat) {
            current.Format = dateFormat;
            return this;
        }

        public HtmlFuelInfoBuilder RetailType(IRetailInfo retailInfo) {
            current._retailInfo = retailInfo;
            return this;
        }
        public HtmlFuelInfoBuilder Address(string address) {
            current._address = address;
            return this;
        }
        public HtmlFuelInfoBuilder Retailer(Retailer retailer) {
            current._retailer = retailer;
            return this;
        }
        

        public HtmlFuelInfo Build() {
            return current;
        }

    }


}