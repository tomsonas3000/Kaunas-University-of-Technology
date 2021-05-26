using System;
using System.Collections.Generic;
using System.IO;

namespace Lab1.Step1
{
    class Program
    {
        static void Main(string[] args)
        {
            /*Dog dog = new Dog("Bimas", 1234, 24, 5, "taksas", "Antanas Kavaliauskas", "+37061485555", new DateTime(2018, 7, 24), true);
            Console.WriteLine("{0, -10} {1, 5:d} {2, 5:f} {3, 5:d}, {4, -16} {5, -12}" +
                "{6, 8:Y} {7}", dog.Name, dog.ChipId, dog.Weight, dog.Age, dog.Owner,
                dog.Phone, dog.VaccinationDate, dog.Aggressive);
            //Console.Read();*/
            Program p = new Program();

            List<Dog> dogs = p.ReadDogData();
            p.SaveReportToFile(dogs);

            Console.WriteLine("Kokio amžiaus agresyvius šunis skaičiuoti?");
            int age = int.Parse(Console.ReadLine());
            Console.WriteLine("Agresyvių šunų kiekis: " + p.CountAggressive(dogs, age));

            Console.WriteLine("Kurios veislės šunis filtruoti?");
            string breedToFilter = Console.ReadLine();
            List<Dog> filteredByBreed = p.FilterByBreed(dogs, breedToFilter);
            p.PrintDogNamesToConsole(filteredByBreed);

            Console.WriteLine("Kurios veisles seniausius sunis surasti?");
            breedToFilter = Console.ReadLine();
            List<Dog> oldestDogs = p.FindOldestDogs(dogs, breedToFilter);
            p.PrintDogsToConsole(oldestDogs);

            Console.WriteLine("Visos skirtingos veisles: ");
            foreach(string breeds in p.GetBreeds(dogs))
            {
                Console.WriteLine(breeds);
            }
            //List<string> breeds = p.GetBreeds(dogs);
            Console.WriteLine("Vakcinos galiojimas baigesi: ");
            List<Dog> needVaccines = p.FilterByVaccinationExpired(dogs);
            p.PrintDogsToConsole(needVaccines);

           Console.WriteLine("Populiariausia veisle: " + p.MostPopular(dogs, p.GetBreeds(dogs)));
            Console.WriteLine("Sunys, paskiepyti siais metasi: ");
            List<Dog> vaccined2018 = p.VaccinedLastYear(dogs);
            p.PrintDogsToConsole(vaccined2018);
        }
        List<Dog> ReadDogData()
        {
            List<Dog> dogs = new List<Dog>();

            string[] lines = File.ReadAllLines(@"L1Data.csv");
            foreach (string line in lines)
            {
                string[] values = line.Split(';');
                string name = values[0];
                int chipId = int.Parse(values[1]);
                double weight = Convert.ToDouble(values[2]);
                int age = int.Parse(values[3]);
                string breed = values[4];
                string owner = values[5];
                string phone = values[6];
                DateTime vaccinationDate = DateTime.Parse(values[7]);
                bool aggresive = bool.Parse(values[8]);
                Dog dog = new Dog(name, chipId, weight, age, breed, owner, phone,
                    vaccinationDate, aggresive);
                dogs.Add(dog);
            }
            return dogs;
        }
        void SaveReportToFile(List<Dog> dogs)
        {
            string[] lines = new string[dogs.Count];
            for (int i = 0; i < dogs.Count; i++)
            {
                lines[i] = String.Format("{0}; {1}; {2}; {3}; {4}; {5}; {6}; {7}",
                    dogs[i].Name, dogs[i].ChipId, dogs[i].Weight, dogs[i].Age, dogs[i].Owner,
                    dogs[i].Phone, dogs[i].VaccinationDate, dogs[i].Aggressive);
            }
            File.WriteAllLines(@"L1SavedData.csv", lines);
        }
        void PrintDogsToConsole(List<Dog> dogs)
        {
            foreach (Dog dog in dogs)
            {
                Console.WriteLine("Vardas: {0}\nMikroschemos ID: {1}\nSvoris: {2}\nAmžois: " +
                " {3}\nSavininkas: {4}\nTelefonas: {5}\n.Vakcinacijos data: {6}\n" +
                "Agresyvus: {7}\n", dog.Name, dog.ChipId, dog.Weight, dog.Age, dog.Owner, dog.Phone,
                dog.VaccinationDate, dog.Aggressive);
            }
        }
        int CountAggressive(List<Dog> dogs, int age)
        {
            int counter = 0;
            foreach (Dog dog in dogs)
            {
                if (dog.Aggressive && (dog.Age == age))
                {
                    counter++;
                }
            }
            return counter;
        }
        List<Dog> FilterByBreed(List<Dog> dogs, string breed)
        {
            List<Dog> filtered = new List<Dog>();
            foreach (Dog dog in dogs)
            {
                if (breed.Equals(dog.Breed))
                {
                    filtered.Add(dog);
                }
            }
            return filtered;
        }
        void PrintDogNamesToConsole(List<Dog> dogs)
        {
            foreach (Dog dog in dogs)
            {
                Console.WriteLine("Vardas: {0}", dog.Name);
            }
        }
        int FindOldestDogAge(List<Dog> dogs)
        {
            int maxAge = 0;
            foreach (Dog dog in dogs)
            {
                if (dog.Age > maxAge)
                {
                    maxAge = dog.Age;
                }
            }
            return maxAge;
        }
        List<Dog> FindOldestDogs(List<Dog> dogs, string breed)
        {
            List<Dog> filteredDogs = FilterByBreed(dogs, breed);
            int maxAge = FindOldestDogAge(filteredDogs);

            List<Dog> oldestDogs = new List<Dog>();
            foreach (Dog dog in filteredDogs)
            {
                if (dog.Age == maxAge)
                {
                    oldestDogs.Add(dog);
                }
            }
            return oldestDogs;
        }
        List<string> GetBreeds(List<Dog> dogs)
        {
            List<string> breeds = new List<string>();
            foreach (Dog dog in dogs)
            {
                if (!breeds.Contains(dog.Breed))
                {
                    breeds.Add(dog.Breed);
                }
            }
            return breeds;
        }
        List<Dog> FilterByVaccinationExpired(List<Dog> dogs)
        {
            List<Dog> filtered = new List<Dog>();
            foreach (Dog dog in dogs)
            {
                if (dog.IsVaccinationExpired())
                {
                    filtered.Add(dog);
                }
            }
            return filtered;
        }
        string MostPopular(List<Dog> dogs, List<string> breeds )
        {
            string mostPopularBreed = breeds[0];
            int[] counters = new int[breeds.Count];
            int maxCounter = 0;
            int k=0;
            for (int i = 0; i < breeds.Count; i++)
            {
                foreach (Dog dog in dogs)
                {
                    if (dog.Breed.Equals(breeds[i]))
                    {
                        counters[k]++;
                    }
                }
                if (counters[k] > maxCounter)
                {
                    mostPopularBreed = breeds[i];
                    maxCounter = counters[k];
                }
                k++;
            }
            return mostPopularBreed;
        }
        List<Dog> VaccinedLastYear (List<Dog> dogs)
        {
            List<Dog> vaccined2018 = new List<Dog>();
            DateTime time0 = DateTime.Parse("01/01/2015");
            foreach(Dog dog in dogs) {
                if (dog.VaccinationDate > time0)
                {
                    vaccined2018.Add(dog);
                }
            }
            return vaccined2018;
        }
        

    }
}
