import java.io.BufferedReader;
import java.io.FileReader;
import java.io.IOException;
import java.util.ArrayList;
import java.util.List;

public class Execution {
    public static void main(String[] args)
    {
        long a = System.nanoTime();
        List<DistanceData> allDistanceData = parseData("C:\\Users\\tomas\\OneDrive - Kaunas University of Technology\\ANTRAS KURSAS\\Diskreciosios strukturos\\Projektas\\distanceData.csv");
        //List<FlowData> allFlowData = parseData("C:\\Users\\tomas\\OneDrive - Kaunas University of Technology\\ANTRAS KURSAS\\Diskreciosios strukturos\\Projektas\\flowData.csv");
        allDistanceData.sort(DistanceData.byDistance);
        List<DistanceData> lithuaniaList = getByCountry("LT", allDistanceData);
        for(DistanceData d : lithuaniaList)
            System.out.println(d.toString());
    }

    public static List<DistanceData> getByCountry(String countryCode, List<DistanceData> initialData)
    {
        List<DistanceData> countryList = new ArrayList<>();
        for(DistanceData d : initialData)
        {
            if(d.nuts2Origin.contains(countryCode))
            {
                countryList.add(d);
            }

        }
        return countryList;
    }
    public static List<DistanceData> parseData(String csvFile) {
        List<DistanceData> Data = new ArrayList<DistanceData>();
        String line = "";
        String csvSplitBy = ",";

        try (BufferedReader br = new BufferedReader(new FileReader(csvFile))) {
            while ((line = br.readLine()) != null) {
                String[] dataDistance = line.split(csvSplitBy);
                DistanceData newData = new DistanceData(dataDistance[0], dataDistance[1],
                        dataDistance[2], Double.valueOf(dataDistance[3]), Double.valueOf(dataDistance[4]));
                Data.add(newData);
            }
        } catch (IOException e) {
            e.printStackTrace();
        }
        return Data;
    }
}


