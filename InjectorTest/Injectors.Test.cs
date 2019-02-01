using injects;
using NUnit.Framework;

namespace Injectors.Test
{
    public class InjectorTestBase
    {
        protected Injector Injector;
        [SetUp]
        public void BeforeEach()
        {
            Injector = new Injector();
        }
        [TearDown]
        public void AfterEach()
        {
            Injector = null;
        }
    }

    [TestFixture]
    public class InjectorResolve : InjectorTestBase
    {
        [Test]
        public void ResolveWithNoParams()
        {
            var subject = (TestA)Injector.Resolve(typeof(TestA));
            Assert.IsInstanceOf<TestA>(subject);
        }
        [Test]
        public void ResolveWithParams()
        {
            var subject = (TestB)Injector.Resolve(typeof(TestB));
            Assert.IsInstanceOf<TestA>(subject.A);
        }
        [Test]
        public void ItAllowsAParametrelessConstructor()
        {
            var subject = (TestC)Injector.Resolve(typeof(TestC));
            Assert.IsTrue(subject.Invoked);
        }
        [Test]
        public void ItAllowsGenericInitialization()
        {
            var subject = Injector.Resolve<TestA>();
            Assert.IsInstanceOf(typeof(TestA), subject);
        }
        class TestA
        {
            
        }
        class TestB
        {
            public TestA A { get; }
            public TestB()
            { }

            public TestB(TestA a)
            {
                A = a;
            }
        }

        class TestC
        {
            public bool? Invoked { get; set; }
            public TestC()
            {
                Invoked = true;
            }
        }
    }

    [TestFixture]
    public class InjectorRegister : InjectorTestBase
    {
        [Test]
        public void RegisterATypeFromAnInterface()
        {
            Injector.Register<IConstruction, Skyscraper>();
            var subject = Injector.Resolve<IConstruction>();
            Assert.IsInstanceOf<Skyscraper>(subject);
        }

        [Test]
        public void InitializeObjectWithDependencies()
        {
            Injector.Register<IConstruction, Mall>();
            var subject = (Mall)Injector.Resolve<IConstruction>();
            Assert.IsInstanceOf<Building>(subject);
        }

        interface IConstruction
        {
            string Type { get; }
        }

        abstract class Building : IConstruction
        {
            public double Height { get; }
            public double Width { get; }
            public string Type => "Building";
        }

        class Skyscraper : Building
        {
            public new string Type => "business center";
            public new double Height => 630.0;
            public new double Width => 300.0;
        }

        class Mall : Building, IConstruction
        {
            public new string Type => "shopping mall";
            public new double Height => 60.0;
            public new double Width => 860.0;
        }

    }

    [TestFixture]
    public class InjectorRegisterSingleton : InjectorTestBase
    {
        [Test]
        public void ReturnsASingleInstance()
        {
            var food = new Food();
            Injector.RegisterSingleton(food);
            var subject = Injector.Resolve<Food>();
            Assert.AreEqual(subject, food);
        }

        class Food
        {
            
        }

    }

}
