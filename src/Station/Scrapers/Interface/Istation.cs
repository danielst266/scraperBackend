namespace gasStation {

    interface Istation {
        abstract public List<IFuel> fetchData();
        abstract public void printCurrentFuels();

        
    }
    
}