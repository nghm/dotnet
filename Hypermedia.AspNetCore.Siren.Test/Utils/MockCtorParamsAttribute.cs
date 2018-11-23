using AutoFixture;
using AutoFixture.Kernel;
using AutoFixture.Xunit2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Hypermedia.AspNetCore.Siren.Test.Utils
{
    [Obsolete("Use InlineAutoMockData instead.")]
    public class MockCtorParamsAttribute : CustomizeAttribute
    {
        private readonly object[] _parameters;

        public MockCtorParamsAttribute(params object[] parameters) : base()
        {
            _parameters = parameters;
        }

        public override ICustomization GetCustomization(ParameterInfo parameter)
        {
            return new ParamConstructorCustomization(parameter, _parameters, StaticIndexes);
        }

        public int[] StaticIndexes { get; set; } = new int[0];
    }

    public class ParamConstructorCustomization : ICustomization
    {
        private readonly ParameterInfo _parameter;
        private readonly object[] _parameters;
        private readonly int[] _staticIndexes;

        public ParamConstructorCustomization(ParameterInfo parameter, object[] parameters, int[] staticIndexes)
        {
            _parameter = parameter;
            _parameters = parameters;
            _staticIndexes = staticIndexes.Select(i => i - 1).ToArray();
        }

        public void Customize(IFixture fixture)
        {
            if (_staticIndexes.Length > 0)
            {
                var type = _parameter.Member.DeclaringType;

                foreach (var staticIndex in _staticIndexes)
                {
                    if (staticIndex >= _parameters.Length) { continue; }

                    var staticMethodName = _parameters[staticIndex] as string;
                    var staticMethod = type.GetMethods()
                        .SingleOrDefault(m => GetStaticMethodByName(m, staticMethodName));

                    if (staticMethod == null)
                    {
                        throw new ArgumentException();
                    }

                    _parameters[staticIndex] = staticMethod.Invoke(null, new object[] { fixture });
                }
            }

            ISpecimenBuilder builder = new PredefinedParamSpecimenBuilder(_parameter, _parameters);

            fixture.Customizations.Add(builder);
        }

        private static bool GetStaticMethodByName(MethodBase methodInfo, string staticMethodName)
        {
            if (!methodInfo.IsStatic || methodInfo.IsAbstract || methodInfo.Name != staticMethodName)
            {
                return false;
            }

            var parameters = methodInfo.GetParameters();

            return parameters.Length == 1 && parameters[0].ParameterType == typeof(IFixture);
        }
    }

    public class PredefinedParamSpecimenBuilder : ISpecimenBuilder
    {
        private readonly ParameterInfo _parameter;
        private readonly object[] _parameters;

        public PredefinedParamSpecimenBuilder(ParameterInfo parameter, object[] parameters)
        {
            _parameter = parameter;
            _parameters = parameters;
        }

        public object Create(object request, ISpecimenContext context)
        {
            var par = request as ParameterInfo;

            if (par == _parameter)
            {
                var constructor = par.ParameterType
                    .GetConstructors()
                    .FirstOrDefault(c => c.GetParameters().Length == _parameters.Length);

                if (constructor == null)
                {
                    throw new ArgumentException();
                }

                var constructorParams = constructor.GetParameters();
                var finalParamList = new List<object>();

                for (var index = 0; index < _parameters.Length; index++)
                {
                    var parameter = _parameters[index];

                    if (parameter == null)
                    {
                        var req2 = constructorParams[index];

                        parameter = context.Resolve(req2);
                    }

                    finalParamList.Add(parameter);
                }

                return constructor.Invoke(finalParamList.ToArray());
            }

            return new NoSpecimen();
        }
    }
}