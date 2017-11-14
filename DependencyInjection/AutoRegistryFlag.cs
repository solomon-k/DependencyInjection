using System;

namespace Solomonic.DependencyInjection
{
    [Flags]
    public enum AutoRegistryFlag
    {
        Interfaces,
        AbstractClasses
    }
}