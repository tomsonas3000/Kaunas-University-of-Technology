using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3.Step1
{
    class AnimalsContainer
    {
        private Animal[] Animals;
        public int Count { get; private set; }
        public AnimalsContainer(int size)
        {
            Animals = new Animal[size];
        }
        public void AddAnimal(Animal animal)
        {
            Animals[Count] = animal;
            Count++;
        }
        public void SetAnimal(int index, Animal animal)
        {
            Animals[index] = animal;
        }
        public Animal GetAnimal (int index)
        {
            return Animals[index];
        }
        public void RemoveAnimal(Animal animal)
        {
            int i = 0;
            while (i < Count)
            {
                if (Animals[i].Equals(animal))
                {
                    Count--;
                    for (int j = i; j < Count; j++)
                    {
                        Animals[j] = Animals[j + 1];
                    }
                    break;
                }
                i++;
            }
        }
        public bool Contains(Animal animal)
        {
            return Animals.Contains(animal);
        }
        public void SortAnimals()
        {
            for (int i = 0; i < Count - 1; i++)
            {
                Animal minValueAnimal = Animals[i];
                int minValueIndex = i;
                for (int j = i+1; j < Count; j++)
                {
                    if (Animals[j] <= minValueAnimal)
                    {
                        minValueAnimal = Animals[j];
                        minValueIndex = j;
                    }
                }
                Animals[minValueIndex] = Animals[i];
                Animals[i] = minValueAnimal;
            }
        }
    }
}
