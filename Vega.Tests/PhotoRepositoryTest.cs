using Moq;
using NUnit.Framework;
using Vega.Persistence;

namespace Vega.Tests
{
    public class Tests
    {
        [Test]
        public void Test_ReturnRepositoryObject()
        {
            // Arrange
            var photoRepository = CreateDefaultPhotoRepository();

            // Assert
            Assert.IsNotNull(photoRepository);
        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }

        private PhotoRepository CreateDefaultPhotoRepository()
        {
            var stubVegaDbContext = new Mock<VegaDbContext>().Object;
            return new PhotoRepository(stubVegaDbContext);
        }
    }
}