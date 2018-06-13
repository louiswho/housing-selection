using System;

namespace Housing.Selection.Library.ViewModels
{
    public class AddRemoveUserFromRoomModel
    {
        public Guid UserId { get; set; }
        public Guid RoomId { get; set; }
    }
}
