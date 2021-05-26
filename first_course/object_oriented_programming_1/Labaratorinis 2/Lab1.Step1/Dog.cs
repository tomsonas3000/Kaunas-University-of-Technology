using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2.Step1
{
    class Dog
    {
        private const int VaccinationDuration = 1;

        public string Name { get; set; }
        public int ChipId { get; set; }
        public double Weight { get; set; }
        public int Age { get; set; }
        public string Breed { get; set; }
        public string Phone { get; set; }
        public string Owner { get; set; }
        public DateTime VaccinationDate { get; set; }
        public bool Aggressive { get; set; }

        public Dog(string name, int chipId, double weight, int age, string breed, string owner, string phone, DateTime vaccinationDate, bool aggressive)
        {
            Name = name;
            ChipId = chipId;
            Weight = weight;
            Age = age;
            Breed = breed;
            Owner = owner;
            Phone = phone;
            VaccinationDate = vaccinationDate;
            Aggressive = aggressive;
        }
        public Dog(string name, int chipId, string breed, string owner, string phone, DateTime vaccinationDate, bool aggressive)
        {
            Name = name;
            ChipId = chipId;
            Breed = breed;
            Owner = owner;
            Phone = phone;
            VaccinationDate = vaccinationDate;
            Aggressive = aggressive;
        }
        public bool IsVaccinationExpired()
        {
            return VaccinationDate.AddYears(VaccinationDuration).CompareTo(DateTime.Now) < 0;
        }
        public override string ToString()
        {
            return String.Format("ChipId: {0,5}, Name: {1,10}, Owner: {2, 16}" +
                "({3}), Last vaccination date: {4:yyyy-MM-dd}", ChipId, Name,
                Owner, Phone, VaccinationDate);
        }
        public override bool Equals(object obj)
        {
            return this.Equals(obj as Dog);
        }
        public bool Equals(Dog dog)
        {
            if (Object.ReferenceEquals(dog, null))
            {
                return false;
            }
            if (this.GetType() != dog.GetType())
            {
                return false;
            }
            return (ChipId == dog.ChipId) && (Name == dog.Name);
        }
        public override int GetHashCode()
        {
            return ChipId.GetHashCode() ^ Name.GetHashCode();
        }
        public static bool operator ==(Dog lhs, Dog rhs)
        {
            if (Object.ReferenceEquals(lhs, null))
            {
                if (Object.ReferenceEquals(rhs, null))
                {
                    return true;
                }
                return false;
            }
            return lhs.Equals(rhs);
        }
        public static bool operator !=(Dog lhs, Dog rhs)
        {
            return !(lhs == rhs);
        }
        public static bool operator <=(Dog lhs, Dog rhs)
        {
            return (lhs.ChipId <= rhs.ChipId);
        }
        public static bool operator >=(Dog lhs, Dog rhs)
        {
            return (lhs.ChipId >= rhs.ChipId);
        }
    }
}
