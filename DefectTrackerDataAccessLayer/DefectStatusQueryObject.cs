using DefectTracker.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace DefectTracker.DataAccessLayer
{
    public class DefectStatusQueryObject
    {
        private string connectionString;

        public DefectStatusQueryObject()
        {
            this.connectionString = ConfigurationManager.AppSettings.Get("DefectTrackerContext");
        }

        public IEnumerable<DefectStatus> GetDefectStatusList()
        {
            using (var dbContext = new DefectTrackerContext())
            {
                var result = new List<DefectStatus>();
                result = dbContext.DefectStatus.OrderBy(i => i.Status).ToList();
                return result;
            }
        }

        public DefectStatus GetDefectStatus(int id)
        {
            using (var dbContext = new DefectTrackerContext())
            {
                var result = dbContext.DefectStatus.Where(s => s.Id == id).FirstOrDefault();
                return result;
            }
        }

        public void AddDefectStatus(DefectStatus defectStatus)
        {
            using (var dbContext = new DefectTrackerContext())
            {
                try
                {
                    dbContext.DefectStatus.Add(defectStatus);
                    dbContext.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public void EditDefectStatus(DefectStatus defectStatus)
        {
            try
            {
                using (var dbContext = new DefectTrackerContext())
                {
                    var existingStatus = dbContext.DefectStatus.Where(s => s.Id == defectStatus.Id).FirstOrDefault<DefectStatus>();

                    if (existingStatus != null)
                    {
                        existingStatus.Status = defectStatus.Status;
                        dbContext.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteDefectStatus(int defectStatusId)
        {
            try
            {
                using (var dbContext = new DefectTrackerContext())
                {
                    var defectStatus = dbContext.DefectStatus
                        .Where(s => s.Id == defectStatusId)
                        .FirstOrDefault();

                    dbContext.Entry(defectStatus).State = System.Data.Entity.EntityState.Deleted;
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