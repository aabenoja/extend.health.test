using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using NUnit.Framework;

using ExtendHealth.Modules.IoC;
using Moq;

namespace ExtendHealth.Tests.UnitTests.Modules
{
    [TestFixture]
    public class IoCContainerTests
    {
        private Dictionary<Type, Type> containerDict = new Dictionary<Type, Type>();
        private IInjectionContainer injectionContainer;

        #region SetUp / TearDown

        [SetUp]
        public void Init()
        {
            var mock = new Mock<IInjectionContainer>();
            mock.Setup(x => x.Register<ITestInterface, TestImplementation>(It.IsAny<LifeCycle>())).Callback(() =>
            {
                containerDict.Add(typeof(ITestInterface), typeof(TestImplementation));
            });
            mock.Setup(x => x.Resolve<ITestInterface>()).Returns(() =>
            {
                throw new NotImplementedException();
            });

            injectionContainer = mock.Object;
        }

        [TearDown]
        public void Dispose()
        {

        }

        #endregion

        #region Tests

        [Test]
        public void IoC_Register_AddedToContainer()
        {
            injectionContainer.Register<ITestInterface, TestImplementation>();
            Assert.AreEqual(containerDict[typeof(ITestInterface)], typeof(TestImplementation));
        }

        [Test]
        public void IoC_Resolve_SuccessfulResolve()
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
}
