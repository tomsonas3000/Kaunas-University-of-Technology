using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3.Step1
{
    class Cat : Animal
    {
        private static int VaccinationDurationMonths = 6;
        public Cat(string name, int chipId, string breed, string owner,
            string phone, DateTime vaccinationDate) :
            base(name, chipId, breed, owner, phone, vaccinationDate)
        {

        }
        public override bool isVaccinationExpired()
        {
            return VaccinationDate.AddMonths(VaccinationDurationMonths).
                CompareTo(DateTime.Now) > 0;
        }
        public override string ToString()
        {
            return String.Format("ChipId: {0, -5} Breed: {1, -20} Name:" +
                "{2, -10} Owner: {3, -10} ({4}) Last vaccination date:" +
                "{5:yyyy-MM-dd}", ChipId, Breed, Name, Owner, Phone,
                VaccinationDate);
        }
        public override bool Equals(object obj)
        {
            return this.Equals(obj as Cat);
        }
        public bool Equals(Cat cat)
        {
            return base.Equals(cat);
        }
        public override int GetHashCode()
        {
            return ChipId.GetHashCode() ^ Name.GetHashCode();
        }
        public static bool operator ==(Cat lhs, Cat rhs)
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
        public static bool operator != (Cat lhs, Cat rhs)
        {
            return !(lhs == rhs);
        }
    }
}
