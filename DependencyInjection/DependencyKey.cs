using System;

namespace Solomonic.DependencyInjection
{
    public struct DependencyKey
    {
        public Guid ContainerGuid { get; private set; }
        public string Name { get; private set; }
        public Type Type { get; private set; }

        internal DependencyKey(Guid containerGuid, string name, Type type)
            : this()
        {
            ContainerGuid = containerGuid;
            Name = name;
            Type = type;
        }
    }
}