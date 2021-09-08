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

        protected override void DrawWatchableProperty(Rect position, SerializedProperty property, GUIContent label)
        {
            Quaternion rotation = property.FindPropertyRelative("_value").quaternionValue;
            Vector3 newValue = EditorGUI.Vector3Field(position, label, rotation.eulerAngles);
            property.FindPropertyRelative("_value").quaternionValue = Quaternion.Euler(newValue);
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return (EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing) * 2f;
        }
    }
}