using AutoMapper;
using Housing.Selection.Context.Selection;
using Housing.Selection.Library.HousingModels;
using Housing.Selection.Library.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Housing.Selection.Service.Controllers
{
    [Produces("application/json")]
    [Route("api/Selection")]
    public class SelectionController : Controller
    {
        private readonly ISelectionService _selection;
        private readonly IMapper _mapper;

        public SelectionController(ISelectionService selection, IMapper mapper)
        {
            _selection = selection;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetBatches()
        {
            var batches = _selection.GetBatches();
            var viewModel = _mapper.Map<IEnumerable<Batch>>(batches);

            return Ok(viewModel);
        }

        [HttpGet]
        public IActionResult GetRooms()
        {
            var rooms = _selection.GetRooms();
            var viewModel = _mapper.Map<IEnumerable<Room>>(rooms);

            return Ok(_selection.GetRooms());
        }

        [HttpGet]
        public IActionResult CustomSearch(RoomSearchViewModel roomSearchViewModel)
        {
            if(!ModelState.IsValid) { return BadRequest(); };

            var rooms = _selection.CustomSearch(roomSearchViewModel);
            var viewModel = _mapper.Map<IEnumerable<Room>>(rooms);

            return Ok(_selection.CustomSearch(roomSearchViewModel));
        }

        [HttpPut]
        public IActionResult AddUserToRoom(AddRemoveUserFromRoomModel addUserToRoomModel)
        {
            if(!ModelState.IsValid) { return BadRequest(); };

            _selection.AddUserToRoom(addUserToRoomModel);

            return Ok();
        }

        [HttpPut]
        public IActionResult RemoveUserFromRoom(AddRemoveUserFromRoomModel removeUserFromRoomModel)
        {
            if(!ModelState.IsValid) { return BadRequest(); };

            _selection.RemoveUserFromRoom(removeUserFromRoomModel);

            return Ok();
        }
    }
}