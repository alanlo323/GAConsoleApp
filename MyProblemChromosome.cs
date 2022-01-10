using GeneticSharp.Domain.Chromosomes;
using System;
using System.Linq;

namespace GAConsoleApp
{
    class MyProblemChromosome : ChromosomeBase
    {
        public static int GenesCount { get; } = 100;

        IGeneable Reference { get; }

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

        public string ToGenesString { get => string.Join("", GetGenes().Select(x => Convert.ToChar(x.Value))); }
    }

    public interface IGeneable
    {
        int GenTypes { get; }

        Gene GenerateGene(int geneIndex);
    }
}
