using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace AspNetCore.ReflectMap
{
    internal class TypeInformationWrapper : ReflectedTypeWrapper<TypeInfo>, ITypeInformation
    {
        public TypeInformationWrapper(IReflection reflection, TypeInfo typeInfo) : base(reflection, typeInfo)
        { }

        public IType AsType()
            => _reflection.TypeOf(_underlyingType.AsType());

        public IType[] GenericTypeParameters
            => _underlyingType.GenericTypeParameters.Select(param => _reflection.TypeOf(param)).ToArray();

        public IEnumerable<IConstructorInfo> DeclaredConstructors
            => _underlyingType.DeclaredConstructors.Select(constructor => _reflection.ConstructorInfo(constructor));

        public IEnumerable<EventInfo> DeclaredEvents
            => _underlyingType.DeclaredEvents;

        public IEnumerable<IFieldInformation> DeclaredFields
            => _underlyingType.DeclaredFields.Select(field => _reflection.FieldInfo(field));

        public IEnumerable<IMemberInfo> DeclaredMembers
            => _underlyingType.DeclaredMembers.Select(member => _reflection.MemberInfo(member));

        public IEnumerable<IMethodInfo> DeclaredMethods
            => _underlyingType.DeclaredMethods.Select(method => _reflection.MethodInfo(method));

        public IEnumerable<ITypeInformation> DeclaredNestedTypes
            => _underlyingType.DeclaredNestedTypes.Select(nestedType => _reflection.TypeInfo(nestedType));

        public IEnumerable<IPropertyInfo> DeclaredProperties
            => _underlyingType.DeclaredProperties.Select(property => _reflection.PropertyInfo(property));

        public IEnumerable<IType> ImplementedInterfaces
            => _underlyingType.ImplementedInterfaces.Select(interfaceType => _reflection.TypeOf(interfaceType));

        public EventInfo GetDeclaredEvent(string name)
            => _underlyingType.GetDeclaredEvent(name);

        public IFieldInformation GetDeclaredField(string name)
            => _reflection.FieldInfo(_underlyingType.GetDeclaredField(name));

        public IMethodInfo GetDeclaredMethod(string name)
            => _reflection.MethodInfo(_underlyingType.GetDeclaredMethod(name));

        public ITypeInformation GetDeclaredNestedType(string name)
            => _reflection.TypeInfo(_underlyingType.GetDeclaredNestedType(name));

        public IPropertyInfo GetDeclaredProperty(string name)
            => _reflection.PropertyInfo(_underlyingType.GetDeclaredProperty(name));

        public IEnumerable<IMethodInfo> GetDeclaredMethods(string name)
            => _underlyingType.GetDeclaredMethods(name).Select(method => _reflection.MethodInfo(method));

        public bool IsAssignableFrom(ITypeInformation typeInfo)
            => _underlyingType.IsAssignableFrom(typeInfo.GetUnderlyingType());
    }
}