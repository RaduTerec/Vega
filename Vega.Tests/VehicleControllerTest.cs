using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using Vega.Controllers;
using Vega.Controllers.DataTransferObjects;
using Vega.Core;
using Vega.Core.Models;
using Vega.Core.Repositories;
using Vega.Mappings;

namespace Vega.Tests
{
    public class VehicleControllerTest
    {
        private IMapper _mapper;
        private Mock<IVehicleRepository> _vehicleRepositoryStub;
        private Mock<IUnitOfWork> _unitOfWorkStub;

        [Test]
        public void TestConstructor_ReturnsNotNull()
        {
            // Arrange
            var vehicleController = CreateDefaultVehicleController();

            // Assert
            Assert.IsNotNull(vehicleController);
        }

        [Test]
        public async Task TestGet_ReturnsBadRequest_WhenIdDoesNotExist()
        {
            // Arrange
            var vehicleController = CreateDefaultVehicleController();

            // Act
            var result = await vehicleController.Get(17);

            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result.Result);
        }

        [Test]
        public async Task TestGet_ReturnsVehicle_WhenIdExists()
        {
            // Arrange
            var vehicleController = CreateDefaultVehicleController();
            const int vehicleId = 23;
            var vehicle = new Vehicle
            {
                Id = 23,
                ContactEmail = "maria@gmail.com",
                ContactName = "Maria",
                ContactPhone = "078970",
                IsRegistered = false,
                LastUpdate = DateTime.Today.Subtract(new TimeSpan(1, 0, 0, 0))
            };
            var vehicleDto = new VehicleDTO
            {
                Contact = new ContactDTO { Email = "maria@gmail.com", Name = "Maria", Phone = "078970" },
                Id = 23,
                IsRegistered = false,
                LastUpdate = DateTime.Today.Subtract(new TimeSpan(1, 0, 0, 0))
            };
            _vehicleRepositoryStub.Setup(repo => repo.GetWithRelated(vehicleId)).Returns(Task.FromResult(vehicle));

            // Act
            var result = await vehicleController.Get(vehicleId);

            // Assert
            Assert.IsNotNull(result.Result);
            Assert.IsInstanceOf<OkObjectResult>(result.Result);
            Assert.AreEqual(vehicleDto.Id, ((VehicleDTO)((OkObjectResult)result.Result).Value).Id);
            Assert.AreEqual(vehicleDto.Contact.Email, ((VehicleDTO)((OkObjectResult)result.Result).Value).Contact.Email);
            Assert.AreEqual(vehicleDto.LastUpdate, ((VehicleDTO)((OkObjectResult)result.Result).Value).LastUpdate);
        }

        [Test]
        public async Task TestQueryAll_ReturnsZeroItems_WhenNoDataExists()
        {
            // Arrange
            var vehicleController = CreateDefaultVehicleController();
            _vehicleRepositoryStub.Setup(repo => repo.QueryAll(It.IsAny<VehicleQuery>()))
                .Returns(Task.FromResult(new QueryResult<Vehicle>()));

            // Act
            var result = await vehicleController.Get(null);

            // Assert
            Assert.AreEqual(0, result.TotalItems);
            Assert.IsInstanceOf<IEnumerable<VehicleDTO>>(result.Items);
            Assert.IsEmpty(result.Items);
        }

        [Test]
        public async Task TestQueryAll_ReturnsAllItems_WhenQuery()
        {
            // Arrange
            var vehicleController = CreateDefaultVehicleController();
            var vehicleQueryResult = new QueryResult<Vehicle>
            {
                Items = new List<Vehicle>
                {
                    new Vehicle
                    {
                        Id = 21, ContactEmail = "radu@gmail.com", ContactName = "Radu", ContactPhone = "097987",
                        IsRegistered = true, LastUpdate = DateTime.Today.Subtract(new TimeSpan(1, 3, 29, 0))
                    },
                    new Vehicle
                    {
                        Id = 17, ContactEmail = "maria@gmail.com", ContactName = "Maria", ContactPhone = "078970",
                        IsRegistered = false, LastUpdate = DateTime.Today.Subtract(new TimeSpan(1, 0, 0, 0))
                    }
                },
                TotalItems = 2
            };
            _vehicleRepositoryStub.Setup(repo => repo.QueryAll(It.IsAny<VehicleQuery>())).Returns(Task.FromResult(vehicleQueryResult));

            // Act
            var result = await vehicleController.Get(null);

            // Assert
            Assert.AreEqual(2, result.TotalItems);
            Assert.AreEqual(21, result.Items.First().Id);
            Assert.AreEqual("radu@gmail.com", result.Items.First().Contact.Email);
        }

