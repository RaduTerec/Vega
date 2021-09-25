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
using Vega.Mappings;

namespace Vega.Tests
{
    public class VehicleControllerTest
    {
        private IMapper _mapper;
        private Mock<IVehicleRepository> _vehicleRepository;
        private Mock<IUnitOfWork> _unitOfWork;

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
            _vehicleRepository.Setup(repo => repo.GetWithRelated(vehicleId)).Returns(Task.FromResult(vehicle));

            // Act
            var result = await vehicleController.Get(vehicleId);

            // Assert
            Assert.IsNotNull(result.Result);
            Assert.IsInstanceOf<OkObjectResult>(result.Result);
            Assert.AreEqual(vehicleDto.Id, ((result.Result as OkObjectResult).Value as VehicleDTO).Id);
            Assert.AreEqual(vehicleDto.Contact.Email, ((result.Result as OkObjectResult).Value as VehicleDTO).Contact.Email);
            Assert.AreEqual(vehicleDto.LastUpdate, ((result.Result as OkObjectResult).Value as VehicleDTO).LastUpdate);
        }

        [Test]
        public async Task TestGetAll_ReturnsZeroItems_WhenNoDataExists()
        {
            // Arrange
            var vehicleController = CreateDefaultVehicleController();
            _vehicleRepository.Setup(repo => repo.GetAll(It.IsAny<VehicleQuery>()))
                .Returns(Task.FromResult(new QueryResult<Vehicle>()));

            // Act
            var result = await vehicleController.Get(null);

            // Assert
            Assert.AreEqual(0, result.TotalItems);
            Assert.IsInstanceOf<IEnumerable<VehicleDTO>>(result.Items);
            Assert.IsEmpty(result.Items);
        }

        [Test]
        public async Task TestGetAll_ReturnsAllItems_WhenQuery()
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
            _vehicleRepository.Setup(repo => repo.GetAll(It.IsAny<VehicleQuery>())).Returns(Task.FromResult(vehicleQueryResult));

            // Act
            var result = await vehicleController.Get(null);

            // Assert
            Assert.AreEqual(2, result.TotalItems);
            Assert.AreEqual(21, result.Items.First().Id);
            Assert.AreEqual("radu@gmail.com", result.Items.First().Contact.Email);
        }

        private VehicleController CreateDefaultVehicleController()
        {
            _vehicleRepository = new Mock<IVehicleRepository>();
            _unitOfWork = new Mock<IUnitOfWork>();

            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            _mapper = mockMapper.CreateMapper();

            var vehicleController = new VehicleController(_mapper, _vehicleRepository.Object, _unitOfWork.Object);

            return vehicleController;
        }
    }
}