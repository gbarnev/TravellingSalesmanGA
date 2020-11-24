using GeneticAlgorithm.CrossoverOperator;
using GeneticAlgorithm.MutationOperator;
using GeneticAlgorithm.SelectionOperator;
using System;
using System.Collections.Generic;

namespace GeneticAlgorithm
{
    public class Population : IPopulation
    {
        private readonly List<IIndividual> individuals;
        private readonly ICrossoverOperator crossoverOperator;
        private readonly IMutationOperator mutationOperator;
        private readonly ISelectionOperator selectionOperator;
        private readonly int size;
        private bool isSorted;

        public Population(
            ISelectionOperator selectionOperator,
            ICrossoverOperator crossoverOperator,
            IMutationOperator mutationOperator,
            int maxPopulationSize)
        {
            this.selectionOperator = selectionOperator ?? throw new ArgumentNullException(nameof(selectionOperator));
            this.crossoverOperator = crossoverOperator ?? throw new ArgumentNullException(nameof(crossoverOperator));
            this.mutationOperator = mutationOperator ?? throw new ArgumentNullException(nameof(mutationOperator));
            this.size = maxPopulationSize;
            this.individuals = new List<IIndividual>();
            this.isSorted = false;
        }

        public int Size => this.size;

        public IReadOnlyList<IIndividual> Individuals
        {
            get
            {
                if (!this.isSorted)
                {
                    this.individuals.Sort();
                }
                return this.individuals;
            }
        }

        public bool AddIndividual(IIndividual individual)
        {
            if (this.individuals.Count >= this.size)
            {
                return false;
            }

            this.individuals.Add(individual);
            this.isSorted = false;
            return true;
        }

        public virtual void GenNextPopulation(double childPercentage)
        {
            if (childPercentage < 0 || childPercentage > 1)
            {
                throw new ArgumentException("Child percentage must be in the range [0,1]");
            }
            var neededChildren = childPercentage * this.individuals.Count;
            List<IIndividual> allChildren = new List<IIndividual>();
            while (allChildren.Count < neededChildren)
            {
                (var firstParent, var secondParent) = this.selectionOperator.SelectParents(this);
                var children = this.crossoverOperator.Process(firstParent, secondParent);
                foreach (var child in children)
                {
                    var mutated = this.mutationOperator.Mutate(child);
                    allChildren.Add(mutated);
                }
            }
            this.individuals.AddRange(allChildren);
            this.isSorted = false;
            var curPopCount = this.individuals.Count;
            if (curPopCount > this.size)
            {
                this.individuals.Sort();
                this.isSorted = true;
                this.individuals.RemoveRange(this.size, curPopCount - this.size);
            }
        }
    }
}
