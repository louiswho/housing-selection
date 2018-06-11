using System;


namespace Housing.Selection.Context.Interfaces
{
    public interface IApiPathBuilder
    {
        string GetBatchServicePath ();
        string GetBatchServicePath (Guid id);

        string GetRoomServicePath();
        string GetRoomServicePath(Guid id);
        string GetUserServicePath();
        string GetUserServicePath(Guid id);
    }    
}