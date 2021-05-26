using System;
using System.Collections.Generic;
using System.IO;

namespace Lab2.Step1
{
    class Program
    {
        public const int NumberOfBranches = 3;
        public const int MaxNumberOfBreeds = 10;
        static void Main(string[] args)
        {
            Program p = new Program();
            Branch[] branches = new Branch[NumberOfBranches];

            branches[0] = new Branch("Kaunas");
            branches[1] = new Branch("Vilnius");
            branches[2] = new Branch("Šiauliai");

            string[] filePaths =
                Directory.GetFiles(Directory.GetCurrentDirectory(),
                "*csv");
            Console.WriteLine(filePaths);
            foreach (string path in filePaths)
            {
                bool rado = p.ReadDogData(path, branches);
                if (rado == false)
                {
                    Console.WriteLine("Neatpažintas failo {0} miestas", path);
                }
                p.PrintDogsToConsole(branches[0].Dogs);
            }
            /*List<string> breeds = p.GetBreeds(branches[2].Dogs);

            Console.WriteLine("Šiauliuose užregistruotos šunų veislės:");
            for (int i = 0; i< breeds.Count; i++)
            {
                Console.WriteLine(breeds[i]);
            }
            Console.WriteLine("Agresyviausi Kauno šunys: {0}",
                p.CountAggressive(branches[0].Dogs));

            DogsContainer labradorai = new DogsContainer(10);
            labradorai = p.FilterByBreed(branches[0].Dogs, "Labradoro retriveris");

            Console.WriteLine("Kauno labradorai retriveriai ");
            p.PrintDogsToConsole(labradorai);

            Console.WriteLine("Populiariausia(-ios) veislė(-ės) Vilniuje: ");
                p.PrintDogsToConsole(p.GetMostPopularBreed(branches[1].Dogs));

            Console.WriteLine("Dvigubai uzregistruoti sunys");
            Console.WriteLine();
            Console.WriteLine("Vilniuje ir Kaune: ");

            DogsContainer doubleplacedDogs = p.GetDoublePlacedDogs(branches[1],
                branches[0]);
            p.PrintDogsToConsole(doubleplacedDogs);

            Console.WriteLine();
            Console.WriteLine("Sarasas, is vilniaus registro pasalinus besikartojinicius:");
            Console.WriteLine();
            p.RemoveDoublePlacedDogs(branches[1], doubleplacedDogs);
            p.PrintDogsToConsole(branches[1].Dogs);

            Console.WriteLine();
            Console.WriteLine("Surusiuotas Kauno sunu sarasas");

            Console.WriteLine();
            branches[0].Dogs.SortDogs();
            p.PrintDogsToConsole(branches[0].Dogs);*/


        }
        private bool ReadDogData(string file, Branch[] branches)
        {
            string town = null;

            using (StreamReader reader = new StreamReader(@file))
            {
                string line = null;
                line = reader.ReadLine();
                if (line != null)
                {
                    town = line;
                }
               // Console.WriteLine(town);
                Branch branch = GetBranchByTown(branches, town);
                if (branch == null)
                {
                    return false;
                }
                while (null != (line = reader.ReadLine()))
                {
                    string[] values = line.Split(',');
                    string name = values[0];
                    int chipId = int.Parse(values[1]);
                    string breed = values[2];
                    string owner = values[3];
                    string phone = values[4];
                    DateTime vd = DateTime.Parse(values[5]);
                    bool aggressive = bool.Parse(values[6]);

                    Dog dog = new Dog(name, chipId, breed, owner, phone, vd, aggressive);
                    branch.Dogs.AddDog(dog);
                    
                }
                return true;
            }
        }
        //void SaveReportToFile(List<Dog> dogs)
        //{
        //    string[] lines = new string[dogs.Count];
        //    for (int i = 0; i < dogs.Count; i++)
        //    {
        //        lines[i] = String.Format("{0}; {1}; {2}; {3}; {4}; {5}; {6}; {7}",
        //            dogs[i].Name, dogs[i].ChipId, dogs[i].Weight, dogs[i].Age, dogs[i].Owner,
        //            dogs[i].Phone, dogs[i].VaccinationDate, dogs[i].Aggressive);
        //    }
        //    File.WriteAllLines(@"L1SavedData.csv", lines);
        //}
        void PrintDogsToConsole(DogsContainer dogs)
        {
            for (int i =0; i < dogs.Count; i++)
            {
                Console.WriteLine("Nr {0}: {1}", (i + 1), dogs.GetDog(i).ToString());
            }
        }
        private Branch GetBranchByTown(Branch[] branches, string town)
        {
            for (int i = 0; i < NumberOfBranches; i++)
            {
                if (branches[i].Town == town)
                {
                    return branches[i];
                }
            }
            return null;
        }
        private int CountAggressive(DogsContainer dogs)
        {
            int counter = 0;
            for (int i = 0; i < dogs.Count; i++)
            {
                if (dogs.GetDog(i).Aggressive)
                {
                    counter++;
                }
            }
            return counter;
        }
        private DogsContainer FilterByBreed(DogsContainer dogs, string breed)
        {
            DogsContainer filteredDogs = new DogsContainer(Branch.MaxNumberOfDogs);
            for (int i =0; i < dogs.Count; i++)
            {
                if (dogs.GetDog(i).Breed == breed)
                {
                    filteredDogs.AddDog(dogs.GetDog(i));
                }
            }            
            return filteredDogs;
        }
        private DogsContainer GetMostPopularBreed(DogsContainer dogs)
        {
            string popular = "not found";
            int count = 0;
            DogsContainer popularDogs = new DogsContainer(Branch.MaxNumberOfDogs);

            List<string> breeds = GetBreeds(dogs);

            for (int i = 0; i < breeds.Count; i++)
            {
                DogsContainer filtered = FilterByBreed(dogs, breeds[i]);
                if (filtered.Count > count)
                {
                    popular = breeds[i];
                    count = filtered.Count;
                }
            }
            for (int i = 0; i < dogs.Count; i++)
            {
                if (dogs.GetDog(i).Breed == popular)
                {
                    popularDogs.AddDog(dogs.GetDog(i));
                }
            }
            return popularDogs;
        }
        private DogsContainer GetDoublePlacedDogs(Branch branch1, Branch branch2)
        {
            DogsContainer doublePlacedDogs = new DogsContainer(Branch.MaxNumberOfDogs);
            for (int i = 0; i < branch1.Dogs.Count; i++)
            {
                if (branch2.Dogs.Contains(branch1.Dogs.GetDog(i)))
                {
                    doublePlacedDogs.AddDog(branch1.Dogs.GetDog(i));
                }
            }
            return doublePlacedDogs;
        }
        private void RemoveDoublePlacedDogs (Branch branch, DogsContainer doublePlacedDogs)
        {
            for (int i = 0; i < doublePlacedDogs.Count; i++)
            {
                branch.Dogs.RemoveDog(doublePlacedDogs.GetDog(i));
            }
        }
        //void PrintDogNamesToConsole(List<Dog> dogs)
        //{
        //    foreach (Dog dog in dogs)
        //    {
        //        Console.WriteLine("Vardas: {0}", dog.Name);
        //    }
        //}
        //int FindOldestDogAge(List<Dog> dogs)
        //{
        //    int maxAge = 0;
        //    foreach (Dog dog in dogs)
        //    {
        //        if (dog.Age > maxAge)
        //        {
        //            maxAge = dog.Age;
        //        }
        //    }
        //    return maxAge;
        //}
        //List<Dog> FindOldestDogs(List<Dog> dogs, string breed)
        //{
        //    List<Dog> filteredDogs = FilterByBreed(dogs, breed);
        //    int maxAge = FindOldestDogAge(filteredDogs);

