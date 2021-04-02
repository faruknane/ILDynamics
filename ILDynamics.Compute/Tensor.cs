using ILGPU;
using ILGPU.Runtime;
using System;

namespace ILDynamics.Compute
{
    public class Tensor<T> where T : unmanaged
    {
        public MemoryBuffer<T> Buffer { get; private set; }
        public Shape Shape;

        public Tensor(MemoryBuffer<T> buff, Shape shape = null)
        {
            Buffer = buff;
            if (shape == null)
                this.Shape = new Shape(buff.Length);
            else
                this.Shape = shape;
        }
        public Tensor(MemoryBuffer<T> buff)
        {
            Buffer = buff;
            Shape = new Shape(buff.Length);
        }

        public Tensor()
        {

        }


    }
}
