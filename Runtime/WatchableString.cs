using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ABXY.Watchables.Core;


namespace ABXY.Watchables
{
    [System.Serializable]
    public class WatchableString : WatchableAssignable<string>
    {
        public WatchableString()
        {
        }

        public WatchableString(string value) : base(value)
        {
        }
    }
}