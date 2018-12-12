using System;
using System.Globalization;
using System.Reflection;
using System.Runtime.InteropServices;

namespace AspNetCore.ReflectMap
{
    public interface IType : IReflectedTypeWrapper<Type>
    {
        bool IsInterface { get; }

        MemberTypes MemberType { get; }

        string Namespace { get; }

        string AssemblyQualifiedName { get; }

        string FullName { get; }

        Assembly Assembly { get; }

        Module Module { get; }

        bool IsNested { get; }

        IType DeclaringType { get; }

        IMethodBase DeclaringMethod { get; }

        IType ReflectedType { get; }

        IType UnderlyingSystemType { get; }

        bool IsTypeDefinition { get; }

        bool IsArray { get; }

        bool IsByRef { get; }

        bool IsPointer { get; }

        bool IsConstructedGenericType { get; }

        bool IsGenericParameter { get; }

        bool IsGenericTypeParameter { get; }

        bool IsGenericMethodParameter { get; }

        bool IsGenericType { get; }

        bool IsGenericTypeDefinition { get; }

        bool IsSZArray { get; }

        bool IsVariableBoundArray { get; }

        bool IsByRefLike { get; }

        bool HasElementType { get; }

        IType[] GenericTypeArguments { get; }

        int GenericParameterPosition { get; }

        GenericParameterAttributes GenericParameterAttributes { get; }

        TypeAttributes Attributes { get; }

        bool IsAbstract { get; }

        bool IsImport { get; }

        bool IsSealed { get; }

        bool IsSpecialName { get; }

        bool IsClass { get; }

        bool IsNestedAssembly { get; }

        bool IsNestedFamANDAssem { get; }

        bool IsNestedFamily { get; }

        bool IsNestedFamORAssem { get; }

        bool IsNestedPrivate { get; }

        bool IsNestedPublic { get; }

        bool IsNotPublic { get; }

        bool IsPublic { get; }

        bool IsAutoLayout { get; }

        bool IsExplicitLayout { get; }

        bool IsLayoutSequential { get; }

        bool IsAnsiClass { get; }

        bool IsAutoClass { get; }

        bool IsUnicodeClass { get; }

        bool IsCOMObject { get; }

        bool IsContextful { get; }

        bool IsCollectible { get; }

        bool IsEnum { get; }

        bool IsMarshalByRef { get; }

        bool IsPrimitive { get; }

        bool IsValueType { get; }

        bool IsSignatureType { get; }

        bool IsSecurityCritical { get; }

        bool IsSecuritySafeCritical { get; }

        bool IsSecurityTransparent { get; }

        StructLayoutAttribute StructLayoutAttribute { get; }

        IConstructorInfo TypeInitializer { get; }

        RuntimeTypeHandle TypeHandle { get; }

        Guid GUID { get; }

        IType BaseType { get; }

        bool IsSerializable { get; }

        bool ContainsGenericParameters { get; }

        bool IsVisible { get; }

        IType GetType();

        IType GetElementType();

        int GetArrayRank();

        IType GetGenericTypeDefinition();

        IType[] GetGenericArguments();

        IType[] GetGenericParameterConstraints();

        IConstructorInfo GetConstructor(IType[] types);

        IConstructorInfo GetConstructor(
            BindingFlags bindingAttr,
            Binder binder,
            IType[] types,
            ParameterModifier[] modifiers);

        IConstructorInfo GetConstructor(
            BindingFlags bindingAttr,
            Binder binder,
            CallingConventions callConvention,
            IType[] types,
            ParameterModifier[] modifiers);

        IConstructorInfo[] GetConstructors();

        IConstructorInfo[] GetConstructors(BindingFlags bindingAttr);

        EventInfo GetEvent(string name);

        EventInfo GetEvent(string name, BindingFlags bindingAttr);

        EventInfo[] GetEvents();

        EventInfo[] GetEvents(BindingFlags bindingAttr);

        IFieldInformation GetField(string name);

        IFieldInformation GetField(string name, BindingFlags bindingAttr);

        IFieldInformation[] GetFields();

        IFieldInformation[] GetFields(BindingFlags bindingAttr);

        IMemberInfo[] GetMember(string name);

        IMemberInfo[] GetMember(string name, BindingFlags bindingAttr);

