using GAConsoleApp.Test.GameEngine;
using GeneticSharp.Domain;
using GeneticSharp.Domain.Chromosomes;
using GeneticSharp.Domain.Crossovers;
using GeneticSharp.Domain.Mutations;
using GeneticSharp.Domain.Populations;
using GeneticSharp.Domain.Reinsertions;
using GeneticSharp.Domain.Selections;
using GeneticSharp.Domain.Terminations;
using GeneticSharp.Infrastructure.Framework.Threading;
using System;

namespace GAConsoleApp
{
    class Program
    {
        /// <summary>
        /// GeneticSharp Console Application template.
        /// <see href="https://github.com/giacomelli/GeneticSharp"/>
        /// </summary>
        static void Main(string[] args)
        {
            var game = new MostClosedNumber();

            var selection = new EliteSelection();
            var crossover = new UniformCrossover(0.8f);
            var mutation = new UniformMutation(true);

            var fitness = new MyProblemFitness(game);
            var chromosome = new MyProblemChromosome(game);

            var population = new Population(50, 70, chromosome);
            population.GenerationStrategy = new PerformanceGenerationStrategy();

            var ga = new GeneticAlgorithm(population, fitness, selection, crossover, mutation);
            ga.MutationProbability = 0.1f;
            ga.Reinsertion = new ElitistReinsertion();
            //ga.TaskExecutor = new ParallelTaskExecutor
            //{
            //    MinThreads = 100,
            //    MaxThreads = 100
            //};
            ga.Termination = new OrTermination(new TerminationBase[]
            {
                new FitnessThresholdTermination(0.99f),
                new TimeEvolvingTermination(TimeSpan.FromSeconds(10)),
                //new FitnessStagnationTermination(2500)
            });
            ga.GenerationRan += (s, e) =>
            {
                Console.CursorLeft = 0;
                Console.Write($"Generation {ga.GenerationsNumber}. Best fitness: {ga.BestChromosome.Fitness.Value}");
            };
            //ga.GenerationRan += (s, e) => drawTextProgressBar($"Generation {ga.GenerationsNumber}. Best fitness: {ga.BestChromosome.Fitness.Value}", (int)(ga.BestChromosome.Fitness.Value * 100), 100);

            Console.WriteLine("GA running...");
            ga.Start();

            Console.WriteLine();
            Console.WriteLine($"Best solution found has fitness: {ga.BestChromosome.Fitness}");
            Console.WriteLine($"Elapsed time: {ga.TimeEvolving}");
            Console.ReadKey();
        }

        private static void drawTextProgressBar(string msg, int progress, int total)
        {
            string leftMsg = $"{msg} [";
            //draw empty progress bar
            Console.CursorLeft = 0;
            Console.Write(leftMsg); //start
            Console.CursorLeft = 31 + leftMsg.Length;
            Console.Write("]"); //end
            Console.CursorLeft = 1;
            float onechunk = 30.0f / total;

            //draw filled part
            int position = leftMsg.Length;
            for (int i = 0; i < onechunk * progress; i++)
            {
                Console.BackgroundColor = ConsoleColor.Green;
                Console.CursorLeft = position++;
                Console.Write(" ");
            }

            //draw unfilled part
            for (int i = position; i <= 30 + leftMsg.Length; i++)
            {
                Console.BackgroundColor = ConsoleColor.Gray;
                Console.CursorLeft = position++;
                Console.Write(" ");
            }

            //draw totals
            Console.CursorLeft = 34 + leftMsg.Length;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Write(progress.ToString() + " of " + total.ToString() + "    "); //blanks at the end remove any excess
        }
    }
}
