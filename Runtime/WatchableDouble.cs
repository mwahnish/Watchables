using ABXY.Watchables.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ABXY.Watchables
{

    [System.Serializable]
    public class WatchableDouble : WatchableAssignable<double>
    {
        public WatchableDouble()
        {
        }

        public WatchableDouble(double value) : base(value)
        {
        }
    }
}