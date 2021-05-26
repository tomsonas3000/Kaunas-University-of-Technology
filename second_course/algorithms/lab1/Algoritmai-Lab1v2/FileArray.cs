using System;
using System.Text;
using System.IO;


namespace Algoritmai_Lab1v2
{
    class FileArray
    {
        public BinaryWriter FileBinaryWriter { get; set; }
        public BinaryReader FileBinaryReader { get; set; }
        public int Length { get; set; }

        public FileArray(string fileName, int n, Random rnd)
        {
            FileStream fs = new FileStream(fileName, FileMode.Create, FileAccess.ReadWrite);
            FileBinaryWriter = new BinaryWriter(fs, Encoding.Unicode);
            FileBinaryReader = new BinaryReader(fs);
            Length = n;
            for(int i = 0; i < n; i++)
            {
                Data dataElement =
                    new Data(Program.CreateString(4, rnd), (float)rnd.NextDouble());
                FileBinaryWriter.Write(dataElement.TextElement);
                FileBinaryWriter.Write(dataElement.FloatElement);
                FileBinaryWriter.Flush();
            }
        }
        public FileArray()
        {

        }

        public Data GetData(int i)
        {
            int k = i * 12;
            FileBinaryReader.BaseStream.Seek(k + i + 1, SeekOrigin.Begin);
            Data dataToReturn = new Data(Encoding.Unicode.GetString(FileBinaryReader.ReadBytes(8)), FileBinaryReader.ReadSingle());
            return dataToReturn;

        }

        public void SetData(int i, Data data)
        {
            int k = i * 12;
            FileBinaryWriter.BaseStream.Seek(k + i, SeekOrigin.Begin);
            FileBinaryWriter.Write(data.TextElement);
            FileBinaryWriter.Write(data.FloatElement);
            FileBinaryWriter.Flush();                
        }

        public void Print(int fromWhich)
        {
            for(int i = fromWhich; i < Length; i++)
                Console.WriteLine(this.GetData(i));
        }
        public void Print()
        {
            for (int i = 0; i < Length; i++)
                Console.WriteLine(this.GetData(i));
        }

        public void HeapSort()
        {
            int n = this.Length;

            for (int i = n / 2 - 1; i >= 0; i--)
            {
                Heapify(this, n, i);
            }
            for (int i = n - 1; i >= 0; i--)
            {
                Data temp = this.GetData(0);
                this.SetData(0, this.GetData(i));
                this.SetData(i, temp);

                Heapify(this, i, 0);
            }
        }

        private void Heapify(FileArray Array, int n, int i)
        {
            int largest = i;
            int left = 2 * i;
            int right = 2 * i + 1;

            if (left < n && (Array.GetData(left).CompareTo(Array.GetData(largest)) > 0))
                largest = left;

            if (right < n && (Array.GetData(right).CompareTo(Array.GetData(largest)) > 0))
                largest = right;

            if (largest != i)
            {
                Data swap = Array.GetData(i);
                Array.SetData(i, Array.GetData(largest));
                Array.SetData(largest, swap);

                Heapify(Array, n, largest);
            }

        }
    }
}
