using System.Collections.Generic;

namespace GeneticAl
{
    public class Chromosome
    {
        public List<Gen> Gens = new List<Gen>();
        public Chromosome(List<Gen> gens = null)
        {
            Gens = gens ?? new List<Gen>();
        }
        public List<bool> GetExons()
        {
            List<bool> exons = new List<bool>();
            foreach (var gene in Gens)
            {
                exons.AddRange(gene.Exons);
            }
            return exons;
        }
    }
}
