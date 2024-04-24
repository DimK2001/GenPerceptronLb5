using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAl
{
    public class Gen
    {
        public double Value;
        public List<bool> Exons = new List<bool>();
        public Gen(List<bool> exons = null)
        {
            Exons = exons ?? new List<bool>();
        }
        public void SetValue(List<bool> exons)
        {
            Exons = exons;
        }
        public int[] GetInts(double min, double max)
        {
            int[] output = new int[25];
            for (int k = 0; k < 25; ++k)
            {
                var exons = Exons.Skip(k * 16).Take(16).ToList();
                string gray = "";
                exons.ForEach(item =>
                {
                    gray += item ? '1' : '0';
                });
                int n = gray.Count();
                string binary = "";
                binary += gray[0];
                for (int i = 1; i < n; i++)
                {
                    if (gray[i] == '0')
                        binary += binary[i - 1];
                    else
                    {
                        if (binary[i - 1] == '0')
                            binary += '1';
                        else
                            binary += '0';
                    }
                }
                double dec = Convert.ToInt32(binary, 2);
                int ret = Convert.ToInt32(min + dec * (max - min) / Math.Pow(2, exons.Count) - 1);
                output[k] = ret;
            }
            return output;
        }
        public double GetDouble(double min, double max)
        {
            string gray = "";
            Exons.ForEach(item =>
            {
                gray += item ? '1' : '0';
            });
            int n = gray.Count();
            string binary = "";
            binary += gray[0];
            for (int i = 1; i < n; i++)
            {
                if (gray[i] == '0')
                    binary += binary[i - 1];
                else
                {
                    if (binary[i - 1] == '0')
                        binary += '1';
                    else
                        binary += '0';
                }
            }
            double dec = Convert.ToInt32(binary, 2);
            var ret = min + dec * (max - min) / Math.Pow(2, Exons.Count) - 1;
            return ret;
        }
        public void Mutation()
        {
            Random random = new Random();
            int r1 = random.Next(Exons.Count());
            /*int r2 = random.Next(Exons.Count());
            if (r1 == r2)
            {
                r2 = random.Next(Exons.Count());
            }
            Exons[r2] = !Exons[r2];*/
            Exons[r1] = !Exons[r1];
        }
    }
}
