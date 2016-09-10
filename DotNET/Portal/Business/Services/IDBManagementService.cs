using System;
using System.Collections.Generic;
using Business.Models;
using Common;

namespace Business.Services
{
    public interface IDbManagementService : IDisposable
    {
        IEnumerable<Requirement> GetRequirements(ICriteria criteria);
        IEnumerable<Requirement> GetAllRequirements();

        int GetTotalRequirements();

        Requirement CreateRequirement(Requirement contact);

        Requirement GetRequirement(string code);
        Requirement GetRequirement(int id);

        Requirement UpdateRequirement(Requirement contact);

        Requirement DeleteRequirement(Requirement contact);
    }
}