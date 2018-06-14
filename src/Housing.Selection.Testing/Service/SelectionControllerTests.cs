using Housing.Selection.Context.Selection;
using Housing.Selection.Library.HousingModels;
using Housing.Selection.Library.ViewModels;
using Housing.Selection.Service.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Housing.Selection.Testing.Service
{
    public class SelectionControllerTests
    {
        private SelectionController controller;

        public SelectionControllerTests()
        {
            var moqService = new Mock<ISelectionService>();

            moqService.Setup(x => x.GetBatches()).Returns(new List<Batch>());
            moqService.Setup(x => x.GetRooms()).Returns(new List<Room>());
            moqService.Setup(x => x.CustomSearch(It.IsAny<RoomSearchViewModel>())).Returns(new List<Room>());
            moqService.Setup(x => x.AddUserToRoom(It.IsAny<AddRemoveUserFromRoomModel>()));
            moqService.Setup(x => x.RemoveUserFromRoom(It.IsAny<AddRemoveUserFromRoomModel>()));

            controller = new SelectionController(moqService.Object);
        }

        [Fact]
        public void GetBatches_ReturnsOkResult()
        {
            var result = controller.GetBatches();

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void GetBatches_ReturnType_ReturnsListOfBatches()
        {
            var result = controller.GetBatches();

            var resultType = result as ObjectResult;

            Assert.IsType<List<Batch>>(resultType.Value);
        }

        [Fact]
        public void GetRooms_ReturnsOkResult()
        {
            var result = controller.GetRooms();

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void GetRooms_ReturnType_ReturnsListOfRooms()
        {
            var result = controller.GetRooms();

            var resultType = result as ObjectResult;

            Assert.IsType<List<Room>>(resultType.Value);
        }

        [Fact]
        public void CustomSearch_ValidModel_ReturnsOkResult()
        {
            var result = controller.CustomSearch(new RoomSearchViewModel());

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void CustomSearch_InvalidModel_ReturnsBadRequest()
        {
            controller.ModelState.AddModelError("bad", "error");

            var result = controller.CustomSearch(new RoomSearchViewModel());

            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public void CustomSearch_ReturnType_ReturnsListOfRooms()
        {
            var result = controller.CustomSearch(new RoomSearchViewModel());

            var resultType = result as ObjectResult;

            Assert.IsType<List<Room>>(resultType.Value);
        }

        [Fact]
        public void AddUserToRoom_ValidModel_ReturnsOkResult()
        {
            var result = controller.AddUserToRoom(new AddRemoveUserFromRoomModel());

            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public void AddUserToRoom_InvalidModel_ReturnsBadRequest()
        {
            controller.ModelState.AddModelError("bad", "error");

            var result = controller.AddUserToRoom(new AddRemoveUserFromRoomModel());

            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public void RemoveUserFromRoom_ValidModel_ReturnsOkResult()
        {
            var result = controller.RemoveUserFromRoom(new AddRemoveUserFromRoomModel());

            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public void RemoveUserFromRoom_InvalidModel_ReturnsBadRequest()
        {
            controller.ModelState.AddModelError("bad", "error");

            var result = controller.RemoveUserFromRoom(new AddRemoveUserFromRoomModel());

            Assert.IsType<BadRequestResult>(result);
        }
    }
}
