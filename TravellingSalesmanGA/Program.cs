using GeneticAlgorithm;
using GeneticAlgorithm.CrossoverOperator;
using GeneticAlgorithm.MutationOperator;
using GeneticAlgorithm.SelectionOperator;
using System;
using System.Linq;
using System.Text;

namespace TravellingSalesmanGA
{
    class Program
    {
        static void Main(string[] args)
        {
            //var graph = new byte[,]
            //{
            //    {0, 2, 3, 1, 3},
            //    {2, 0, 7, 6, 9},
            //    {3, 7, 0, 6, 4},
            //    {1, 6, 6, 0, 2},
            //    {3, 9, 4, 2, 0},
            //};
            Console.WriteLine("Enter number of cities:");
            var n = int.Parse(Console.ReadLine());
            var random = new Random();
            var graph = new byte[n,n];
            InitGraph(n, graph, random);

            var selectionOperator = new RandomFromTheFittestSelection(Constants.CrossoverTopPercent, random);
            var crossoverOperator = new SinglePointCrossover(random, (childPath => new Path(childPath, graph)));
            var mutationOperator = new RandomSwapMutation(random, Constants.MutationChance, (childPath => new Path(childPath, graph)));

            var pop = new Population(selectionOperator, crossoverOperator, mutationOperator, Constants.PopulationCount);

            for (int i = 0; i < Constants.PopulationCount; i++)
            {
                pop.AddIndividual(new Path(GenerateRandomPath(n, random), graph));
            }

            var k = 1;
            for (int i = 0; i < Constants.IterationsCount; i++)
            {
                pop.GenNextPopulation(Constants.ChildrenCountPercentage);
                
                if(i == 2 * k)
                {
                    k *= 2;
                    Console.WriteLine($"Shortest path for population {i} is: {pop.Individuals.First().Fitness}");
                }
            }
            Console.WriteLine($"Shortest path for population {Constants.IterationsCount} is: {pop.Individuals.First().Fitness}");
        }

        private static byte[] GenerateRandomPath(int n, Random rnd)
        {
            var randomPath = Enumerable.Range(0, n).Select(x => (byte)x).ToArray();
            Shuffle(rnd, randomPath);
            return randomPath;
        }

        public static void Shuffle<T>(Random rng, T[] array)
        {
            int n = array.Length;
            while (n > 1)
            {
                int k = rng.Next(n--);
                T temp = array[n];
                array[n] = array[k];
                array[k] = temp;
            }
        }

        public static void InitGraph(int n, byte[,] graph, Random random)
        {
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (j >= i)
                    {
                        continue;
                    }
                    graph[i, j] = (byte)random.Next(1, 250);
                }
            }

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (j < i)
                    {
                        continue;
                    }
                    graph[i, j] = graph[j, i];
                }
            }
        }
    }
}
