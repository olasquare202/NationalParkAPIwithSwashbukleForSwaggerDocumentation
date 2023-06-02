using Microsoft.EntityFrameworkCore;
using NationalParkAPI.Data;
using NationalParkAPI.Models;
using NationalParkAPI.Repository.IRepository;
using TrailAPI.Repository.IRepository;

namespace NationalParkAPI.Repository
{
    public class TrailRepository : ITrailRepository
    {
        //Add d methods implemented into Dis class 'NationalParkRepository'
        private readonly ApplicationDbContext _db;
        public TrailRepository(ApplicationDbContext db) //Constructor to do Dependency Injection
        {
            _db = db; //To access d ApplicationDbContext
        }
        //We implement all d interface as below
        public bool CreateTrail(Trail trail)
        {
            _db.Trails.Add(trail);
            return Save();
        }

        public bool DeleteTrail(Trail trail)
        {
            _db.Trails.Remove(trail);
            return Save();
        }

        public Trail GetTrailById(int trailId)
        {
            return _db.Trails.Include(w => w.NationalPark).FirstOrDefault(a => a.Id == trailId);//We get only one nationalPark by using ".FirstOrDefault"
        }

        public ICollection<Trail> GetTrails()
        {
            return _db.Trails.Include(w => w.NationalPark).OrderBy(a => a.Name).ToList();//We get all nationalParks by using ".OrderBy"
        }

        public bool TrailExists(string name)
        {
            bool value = _db.Trails.Any(a => a.Name.ToLower().Trim() == name.ToLower().Trim());
            return value;
        }

        public bool TrailExists(int id)
        {
            return _db.Trails.Any(a =>a.Id == id);
        }

        public bool Save()
        {
           return _db.SaveChanges() >= 0 ? true : false;
        }

        public bool UpdateTrail(Trail trail)
        {
            _db.Trails.Update(trail);
            return Save();
        }

        //ICollection<Trail> ITrailRepository.GetTrails()
        //{
        //    throw new NotImplementedException();
        //}

        public ICollection<Trail> GetAllTrailsInNationalPark(int nationalParkId)
        {
            return _db.Trails.Include(w => w.NationalPark).Where(c => c.NationalParkId == nationalParkId).ToList();
        }

        //Trail ITrailRepository.GetTrailById(int trailId)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
