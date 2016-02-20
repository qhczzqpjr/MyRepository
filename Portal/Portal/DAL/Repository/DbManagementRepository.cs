using System.Collections.Generic;
using System.Linq;
using Common;
using Data;
using Repository.Repository.Core;

namespace Repository.Repository
{
    public class DbManagementRepository : Repo<XSearchDbContent>, IDbManagementRepository
    {
        #region Requirement

        public IEnumerable<Requirement> GetAllRequirements()
        {
            return Context.Requirements.OrderBy(one => one.Id);
        }

        public int GetTotalRequirements()
        {
            return Context.Requirements.Count();
        }

        public Requirement GetRequirement(string code)
        {
            return Context.Requirements.FirstOrDefault(one => one.Code == code);
        }

        public Requirement GetRequirement(int id)
        {
            return Context.Requirements.FirstOrDefault(one => one.Id == id);
        }

        public IEnumerable<Requirement> GetRequirements(string code)
        {
            return Context.Requirements.Where(one => one.Code.Contains(code));
        }

        public IEnumerable<Requirement> GetRequirements(ICriteria criteria)
        {
            IQueryable<Requirement> query = Context.Requirements;
            if (criteria.IsSearch)
            {
                var value = criteria.GetFieldData(criteria.FilterColumn);
                switch (criteria.FilterColumn)
                {
                    case "Code":
                        query = query.Where(one => one.Code.Contains(value));
                        break;
                    case "Description":
                        query = query.Where(one => one.Description.Contains(value));
                        break;
                    case "Context":
                        query = query.Where(one => one.Context.Contains(value));
                        break;
                }
            }
            if (criteria.SortColumn == "Code" && criteria.SortOrder == "asc")
            {
                query = query.OrderBy(one => one.Code);
            }
            else if (criteria.SortColumn == "Code" && criteria.SortOrder == "desc")
            {
                query = query.OrderByDescending(one => one.Code);
            }
            else
                query = query.OrderBy(one => one.Id);
            query = query.Skip((criteria.PageIndex - 1)*criteria.PageSize).Take(criteria.PageSize);
            return query;
        }

        #endregion
    }
}