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

        public ApiPathBuilder()
        {
            BatchServicePath = "http://ec2-13-57-218-138.us-west-1.compute.amazonaws.com:9040/";
            RoomServicePath = "http://ec2-13-57-218-138.us-west-1.compute.amazonaws.com:9030/";
            UserServicePath = "http://ec2-13-57-218-138.us-west-1.compute.amazonaws.com:9050/";
        }

        public string GetBatchServicePath()
        {
            return BatchServicePath + "api/Batches";
        }

        public string GetBatchServicePath(Guid id)
        {
            return BatchServicePath + id;
        }

        public string GetRoomServicePath()
        {
            return RoomServicePath + "api/Rooms";
        }

        public string GetRoomServicePath(Guid id)
        {
            return RoomServicePath + id;
        }

        public string GetUserServicePath()
        {
            return UserServicePath + "api/Users";
        }

        public string GetUserServicePath(Guid id)
        {
            return UserServicePath + id;
        }
    }
}
