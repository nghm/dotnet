using System;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;

namespace AspNetCore.ReflectMap
{
    internal class TypeWrapper : ReflectedTypeWrapper<Type>, IType
    {
        public TypeWrapper(IReflection reflection, Type type)
            : base(reflection, type)
        { }

        public bool IsInterface
            => _underlyingType.IsInterface;

        public MemberTypes MemberType
            => _underlyingType.MemberType;

        public string Namespace
            => _underlyingType.Namespace;

        public string AssemblyQualifiedName
            => _underlyingType.AssemblyQualifiedName;

        public string FullName
            => _underlyingType.FullName;

        public Assembly Assembly
            => _underlyingType.Assembly;

        public Module Module
            => _underlyingType.Module;

        public bool IsNested
            => _underlyingType.IsNested;

        public IType DeclaringType
            => _reflection.TypeOf(_underlyingType.DeclaringType);

        public IMethodBase DeclaringMethod
            => _reflection.MethodBase(_underlyingType.DeclaringMethod);

        public IType ReflectedType
            => _reflection.TypeOf(_underlyingType.ReflectedType);

        public IType UnderlyingSystemType
            => _reflection.TypeOf(_underlyingType.UnderlyingSystemType);

        public bool IsTypeDefinition
            => _underlyingType.IsTypeDefinition;

        public bool IsArray
            => _underlyingType.IsArray;

        public bool IsByRef
            => _underlyingType.IsByRef;

        public bool IsPointer
            => _underlyingType.IsPointer;

        public bool IsConstructedGenericType
            => _underlyingType.IsConstructedGenericType;

        public bool IsGenericParameter
            => _underlyingType.IsGenericParameter;

        public bool IsGenericTypeParameter
            => _underlyingType.IsGenericTypeParameter;

        public bool IsGenericMethodParameter
            => _underlyingType.IsGenericMethodParameter;

        public bool IsGenericType
            => _underlyingType.IsGenericType;

        public bool IsGenericTypeDefinition
            => _underlyingType.IsGenericTypeDefinition;

        public bool IsSZArray
            => _underlyingType.IsSZArray;

        public bool IsVariableBoundArray
            => _underlyingType.IsVariableBoundArray;

        public bool IsByRefLike
            => _underlyingType.IsByRefLike;

        public bool HasElementType
            => _underlyingType.HasElementType;

        public IType[] GenericTypeArguments
            => _underlyingType.GenericTypeArguments
                .Select(argument => _reflection.TypeOf(argument))
                .ToArray();

        public int GenericParameterPosition
            => _underlyingType.GenericParameterPosition;

        public GenericParameterAttributes GenericParameterAttributes
            => _underlyingType.GenericParameterAttributes;

        public TypeAttributes Attributes
            => _underlyingType.Attributes;

        public bool IsAbstract
            => _underlyingType.IsAbstract;

        public bool IsImport
            => _underlyingType.IsImport;

        public bool IsSealed
            => _underlyingType.IsSealed;

        public bool IsSpecialName
            => _underlyingType.IsSpecialName;

        public bool IsClass
            => _underlyingType.IsClass;

        public bool IsNestedAssembly
            => _underlyingType.IsNestedAssembly;

        public bool IsNestedFamANDAssem
            => _underlyingType.IsNestedFamANDAssem;

        public bool IsNestedFamily
            => _underlyingType.IsNestedFamily;

        public bool IsNestedFamORAssem
            => _underlyingType.IsNestedFamORAssem;

        public bool IsNestedPrivate
            => _underlyingType.IsNestedPrivate;

        public bool IsNestedPublic
            => _underlyingType.IsNestedPublic;

        public bool IsNotPublic
            => _underlyingType.IsNotPublic;

        public bool IsPublic
            => _underlyingType.IsPublic;

        public bool IsAutoLayout
            => _underlyingType.IsAutoLayout;

        public bool IsExplicitLayout
            => _underlyingType.IsExplicitLayout;

        public bool IsLayoutSequential
            => _underlyingType.IsLayoutSequential;

