using NationalParkAPI.Data;
using NationalParkAPI.Models;
using NationalParkAPI.Repository.IRepository;

namespace NationalParkAPI.Repository
{
    public class NationalParkRepository : INationalParkRepository
    {
        //Add d methods implemented into Dis class 'NationalParkRepository'
        private readonly ApplicationDbContext _db;
        public NationalParkRepository(ApplicationDbContext db) //Constructor to do Dependency Injection
        {
            _db = db; //To access d ApplicationDbContext
        }
        //We implement all d interface as below
        public bool CreateNationalPark(NationalPark nationalPark)
        {
            _db.NationalParks.Add(nationalPark);
            return Save();
        }

        public bool DeleteNationalPark(NationalPark nationalPark)
        {
            _db.NationalParks.Remove(nationalPark);
            return Save();
        }

        public NationalPark GetNationalParkById(int nationalParkId)
        {
            return _db.NationalParks.FirstOrDefault(a => a.Id == nationalParkId);//We get only one nationalPark by using ".FirstOrDefault"
        }

        public ICollection<NationalPark> GetNationalParks()
        {
            return _db.NationalParks.OrderBy(a => a.Name).ToList();//We get all nationalParks by using ".OrderBy"
        }

        public bool NationalParkExists(string name)
        {
            bool value = _db.NationalParks.Any(a => a.Name.ToLower().Trim() == name.ToLower().Trim());
            return value;
        }

        public bool NationalParkExists(int id)
        {
            return _db.NationalParks.Any(a =>a.Id == id);
        }

        public bool Save()
        {
           return _db.SaveChanges() >= 0 ? true : false;
        }

        public bool UpdateNationalPark(NationalPark nationalPark)
        {
            _db.NationalParks.Update(nationalPark);
            return Save();
        }
    }
}
