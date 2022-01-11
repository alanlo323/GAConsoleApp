using GeneticSharp.Domain.Chromosomes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GAConsoleApp.Test.GameEngine
{
    internal class MostClosedNumber : IGeneable, IFitnessable
    {
        private static int[][] _Genes;

        private Random rng = new();
        int IGeneable.GenTypes { get => 1000; }

        public int[][] Genes
        {
            get
            {
                if (_Genes == null)
                {
                    int genTypes = ((IGeneable)this).GenTypes;
                    _Genes = new int[MyProblemChromosome.GenesCount][];
                    for (int i = 0; i < MyProblemChromosome.GenesCount; i++)
                    {
                        _Genes[i] = new int[genTypes];
                        List<int> geneList = new List<int>();

                        for (int j = 0; j < genTypes; j++)
                        {
                            geneList.Add(j);
                        }

                        for (int j = 0; j < genTypes; j++)
                        {
                            int nextIndex = rng.Next(0, geneList.Count);
                            _Genes[i][j] = geneList[nextIndex];
                            geneList.RemoveAt(nextIndex);
                        }
                    }
                }
                return _Genes;
            }
        }

        public double Evaluate(IChromosome chromosome)
        {
            int genTypes = ((IGeneable)this).GenTypes;
            double diff = 0;
            var genes = chromosome.GetGenes();
            for (int i = 0; i < genes.Length; i++)
            {
                int value = (int)genes[i].Value;
                diff += Math.Abs(value - i);
            }

            double maxDiff = genes.Length * (genes.Length + 1) / 2;
            double fitness = (maxDiff - diff) / maxDiff;

            return fitness;
        }

        public Gene GenerateGene(int geneIndex)
        {
            int geneValue = Genes[geneIndex][rng.Next(0, Genes[geneIndex].Length - 1)];
            Gene gene = new(geneValue);
            return gene;
        }
    }
}