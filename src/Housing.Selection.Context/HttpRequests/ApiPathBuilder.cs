using System;

namespace Housing.Selection.Context.HttpRequests
{
    /// <summary>
    /// This class allows for the injection of Api paths.
    /// </summary>
    public class ApiPathBuilder : IApiPathBuilder
    {
        private string BatchServicePath { get; set; }
        private string RoomServicePath { get; set; }
        private string UserServicePath { get; set; }

        /// <summary>
        /// This is the constructor.  The paths to the services are wired into here.
        /// </summary>
        public ApiPathBuilder()
        {
            BatchServicePath = "api/batch";
            RoomServicePath = "api/room";
            UserServicePath = "api/service";
        }

        public string GetBatchServicePath()
        {
            return BatchServicePath;
        }

        public string GetBatchServicePath(Guid id)
        {
            return BatchServicePath + id;
        }

        public string GetRoomServicePath()
        {
            return RoomServicePath;
        }

        public string GetRoomServicePath(Guid id)
        {
            return RoomServicePath + id;
        }

        public string GetUserServicePath()
        {
            return UserServicePath;
        }

        public string GetUserServicePath(Guid id)
        {
                return UserServicePath + id;
        }
    }
}
