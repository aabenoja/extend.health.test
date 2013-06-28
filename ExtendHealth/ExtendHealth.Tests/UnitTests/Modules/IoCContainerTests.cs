using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Reflection;
using NUnit.Framework;

using ExtendHealth.Modules.IoC;
using Moq;

namespace ExtendHealth.Tests.UnitTests.Modules
{
    [TestFixture]
    public class IoCContainerTests
    {
        private InjectionContainer injectionContainer;

        public IoCContainerTests()
        {
            injectionContainer = new InjectionContainer();
        }

        #region SetUp / TearDown

        [SetUp]
        public void Init()
        { }

        [TearDown]
        public void Dispose()
        {
            injectionContainer.ClearContainer();
        }

        #endregion

        #region Tests

        [Test]
        public void IoC_Register_AddedToContainer()
        {
            injectionContainer.Register<ITestInterface, TestSimpleImplementation>();
            var resolvedObjected = injectionContainer.Resolve<ITestInterface>();
            
            Assert.IsInstanceOf(typeof(TestSimpleImplementation), resolvedObjected, "Incorrect type has been registered in the container");
        }

        [Test]
        public void IoC_Register_IsLifeCycleAware()
        {
            injectionContainer.Register<ITestInterface, TestSimpleImplementation>();
            injectionContainer.Register<ITestSingleton, TestSingleton>(LifeCycle.Singleton);

            var testImpContainerResult_1 = injectionContainer.Resolve<ITestInterface>();
            var testImpContainerResult_2 = injectionContainer.Resolve<ITestInterface>();
            var testSglContainerResult_1 = injectionContainer.Resolve<ITestSingleton>();
            var testSglContainerResult_2 = injectionContainer.Resolve<ITestSingleton>();

            Assert.IsInstanceOf(typeof(ITestInterface), testImpContainerResult_1, "Resolved object not of the expected type (ITestInterface)");
            Assert.AreNotSame(testImpContainerResult_2, testImpContainerResult_1, "Container has returned the same instance of the expected type (ITestInterface)");
            Assert.IsInstanceOf(typeof(ITestSingleton), testSglContainerResult_1, "Resolved object not of the expected type (ITestSingleton)");
            Assert.AreSame(testSglContainerResult_2, testSglContainerResult_1, "Container has returned two separate instances of the expected type (ITestSingleton)");
        }

        [Test]
        public void IoC_Resolve_SuccessfulSingleTypeResolve()
        {
            injectionContainer.Register<ITestInterface, TestSimpleImplementation>();
            var dependency = injectionContainer.Resolve<ITestInterface>();

            Assert.IsInstanceOf<TestSimpleImplementation>(dependency, "The resolved type does not match the desired type");
        }

        [Test]
        public void IoC_Resolve_SuccessfulSingletonResolved()
        {
            injectionContainer.Register<ITestSingleton, TestSingleton>(LifeCycle.Singleton);
            var dependency_1 = injectionContainer.Resolve(typeof(ITestSingleton));
            var dependency_2 = injectionContainer.Resolve(typeof(ITestSingleton));

            Assert.IsInstanceOf(typeof(ITestSingleton), dependency_1, "The resolved type does not match the desired type");
            Assert.AreSame(dependency_1, dependency_2, "Container has resolved to generate two separate instances of a singleton");
        }

        [Test]
        public void IoC_Resolve_ErrorOnUnregisteredType()
        {
            Assert.Throws(typeof(UnregisteredTypeException),
                new TestDelegate(() => { injectionContainer.Resolve(typeof(ITestInterface)); }), 
                "The requested type has been registered within the container");
        }

        [Test]
        public void IoC_Resolve_SuccessfulComplexType()
        {
            injectionContainer.Register<ITestInterface, TestSimpleImplementation>();
            injectionContainer.Register<ITestSingleton, TestSingleton>(LifeCycle.Singleton);
            injectionContainer.Register<ComplexType, ComplexType>();
            var dependency = injectionContainer.Resolve<ComplexType>();

            Assert.IsInstanceOf(typeof(ITestInterface), dependency.PublicDependencyA, "Complex type does not have an ITestInterface dependency");
            Assert.IsInstanceOf(typeof(ITestSingleton), dependency.PublicDependencyB, "Complex type does not have an ITestSingleton dependency");
        }

        #endregion

        #region private methods

        

        #endregion
    }

    #region test types

    interface ITestInterface
    {
    }

    class TestSimpleImplementation : ITestInterface
    {
    }

    interface ITestSingleton { }

    class TestSingleton : ITestSingleton { }

    interface INotExists { }

    class ComplexType
    {
        public ITestInterface PublicDependencyA { get; private set; }
        public ITestSingleton PublicDependencyB { get; private set; }

        public ComplexType(ITestInterface depenA, ITestSingleton depenB)
        {
            PublicDependencyA = depenA;
            PublicDependencyB = depenB;
        }
    }

    #endregion
}
