using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Documental.Global
{
    public static class Archivos
    {
        public static List<string[]> parseCSV(string path)
        {
            List<string[]> parsedData = new List<string[]>();

            using (StreamReader readFile = new StreamReader(path))
            {
                string line;
                string[] row;

                while ((line = readFile.ReadLine()) != null)
                {
                    row = line.Split(',');
                    parsedData.Add(row);
                }
            }
            return parsedData;
        }


    }
}
