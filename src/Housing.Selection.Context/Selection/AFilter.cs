using System.Collections.Generic;
using System.Linq;
using Housing.Selection.Library.HousingModels;
using Housing.Selection.Library.ViewModels;

namespace Housing.Selection.Context.Selection
{
    public abstract class AFilter
    {
        protected AFilter Successor;
        
        public void SetSuccessor(AFilter successor)
        {
            Successor = successor;
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
            Successor?.FilterRequest(ref filterRooms, roomSearchViewModel);
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
            Successor?.FilterRequest(ref filterRooms, roomSearchViewModel);
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
            Successor?.FilterRequest(ref filterRooms, roomSearchViewModel);
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
            Successor?.FilterRequest(ref filterRooms, roomSearchViewModel);
        }
    }

    public class HasBedAvailableFilter : AFilter
    {
        public override void FilterRequest(ref List<Room> filterRooms, RoomSearchViewModel roomSearchViewModel)
        {
            if (roomSearchViewModel.HasBedAvailable)
            {
                var result = filterRooms.Where(x => x.Vacancy > 0);
                filterRooms = result.ToList();
            }
            else
            {
                var result = filterRooms.Where(x => x.Vacancy == 0);
                filterRooms = result.ToList();
            }
            Successor?.FilterRequest(ref filterRooms, roomSearchViewModel);
        }
    }
}
