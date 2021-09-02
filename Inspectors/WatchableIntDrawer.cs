using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
namespace ABXY.Watchables.Editor
{
    [CustomPropertyDrawer(typeof(WatchableInt), true)]
    public class WatchableIntDrawer : WatchablePropertyDrawer
    {
        public override List<Delegate> GetDelegates(SerializedProperty property)
        {
            WatchableInt watchable = (WatchableInt)Helper.GetTargetObjectOfProperty(property);
            return new List<Delegate>() { watchable.onAssignment, watchable.onModification };
        }

        protected override void OnWatchableChange(SerializedProperty property)
        {
            WatchableInt watchable = (WatchableInt)Helper.GetTargetObjectOfProperty(property);
            watchable.onAssignment?.Invoke(property.FindPropertyRelative("_value").intValue);
            watchable.onModification?.Invoke(property.FindPropertyRelative("_value").intValue);
        }
    }
}