namespace GeneticAlgorithm.MutationOperator
{
    public interface IMutationOperator
    {
        IIndividual Mutate(IIndividual individual);
    }
}
