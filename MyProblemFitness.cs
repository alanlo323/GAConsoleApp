using GeneticSharp.Domain.Chromosomes;
using GeneticSharp.Domain.Fitnesses;
using System;

namespace GAConsoleApp
{
    class MyProblemFitness : IFitness
    {
        IFitnessable Reference { get; }

        public MyProblemFitness(in IFitnessable reference)
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
