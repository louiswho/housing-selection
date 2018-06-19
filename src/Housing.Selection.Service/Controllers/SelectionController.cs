using AutoMapper;
using Housing.Selection.Context.Selection;
using Housing.Selection.Library.HousingModels;
using Housing.Selection.Library.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

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
        public async Task<IActionResult> GetBatches()
        {
            var batches = await _selection.GetBatches();
            var viewModel = _mapper.Map<IEnumerable<BatchViewModel>>(batches);

            return Ok(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> GetRooms()
        {
            var rooms = await _selection.GetRooms();
            var viewModel = _mapper.Map<IEnumerable<RoomViewModel>>(rooms);

            return Ok(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _selection.GetUsers();
            var viewModel = _mapper.Map<IEnumerable<UserViewModel>>(users);

            return Ok(viewModel);
        }

        [HttpPut]
        public async Task<IActionResult> CustomSearch(RoomSearchViewModel roomSearchViewModel)
        {
            if (!ModelState.IsValid) { return BadRequest(); };

            var rooms = await _selection.CustomSearch(roomSearchViewModel);
            var viewModel = _mapper.Map<IEnumerable<RoomViewModel>>(rooms);

            return Ok(viewModel);
        }

        [HttpPut]
        public async Task<IActionResult> CustomUserSearch(UserSearchViewModel userSearchViewModel)
        {
            if (!ModelState.IsValid) { return BadRequest(); };

            var users = await _selection.CustomUserSearch(userSearchViewModel);
            var viewModel = _mapper.Map<IEnumerable<UserSearchViewModel>>(users);

            return Ok(viewModel);
        }

        [HttpPut]
        public async Task<IActionResult> AddUserToRoom(AddRemoveUserFromRoomModel addUserToRoomModel)
        {
            if (!ModelState.IsValid) { return BadRequest(); };

            await _selection.AddUserToRoom(addUserToRoomModel);

            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> RemoveUserFromRoom(AddRemoveUserFromRoomModel removeUserFromRoomModel)
        {
            if (!ModelState.IsValid) { return BadRequest(); };

            await _selection.RemoveUserFromRoom(removeUserFromRoomModel);

            return Ok();
        }
    }
}