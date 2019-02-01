using System;
using System.Collections.Generic;
using System.Linq;

namespace injects
{
        public class Injector : IInjector, IDisposable
        {
            private readonly Dictionary<Type, Func<object>> _registeredTypes = new Dictionary<Type, Func<object>>();
            public void Register<TypeIn, TypeOut>() where TypeOut : TypeIn
            {
                _registeredTypes[typeof(TypeIn)] = () => Resolve<TypeOut>();
            }
            public void RegisterSingleton<T>(T obj)
            {
                _registeredTypes[typeof(T)] = () => obj;
            }
            public void Register<T>(T type)
            {
                _registeredTypes[typeof(T)] = () => type;
            }
            public T Resolve<T>()
            {
                return (T)Resolve(typeof(T));
            }
            public object Resolve(Type type)
            {
                if (_registeredTypes.ContainsKey(type))
                {
                    return _registeredTypes[type]();
                }

            var constructor = type.GetConstructors()
                    .OrderByDescending(x => x.GetParameters().Length)
                    .First();

            var args = constructor.GetParameters()
                    .Select(param => Resolve(param.ParameterType))
                    .ToArray();

                return Activator.CreateInstance(type, args);
            }
            public void Dispose()
            {
             
            }
        }
}
