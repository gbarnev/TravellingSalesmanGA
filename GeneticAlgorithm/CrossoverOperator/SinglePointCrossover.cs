using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeneticAlgorithm.CrossoverOperator
{
    public class SinglePointCrossover : ICrossoverOperator
    {
        private readonly Random rand;
        private readonly Func<byte[], IIndividual> createFromChromosome;
        public SinglePointCrossover(Random random, Func<byte[], IIndividual> createFromChromosome)
        {
            this.rand = random;
            this.createFromChromosome = createFromChromosome;
        }

        public IEnumerable<IIndividual> Process(IIndividual first, IIndividual second)
        {
            var children = new List<IIndividual>();
            var parent1Chromosome = first.Chromosome; 
            var parent2Chromosome = second.Chromosome; 
            var chromosomeLength = parent1Chromosome.Length;

            // point is the index before slice
            // for example: point = 3, length = 7
            // |x|x|x-P-x|x|x|x|
            var point = this.rand.Next(1, chromosomeLength);

            byte[] firstChildChromosome = new byte[chromosomeLength];
            byte[] secondChildChromosome = new byte[chromosomeLength];

            if (point > chromosomeLength / 2)
            {
                Buffer.BlockCopy(parent1Chromosome, 0, firstChildChromosome, 0, point);
                Buffer.BlockCopy(parent2Chromosome, 0, secondChildChromosome, 0, point);
                this.FillValuesFromParent(point, chromosomeLength, parent1Chromosome, secondChildChromosome, chromosomeLength);
                this.FillValuesFromParent(point, chromosomeLength, parent2Chromosome, firstChildChromosome, chromosomeLength);
            }
            else
            {
                Buffer.BlockCopy(parent1Chromosome, point, firstChildChromosome, point, chromosomeLength - point);
                Buffer.BlockCopy(parent2Chromosome, point, secondChildChromosome, point, chromosomeLength - point);
                this.FillValuesFromParent(0, point, parent1Chromosome, secondChildChromosome, chromosomeLength);
                this.FillValuesFromParent(0, point, parent2Chromosome, firstChildChromosome, chromosomeLength);
            }

            children.Add(this.createFromChromosome(firstChildChromosome));
            children.Add(this.createFromChromosome(secondChildChromosome));

            return children;
        }

        private void FillValuesFromParent(int from, int count, byte[] parentChromosome, byte[] childChromosome, int chromosomeLength)
        {
            var k = from;
            for (int i = 0; i < chromosomeLength; i++)
            {
                if (k == count)
                {
                    break;
                }

                var cur = parentChromosome[i];
                if (!childChromosome.Contains(cur))
                {
                    childChromosome[k] = cur;
                    k++;
                }
            }
        }
    }
}
