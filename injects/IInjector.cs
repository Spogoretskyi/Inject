using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace injects
{
    public interface IInjector
    {
        void Register<TypeIn, TypeOut>() where TypeOut : TypeIn;
        void Register<T>(T type);
         T Resolve<T>();
        object Resolve(Type type);

    }
}
