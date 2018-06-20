using Housing.Selection.Context.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace Housing.Selection.Testing.Integration
{
    public class IntegrationHelpers
    {
        public static DbContextOptions ResolveOptions()
        {
            var options = new DbContextOptionsBuilder<HousingSelectionDbContext>();
            options.UseSqlServer(
                "Server=tcp:housingselection.database.windows.net,1433;Initial Catalog=Selection;Persist Security Info=False;User ID=housingselectionadmin;Password=selectionadmin123!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");

            return options.Options;
        }
    }
}