        public bool IsAnsiClass
            => _underlyingType.IsAnsiClass;

        public bool IsAutoClass
            => _underlyingType.IsAutoClass;

        public bool IsUnicodeClass
            => _underlyingType.IsUnicodeClass;

        public bool IsCOMObject
            => _underlyingType.IsCOMObject;

        public bool IsContextful
            => _underlyingType.IsContextful;

        public bool IsCollectible
            => true; //_underlyingType.IsCollectible; TODO find a better solution here

        public bool IsEnum
            => _underlyingType.IsEnum;

        public bool IsMarshalByRef
            => _underlyingType.IsMarshalByRef;

        public bool IsPrimitive
            => _underlyingType.IsPrimitive;

        public bool IsValueType
            => _underlyingType.IsValueType;

        public bool IsSignatureType
            => _underlyingType.IsSignatureType;

        public bool IsSecurityCritical
            => _underlyingType.IsSecurityCritical;

        public bool IsSecuritySafeCritical
            => _underlyingType.IsSecuritySafeCritical;

        public bool IsSecurityTransparent
            => _underlyingType.IsSecurityTransparent;

        public StructLayoutAttribute StructLayoutAttribute
            => _underlyingType.StructLayoutAttribute;

        public IConstructorInfo TypeInitializer
            => _reflection.ConstructorInfo(_underlyingType.TypeInitializer);

        public RuntimeTypeHandle TypeHandle
            => _underlyingType.TypeHandle;

        public Guid GUID
            => _underlyingType.GUID;

        public IType BaseType
            => _reflection.TypeOf(_underlyingType.BaseType);

        public bool IsSerializable
            => _underlyingType.IsSerializable;

        public bool ContainsGenericParameters
            => _underlyingType.ContainsGenericParameters;

        public bool IsVisible
            => _underlyingType.IsVisible;

        public new IType GetType()
            => _reflection.TypeOf(_underlyingType.GetType());

        public IType GetElementType()
            => _reflection.TypeOf(_underlyingType.GetElementType());

        public int GetArrayRank()
            => _underlyingType.GetArrayRank();

        public IType GetGenericTypeDefinition()
            => _reflection.TypeOf(_underlyingType.GetGenericTypeDefinition());

        public IType[] GetGenericArguments()
            => _underlyingType.GetGenericArguments()
                .Select(argument => _reflection.TypeOf(argument))
                .ToArray();

        public IType[] GetGenericParameterConstraints()
            => _underlyingType.GetGenericParameterConstraints()
                .Select(argument => _reflection.TypeOf(argument))
                .ToArray();

        public IConstructorInfo GetConstructor(IType[] types)
            => _reflection.ConstructorInfo(
                _underlyingType.GetConstructor(
                    types.Select(argument => argument.GetUnderlyingType()).ToArray()));

        public IConstructorInfo GetConstructor(BindingFlags bindingAttr, Binder binder, IType[] types,
            ParameterModifier[] modifiers)
            => _reflection.ConstructorInfo(
                _underlyingType.GetConstructor(
                    bindingAttr,
                    binder,
                    types.Select(argument => argument.GetUnderlyingType()).ToArray(),
                    modifiers));

        public IConstructorInfo GetConstructor(BindingFlags bindingAttr, Binder binder, CallingConventions callConvention,
            IType[] types, ParameterModifier[] modifiers)
            => _reflection.ConstructorInfo(
                _underlyingType.GetConstructor(
                    bindingAttr,
                    binder,
                    callConvention,
                    types.Select(argument => argument.GetUnderlyingType()).ToArray(),
                    modifiers));

        public IConstructorInfo[] GetConstructors()
            => _underlyingType.GetConstructors()
                .Select(constructor => _reflection.ConstructorInfo(constructor))
                .ToArray();

        public IConstructorInfo[] GetConstructors(BindingFlags bindingAttr)
            => _underlyingType.GetConstructors(bindingAttr)
                .Select(constructor => _reflection.ConstructorInfo(constructor))
                .ToArray();

