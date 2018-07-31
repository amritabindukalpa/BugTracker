using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using DefectTracker.Models;

namespace DefectTracker.DataAccessLayer
{
    public class DefectQueryObject
    {
        private string _connectionString;

        public DefectQueryObject()
        {
            this._connectionString = ConfigurationManager.AppSettings.Get("DefectTrackerContext");
        }

        public IEnumerable<Defect> GetDefectList()
        {
            using (var dbContext = new DefectTrackerContext())
            {
                var result = dbContext.Defects
                    .Include("UserAssigned")
                    .Include("Status")
                    .OrderBy(d => d.CreationDate).ToList();
                return result;
            }
        }

        public static Defect GetDefect(int id)
        {
            using (var dbContext = new DefectTrackerContext())
            {
                var result = dbContext.Defects.FirstOrDefault(d => d.Id == id);
                return result;
            }
        }

        public static void AddDefect(Defect defect)
        {
            using (var dbContext = new DefectTrackerContext())
            {
                dbContext.Defects.Add(defect);
                dbContext.SaveChanges();
            }
        }

        public void EditDefect(Defect defect)
        {
            try
            {
                using (var dbContext = new DefectTrackerContext())
                {
                    var existingDefect = dbContext.Defects.FirstOrDefault(d => d.Id == defect.Id);

                    if (existingDefect == null) return;

                    existingDefect.UserAssignedId = defect.UserAssignedId;
                    existingDefect.DefectDescription = defect.DefectDescription;
                    existingDefect.DefectTitle = defect.DefectTitle;
                    existingDefect.ModifiedDate = DateTime.Now;
                    existingDefect.ResolutionDate = defect.ResolutionDate;
                    existingDefect.StatusId = defect.StatusId;
                    dbContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteDefect(int DefectId)
        {
            try
            {
                using (var dbContext = new DefectTrackerContext())
                {
                    var Defect = dbContext.Defects
                        .Where(d => d.Id == DefectId)
                        .FirstOrDefault();

                    dbContext.Entry(Defect).State = System.Data.Entity.EntityState.Deleted;
                    dbContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}