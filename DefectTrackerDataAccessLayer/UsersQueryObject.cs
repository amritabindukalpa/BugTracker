using DefectTracker.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace DefectTracker.DataAccessLayer
{
    public class UsersQueryObject
    {
        private string connectionString;

        public UsersQueryObject()
        {
            this.connectionString = ConfigurationManager.AppSettings.Get("DefectTrackerContext");
        }

        public IEnumerable<Users> GetUsersList()
        {
            using (var dbContext = new DefectTrackerContext())
            {
                var result = new List<Users>();
                result = dbContext.Users.OrderBy(u => u.Name).ToList();
                return result;
            }
        }

        public Users GetUsers(int id)
        {
            using (var dbContext = new DefectTrackerContext())
            {
                var result = dbContext.Users.Where(u => u.Id == id).FirstOrDefault();
                return result;
            }
        }

        public void AddUsers(Users Users)
        {
            using (var dbContext = new DefectTrackerContext())
            {
                try
                {
                    dbContext.Users.Add(Users);
                    dbContext.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public void EditUsers(Users Users)
        {
            try
            {
                using (var dbContext = new DefectTrackerContext())
                {
                    var existingUser = dbContext.Users.Where(u => u.Id == Users.Id).FirstOrDefault<Users>();

                    if (existingUser != null)
                    {
                        existingUser.Name = Users.Name;
                        existingUser.IsAdmin = Users.IsAdmin;
                        dbContext.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteUsers(int UsersId)
        {
            try
            {
                using (var dbContext = new DefectTrackerContext())
                {
                    var Users = dbContext.Users
                        .Where(u => u.Id == UsersId)
                        .FirstOrDefault();

                    dbContext.Entry(Users).State = System.Data.Entity.EntityState.Deleted;
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