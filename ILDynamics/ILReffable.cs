using System;

namespace ILDynamics
{
    public interface ILReffable
    {
        public Type Type { get; }
        public void Load();
        public void Store();
        public void LoadAddress();
    }
}