using ABXY.Watchables.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ABXY.Watchables
{
    [System.Serializable]
    public class WatchableQuaternion : WatchableAssignable<Quaternion>
    {
        public WatchableQuaternion()
        {
        }

        public WatchableQuaternion(Quaternion value) : base(value)
        {
        }
    }
}