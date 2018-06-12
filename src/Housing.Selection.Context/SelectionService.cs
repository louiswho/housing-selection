﻿using System;
using System.Collections.Generic;
using System.Linq;
using Housing.Selection.Context.DataAccess;
using Housing.Selection.Context.Filters;
using Housing.Selection.Library;
using Housing.Selection.Library.HousingModels;

namespace Housing.Selection.Context
{


    public class SelectionService : ISelectionService
    {
        private readonly IUserRepository userRepo;
        private readonly IRoomRepository roomRepo;
        private readonly IBatchRepository batchRepo;

        public SelectionService(IUserRepository _users, IRoomRepository _rooms, IBatchRepository _batches)
        {
            userRepo = _users;
            roomRepo = _rooms;
            batchRepo = _batches;
        }
        public void AddUserToRoom(AddRemoveUserFromRoomModel addRemoveUserFromRoomModel)
        {
            var newUser = userRepo.GetUserByUserId(addRemoveUserFromRoomModel.UserId);
            var addRoom = roomRepo.GetRoomByRoomId(addRemoveUserFromRoomModel.RoomId);

            newUser.Room = addRoom;
            addRoom.Users.Add(newUser);

            userRepo.SaveChanges();
            roomRepo.SaveChanges();
        }

        public List<Room> CustomSearch(RoomSearchViewModel roomSearchViewModel)
        {
            var returnedRooms = roomRepo.GetRooms().ToList();

            AFilter genderFilter = new GenderFilter();
            AFilter locationFilter = new LocationFilter();
            AFilter batchFilter = new BatchFilter();
            AFilter batchMinimumPercentageFilter = new BatchMinimumPercentageFilter();
            AFilter isCompletelyUnassignedFilter = new IsCompletelyUnassignedFilter();

            genderFilter.SetSuccessor(locationFilter);
            locationFilter.SetSuccessor(batchFilter);
            batchFilter.SetSuccessor(batchMinimumPercentageFilter);
            batchMinimumPercentageFilter.SetSuccessor(isCompletelyUnassignedFilter);

            genderFilter.FilterRequest(returnedRooms, roomSearchViewModel);
        }

        public List<Batch> GetBatches()
        {
            return batchRepo.GetBatches().ToList();
        }

        public List<Room> GetRooms()
        {
            return roomRepo.GetRooms().ToList();
        }

        public List<User> GetUsers()
        {
            return userRepo.GetUsers().ToList();
        }

        public AddRemoveUserFromRoomModel RemoveUserFromRoom(AddRemoveUserFromRoomModel addRemoveUserFromRoomModel)
        {
            throw new NotImplementedException();
        }
    }
}
