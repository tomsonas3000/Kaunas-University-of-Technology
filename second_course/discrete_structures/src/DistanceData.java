import java.io.BufferedReader;
import java.io.FileReader;
import java.io.IOException;
import java.util.ArrayList;
import java.util.Comparator;
import java.util.List;

public class DistanceData implements Comparable<DistanceData> {

    String key;
    String nuts2Origin;
    String nuts2Destination;
    double distance;
    double time;

    public DistanceData()
    {

    }
    public DistanceData(String key, String nuts2Origin, String nuts2Destination,
                        double distance, double time)
    {
        this.key = key;
        this.nuts2Origin = nuts2Origin;
        this.nuts2Destination = nuts2Destination;
        this.distance = distance;
        this.time = time;
    }

    @Override
    public String toString()
    {
        return String.format(key, nuts2Origin, nuts2Destination, distance, time);
    }

    @Override
    public int compareTo(DistanceData o) {
        if(o.distance > this.distance)
        {
            return 1;
        }
        if(o.distance < this.distance)
        {
            return -1;
        }
        return 0;
    }
    public static Comparator<DistanceData> byDistance = (DistanceData d1, DistanceData d2) -> {
       
        if (d1.distance > d2.distance) {
            return -1;
        }
        if (d1.distance < d2.distance) {
            return +1;
        }
        return 0;
    };
}
