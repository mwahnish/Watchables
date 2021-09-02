using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ABXY.Watchables.Core;

namespace ABXY.Watchables
{

    [System.Serializable]
    public class WatchableBool : WatchableAssignable<bool>
    {
        public WatchableBool()
        {
        }

        public WatchableBool(bool value) : base(value)
        {
        }
    }
}