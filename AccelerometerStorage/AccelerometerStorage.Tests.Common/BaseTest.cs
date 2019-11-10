using Moq;
using System;
using System.IO;

namespace AccelerometerStorage.Tests.Common
{
    public abstract class BaseTest<T>
        where T : class
    {
        public readonly string basePath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..\\..\\..\\..\\"));
        public readonly string testDataPath = "\\AccelerometerStorage.Tests.Common\\TestData\\";

        protected BaseTest()
        {
            SetupMocks(new MockRepository(MockBehavior.Strict));
            SystemUnderTest = CreateSystemUnderTest();
        }

        public T SystemUnderTest { get; }

        protected abstract void SetupMocks(MockRepository mockRepository);

        protected abstract T CreateSystemUnderTest();
    }
}
