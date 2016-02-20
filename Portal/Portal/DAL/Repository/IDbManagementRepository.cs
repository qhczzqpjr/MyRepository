using System.Collections.Generic;
using Common;
using Data;
using Repository.Repository.Core;

namespace Repository.Repository
{
    public interface IDbManagementRepository : IRepo
    {
        int GetTotalRequirements();
        Requirement GetRequirement(int id);
        Requirement GetRequirement(string code);
        IEnumerable<Requirement> GetRequirements(ICriteria criteria);
        IEnumerable<Requirement> GetRequirements(string code);

        IEnumerable<Requirement> GetAllRequirements();
    }
}