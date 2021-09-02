using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ABXY.Watchables.Core;

namespace ABXY.Watchables
{

    [System.Serializable]
    public class WatchableInt : WatchableAssignable<int>
    {
        public WatchableInt()
        {
        }

        public WatchableInt(int value) : base(value)
        {
        }
    }
}