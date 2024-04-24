using System;
using System.Collections;
using System.Collections.Generic;

namespace GeneticAl
{
    internal class EvolutionManager
    {
        List<Population> populations = new List<Population>();
        Random rnd = new Random();
        public int[][] BestW = new int[10][];
        int theBest = 0;
        public void InitPipolation (int populationCount)
        {
            Population population = new Population();
            for (int i = 0; i < populationCount; i++)
            {
                List<bool> exons = new List<bool>();
                for (int j = 0; j < 10 * 16 * 25; ++j)
                {
                    exons.Add(rnd.NextDouble() > 0.5);
                }
                population.NewIndovidual(exons);
            }
            populations.Add(population);
        }
        public double GetNewGeneration(Func<int[][], int> fitness)
        {
            double right = int.MinValue;
            Population currentPopulation = populations[populations.Count - 1];
            List<Individual> winners = new List<Individual>();
            Population newPopulation = new Population();
            for (int i = 0; i < currentPopulation.individuals.Count; i++)
            {
                int winner = 0;
                int winnerVal = int.MinValue;
                for (int j = 0; j < 2; ++j)
                {
                    int spc = rnd.Next(currentPopulation.individuals.Count);
                    int[][] vals = new int[currentPopulation.individuals[spc].Chromosome.Gens.Count][];
                    for (int k = 0; k < currentPopulation.individuals[spc].Chromosome.Gens.Count; ++k)
                    {
                        vals[k] = currentPopulation.individuals[spc].Chromosome.Gens[k].GetInts(-6, 6);
                    }
                    var fit = fitness(vals);
                    if (fit > winnerVal)
                    {
                        winner = spc;
                        winnerVal = fit;
                        if (right < winnerVal)
                        {
                            right = winnerVal;
                            if (winnerVal > theBest)
                            {
                                BestW = vals;
                                theBest = winnerVal;
                            }
                        }
                    }
                }
                winners.Add(currentPopulation.individuals[winner]);
            }

            for (int i = 0; i < winners.Count / 2; ++i)
            {
                List<Individual> crossed = newPopulation.Crossingover(new List<Individual> 
                { winners[i * 2], winners[i * 2 + 1] }, 3);
                newPopulation.NewIndovidual(crossed[0].Chromosome.GetExons());
                newPopulation.NewIndovidual(crossed[1].Chromosome.GetExons());
            }
            if (winners.Count % 2 > 0)
            {
                List<Individual> crossed = newPopulation.Crossingover(new List<Individual>
                { winners[winners.Count - 1], winners[winners.Count - 2] }, 3);
                newPopulation.NewIndovidual(crossed[0].Chromosome.GetExons());
                newPopulation.NewIndovidual(crossed[1].Chromosome.GetExons());
            }
            foreach (Individual individual in newPopulation.individuals)
            {
                individual.Mutation();
            }
            populations.Remove(currentPopulation); populations.Add(newPopulation);
            //Console.WriteLine(populations.Count - 1 + " Error: " + loss + " X=" + bestX + " Y=" + bestY);
            return right;
        }
    }
}
