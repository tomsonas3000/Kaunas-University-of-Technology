using System;
using System.Collections.Generic;
using System.Linq;


namespace Algoritmai_Lab1v2


{
    class Program
    {
        static void Main(string[] args)
        {
            Benchmark();

        }

        public static string CreateString(int stringLength, Random rd)
        {
            const string allowedChars = "ABCDEFGHJKLMNOPQRSTUVWXYZ";
            char[] chars = new char[stringLength];

            for (int i = 0; i < stringLength; i++)
            {
                chars[i] = allowedChars[rd.Next(0, allowedChars.Length)];
            }

            return new string(chars);
        }
        static void ArrayHeapSort(Data[] DataArray)         
        {
            int n = DataArray.Length;                       
                                
            for(int i = n/2 - 1; i >= 0; i--)                     
            {
                HeapifyArray(DataArray, n, i);                      
            }
            for(int i = n - 1; i >= 0; i--)
            {
                Data temp = DataArray[0];
                DataArray[0] = DataArray[i];
                DataArray[i] = temp;

                HeapifyArray(DataArray, i, 0);
            }
        }

        static void HeapifyArray(Data[] DataArray, int n, int i)
        {
            int largest = i;
            int left = 2 * i;
            int right = 2 * i + 1;

            if (left < n && (DataArray[left].CompareTo(DataArray[largest]) > 0))
                largest = left;

            if (right < n && (DataArray[right].CompareTo(DataArray[largest]) > 0))
                largest = right;

            if(largest != i)
            {
                Data swap = DataArray[i];
                DataArray[i] = DataArray[largest];
                DataArray[largest] = swap;

                HeapifyArray(DataArray, n, largest);
            }

        }

        static void ListHeapSort(List<Data> DataList)
        {
            int n = DataList.Count;

            for (int i = n / 2 - 1; i >= 0; i--)
            {
                HeapifyList(DataList, n, i);
            }

            for (int i = n - 1; i >= 0; i--)
            {
                Data temp = DataList[0];
                DataList[0] = DataList[i];
                DataList[i] = temp;

                HeapifyList(DataList, i, 0);
            }

        }
        static void HeapifyList(List<Data> DataList, int n, int i)
        {
            int largest = i;
            int left = 2 * i + 1;
            int right = 2 * i + 2;

            if (left < n && (DataList[left].CompareTo(DataList[largest]) > 0))
                largest = left;

            if (right < n && (DataList[right].CompareTo(DataList[largest]) > 0))
                largest = right;

            if (largest != i)
            {
                Data swap = DataList[i];
                DataList[i] = DataList[largest];
                DataList[largest] = swap;

                HeapifyList(DataList, n, largest);
            }
        }
        static void Benchmark()
        {
            int[] counts = { 10, 1500, 3000, 4500, 6000, 7500, 9000, 10500, 12000, 13500, 15000, 16500, 18000, 19500, 22000 };
            int seed = (int)DateTime.Now.Ticks & 0x0000FFFF;
            Random rnd = new Random(seed);
            Console.WriteLine("List benchmark results \n");
            BenchmarkListRAM(counts, rnd);
            Console.WriteLine("Array RAM benchmark results \n");
            BenchmarkArrayRAM(counts, rnd);  
            Console.WriteLine("Array FILE benchmark results \n");
            BenchmarkArrayFile(counts, rnd);
        }

        static void BenchmarkListRAM(int[] counts, Random rnd)
        {
            Console.WriteLine("{0,21} {1,7} {2, -1}", "| Count of elements |", "Time", " |");
            for (int i = 0; i < counts.Length; i++)
            {
                List<Data> ListToSort = FillList(counts[i], rnd);
                var timer = System.Diagnostics.Stopwatch.StartNew();
                ListHeapSort(ListToSort);
                timer.Stop();
                var timePassed = timer.ElapsedMilliseconds;
                Console.WriteLine("{0,2} {1,18} {2,10}", "| " , counts[i] + "|", timePassed + "|");
            }
        }

        static List<Data> FillList(int n, Random rnd)
        {
            List<Data> ListToReturn = new List<Data>();
            for(int i = 0; i < n; i++)
            {
                Data dataElement = new Data(Program.CreateString(4, rnd), (float)rnd.NextDouble());
                ListToReturn.Add(dataElement);
            }
            
            return ListToReturn;
        }
        static void BenchmarkArrayRAM(int[] counts, Random rnd)
        {
            Console.WriteLine("{0,21} {1,7} {2, -1}", "| Count of elements |", "Time", " |");
            for (int i = 0; i < counts.Length; i++)
            {
                Data[] ArrayToSort = FillArray(counts[i], rnd);
                var timer = System.Diagnostics.Stopwatch.StartNew();
                ArrayHeapSort(ArrayToSort);
                timer.Stop();
                var timePassed = timer.ElapsedMilliseconds;
                Console.WriteLine("{0,2} {1,18} {2,10}", "| ", counts[i] + "|", timePassed + "|");
            }
        }

        static Data[] FillArray(int n, Random rnd)
        {
            Data[] ArrayToReturn = new Data[n];
            for (int i = 0; i < n; i++)
            {
                Data dataElement = new Data(Program.CreateString(4, rnd), (float)rnd.NextDouble());
                ArrayToReturn[i] = dataElement;
            }
            return ArrayToReturn;
        }
        static void BenchmarkArrayFile(int [] counts, Random rnd)
        {
            Console.WriteLine("{0,21} {1,7} {2, -1}", "| Count of elements |", "Time", " |");
            for (int i = 0; i < counts.Length; i++)
            {
                FileArray ArrayToSort = new FileArray("file_array.txt", counts[i], rnd);
                var timer = System.Diagnostics.Stopwatch.StartNew();
                ArrayToSort.HeapSort();
                timer.Stop();
                var timePassed = timer.ElapsedMilliseconds;
                Console.WriteLine("{0,2} {1,18} {2,10}", "| ", counts[i] + "|", timePassed + "|");
                ArrayToSort.FileBinaryReader.Close();
                ArrayToSort.FileBinaryWriter.Close();
            }
        }
        static void PrintArray(Data[] Array, int fromWhich)
        {

            for(int i = fromWhich; i < Array.Length; i++)
            {
                Console.WriteLine(Array[i]);
            }
        }
        static void PrintArray(Data[] Array)
        {

            for (int i = 0; i < Array.Length; i++)
            {
                Console.WriteLine(Array[i]);
            }
        }
        static void PrintList(List<Data> List, int fromWhich)
        {
            for(int i = fromWhich; i < List.Count; i++)
            {
                Console.WriteLine(List.ElementAt(i));
            }
        }
        static void PrintList(List<Data> List)
        {
            foreach(Data data in List)
            {
                Console.WriteLine(data);
            }
        }
    }
}
