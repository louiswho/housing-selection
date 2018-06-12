using Housing.Selection.Library;
using Housing.Selection.Library.HousingModels;
using System.Collections.Generic;
using System.Linq;

namespace Housing.Selection.Context.Filters
{
    abstract class AFilter
    {
        protected AFilter _successor;

        public void SetSuccessor(AFilter successor)
        {
            _successor = successor;
        }

        public abstract void FilterRequest(List<Room> filterRooms, RoomSearchViewModel roomSearchViewModel);
    }

    class LocationFilter : AFilter
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

    class BatchFilter : AFilter
    {
        public override void FilterRequest(List<Room> filterRooms, RoomSearchViewModel roomSearchViewModel)
        {
            if(roomSearchViewModel.Batch != null)
            {
                var batches = from x in filterRooms
                             from y in x.Users
                             select y.Batch;
                
            }
        }
    }

    class BatchMinimumPercentageFilter : AFilter
    {
        public override void FilterRequest(List<Room> filterRooms, RoomSearchViewModel roomSearchViewModel)
        {
            throw new System.NotImplementedException();
        }
    }

    class GenderFilter : AFilter
    {
        public override void FilterRequest(List<Room> filterRooms, RoomSearchViewModel roomSearchViewModel)
        {
            throw new System.NotImplementedException();
        }
    }

    class IsCompletelyUnassignedFilter : AFilter
    {
        public override void FilterRequest(List<Room> filterRooms, RoomSearchViewModel roomSearchViewModel)
        {
            throw new System.NotImplementedException();
        }
    }
}
