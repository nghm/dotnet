using System.Collections.Generic;
using System.Reflection;

namespace AspNetCore.ReflectMap
{
    public interface ITypeInformation
    {
        IType AsType();
        IType[] GenericTypeParameters { get; }
        IEnumerable<IConstructorInfo> DeclaredConstructors { get; }
        IEnumerable<EventInfo> DeclaredEvents { get; }
        IEnumerable<IFieldInformation> DeclaredFields { get; }
        IEnumerable<IMemberInfo> DeclaredMembers { get; }
        IEnumerable<IMethodInfo> DeclaredMethods { get; }
        IEnumerable<ITypeInformation> DeclaredNestedTypes { get; }
        IEnumerable<IPropertyInfo> DeclaredProperties { get; }
        IEnumerable<IType> ImplementedInterfaces { get; }
        EventInfo GetDeclaredEvent(string name);
        IFieldInformation GetDeclaredField(string name);
        IMethodInfo GetDeclaredMethod(string name);
        ITypeInformation GetDeclaredNestedType(string name);
        IPropertyInfo GetDeclaredProperty(string name);
        IEnumerable<IMethodInfo> GetDeclaredMethods(string name);
        bool IsAssignableFrom(ITypeInformation typeInfo);
    }
}