        //    List<Dog> oldestDogs = new List<Dog>();
        //    foreach (Dog dog in filteredDogs)
        //    {
        //        if (dog.Age == maxAge)
        //        {
        //            oldestDogs.Add(dog);
        //        }
        //    }
        //    return oldestDogs;
        //}
        List<string> GetBreeds(DogsContainer dogs)
        {
            List<string> breeds = new List<string>();
            for (int i = 0; i < dogs.Count; i++)
            {
                Dog dog = dogs.GetDog(i);
                if (!breeds.Contains(dog.Breed))
                {
                    breeds.Add(dog.Breed);
                }
            }
            return breeds;
        }
        //List<Dog> FilterByVaccinationExpired(List<Dog> dogs)
        //{
        //    List<Dog> filtered = new List<Dog>();
        //    foreach (Dog dog in dogs)
        //    {
        //        if (dog.IsVaccinationExpired())
        //        {
        //            filtered.Add(dog);
        //        }
        //    }
        //    return filtered;
        //}
        //string MostPopular(List<Dog> dogs, List<string> breeds )
        //{
        //    string mostPopularBreed = breeds[0];
        //    int[] counters = new int[breeds.Count];
        //    int maxCounter = 0;
        //    int k=0;
        //    for (int i = 0; i < breeds.Count; i++)
        //    {
        //        foreach (Dog dog in dogs)
        //        {
        //            if (dog.Breed.Equals(breeds[i]))
        //            {
        //                counters[k]++;
        //            }
        //        }
        //        if (counters[k] > maxCounter)
        //        {
        //            mostPopularBreed = breeds[i];
        //            maxCounter = counters[k];
        //        }
        //        k++;
        //    }
        //    return mostPopularBreed;
        //}
        //List<Dog> VaccinedLastYear (List<Dog> dogs)
        //{
        //    List<Dog> vaccined2018 = new List<Dog>();
        //    DateTime time0 = DateTime.Parse("01/01/2015");
        //    foreach(Dog dog in dogs) {
        //        if (dog.VaccinationDate > time0)
        //        {
        //            vaccined2018.Add(dog);
        //        }
        //    }
        //    return vaccined2018;
        //}

        //-------------------------------------------------------------

    }
}
