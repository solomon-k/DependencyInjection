using System;

namespace Solomonic.DependencyInjection.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class DependencyPropertyAttribute : Attribute
    {
        private readonly string _name;

        public DependencyPropertyAttribute()
        {
            
        }

        public DependencyPropertyAttribute(string name)
        {
            _name = name;
        }

        public string Name
        {
            get { return _name; }
        }
    }
}
