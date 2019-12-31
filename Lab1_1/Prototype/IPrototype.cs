using System;
using System.Collections.Generic;
using System.Text;

namespace Lab1_1.Prototype
{
    public interface IPrototype
    {
        IPrototype ShallowCopy();

        IPrototype DeepCopy();
    }
}
