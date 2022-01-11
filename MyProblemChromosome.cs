using GeneticSharp.Domain.Chromosomes;
using System;
using System.Linq;

namespace GAConsoleApp
{
    internal class MyProblemChromosome : ChromosomeBase
    {
        public static int GenesCount { get; } = 1000;

        private IGeneable Reference { get; }

        public MyProblemChromosome(in IGeneable reference)
            : base(GenesCount)
        {
            this.Reference = reference;
            CreateGenes();
        }

        public override Gene GenerateGene(int geneIndex)
        {
            return Reference.GenerateGene(geneIndex);
        }

        public override IChromosome CreateNew()
        {
            var reference = Reference;
            return new MyProblemChromosome(in reference);
        }
    }

    public interface IGeneable
    {
        int GenTypes { get; }

        Gene GenerateGene(int geneIndex);
    }
}