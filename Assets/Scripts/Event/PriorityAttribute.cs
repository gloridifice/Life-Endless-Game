using System;

namespace GGJ2023.Event
{
    public class PriorityAttribute: Attribute
    {
        public PriorityType priority;

        public PriorityAttribute(PriorityType priority)
        {
            this.priority = priority;
        }
    }
}