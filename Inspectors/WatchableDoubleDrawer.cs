using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
namespace ABXY.Watchables.Editor
{
    [CustomPropertyDrawer(typeof(WatchableDouble))]
    public class WatchableDoubleDrawer : WatchablePropertyDrawer
    {
        public override List<Delegate> GetDelegates(SerializedProperty property)
        {
            WatchableDouble watchable = (WatchableDouble)Helper.GetTargetObjectOfProperty(property);
            return new List<Delegate>() { watchable.onAssignment, watchable.onModification };
        }

        protected override void OnWatchableChange(SerializedProperty property)
        {
            WatchableDouble watchable = (WatchableDouble)Helper.GetTargetObjectOfProperty(property);
            watchable.onAssignment?.Invoke(property.FindPropertyRelative("_value").doubleValue);
            watchable.onModification?.Invoke(property.FindPropertyRelative("_value").doubleValue);
        }
    }
}