        public EventInfo GetEvent(string name)
            => _underlyingType.GetEvent(name);

        public EventInfo GetEvent(string name, BindingFlags bindingAttr)
            => _underlyingType.GetEvent(name, bindingAttr);

        public EventInfo[] GetEvents()
            => _underlyingType.GetEvents();

        public EventInfo[] GetEvents(BindingFlags bindingAttr)
            => _underlyingType.GetEvents(bindingAttr);

        public IFieldInformation GetField(string name)
            => _reflection.FieldInfo(_underlyingType.GetField(name));

        public IFieldInformation GetField(string name, BindingFlags bindingAttr)
            => _reflection.FieldInfo(_underlyingType.GetField(name, bindingAttr));

        public IFieldInformation[] GetFields()
            => _underlyingType.GetFields()
                .Select(field => _reflection.FieldInfo(field))
                .ToArray();

        public IFieldInformation[] GetFields(BindingFlags bindingAttr)
            => _underlyingType.GetFields(bindingAttr)
                .Select(field => _reflection.FieldInfo(field))
                .ToArray();

        public IMemberInfo[] GetMember(string name)
            => _underlyingType.GetMember(name)
                .Select(member => _reflection.MemberInfo(member))
                .ToArray();

        public IMemberInfo[] GetMember(string name, BindingFlags bindingAttr)
            => _underlyingType.GetMember(name)
                .Select(member => _reflection.MemberInfo(member))
                .ToArray();

        public IMemberInfo[] GetMember(string name, MemberTypes type, BindingFlags bindingAttr)
            => _underlyingType.GetMember(name, type, bindingAttr)
                .Select(member => _reflection.MemberInfo(member))
                .ToArray();

        public IMemberInfo[] GetMembers()
            => _underlyingType.GetMembers()
                .Select(member => _reflection.MemberInfo(member))
                .ToArray();

        public IMemberInfo[] GetMembers(BindingFlags bindingAttr)
            => _underlyingType.GetMembers(bindingAttr)
                .Select(member => _reflection.MemberInfo(member))
                .ToArray();

        public IMethodInfo GetMethod(string name)
            => _reflection.MethodInfo(_underlyingType.GetMethod(name));

        public IMethodInfo GetMethod(string name, BindingFlags bindingAttr)
            => _reflection.MethodInfo(_underlyingType.GetMethod(name, bindingAttr));

        public IMethodInfo GetMethod(string name, IType[] types)
            => _reflection.MethodInfo(_underlyingType.GetMethod(
                name,
                types.Select(type => type.GetUnderlyingType()).ToArray()));

        public IMethodInfo GetMethod(string name, IType[] types, ParameterModifier[] modifiers)
            => _reflection.MethodInfo(_underlyingType.GetMethod(
                name,
                types.Select(type => type.GetUnderlyingType()).ToArray(),
                modifiers));

        public IMethodInfo GetMethod(string name, BindingFlags bindingAttr, Binder binder, IType[] types, ParameterModifier[] modifiers)
            => _reflection.MethodInfo(_underlyingType.GetMethod(
                name,
                bindingAttr,
                binder,
                types.Select(type => type.GetUnderlyingType()).ToArray(),
                modifiers));

        public IMethodInfo GetMethod(
            string name,
            BindingFlags bindingAttr,
            Binder binder,
            CallingConventions callConvention,
            IType[] types,
            ParameterModifier[] modifiers)
            => _reflection.MethodInfo(_underlyingType.GetMethod(
                name,
                bindingAttr,
                binder,
                callConvention,
                types.Select(type => type.GetUnderlyingType()).ToArray(),
                modifiers));

        public IMethodInfo GetMethod(string name, int genericParameterCount, IType[] types)
            => _reflection.MethodInfo(_underlyingType.GetMethod(
                name,
                genericParameterCount,
                types.Select(type => type.GetUnderlyingType()).ToArray()));

