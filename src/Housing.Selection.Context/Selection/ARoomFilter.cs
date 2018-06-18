﻿/* Chain of responsibility for the room filter custom search. Will parse through a complex object, and filter out rooms based
   on which feilds are populated (not null). This object is received from the angular API */
using System.Collections.Generic;
using System.Linq;
using Housing.Selection.Library.HousingModels;
using Housing.Selection.Library.ViewModels;

namespace Housing.Selection.Context.Selection
{
    public abstract class ARoomFilter
    {
        protected ARoomFilter Successor;
        
        public void SetSuccessor(ARoomFilter successor)
        {
            Successor = successor;
        }

        public abstract void FilterRequest(ref List<Room> filterRooms, RoomSearchViewModel roomSearchViewModel);
    }

    public class LocationFilter : ARoomFilter
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

    public class BatchFilter : ARoomFilter
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

    public class GenderFilter : ARoomFilter
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

    public class IsCompletelyUnassignedFilter : ARoomFilter
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

    public class HasBedAvailableFilter : ARoomFilter
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
