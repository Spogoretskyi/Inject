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
            Injector.Register<TestA, TestA>(n => new TestA());
            var subject = Injector.Resolve<TestA>();
            Assert.IsInstanceOf<TestA>(subject);
        }
        [Test]
        public void ResolveWithParams()
        {
            Injector.Register<TestA, TestA>(n => new TestA());
            Injector.Register<TestB, TestB>(n =>
            {
              var param = Injector.Resolve<TestB>();
              return new TestB(param);
            });
            var subject = Injector.Resolve<TestB>();
            Assert.IsInstanceOf(typeof(TestA), subject.A);
        }
        [Test]
        public void ItAllowsAParametrelessConstructor()
        {
            Injector.Register<TestC, TestC>(n => new TestC());
            var subject = Injector.Resolve<TestC>();
            Assert.IsTrue(subject.Invoked);
        }

        class TestA
        {
            
        }
        class TestB : TestA
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
            Injector.Register<IConstruction, Skyscraper>(n => new Skyscraper());
            var subject = Injector.Resolve<IConstruction>();
            Assert.IsInstanceOf<Skyscraper>(subject);
        }

        [Test]
        public void InitializeObjectWithDependencies()
        {
            Injector.Register<IConstruction, Mall>(c =>
            {
                var p1 = c.Resolve<Skyscraper>();
                return new Mall();
            });
            var subject = Injector.Resolve<IConstruction>();
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
            var food = new Milk();
            Injector.Register<Milk, Milk>(n => food);
            var subject = Injector.Resolve<Milk>();
            Assert.AreEqual(subject, food);
        }
        abstract class Food
        {
            
        }

        class Milk : Food
        {
            
        }
    }
}
