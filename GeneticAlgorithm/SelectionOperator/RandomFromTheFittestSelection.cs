using System;

namespace GeneticAlgorithm.SelectionOperator
{
    public class RandomFromTheFittestSelection : ISelectionOperator
    {
        private readonly double selectTopPercent;
        private readonly Random random;

        public RandomFromTheFittestSelection(double selectTopPercent, Random rand)
        {
            this.random = rand;
            this.selectTopPercent = selectTopPercent;
        }

        public (IIndividual First, IIndividual Second) SelectParents(IPopulation population)
        {
            var topIndividualsCnt = (int)(population.Size * this.selectTopPercent);
            var firstIdx = this.random.Next(0, topIndividualsCnt);
            var secondIdx = this.random.Next(0, topIndividualsCnt);

            while (firstIdx == secondIdx)
            {
                secondIdx = this.random.Next(0, topIndividualsCnt);
            }
            return (population.Individuals[firstIdx], population.Individuals[secondIdx]);
        }
    }
}