        IMemberInfo[] GetMember(
            string name,
            MemberTypes type,
            BindingFlags bindingAttr);

        IMemberInfo[] GetMembers();

        IMemberInfo[] GetMembers(BindingFlags bindingAttr);

        IMethodInfo GetMethod(string name);

        IMethodInfo GetMethod(string name, BindingFlags bindingAttr);

        IMethodInfo GetMethod(string name, IType[] types);

        IMethodInfo GetMethod(string name, IType[] types, ParameterModifier[] modifiers);

        IMethodInfo GetMethod(
            string name,
            BindingFlags bindingAttr,
            Binder binder,
            IType[] types,
            ParameterModifier[] modifiers);

        IMethodInfo GetMethod(
            string name,
            BindingFlags bindingAttr,
            Binder binder,
            CallingConventions callConvention,
            IType[] types,
            ParameterModifier[] modifiers);

        IMethodInfo GetMethod(string name, int genericParameterCount, IType[] types);

        IMethodInfo GetMethod(
            string name,
            int genericParameterCount,
            IType[] types,
            ParameterModifier[] modifiers);

        IMethodInfo GetMethod(
            string name,
            int genericParameterCount,
            BindingFlags bindingAttr,
            Binder binder,
            IType[] types,
            ParameterModifier[] modifiers);

        IMethodInfo GetMethod(
            string name,
            int genericParameterCount,
            BindingFlags bindingAttr,
            Binder binder,
            CallingConventions callConvention,
            IType[] types,
            ParameterModifier[] modifiers);

        IMethodInfo[] GetMethods();

        IMethodInfo[] GetMethods(BindingFlags bindingAttr);

        IType GetNestedType(string name);

        IType GetNestedType(string name, BindingFlags bindingAttr);

        IType[] GetNestedTypes();

        IType[] GetNestedTypes(BindingFlags bindingAttr);

        IPropertyInfo GetProperty(string name);

        IPropertyInfo GetProperty(string name, BindingFlags bindingAttr);

        IPropertyInfo GetProperty(string name, IType returnType);

        IPropertyInfo GetProperty(string name, IType[] types);

        IPropertyInfo GetProperty(string name, IType returnType, IType[] types);

        IPropertyInfo GetProperty(
            string name,
            IType returnType,
            IType[] types,
            ParameterModifier[] modifiers);

        IPropertyInfo GetProperty(
            string name,
            BindingFlags bindingAttr,
            Binder binder,
            IType returnType,
            IType[] types,
            ParameterModifier[] modifiers);

        IPropertyInfo[] GetProperties();

        IPropertyInfo[] GetProperties(BindingFlags bindingAttr);

        IMemberInfo[] GetDefaultMembers();

        object InvokeMember(
            string name,
            BindingFlags invokeAttr,
            Binder binder,
            object target,
            object[] args);

        object InvokeMember(
            string name,
            BindingFlags invokeAttr,
            Binder binder,
            object target,
            object[] args,
            CultureInfo culture);

        object InvokeMember(
            string name,
            BindingFlags invokeAttr,
            Binder binder,
            object target,
            object[] args,
            ParameterModifier[] modifiers,
            CultureInfo culture,
            string[] namedParameters);

        IType GetInterface(string name);

        IType GetInterface(string name, bool ignoreCase);

        IType[] GetInterfaces();

        InterfaceMapping GetInterfaceMap(IType interfaceType);

        bool IsInstanceOfType(object o);

        bool IsEquivalentTo(IType other);

        IType GetEnumUnderlyingType();

        Array GetEnumValues();

        IType MakeArrayType();

        IType MakeArrayType(int rank);

        IType MakeByRefType();

        IType MakeGenericType(params IType[] typeArguments);

        IType MakePointerType();

        string ToString();

        bool Equals(object o);

        int GetHashCode();

        bool Equals(IType o);

        bool IsEnumDefined(object value);

        string GetEnumName(object value);

        string[] GetEnumNames();

        IType[] FindInterfaces(TypeFilter filter, object filterCriteria);

        IMemberInfo[] FindMembers(
            MemberTypes memberType,
            BindingFlags bindingAttr,
            MemberFilter filter,
            object filterCriteria);

        bool IsSubclassOf(IType c);

        bool IsAssignableFrom(IType c);
    }
}