using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Housing.Selection.Context.DataAccess
{
     public interface IDbContext 
    {
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
    }
}
