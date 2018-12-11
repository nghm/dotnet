using System;
using System.Globalization;
using System.Reflection;
using System.Runtime.InteropServices;

namespace AspNetCore.ReflectMap
{
    internal class TypeWrapper : IType
    {
        private readonly Type _type;

        public TypeWrapper(Type type)
        {
            _type = type;
        }

        public bool IsInterface { get; }
        public MemberTypes MemberType { get; }
        public string Namespace { get; }
        public string AssemblyQualifiedName { get; }
        public string FullName { get; }
        public Assembly Assembly { get; }
        public Module Module { get; }
        public bool IsNested { get; }
        public IType DeclaringType { get; }
        public IMethodBase DeclaringMethod { get; }
        public IType ReflectedType { get; }
        public IType UnderlyingSystemType { get; }
        public bool IsTypeDefinition { get; }
        public bool IsArray { get; }
        public bool IsByRef { get; }
        public bool IsPointer { get; }
        public bool IsConstructedGenericType { get; }
        public bool IsGenericParameter { get; }
        public bool IsGenericTypeParameter { get; }
        public bool IsGenericMethodParameter { get; }
        public bool IsGenericType { get; }
        public bool IsGenericTypeDefinition { get; }
        public bool IsSZArray { get; }
        public bool IsVariableBoundArray { get; }
        public bool IsByRefLike { get; }
        public bool HasElementType { get; }
        public IType[] GenericTypeArguments { get; }
        public int GenericParameterPosition { get; }
        public GenericParameterAttributes GenericParameterAttributes { get; }
        public TypeAttributes Attributes { get; }
        public bool IsAbstract { get; }
        public bool IsImport { get; }
        public bool IsSealed { get; }
        public bool IsSpecialName { get; }
        public bool IsClass { get; }
        public bool IsNestedAssembly { get; }
        public bool IsNestedFamANDAssem { get; }
        public bool IsNestedFamily { get; }
        public bool IsNestedFamORAssem { get; }
        public bool IsNestedPrivate { get; }
        public bool IsNestedPublic { get; }
        public bool IsNotPublic { get; }
        public bool IsPublic { get; }
        public bool IsAutoLayout { get; }
        public bool IsExplicitLayout { get; }
        public bool IsLayoutSequential { get; }
        public bool IsAnsiClass { get; }
        public bool IsAutoClass { get; }
        public bool IsUnicodeClass { get; }
        public bool IsCOMObject { get; }
        public bool IsContextful { get; }
        public bool IsCollectible { get; }
        public bool IsEnum { get; }
        public bool IsMarshalByRef { get; }
        public bool IsPrimitive { get; }
        public bool IsValueType { get; }
        public bool IsSignatureType { get; }
        public bool IsSecurityCritical { get; }
        public bool IsSecuritySafeCritical { get; }
        public bool IsSecurityTransparent { get; }
        public StructLayoutAttribute StructLayoutAttribute { get; }
        public IConstructorInfo TypeInitializer { get; }
        public RuntimeTypeHandle TypeHandle { get; }
        public Guid GUID { get; }
        public IType BaseType { get; }
        public bool IsSerializable { get; }
        public bool ContainsGenericParameters { get; }
        public bool IsVisible { get; }
        public IType GetType()
        {
            throw new NotImplementedException();
        }

        public IType GetElementType()
        {
            throw new NotImplementedException();
        }

        public int GetArrayRank()
        {
            throw new NotImplementedException();
        }

        public IType GetGenericTypeDefinition()
        {
            throw new NotImplementedException();
        }

        public IType[] GetGenericArguments()
        {
            throw new NotImplementedException();
        }

        public IType[] GetGenericParameterConstraints()
        {
            throw new NotImplementedException();
        }

        public IConstructorInfo GetConstructor(IType[] types)
        {
            throw new NotImplementedException();
        }

        public IConstructorInfo GetConstructor(BindingFlags bindingAttr, Binder binder, IType[] types, ParameterModifier[] modifiers)
        {
            throw new NotImplementedException();
        }

