using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace GeneticAlgorithm
{
    public abstract class IndividualBase : IIndividual, IEquatable<IndividualBase>
    {
        private string id;

        public abstract int Fitness { get; }

        public string Id
        {
            get
            {
                if (string.IsNullOrEmpty(this.id))
                {
                    this.id = this.GetId();
                }
                return this.id;
            }
        }

        public abstract byte[] Chromosome { get; }

        protected abstract string GetId();

        public virtual int CompareTo([AllowNull] IIndividual other)
        {
            return this.Fitness - other.Fitness;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            var other = obj as IndividualBase;
            if (other == null)
            {
                return false;
            }
            return this.Equals(other);
        }

        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }

        public bool Equals([AllowNull] IndividualBase other)
        {
            return this.Id == other.Id;
        }

    }
}
