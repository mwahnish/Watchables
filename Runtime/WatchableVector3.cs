using ABXY.Watchables.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ABXY.Watchables
{
    [System.Serializable]
    public class WatchableVector3 : WatchableAssignable<Vector3>
    {
        public WatchableVector3()
        {
        }

        public WatchableVector3(Vector3 value) : base(value)
        {
        }
    }
}