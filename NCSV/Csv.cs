using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NCSV
{
    public class Csv
    {
        public IReadOnlyDictionary<string, int> HeaderIndecies { get; }
        public IReadOnlyList<string> Headers { get; }
        public IReadOnlyList<IReadOnlyList<string>> Data { get; }

        public Csv(StreamReader reader)
        {
            Headers = CsvFormat.DecodeRow(reader.ReadLine()).ToArray();
            var data = new List<IReadOnlyList<string>>();
            while(!reader.EndOfStream)
            {
                data.Add(CsvFormat.DecodeRow(reader.ReadLine()).ToArray());
            }
            Data = data;

            HeaderIndecies = Enumerable.Range(0, Headers.Count).ToDictionary(index => Headers[index], index => index);
        }

        public string GetValue(int rowHandle, string column)
        {
            return Data[rowHandle][HeaderIndecies[column]];
        }
    }
}
