//using TrailAPI.Models;

using NationalParkAPI.Models;

namespace TrailAPI.Repository.IRepository
{
    public interface ITrailRepository
    { 
        //Add all d methods/end-points we want to access in Db
        ICollection<Trail> GetTrails(); //We use ICollection to Get all trails
        ICollection<Trail> GetAllTrailsInNationalPark(int nationalParkId);
        Trail GetTrailById(int trailId); // GetBy Id
        bool TrailExists(string name);//Check by name if Exist
        bool TrailExists(int id);//Check by Id if Exist
        bool CreateTrail(Trail trail);//while creating Trail we also pass it object class i.e 'trail'
        bool UpdateTrail(Trail trail);
        bool DeleteTrail(Trail trail);//you can delete based on Id or object
        bool Save();
    }
}