        public IConstructorInfo GetConstructor(BindingFlags bindingAttr, Binder binder, CallingConventions callConvention,
            IType[] types, ParameterModifier[] modifiers)
        {
            throw new NotImplementedException();
        }

        public IConstructorInfo[] GetConstructors()
        {
            throw new NotImplementedException();
        }

        public IConstructorInfo[] GetConstructors(BindingFlags bindingAttr)
        {
            throw new NotImplementedException();
        }

        public EventInfo GetEvent(string name)
        {
            throw new NotImplementedException();
        }

        public EventInfo GetEvent(string name, BindingFlags bindingAttr)
        {
            throw new NotImplementedException();
        }

        public EventInfo[] GetEvents()
        {
            throw new NotImplementedException();
        }

        public EventInfo[] GetEvents(BindingFlags bindingAttr)
        {
            throw new NotImplementedException();
        }

        public IFieldInformation GetField(string name)
        {
            throw new NotImplementedException();
        }

        public IFieldInformation GetField(string name, BindingFlags bindingAttr)
        {
            throw new NotImplementedException();
        }

        public IFieldInformation[] GetFields()
        {
            throw new NotImplementedException();
        }

        public IFieldInformation[] GetFields(BindingFlags bindingAttr)
        {
            throw new NotImplementedException();
        }

        public IMemberInfo[] GetMember(string name)
        {
            throw new NotImplementedException();
        }

        public IMemberInfo[] GetMember(string name, BindingFlags bindingAttr)
        {
            throw new NotImplementedException();
        }

        public IMemberInfo[] GetMember(string name, MemberTypes type, BindingFlags bindingAttr)
        {
            throw new NotImplementedException();
        }

        public IMemberInfo[] GetMembers()
        {
            throw new NotImplementedException();
        }

        public IMemberInfo[] GetMembers(BindingFlags bindingAttr)
        {
            throw new NotImplementedException();
        }

        public IMethodInfo GetMethod(string name)
        {
            throw new NotImplementedException();
        }

        public IMethodInfo GetMethod(string name, BindingFlags bindingAttr)
        {
            throw new NotImplementedException();
        }

        public IMethodInfo GetMethod(string name, IType[] types)
        {
            throw new NotImplementedException();
        }

        public IMethodInfo GetMethod(string name, IType[] types, ParameterModifier[] modifiers)
        {
            throw new NotImplementedException();
        }

        public IMethodInfo GetMethod(string name, BindingFlags bindingAttr, Binder binder, IType[] types,
            ParameterModifier[] modifiers)
        {
            throw new NotImplementedException();
        }

        public IMethodInfo GetMethod(string name, BindingFlags bindingAttr, Binder binder, CallingConventions callConvention,
            IType[] types, ParameterModifier[] modifiers)
        {
            throw new NotImplementedException();
        }

        public IMethodInfo GetMethod(string name, int genericParameterCount, IType[] types)
        {
            throw new NotImplementedException();
        }

        public IMethodInfo GetMethod(string name, int genericParameterCount, IType[] types, ParameterModifier[] modifiers)
        {
            throw new NotImplementedException();
        }

        public IMethodInfo GetMethod(string name, int genericParameterCount, BindingFlags bindingAttr, Binder binder, IType[] types,
            ParameterModifier[] modifiers)
        {
            throw new NotImplementedException();
        }

        public IMethodInfo GetMethod(string name, int genericParameterCount, BindingFlags bindingAttr, Binder binder,
            CallingConventions callConvention, IType[] types, ParameterModifier[] modifiers)
        {
            throw new NotImplementedException();
        }

        public IMethodInfo[] GetMethods()
        {
            throw new NotImplementedException();
        }

        public IMethodInfo[] GetMethods(BindingFlags bindingAttr)
        {
            throw new NotImplementedException();
        }

        public IType GetNestedType(string name)
        {
            throw new NotImplementedException();
        }

        public IType GetNestedType(string name, BindingFlags bindingAttr)
        {
            throw new NotImplementedException();
        }

