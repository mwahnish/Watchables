using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Reflection;
using System.Linq;
using System;
namespace ABXY.Watchables.Editor
{

    [CustomPropertyDrawer(typeof(WatchableString), true)]
    public class WatchableStringDrawer : WatchablePropertyDrawer
    {
        public override List<Delegate> GetDelegates(SerializedProperty property)
        {
            WatchableString watchable = (WatchableString)Helper.GetTargetObjectOfProperty(property);
            return new List<Delegate>() { watchable.onAssignment, watchable.onModification };
        }

        protected override void OnWatchableChange(SerializedProperty property)
        {
            WatchableString watchable = (WatchableString)Helper.GetTargetObjectOfProperty(property);
            watchable.onAssignment?.Invoke(property.FindPropertyRelative("_value").stringValue);
            watchable.onModification?.Invoke(property.FindPropertyRelative("_value").stringValue);
        }

    }
}