        [Test]
        public async Task TestInsert_ReturnsBadRequest_WhenIdHasValue()
        {
            // Arrange
            var vehicleController = CreateDefaultVehicleController();
            var saveVehicleDto = new SaveVehicleDTO
            {
                Id = 11, Contact = new ContactDTO {Email = "radu@gmail.com", Name = "Radu", Phone = "0723"},
                IsRegistered = true
            };

            // Act
            var result = await vehicleController.Insert(saveVehicleDto);

            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result.Result);
        }

        [Test]
        public async Task TestInsert_ReturnsVehicleDto_WhenNoId()
        {
            // Arrange
            var vehicleController = CreateDefaultVehicleController();
            var now = DateTime.Now;
            const int generatedId = 11;
            var saveVehicleDto = new SaveVehicleDTO
            {
                Contact = new ContactDTO {Email = "radu@gmail.com", Name = "Radu", Phone = "0723"},
                IsRegistered = true
            };
            var vehicle = new Vehicle
            {
                Id = generatedId, ContactEmail = "radu@gmail.com", ContactName = "Radu", ContactPhone = "0723",
                IsRegistered = true, LastUpdate = now
            };

            _unitOfWorkStub.Setup(uWork => uWork.Complete()).Returns(Task.FromResult(1));
            _vehicleRepositoryStub.Setup(repo => repo.AddAsync(It.IsAny<Vehicle>())).Returns(Task.FromResult(vehicle));
            _vehicleRepositoryStub.Setup(repo => repo.GetWithRelated(It.IsAny<long>())).Returns(Task.FromResult(vehicle));

            // Act
            var result = await vehicleController.Insert(saveVehicleDto);

            // Assert
            Assert.IsNotNull(result.Result);
            Assert.IsInstanceOf<OkObjectResult>(result.Result);
            Assert.AreEqual(generatedId, ((VehicleDTO)((OkObjectResult)result.Result).Value).Id);
            Assert.AreEqual(saveVehicleDto.Contact.Email, ((VehicleDTO)((OkObjectResult)result.Result).Value).Contact.Email);
            Assert.AreEqual(now, ((VehicleDTO)((OkObjectResult)result.Result).Value).LastUpdate);
        }

        [Test]
        public async Task TestUpdate_ReturnsBadRequest_WhenIdDoesNotExist()
        {
            // Arrange
            var vehicleController = CreateDefaultVehicleController();
            var saveVehicleDto = new SaveVehicleDTO
            {
                Contact = new ContactDTO {Email = "radu@gmail.com", Name = "Radu", Phone = "0723"},
                IsRegistered = true
            };

            // Act
            var result = await vehicleController.Update(12, saveVehicleDto);

            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result.Result);
        }

        [Test]
        public async Task TestUpdate_ReturnsVehicleDto_WhenIdExists()
        {
            // Arrange
            var vehicleController = CreateDefaultVehicleController();
            var now = DateTime.Now;
            const int generatedId = 12;
            var saveVehicleDto = new SaveVehicleDTO
            {
                Contact = new ContactDTO {Email = "maria@gmail.com", Name = "Maria", Phone = "074118"},
                IsRegistered = true
            };
            var vehicle = new Vehicle
            {
                Id = generatedId, ContactEmail = "maria@gmail.com", ContactName = "Maria", ContactPhone = "074118",
                IsRegistered = true, LastUpdate = now
            };
            _unitOfWorkStub.Setup(uWork => uWork.Complete()).Returns(Task.FromResult(1));
            _vehicleRepositoryStub.Setup(repo => repo.GetWithFeatures(It.IsAny<long>())).Returns(Task.FromResult(vehicle));
            _vehicleRepositoryStub.Setup(repo => repo.GetWithRelated(It.IsAny<long>())).Returns(Task.FromResult(vehicle));

            // Act
            var result = await vehicleController.Update(generatedId, saveVehicleDto);

            // Assert
            Assert.IsNotNull(result.Result);
            Assert.IsInstanceOf<OkObjectResult>(result.Result);
            Assert.AreEqual(generatedId, ((VehicleDTO)((OkObjectResult)result.Result).Value).Id);
            Assert.AreEqual(saveVehicleDto.Contact.Email, ((VehicleDTO)((OkObjectResult)result.Result).Value).Contact.Email);
            Assert.AreNotEqual(now, ((VehicleDTO)((OkObjectResult)result.Result).Value).LastUpdate);
        }

        [Test]
        public async Task TestDelete_ReturnsBadRequest_WhenIdDoesNotExist()
        {
            // Arrange
            var vehicleController = CreateDefaultVehicleController();

            // Act
            var result = await vehicleController.Delete(17);

            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result.Result);
        }

        [Test]
        public async Task TestDelete_ReturnsDeletedId_WhenIdExists()
        {
            // Arrange
            var vehicleController = CreateDefaultVehicleController();
            const int generatedId = 17;
            var vehicle = new Vehicle
            {
                Id = generatedId, ContactEmail = "laura@gmail.com", ContactName = "Laura", ContactPhone = "0735",
                IsRegistered = false, LastUpdate = DateTime.Now
            };

            _unitOfWorkStub.Setup(uWork => uWork.Complete()).Returns(Task.FromResult(1));
            _vehicleRepositoryStub.Setup(repo => repo.Get(It.IsAny<long>())).Returns(Task.FromResult(vehicle));

            // Act
            var result = await vehicleController.Delete(generatedId);

            // Assert
            Assert.IsNotNull(result.Result);
            Assert.IsInstanceOf<OkObjectResult>(result.Result);
            Assert.AreEqual(generatedId, (long)((OkObjectResult)result.Result).Value);
        }

        private VehicleController CreateDefaultVehicleController()
        {
            _vehicleRepositoryStub = new Mock<IVehicleRepository>();
            _unitOfWorkStub = new Mock<IUnitOfWork>();

            var mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            _mapper = mapperConfiguration.CreateMapper();

            var vehicleController = new VehicleController(_mapper, _vehicleRepositoryStub.Object, _unitOfWorkStub.Object);

            return vehicleController;
        }
    }
}