using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ABXY.Watchables.Core;

namespace ABXY.Watchables
{

    [System.Serializable]
    public class WatchableFloat : WatchableAssignable<float>
    {
        public WatchableFloat()
        {
        }

        public WatchableFloat(float value) : base(value)
        {
        }
    }
}