        public IType[] GetNestedTypes()
        {
            throw new NotImplementedException();
        }

        public IType[] GetNestedTypes(BindingFlags bindingAttr)
        {
            throw new NotImplementedException();
        }

        public IPropertyInfo GetProperty(string name)
        {
            throw new NotImplementedException();
        }

        public IPropertyInfo GetProperty(string name, BindingFlags bindingAttr)
        {
            throw new NotImplementedException();
        }

        public IPropertyInfo GetProperty(string name, IType returnType)
        {
            throw new NotImplementedException();
        }

        public IPropertyInfo GetProperty(string name, IType[] types)
        {
            throw new NotImplementedException();
        }

        public IPropertyInfo GetProperty(string name, IType returnType, IType[] types)
        {
            throw new NotImplementedException();
        }

        public IPropertyInfo GetProperty(string name, IType returnType, IType[] types, ParameterModifier[] modifiers)
        {
            throw new NotImplementedException();
        }

        public IPropertyInfo GetProperty(string name, BindingFlags bindingAttr, Binder binder, IType returnType, IType[] types,
            ParameterModifier[] modifiers)
        {
            throw new NotImplementedException();
        }

        public IPropertyInfo[] GetProperties()
        {
            throw new NotImplementedException();
        }

        public IPropertyInfo[] GetProperties(BindingFlags bindingAttr)
        {
            throw new NotImplementedException();
        }

        public IMemberInfo[] GetDefaultMembers()
        {
            throw new NotImplementedException();
        }

        public object InvokeMember(string name, BindingFlags invokeAttr, Binder binder, object target, object[] args)
        {
            throw new NotImplementedException();
        }

        public object InvokeMember(string name, BindingFlags invokeAttr, Binder binder, object target, object[] args,
            CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public object InvokeMember(string name, BindingFlags invokeAttr, Binder binder, object target, object[] args,
            ParameterModifier[] modifiers, CultureInfo culture, string[] namedParameters)
        {
            throw new NotImplementedException();
        }

        public IType GetInterface(string name)
        {
            throw new NotImplementedException();
        }

        public IType GetInterface(string name, bool ignoreCase)
        {
            throw new NotImplementedException();
        }

        public IType[] GetInterfaces()
        {
            throw new NotImplementedException();
        }

        public InterfaceMapping GetInterfaceMap(IType interfaceType)
        {
            throw new NotImplementedException();
        }

        public bool IsInstanceOfType(object o)
        {
            throw new NotImplementedException();
        }

        public bool IsEquivalentTo(IType other)
        {
            throw new NotImplementedException();
        }

        public IType GetEnumUnderlyingType()
        {
            throw new NotImplementedException();
        }

        public Array GetEnumValues()
        {
            throw new NotImplementedException();
        }

        public IType MakeArrayType()
        {
            throw new NotImplementedException();
        }

        public IType MakeArrayType(int rank)
        {
            throw new NotImplementedException();
        }

        public IType MakeByRefType()
        {
            throw new NotImplementedException();
        }

        public IType MakeGenericType(params IType[] typeArguments)
        {
            throw new NotImplementedException();
        }

        public IType MakePointerType()
        {
            throw new NotImplementedException();
        }

        public bool Equals(IType o)
        {
            throw new NotImplementedException();
        }

        public bool IsEnumDefined(object value)
        {
            throw new NotImplementedException();
        }

        public string GetEnumName(object value)
        {
            throw new NotImplementedException();
        }

        public string[] GetEnumNames()
        {
            throw new NotImplementedException();
        }

        public IType[] FindInterfaces(TypeFilter filter, object filterCriteria)
        {
            throw new NotImplementedException();
        }

        public IMemberInfo[] FindMembers(MemberTypes memberType, BindingFlags bindingAttr, MemberFilter filter, object filterCriteria)
        {
            throw new NotImplementedException();
        }

        public bool IsSubclassOf(IType c)
        {
            throw new NotImplementedException();
        }

        public bool IsAssignableFrom(IType c)
        {
            throw new NotImplementedException();
        }
    }
}