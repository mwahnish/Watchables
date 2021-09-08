using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
namespace ABXY.Watchables.Editor
{
    public abstract class WatchablePropertyDrawer : PropertyDrawer
    {
        private bool expanded = false;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginChangeCheck();

            float expectedPropertyHeight = GetWatchablePropertyHeight(property, label);

            Rect propertyRect = new Rect(position.x, position.y, position.width - 20, expectedPropertyHeight);
            DrawWatchableProperty(propertyRect, property, label);
            if (EditorGUI.EndChangeCheck())
            {
                OnWatchableChange(property);
            }


            Rect expandRect = new Rect(position.x + position.width - 2, position.y, 10, EditorGUIUtility.singleLineHeight);
            expanded = EditorGUI.Foldout(expandRect, expanded, "");

            if (expanded)
            {
                float startY = position.y + GetWatchablePropertyHeight(property, label);

                foreach (var del in GetDelegates(property))
                {
                    DrawDelegateMethodInvocationList(position.x, startY, position.width, del);
                    startY += GetDelegateMethodInvocationListHeight(del);
                }
            }
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            float height = GetWatchablePropertyHeight(property, label);

            if (expanded)
            {
                foreach (var del in GetDelegates(property))
                {
                    height += GetDelegateMethodInvocationListHeight(del);
                }
            }
            return height;
        }

        /// <summary>
        /// Draws the watchable property
        /// </summary>
        /// <param name="position"></param>
        /// <param name="property"></param>
        /// <param name="label"></param>
        protected virtual void DrawWatchableProperty(Rect position, SerializedProperty property, GUIContent label)
        {
            Rect textPosition = new Rect(position.x, position.y, position.width - 20, position.height);
            SerializedProperty prop = property.FindPropertyRelative("_value");
            EditorGUI.PropertyField(textPosition, property.FindPropertyRelative("_value"), label);
        }

        public abstract List<System.Delegate> GetDelegates(SerializedProperty property);

        /// <summary>
        /// Returns the height of the watchable property
        /// </summary>
        /// <param name="property"></param>
        /// <param name="label"></param>
        /// <returns></returns>
        protected virtual float GetWatchablePropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUIUtility.singleLineHeight;
        }

        /// <summary>
        /// Called when the drawn watchable has changed
        /// </summary>
        protected abstract void OnWatchableChange(SerializedProperty property);

        protected void DrawDelegateMethodInvocationList(float x, float startY, float width, System.Delegate del)
        {
            if (del == null)
                return;

            System.Delegate[] invocationList = del.GetInvocationList();
            foreach (var invocationDel in invocationList)
            {
                Rect rect = new Rect(x + 10, startY, width - 10, EditorGUIUtility.singleLineHeight);

                string methodString = "Null";
                if (invocationDel != null)
                {
                    var parameters = invocationDel.Method.GetParameters();
                    string parameterString = string.Join(", ", parameters.Select(x => $"{x.ParameterType.Name} {x.Name}"));
                    methodString = $"{invocationDel.Target.GetType()}.{invocationDel.Method.Name}({parameterString})";

                }
                //invocationDel!=null? $"{invocationDel.Target.GetType()}.{invocationDel.Method.Name}" :"Null"
                if (GUI.Button(rect, methodString, EditorStyles.label))
                {
                    if (invocationDel.Target is Object)
                        EditorGUIUtility.PingObject(invocationDel.Target as Object);
                }
                //EditorGUI.LabelField(rect, methodString);
                startY += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
            }
        }

        protected float GetDelegateMethodInvocationListHeight(System.Delegate del)
        {
            if (del == null)
                return 0f;

            System.Delegate[] invocationList = del.GetInvocationList();
            return invocationList.Length * (EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing);
        }

    }
}