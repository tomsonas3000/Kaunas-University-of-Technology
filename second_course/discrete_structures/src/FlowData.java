public class FlowData {
    String type;
    String nuts2Load;
    String nuts2Unload;
    double flowTons;
    double flowTonsPerKilometers;

    public FlowData()
    {

    }
    public FlowData(String nuts2Load, String nuts2Unload, String type, double flowTons, double flowTonsPerKilometers)
    {
        this.type = type;
        this.nuts2Load = nuts2Load;
        this.nuts2Unload = nuts2Unload;
        this.flowTons = flowTons;
        this.flowTonsPerKilometers = flowTonsPerKilometers;
    }

    @Override
    public String toString()
    {
        return String.format(nuts2Load, nuts2Unload, type, flowTons, flowTonsPerKilometers);
    }
}
