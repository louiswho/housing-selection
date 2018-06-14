using Housing.Selection.Context.Selection;
using Housing.Selection.Library.ViewModels;
using Microsoft.AspNetCore.Mvc;


namespace Housing.Selection.Service.Controllers
{
    [Produces("application/json")]
    [Route("api/Selection")]
    public class SelectionController : Controller
    {
        private readonly ISelectionService _selection;

        public SelectionController(ISelectionService selection)
        {
            _selection = selection;
        }

        [HttpGet]
        public IActionResult GetBatches()
        {
            return Ok(_selection.GetBatches());
        }

        [HttpGet]
        public IActionResult GetRooms()
        {
            return Ok(_selection.GetRooms());
        }

        [HttpGet]
        public IActionResult CustomSearch(RoomSearchViewModel roomSearchViewModel)
        {
            if(!ModelState.IsValid) { return BadRequest(); };

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