namespace GeneticAlgorithm.SelectionOperator
{
    public interface ISelectionOperator
    {
        (IIndividual First, IIndividual Second) SelectParents(IPopulation population);
    }
}
