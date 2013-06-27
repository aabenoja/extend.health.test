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
        private Dictionary<Type, IContainerResult> containerDict;// = new Dictionary<Type, IContainerResult>();
        private IInjectionContainer injectionContainer;

        #region SetUp / TearDown

        [SetUp]
        public void Init()
        {
            //injectionContainer = CreateMoqContainer();
            injectionContainer = new InjectionContainer();
        }

        [TearDown]
        public void Dispose()
        {
            //containerDict.Clear();
        }

        #endregion

        #region Tests

        [Test]
        public void IoC_Register_AddedToContainer()
        {
            injectionContainer.Register<ITestInterface, TestSimpleImplementation>();
            //var containerResult = containerDict[typeof(ITestInterface)];
            var resolvedObjected = injectionContainer.Resolve(typeof(ITestInterface));
            
            //Assert.AreEqual(typeof(TestSimpleImplementation), containerResult.ResultType, "Incorrect type has been registered in the container");
            Assert.IsInstanceOf(typeof(TestSimpleImplementation), resolvedObjected, "Incorrect type has been registered in the container");
        }

        [Test]
        public void IoC_Register_IsLifeCycleAware()
        {
            injectionContainer.Register<ITestInterface, TestSimpleImplementation>();
            injectionContainer.Register<ITestSingleton, TestSingleton>(LifeCycle.Singleton);

            var testImpContainerResult_1 = injectionContainer.Resolve(typeof(ITestInterface));// containerDict[typeof(ITestInterface)];
            var testImpContainerResult_2 = injectionContainer.Resolve(typeof(ITestInterface));
            var testSglContainerResult_1 = injectionContainer.Resolve(typeof(ITestSingleton));// containerDict[typeof(ITestSingleton)];
            var testSglContainerResult_2 = injectionContainer.Resolve(typeof(ITestSingleton));// containerDict[typeof(ITestSingleton)];

            //Assert.AreEqual(testImpContainerResult_1.ResultType, typeof(TestSimpleImplementation), "Incorrect type has been registered in the container");
            //Assert.AreEqual(testImpContainerResult_1.LifeCycle, LifeCycle.Transient, "Incorrect life cycle type has been registered");
            //Assert.AreEqual(testSglContainerResult_1.ResultType, typeof(TestSingleton), "Incorrect type has been registered in the container");
            //Assert.AreSame(testSglContainerResult_1, testSglContainerResult_2, "Container has returned two separate instances of a singleton");

            Assert.IsInstanceOf(typeof(ITestInterface), testImpContainerResult_1, "Resolved object not of the expected type (ITestInterface)");
            Assert.AreNotSame(testImpContainerResult_2, testImpContainerResult_1, "Container has returned the same instance of the expected type (ITestInterface)");
            Assert.IsInstanceOf(typeof(ITestSingleton), testSglContainerResult_1, "Resolved object not of the expected type (ITestSingleton)");
            Assert.AreSame(testSglContainerResult_2, testSglContainerResult_1, "Container has returned two separate instances of the expected type (ITestSingleton)");
        }

        [Test]
        public void IoC_Resolve_SuccessfulSingleTypeResolve()
        {
            injectionContainer.Register<ITestInterface, TestSimpleImplementation>();
            var dependency = injectionContainer.Resolve(typeof(ITestInterface));
            Assert.AreEqual(dependency.GetType(), typeof(TestSimpleImplementation), "The resolved type does not match the desired type");
        }

        [Test]
        public void IoC_Resolve_SuccessfulSingletonResolved()
        {
            injectionContainer.Register<ITestSingleton, TestSingleton>(LifeCycle.Singleton);
            var dependency_1 = injectionContainer.Resolve(typeof(ITestSingleton));
            var dependency_2 = injectionContainer.Resolve(typeof(ITestSingleton));
            //var containerResult = containerDict[typeof(ITestSingleton)];

            //Assert.AreEqual(dependency.GetType(), typeof(TestSingleton), "The resolved type does not match the desired type");
            //Assert.NotNull(containerResult.Instance, "The container failed to save an instance of a singleton");

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
            var dependency = (ComplexType)injectionContainer.Resolve(typeof(ComplexType));

            Assert.IsInstanceOf(typeof(ITestInterface), dependency.PublicDependencyA, "Complex type does not have an ITestInterface dependency");
            Assert.IsInstanceOf(typeof(ITestSingleton), dependency.PublicDependencyB, "Complex type does not have an ITestSingleton dependency");
        }

        #endregion

        #region private methods

        private IInjectionContainer CreateMoqContainer()
        {
            var mock = new Mock<IInjectionContainer>();

            mock.Setup(x => x.Register<ITestInterface, TestSimpleImplementation>(LifeCycle.Transient))
                .Callback(() =>
            {
                containerDict.Add(typeof(ITestInterface), new ContainerResult(typeof(TestSimpleImplementation), LifeCycle.Transient));
            });

            mock.Setup(x => x.Register<ITestSingleton, TestSingleton>(LifeCycle.Singleton))
                .Callback(() =>
            {
                containerDict.Add(typeof(ITestSingleton), new ContainerResult(typeof(TestSingleton), LifeCycle.Singleton));
            });

            mock.Setup(x => x.Register<ComplexType, ComplexType>(LifeCycle.Transient))
                .Callback(() =>
                {
                    containerDict.Add(typeof(ComplexType), new ContainerResult(typeof(ComplexType), LifeCycle.Transient));
                });

            mock.Setup(x => x.Resolve(typeof(ITestInterface)))
                .Returns(() =>
            {
                try
                {
                    var containerResult = containerDict[typeof(ITestInterface)];
                    var requestedType = containerResult.ResultType;
                    var constructor = requestedType.GetConstructor(Type.EmptyTypes);
                    return (ITestInterface)constructor.Invoke(null);
                }
                catch (KeyNotFoundException)
                {
                    throw new UnregisteredTypeException();
                }
            });

            mock.Setup(x => x.Resolve(typeof(ITestInterface)))
                .Returns(() =>
                {
                    var containerResult = containerDict[typeof(ITestSingleton)];
                    if (containerResult.LifeCycle == LifeCycle.Singleton && containerResult.Instance == null)
                    {
                        //get default constructor
                        var constructor = containerResult.ResultType.GetConstructor(Type.EmptyTypes);
                        containerResult.Instance = constructor.Invoke(null);
                    }
                    return (ITestSingleton)containerResult.Instance;
                });

            mock.Setup(x => x.Resolve(typeof(ITestInterface)))
                .Returns(() =>
                {
                    var containerResult = containerDict[typeof(ComplexType)];
                    var constructor = containerResult.ResultType.GetConstructors().FirstOrDefault();
                    var paramTypes = constructor.GetParameters().Select(x => x.ParameterType);
                    var resolveInfo = injectionContainer.GetType().GetMethod("Resolve");
                    var resolvedParams = paramTypes.Select(x => resolveInfo.MakeGenericMethod(x).Invoke(injectionContainer, null)).ToArray();
                    return (ComplexType)constructor.Invoke(resolvedParams);
                });

            return mock.Object;
        }

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
