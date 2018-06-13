using System.Collections.Generic;
using System.Linq;
using Housing.Selection.Library;
using Housing.Selection.Library.HousingModels;

namespace Housing.Selection.Context.Selection
{
    public abstract class AFilter
    {
        protected AFilter _successor;
        //Change to Successor
        public void SetSuccessor(AFilter successor)
        {
            _successor = successor;
        }

        public abstract void FilterRequest(ref List<Room> filterRooms, RoomSearchViewModel roomSearchViewModel);
    }

    public class LocationFilter : AFilter
    {
        public override void FilterRequest(ref List<Room> filterRooms, RoomSearchViewModel roomSearchViewModel)
        {
            if(roomSearchViewModel.Location != null)
            {
                var result = filterRooms.Where(x => x.Location == roomSearchViewModel.Location);
                filterRooms = result.ToList();
            }
            if(_successor != null) //Change to null propagation, see me.
            {
                _successor.FilterRequest(ref filterRooms, roomSearchViewModel);
            }
        }
    }

    public class BatchFilter : AFilter
    {
        public override void FilterRequest(ref List<Room> filterRooms, RoomSearchViewModel roomSearchViewModel)
        {
            if(roomSearchViewModel.Batch != null && roomSearchViewModel.BatchMinimumPercentage != 0)
            {
                var result = from x in filterRooms
                              where x.BatchPercentage(roomSearchViewModel.Batch) 
                              >= roomSearchViewModel.BatchMinimumPercentage
                              select x;
                filterRooms = result.ToList();
            }
            if (_successor != null) //Null propagation
            {
                _successor.FilterRequest(ref filterRooms, roomSearchViewModel);
            }
        }
    }

    public class GenderFilter : AFilter
    {
        public override void FilterRequest(ref List<Room> filterRooms, RoomSearchViewModel roomSearchViewModel)
        {
            if(roomSearchViewModel.Gender != null)
            {
                var result = filterRooms.Where(x => x.Gender.Equals(roomSearchViewModel.Gender));
                filterRooms = result.ToList();
            }
            if (_successor != null) //Null propagation
            {
                _successor.FilterRequest(ref filterRooms, roomSearchViewModel);
            }
        }
    }

    public class IsCompletelyUnassignedFilter : AFilter
    {
        public override void FilterRequest(ref List<Room> filterRooms, RoomSearchViewModel roomSearchViewModel)
        {
            if(roomSearchViewModel.IsCompletelyUnassigned)
            {
                var result = filterRooms.Where(x => x.Vacancy == x.Occupancy);
                filterRooms = result.ToList();
            }
            else
            {
                var result = filterRooms.Where(x => x.Vacancy < x.Occupancy);
                filterRooms = result.ToList();
            }
            if (_successor != null) //Null propagation
            {
                _successor.FilterRequest(ref filterRooms, roomSearchViewModel);
            }
        }
    }
}
