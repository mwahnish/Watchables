using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
namespace ABXY.Watchables.Editor
{
    [CustomPropertyDrawer(typeof(WatchableFloat))]
    public class WatchableFloatDrawer : WatchablePropertyDrawer
    {
        public override List<Delegate> GetDelegates(SerializedProperty property)
        {
            WatchableFloat watchable = (WatchableFloat)Helper.GetTargetObjectOfProperty(property);
            return new List<Delegate>() { watchable.onAssignment, watchable.onModification };
        }

        protected override void OnWatchableChange(SerializedProperty property)
        {
            WatchableFloat watchable = (WatchableFloat)Helper.GetTargetObjectOfProperty(property);
            watchable.onAssignment?.Invoke(property.FindPropertyRelative("_value").floatValue);
            watchable.onModification?.Invoke(property.FindPropertyRelative("_value").floatValue);
        }
    }
}