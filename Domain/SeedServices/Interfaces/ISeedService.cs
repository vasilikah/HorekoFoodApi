using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.SeedServices.Interfaces
{
    public interface ISeedService
    {
        Task InsertIngredientsIntoDB();
        Task InsertDishedIntoDb();
    }
}
