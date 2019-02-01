using injects;
using NUnit.Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace injects.UnitTests
{
    public class InjectorTestBase
    {

    }

    [TestFixture]
    public class Injector_Resolve : InjectorTestBase
    {
        [Test]
        public void ResolveWithParams()
        {
            Injector injector = new Injector();
            object subject = injector.Resolve(typeof(TestA));
            subject.
        }

        class TestA
        {

        }
    }
}
