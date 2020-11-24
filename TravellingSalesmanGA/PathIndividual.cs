using GeneticAlgorithm;
using System.Text;

namespace TravellingSalesmanGA
{
    public class Path : IndividualBase
    {
        private readonly byte[,] graph;
        private readonly byte[] path;
        private string pathStr;
        private int fitness;

        public Path(byte[] path, byte[,] graph)
        {
            this.graph = graph;
            this.path = path;
        }

        public override int Fitness
        {
            get
            {
                if (this.fitness == 0)
                {
                    this.fitness = this.CalculatePathValue();
                }
                return this.fitness;
            }
        }

        public override byte[] Chromosome => (byte[])this.path.Clone();

        protected override string GetId()
        {
            if (string.IsNullOrEmpty(this.pathStr))
            {
                var sb = new StringBuilder();
                foreach (var node in this.path)
                {
                    sb.Append(node);
                }
                this.pathStr = sb.ToString();
            }
            return this.pathStr;
        }

        private int CalculatePathValue()
        {
            var pathValue = 0;
            var pathLength = this.path.Length;
            for (int i = 0; i < pathLength - 1; i++)
            {
                var cur = this.path[i];
                var next = this.path[i + 1];

                pathValue += this.graph[cur, next];
            }
            return pathValue;
        }
    }
}
