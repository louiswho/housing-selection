using Housing.Selection.Library.HousingModels;
using Housing.Selection.Library.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace Housing.Selection.Context.Selection
{
    public abstract class AUserFilter
    {
        protected AUserFilter Successor;

        public void SetSuccessor(AUserFilter successor)
        {
            Successor = successor;
        }

        public abstract void FilterRequest(ref List<User> filterUsers, UserSearchViewModel userSearchViewModel);
    }

    public class UserGenderFilter : AUserFilter
    {
        public override void FilterRequest(ref List<User> filterUsers, UserSearchViewModel userSearchViewModel)
        {
            if(userSearchViewModel.Gender != null)
            {
                var result = filterUsers.Where(x => x.Gender == userSearchViewModel.Gender);
                filterUsers = result.ToList();
            }
            Successor?.FilterRequest(ref filterUsers, userSearchViewModel);
        }
    }

    public class UserLocationFilter : AUserFilter
    {
        public override void FilterRequest(ref List<User> filterUsers, UserSearchViewModel userSearchViewModel)
        {
            if(userSearchViewModel.Location != null)
            {
                var result = filterUsers.Where(x => x.Location == userSearchViewModel.Location);
                filterUsers = result.ToList();
            }
            Successor?.FilterRequest(ref filterUsers, userSearchViewModel);
        }
    }

    public class UserBatchFilter : AUserFilter
    {
        public override void FilterRequest(ref List<User> filterUsers, UserSearchViewModel userSearchViewModel)
        {
            if(userSearchViewModel.Batch != null)
            {
                var result = filterUsers.Where(x => x.Batch.BatchId == userSearchViewModel.Batch);
                filterUsers = result.ToList();
            }
            Successor?.FilterRequest(ref filterUsers, userSearchViewModel);
        }
    }

    public class UserAssignedFilter : AUserFilter
    {
        public override void FilterRequest(ref List<User> filterUsers, UserSearchViewModel userSearchViewModel)
        {
            if (userSearchViewModel.Assigned)
            {
                var result = filterUsers.Where(x => x.Room != null);
                filterUsers = result.ToList();
            }
            else
            {
                var result = filterUsers.Where(x => x.Room == null);
                filterUsers = result.ToList();
            }
            Successor?.FilterRequest(ref filterUsers, userSearchViewModel);
        }
    }
}
