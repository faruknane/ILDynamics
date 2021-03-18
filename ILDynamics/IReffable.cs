using System;

namespace ILDynamics
{
    public interface IReffable
    {
        public Type Type { get; }
        public void Load();
        public void Store();
        public void LoadAddress();
    }
}