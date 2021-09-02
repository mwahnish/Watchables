using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
namespace ABXY.Watchables.Editor
{
    [CustomPropertyDrawer(typeof(WatchableBool))]
    public class WatchableBoolDrawer : WatchablePropertyDrawer
    {
        public override List<Delegate> GetDelegates(SerializedProperty property)
        {
            WatchableBool watchable = (WatchableBool)Helper.GetTargetObjectOfProperty(property);
            return new List<Delegate>() { watchable.onAssignment, watchable.onModification };
        }

        protected override void OnWatchableChange(SerializedProperty property)
        {
            WatchableBool watchable = (WatchableBool)Helper.GetTargetObjectOfProperty(property);
            watchable.onAssignment?.Invoke(property.FindPropertyRelative("_value").boolValue);
            watchable.onModification?.Invoke(property.FindPropertyRelative("_value").boolValue);
        }
    }
}