        public IMethodInfo GetMethod(string name, int genericParameterCount, IType[] types, ParameterModifier[] modifiers)
            => _reflection.MethodInfo(_underlyingType.GetMethod(
                name,
                genericParameterCount,
                types.Select(type => type.GetUnderlyingType()).ToArray(),
                modifiers));

        public IMethodInfo GetMethod(
            string name,
            int genericParameterCount,
            BindingFlags bindingAttr,
            Binder binder,
            IType[] types,
            ParameterModifier[] modifiers)
            => _reflection.MethodInfo(_underlyingType.GetMethod(
                name,
                genericParameterCount,
                bindingAttr,
                binder,
                types.Select(type => type.GetUnderlyingType()).ToArray(),
                modifiers));

        public IMethodInfo GetMethod(
            string name,
            int genericParameterCount,
            BindingFlags bindingAttr,
            Binder binder,
            CallingConventions callConvention,
            IType[] types,
            ParameterModifier[] modifiers)
            => _reflection.MethodInfo(_underlyingType.GetMethod(
                name,
                genericParameterCount,
                bindingAttr,
                binder,
                callConvention,
                types.Select(type => type.GetUnderlyingType()).ToArray(),
                modifiers));

        public IMethodInfo[] GetMethods()
            => _underlyingType.GetMethods()
                .Select(method => _reflection.MethodInfo(method))
                .ToArray();

        public IMethodInfo[] GetMethods(BindingFlags bindingAttr)
            => _underlyingType.GetMethods(bindingAttr)
                .Select(method => _reflection.MethodInfo(method))
                .ToArray();

        public IType GetNestedType(string name)
            => _reflection.TypeOf(_underlyingType.GetNestedType(name));

        public IType GetNestedType(string name, BindingFlags bindingAttr)
            => _reflection.TypeOf(_underlyingType.GetNestedType(name, bindingAttr));

        public IType[] GetNestedTypes()
            => _underlyingType.GetNestedTypes()
                .Select(type => _reflection.TypeOf(type))
                .ToArray();

        public IType[] GetNestedTypes(BindingFlags bindingAttr)
            => _underlyingType.GetNestedTypes(bindingAttr)
                .Select(type => _reflection.TypeOf(type))
                .ToArray();

        public IPropertyInfo GetProperty(string name)
            => _reflection.PropertyInfo(_underlyingType.GetProperty(name));

        public IPropertyInfo GetProperty(string name, BindingFlags bindingAttr)
            => _reflection.PropertyInfo(_underlyingType.GetProperty(name, bindingAttr));

        public IPropertyInfo GetProperty(string name, IType returnType)
            => _reflection.PropertyInfo(_underlyingType.GetProperty(name, returnType.GetUnderlyingType()));

        public IPropertyInfo GetProperty(string name, IType[] types)
            => _reflection.PropertyInfo(_underlyingType.GetProperty(
                name,
                types.Select(type => type.GetUnderlyingType()).ToArray()));

        public IPropertyInfo GetProperty(string name, IType returnType, IType[] types)
            => _reflection.PropertyInfo(_underlyingType.GetProperty(
                name,
                returnType.GetUnderlyingType(),
                types.Select(type => type.GetUnderlyingType()).ToArray()));

        public IPropertyInfo GetProperty(string name, IType returnType, IType[] types, ParameterModifier[] modifiers)
            => _reflection.PropertyInfo(_underlyingType.GetProperty(
                name,
                returnType.GetUnderlyingType(),
                types.Select(type => type.GetUnderlyingType()).ToArray(),
                modifiers));

        public IPropertyInfo GetProperty(
            string name,
            BindingFlags bindingAttr,
            Binder binder,
            IType returnType,
            IType[] types,
            ParameterModifier[] modifiers)
            => _reflection.PropertyInfo(_underlyingType.GetProperty(
                name,
                bindingAttr,
                binder,
                returnType.GetUnderlyingType(),
                types.Select(type => type.GetUnderlyingType()).ToArray(),
                modifiers));

        public IPropertyInfo[] GetProperties()
            => _underlyingType.GetProperties()
                .Select(property => _reflection.PropertyInfo(property))
                .ToArray();

