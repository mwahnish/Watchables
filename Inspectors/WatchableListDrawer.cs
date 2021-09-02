using Malee.List;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using ABXY.Watchables.Core;

namespace ABXY.Watchables.Editor
{
    [CustomPropertyDrawer(typeof(WatchableListBase), true)]
    public class WatchableListDrawer : WatchablePropertyDrawer
    {
        private ReorderableList displayList;
        private SerializedProperty cachedProperty;

        public override List<Delegate> GetDelegates(SerializedProperty property)
        {
            object target = Helper.GetTargetObjectOfProperty(property);
            return new List<Delegate>() {
            GetDelegate(target, "onElementAssignment"),
            GetDelegate(target, "OnChange"),
            GetDelegate(target, "onInsertion"),
            GetDelegate(target, "onRemoval")
        };
        }

        protected override void OnWatchableChange(SerializedProperty property)
        {
            object target = Helper.GetTargetObjectOfProperty(property);
            GetDelegate(target, "OnChange")?.DynamicInvoke();
        }

        protected override void DrawWatchableProperty(Rect position, SerializedProperty property, GUIContent label)
        {
            InitializeList(property);
            displayList.DoList(position, label);
        }

        protected override float GetWatchablePropertyHeight(SerializedProperty property, GUIContent label)
        {
            InitializeList(property);
            return displayList.GetHeight();
        }

        private void InitializeList(SerializedProperty property)
        {
            cachedProperty = property;
            if (displayList == null)
            {
                displayList = new ReorderableList(property.FindPropertyRelative("_value"));
                displayList.onAddCallback += OnAdd;
                displayList.onRemoveCallback += OnRemove;
                displayList.drawElementCallback += OnDrawElement;
            }
        }

        private void OnDrawElement(Rect rect, SerializedProperty element, GUIContent label, bool selected, bool focused)
        {
            EditorGUI.BeginChangeCheck();
            EditorGUI.PropertyField(rect, element, label);
            if (EditorGUI.EndChangeCheck())
            {
                int propertyIndex = GetListElementIndex(displayList.List, element);

                object target = Helper.GetTargetObjectOfProperty(cachedProperty);
                Delegate onElementAssignment = GetDelegate(target, "onElementAssignment");
                onElementAssignment?.DynamicInvoke(GetObjectValue(element), propertyIndex);
            }
        }

        private object GetObjectValue(SerializedProperty element)
        {
            switch (element.propertyType)
            {
                case SerializedPropertyType.Generic:
                    return element.objectReferenceValue;
                case SerializedPropertyType.Integer:
                    return element.intValue;
                case SerializedPropertyType.Boolean:
                    return element.boolValue;
                case SerializedPropertyType.Float:
                    return element.floatValue;
                case SerializedPropertyType.String:
                    return element.stringValue;
                case SerializedPropertyType.Color:
                    return element.colorValue;
                case SerializedPropertyType.ObjectReference:
                    return element.objectReferenceValue;
                case SerializedPropertyType.LayerMask:
                    return element.intValue;
                case SerializedPropertyType.Enum:
                    return element.enumValueIndex;
                case SerializedPropertyType.Vector2:
                    return element.vector2Value;
                case SerializedPropertyType.Vector3:
                    return element.vector3Value;
                case SerializedPropertyType.Vector4:
                    return element.vector4Value;
                case SerializedPropertyType.Rect:
                    return element.rectValue;
                case SerializedPropertyType.ArraySize:
                    return element.arraySize;
                case SerializedPropertyType.Character:
                    return element.stringValue;
                case SerializedPropertyType.AnimationCurve:
                    return element.animationCurveValue;
                case SerializedPropertyType.Bounds:
                    return element.boundsValue;
                case SerializedPropertyType.Gradient:
                    return element.objectReferenceValue;
                case SerializedPropertyType.Quaternion:
                    return element.quaternionValue;
                case SerializedPropertyType.ExposedReference:
                    return element.exposedReferenceValue;
                case SerializedPropertyType.FixedBufferSize:
                    return element.fixedBufferSize;
                case SerializedPropertyType.Vector2Int:
                    return element.vector2IntValue;
                case SerializedPropertyType.Vector3Int:
                    return element.vector3IntValue;
                case SerializedPropertyType.RectInt:
                    return element.rectIntValue;
                case SerializedPropertyType.BoundsInt:
                    return element.boundsIntValue;
                case SerializedPropertyType.ManagedReference:
                    return null;
            }
            return null;
        }

        private int GetListElementIndex(SerializedProperty list, SerializedProperty element)
        {
            for (int index = 0; index < list.arraySize; index++)
            {
                SerializedProperty currentProperty = list.GetArrayElementAtIndex(index);
                if (currentProperty.propertyPath == element.propertyPath)
                    return index;
            }
            return -1;
        }

        private void OnRemove(ReorderableList list)
        {
            object target = Helper.GetTargetObjectOfProperty(cachedProperty);

            Delegate onRemoval = GetDelegate(target, "onRemoval");

            foreach (int selection in list.Selected)
            {
                SerializedProperty property = list.List.GetArrayElementAtIndex(selection);
                onRemoval?.DynamicInvoke(Helper.GetTargetObjectOfProperty(property));
            }

            list.Remove(list.Selected);
        }

        private void OnAdd(ReorderableList list)
        {
            object target = Helper.GetTargetObjectOfProperty(cachedProperty);
            SerializedProperty newProperty = list.AddItem();
            GetDelegate(target, "onInsertion")?.DynamicInvoke(Helper.GetTargetObjectOfProperty(newProperty), list.List.arraySize - 1);
        }

        private System.Delegate GetDelegate(object targetObject, string delName)
        {
            object delObject = targetObject.GetType().GetField(delName).GetValue(targetObject);
            return (System.Delegate)delObject;
        }
    }
}