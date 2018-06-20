using AutoMapper;
using Housing.Selection.Context.Selection;
using Housing.Selection.Library.HousingModels;
using Housing.Selection.Library.ViewModels;
using Housing.Selection.Service.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Housing.Selection.Testing.Service
{
    public class SelectionControllerTests
    {
        private SelectionController controller;

        public SelectionControllerTests()
        {
            var moqService = new Mock<ISelectionService>();

            var config = new MapperConfiguration(cfg => cfg.AddProfile(new MappingProfile()));
            var mapper = config.CreateMapper();

            moqService.Setup(x => x.GetBatches()).Returns(Task.Run(() => new List<Batch>()));
            moqService.Setup(x => x.GetRooms()).Returns(Task.Run(() => new List<Room>()));
            moqService.Setup(x => x.CustomSearch(It.IsAny<RoomSearchViewModel>())).Returns(Task.Run(() => new List<Room>() as IEnumerable<Room>));
            moqService.Setup(x => x.AddUserToRoom(It.IsAny<AddRemoveUserFromRoomModel>())).Returns(Task.CompletedTask);
            moqService.Setup(x => x.RemoveUserFromRoom(It.IsAny<AddRemoveUserFromRoomModel>())).Returns(Task.CompletedTask);

            controller = new SelectionController(moqService.Object, mapper);
        }

        [Fact]
        public async void GetBatches_ReturnsOkResult()
        {
            var result = await controller.GetBatches();

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async void GetBatches_ReturnType_ReturnsListOfBatches()
        {
            var result = controller.GetBatches();

            var resultType = await result as ObjectResult;

            Assert.IsAssignableFrom<IEnumerable<BatchViewModel>>(resultType.Value);
        }

        [Fact]
        public async void GetRooms_ReturnsOkResult()
        {
            var result = await controller.GetRooms();

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async void GetRooms_ReturnType_ReturnsListOfRooms()
        {
            var result = controller.GetRooms();

            var resultType = await result as ObjectResult;

            Assert.IsAssignableFrom<IEnumerable<RoomViewModel>>(resultType.Value);
        }

        [Fact]
        public async void CustomSearch_ValidModel_ReturnsOkResult()
        {
            var result = await controller.CustomSearch(new RoomSearchViewModel());

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async void CustomSearch_InvalidModel_ReturnsBadRequest()
        {
            controller.ModelState.AddModelError("bad", "error");

            var result = await controller.CustomSearch(new RoomSearchViewModel());

            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async void CustomSearch_ReturnType_ReturnsListOfRooms()
        {
            var result = controller.CustomSearch(new RoomSearchViewModel());

            var resultType = await result as ObjectResult;

            Assert.IsAssignableFrom<IEnumerable<RoomViewModel>>(resultType.Value);
        }

        [Fact]
        public async void AddUserToRoom_ValidModel_ReturnsOkResult()
        {
            var result = await controller.AddUserToRoom(new AddRemoveUserFromRoomModel());

            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async void AddUserToRoom_InvalidModel_ReturnsBadRequest()
        {
            controller.ModelState.AddModelError("bad", "error");

            var result = await controller.AddUserToRoom(new AddRemoveUserFromRoomModel());

            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async void RemoveUserFromRoom_ValidModel_ReturnsOkResult()
        {
            var result = await controller.RemoveUserFromRoom(new AddRemoveUserFromRoomModel());

            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async void RemoveUserFromRoom_InvalidModel_ReturnsBadRequest()
        {
            controller.ModelState.AddModelError("bad", "error");

            var result = await controller.RemoveUserFromRoom(new AddRemoveUserFromRoomModel());

            Assert.IsType<BadRequestResult>(result);
        }
    }
}
