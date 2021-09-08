using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace ABXY.Watchables.Editor
{
    [CustomPropertyDrawer(typeof(WatchableQuaternion))]
    public class WatchableQuaternionDrawer : WatchablePropertyDrawer
    {
        public override List<Delegate> GetDelegates(SerializedProperty property)
        {
            WatchableQuaternion watchable = (WatchableQuaternion)Helper.GetTargetObjectOfProperty(property);
            return new List<Delegate>() { watchable.onAssignment, watchable.onModification };
        }

        protected override void OnWatchableChange(SerializedProperty property)
        {
            WatchableQuaternion watchable = (WatchableQuaternion)Helper.GetTargetObjectOfProperty(property);
            watchable.onAssignment?.Invoke(property.FindPropertyRelative("_value").quaternionValue);
            watchable.onModification?.Invoke(property.FindPropertyRelative("_value").quaternionValue);
        }
    }
}