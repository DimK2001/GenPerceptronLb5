using System;
using System.Collections.Generic;
using System.Linq;

namespace GeneticAl
{
    public class Population
    {
        public List<Individual> individuals = new List<Individual>();
        public void NewIndovidual (List<bool> exons)
        {
            Individual individual = new Individual(exons);
            individuals.Add(individual);
        }
        public List<Individual> Crossingover (List<Individual> parents, int pointsAmount)
        {
            List<bool> p1exons = parents[0].Chromosome.GetExons();
            List<bool> p2exons = parents[1].Chromosome.GetExons();
            Random random = new Random();
            List<int> possible = Enumerable.Range(1, p1exons.Count - 1).ToList();
            List<int> points = new List<int>();
            for (int i = 0; i < pointsAmount; i++)
            {
                int index = random.Next(0, possible.Count);
                points.Add(possible[index]);
                possible.RemoveAt(index);
            }
            points.Sort();
            List<List<bool>> exons = crossingover(p1exons, p2exons, points);
            return new List<Individual>() { new Individual(exons[0]), new Individual(exons[1]) };
        }
        public void Clean()
        {
            individuals.Clear();
        }
        public List<List<bool>> crossingover(List<bool> p1exons, List<bool> p2exons, List<int> points)
        {
            int numP = 0;
            List<bool> exons1 = new List<bool>();
            List<bool> exons2 = new List<bool>();
            bool parentChoose = false;
            for (int i = 0; i < p1exons.Count; ++i)
            {
                if (numP < points.Count)
                {
                    if (i == points[numP])
                    {
                        numP++;
                        parentChoose = !parentChoose;
                    }
                }
                if (!parentChoose)
                {
                    exons1.Add(p1exons[i]);
                    exons2.Add(p2exons[i]);
                }
                else
                {
                    exons1.Add(p2exons[i]);
                    exons2.Add(p1exons[i]);
                }
            }
            return new List<List<bool>>() { exons1, exons2 };
        }
    }
}

