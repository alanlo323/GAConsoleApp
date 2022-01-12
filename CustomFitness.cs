using GeneticSharp.Domain.Chromosomes;
using GeneticSharp.Domain.Fitnesses;
using System;

namespace GAConsoleApp
{
    internal class CustomFitness : IFitness
    {
        private IFitnessable Reference { get; }

        public CustomFitness(in IFitnessable reference)
        {
            this.Reference = reference;
        }

        public double Evaluate(IChromosome chromosome)
        {
            return Reference.Evaluate(chromosome);
        }
    }

    public interface IFitnessable
    {
        double Evaluate(IChromosome chromosome);
    }
}