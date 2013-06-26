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
        private Dictionary<Type, IContainerResult> containerDict = new Dictionary<Type, IContainerResult>();
        private IInjectionContainer injectionContainer;

        #region SetUp / TearDown

        [SetUp]
        public void Init()
        {
            var mock = new Mock<IInjectionContainer>();

            mock.Setup(x => x.Register<ITestInterface, TestImplementation>(LifeCycle.Transient))
                .Callback(() =>
            {
                containerDict.Add(typeof(ITestInterface), new ContainerResult<TestImplementation>(LifeCycle.Transient));
            });

            mock.Setup(x => x.Register<ITestSingleton, TestSingleton>(LifeCycle.Singleton))
                .Callback(() =>
            {
                containerDict.Add(typeof(ITestSingleton), new ContainerResult<TestSingleton>(LifeCycle.Singleton));
            });

            mock.Setup(x => x.Resolve<ITestInterface>())
                .Returns(() =>
            {
                var containerResult = containerDict[typeof(ITestInterface)];
                throw new NotImplementedException();
            });

            injectionContainer = mock.Object;
        }

        [TearDown]
        public void Dispose()
        {
            containerDict.Clear();
        }

        #endregion

        #region Tests

        [Test]
        public void IoC_Register_AddedToContainer()
        {
            injectionContainer.Register<ITestInterface, TestImplementation>();
            var containerResult = containerDict[typeof(ITestInterface)];

            Assert.AreEqual(containerResult.ResultType, typeof(TestImplementation), "Incorrect type has been registered in the container");
        }

        [Test]
        public void IoC_Register_IsLifeCycleAware()
        {
            injectionContainer.Register<ITestInterface, TestImplementation>();
            injectionContainer.Register<ITestSingleton, TestSingleton>(LifeCycle.Singleton);

            var testImpContainerResult = containerDict[typeof(ITestInterface)];
            var testSglContainerResult = containerDict[typeof(ITestSingleton)];

            Assert.AreEqual(testImpContainerResult.ResultType, typeof(TestImplementation), "Incorrect type has been registered in the container");
            Assert.AreEqual(testImpContainerResult.LifeCycle, LifeCycle.Transient, "Incorrect life cycle type has been registered");
            Assert.AreEqual(testSglContainerResult.ResultType, typeof(TestSingleton), "Incorrect type has been registered in the container");
            Assert.AreEqual(testSglContainerResult.LifeCycle, LifeCycle.Singleton, "Incorrect life cycle type has been registered");
        }

        [Test]
        public void IoC_Resolve_SuccessfulSingleTypeResolve()
        {
            var dependency = injectionContainer.Resolve<ITestInterface>();
        }

        #endregion
    }

    interface ITestInterface
    {
    }

    class TestImplementation : ITestInterface
    {
    }

    interface ITestSingleton { }

    class TestSingleton : ITestSingleton { }

    interface INotExists { }
}
