using System;

namespace Algoritmai_Lab1v2
{
    class Data : IComparable<Data>
    {
        public string TextElement { get; set; }
        public float FloatElement { get; set; }

        public Data(string text, float number)
        {
            TextElement = text;
            FloatElement = number;
        }

        public override string ToString()
        {
            return TextElement + " " + FloatElement.ToString();
        }

        public int CompareTo(Data data)
        {
            if(this.TextElement.CompareTo(data.TextElement) > 0)
                return 1;
            if (this.TextElement.CompareTo(data.TextElement) < 0)
                return -1;
            if (this.TextElement.CompareTo(data.TextElement) == 0)
            {
                if (this.FloatElement > data.FloatElement)
                    return 1;
                if (this.FloatElement < data.FloatElement)
                    return -1;
            }
            return 0;
                
        }
    }
}
