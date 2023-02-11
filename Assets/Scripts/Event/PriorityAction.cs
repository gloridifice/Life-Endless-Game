using System;
using System.Reflection;
using UnityEngine.Events;

namespace GGJ2023.Event
{
    public class PriorityAction<T>
    {
        private Action<T> low = (T) => {};
        private Action<T> normal = (T) => {};
        private Action<T> high = (T) => {};

        public void Invoke(T t)
        {
            high.Invoke(t);
            normal.Invoke(t);
            low.Invoke(t);
        }
        
        public void AddListener(Action<T> action)
        {
            PriorityType priority = PriorityType.Normal;
            PriorityAttribute attribute = action.Method.GetCustomAttribute<PriorityAttribute>();
            if (attribute != null)
            {
                priority = attribute.priority;
            }

            switch (priority)
            {
                case PriorityType.Low: low += action; break;
                case PriorityType.Normal: normal += action; break;
                case PriorityType.High: high += action; break;
            }
        }

        public void RemoveListener(Action<T> action)
        {
            low -= action;
            normal -= action;
            high -= action;
        }
    }

    public enum PriorityType
    {
        Low, Normal, High
    }
}