        public IPropertyInfo[] GetProperties(BindingFlags bindingAttr)
            => _underlyingType.GetProperties(bindingAttr)
                .Select(property => _reflection.PropertyInfo(property))
                .ToArray();

        public IMemberInfo[] GetDefaultMembers()
            => _underlyingType.GetDefaultMembers()
                .Select(member => _reflection.MemberInfo(member))
                .ToArray();

        public object InvokeMember(string name, BindingFlags invokeAttr, Binder binder, object target, object[] args)
            => _underlyingType.InvokeMember(name, invokeAttr, binder, target, args);

        public object InvokeMember(
            string name,
            BindingFlags invokeAttr,
            Binder binder,
            object target,
            object[] args,
            CultureInfo culture)
            => _underlyingType.InvokeMember(name, invokeAttr, binder, target, args, culture);

        public object InvokeMember(
            string name,
            BindingFlags invokeAttr,
            Binder binder,
            object target,
            object[] args,
            ParameterModifier[] modifiers,
            CultureInfo culture,
            string[] namedParameters)
            => _underlyingType.InvokeMember(name, invokeAttr, binder, target, args, modifiers, culture, namedParameters);

        public IType GetInterface(string name)
            => _reflection.TypeOf(_underlyingType.GetInterface(name));

        public IType GetInterface(string name, bool ignoreCase)
            => _reflection.TypeOf(_underlyingType.GetInterface(name, ignoreCase));

        public IType[] GetInterfaces()
            => _underlyingType.GetInterfaces()
                .Select(inter => _reflection.TypeOf(inter))
                .ToArray();

        public InterfaceMapping GetInterfaceMap(IType interfaceType)
            => _underlyingType.GetInterfaceMap(interfaceType.GetUnderlyingType());

        public bool IsInstanceOfType(object o)
            => _underlyingType.IsInstanceOfType(o);

        public bool IsEquivalentTo(IType other)
            => _underlyingType.IsEquivalentTo(other.GetUnderlyingType());

        public IType GetEnumUnderlyingType()
            => _reflection.TypeOf(_underlyingType.GetEnumUnderlyingType());

        public Array GetEnumValues()
            => _underlyingType.GetEnumValues();

        public IType MakeArrayType()
            => _reflection.TypeOf(_underlyingType.MakeArrayType());

        public IType MakeArrayType(int rank)
            => _reflection.TypeOf(_underlyingType.MakeArrayType(rank));

        public IType MakeByRefType()
            => _reflection.TypeOf(_underlyingType.MakeByRefType());

        public IType MakeGenericType(params IType[] typeArguments)
            => _reflection.TypeOf(_underlyingType.MakeGenericType());

        public IType MakePointerType()
            => _reflection.TypeOf(_underlyingType.MakePointerType());

        public bool Equals(IType o)
            => _underlyingType.Equals(o.GetUnderlyingType());

        public bool IsEnumDefined(object value)
            => _underlyingType.IsEnumDefined(value);

        public string GetEnumName(object value)
            => _underlyingType.GetEnumName(value);

        public string[] GetEnumNames()
            => _underlyingType.GetEnumNames();

        public IType[] FindInterfaces(TypeFilter filter, object filterCriteria)
            => _underlyingType.FindInterfaces(filter, filterCriteria)
                .Select(inter => _reflection.TypeOf(inter))
                .ToArray();

        public IMemberInfo[] FindMembers(MemberTypes memberType, BindingFlags bindingAttr, MemberFilter filter, object filterCriteria)
            => _underlyingType.FindMembers(memberType, bindingAttr, filter, filterCriteria)
                .Select(member => _reflection.MemberInfo(member))
                .ToArray();

        public bool IsSubclassOf(IType c)
            => _underlyingType.IsSubclassOf(c.GetUnderlyingType());

        public bool IsAssignableFrom(IType c)
            => _underlyingType.IsAssignableFrom(c.GetUnderlyingType());
    }
}