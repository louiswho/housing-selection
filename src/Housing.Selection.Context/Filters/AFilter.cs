﻿using Housing.Selection.Library;
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
            if(_successor != null)
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
            if (_successor != null)
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
            if (_successor != null)
            {
                _successor.FilterRequest(ref filterRooms, roomSearchViewModel);
            }
        }
    }

    public class IsCompletelyUnassignedFilter : AFilter
    {
        public override void FilterRequest(ref List<Room> filterRooms, RoomSearchViewModel roomSearchViewModel)
        {
            throw new System.NotImplementedException();
        }
    }
}
