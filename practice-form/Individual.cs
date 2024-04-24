using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAl
{
    public class Individual
    {
        public Chromosome Chromosome;
        public Individual(List<bool> exons)
        {
            SetChromosome(exons);
        }
        public void SetChromosome(List<bool> exons)
        {
            List<Gen> genes = new List<Gen>();
            for (int g = 0; g < 10; g++)
            {
                Gen gene = new Gen(exons.Skip(g * 16 * 25).Take(16 * 25).ToList());
                /*for (int i = 0; i < 25; i++)
                {
                    for (int j = 0; j < 16; j++)
                    {
                        gene.Exons.Add(exons[(i * 16) + j]);
                    }
                }*/
                genes.Add(gene);
            }
            Chromosome = new Chromosome(genes);
        }
        public void Mutation()
        {
            foreach (var g in Chromosome.Gens)
            {
                Random random = new Random();
                //float probability = random.Next(9000, 9950); //mid 9475
                float probability = 4;
                if (random.Next(0, 10) < probability)
                {
                    g.Mutation();
                }
            }
        }
        public List<bool> GetExons()
        {
            return Chromosome.GetExons();
        }
    }
}
