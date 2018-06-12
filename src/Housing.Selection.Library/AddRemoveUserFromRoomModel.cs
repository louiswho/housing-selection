using System;
using System.Collections.Generic;
using System.Text;

namespace Housing.Selection.Library
{
    public class AddRemoveUserFromRoomModel
    {
        public Guid UserId { get; set; }
        public Guid RoomId { get; set; }
    }
}
