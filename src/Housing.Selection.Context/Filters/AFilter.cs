using Housing.Selection.Library;
using Housing.Selection.Library.HousingModels;
using System.Collections.Generic;
using System.Linq;

namespace Housing.Selection.Context.Filters
{
    public abstract class AFilter
    {
        protected AFilter _successor;

        public void SetSuccessor(AFilter successor)
        {
            _successor = successor;
        }

        public abstract void FilterRequest(List<Room> filterRooms, RoomSearchViewModel roomSearchViewModel);
    }

    public class LocationFilter : AFilter
    {
        public override void FilterRequest(List<Room> filterRooms, RoomSearchViewModel roomSearchViewModel)
        {
            if(roomSearchViewModel.Location != null)
            {
                var result = filterRooms.Where(x => x.Location == roomSearchViewModel.Location);
                filterRooms = result.ToList();
            }
            _successor.FilterRequest(filterRooms, roomSearchViewModel);
        }
    }

    public class BatchFilter : AFilter
    {
        public override void FilterRequest(List<Room> filterRooms, RoomSearchViewModel roomSearchViewModel)
        {
            if(roomSearchViewModel.Batch != null && roomSearchViewModel.BatchMinimumPercentage != 0)
            {
                var batches = from x in filterRooms
                             from y in x.Users
                             select y.Batch;
                
            }
        }
    }

    public class GenderFilter : AFilter
    {
        public override void FilterRequest(List<Room> filterRooms, RoomSearchViewModel roomSearchViewModel)
        {
            throw new System.NotImplementedException();
        }
    }

    public class IsCompletelyUnassignedFilter : AFilter
    {
        public override void FilterRequest(List<Room> filterRooms, RoomSearchViewModel roomSearchViewModel)
        {
            throw new System.NotImplementedException();
        }
    }
}
