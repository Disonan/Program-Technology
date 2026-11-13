using System;
using System.Collections.Generic;
using System.Text;


namespace NumberQuest.Interfaces
{
    public interface IRandomNumberGenerator
    {
        int Generate(int min, int max);
    }
}

