using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ABXY.Watchables.Core
{

    public class WatchableAssignable<InnerType> : Watchable
    {
        /// <summary>
        /// Subscribe to this callback to get notified whenever the 
        /// watched value is assigned to.
        /// </summary>
        public System.Action<InnerType> onAssignment;

        /// <summary>
        /// Subscribe to this callback to get notified whenver the
        /// watched value is modified.
        /// </summary>
        public System.Action<InnerType> onModification;

        /// <summary>
        /// The inner, stored value we are wrapping
        /// </summary>
        [SerializeField]
        private InnerType _value;

        /// <summary>
        /// The current value of the watchable
        /// </summary>
        public virtual InnerType Value
        {
            get { return _value; }
            set
            {
                _value = value;
                onAssignment?.Invoke(_value);
                onModification?.Invoke(_value);
            }
        }


        protected WatchableAssignable()
        {
            _value = default(InnerType);
        }

        protected WatchableAssignable(InnerType value)
        {
            _value = value;
        }
    }
}