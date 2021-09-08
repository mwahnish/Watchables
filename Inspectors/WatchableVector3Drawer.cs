using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace ABXY.Watchables.Editor
{
    [CustomPropertyDrawer(typeof(WatchableVector3))]
    public class WatchableVector3Drawer : WatchablePropertyDrawer
    {
        public override List<Delegate> GetDelegates(SerializedProperty property)
        {
            WatchableVector3 watchable = (WatchableVector3)Helper.GetTargetObjectOfProperty(property);
            return new List<Delegate>() { watchable.onAssignment, watchable.onModification };
        }

        protected override void OnWatchableChange(SerializedProperty property)
        {
            WatchableVector3 watchable = (WatchableVector3)Helper.GetTargetObjectOfProperty(property);
            watchable.onAssignment?.Invoke(property.FindPropertyRelative("_value").vector3Value);
            watchable.onModification?.Invoke(property.FindPropertyRelative("_value").vector3Value);
        }
    }
}