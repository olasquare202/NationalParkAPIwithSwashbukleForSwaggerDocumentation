using NationalParkAPI.Models;

namespace NationalParkAPI.Repository.IRepository
{
    public interface INationalParkRepository
    { 
        //Add all d methods/end-points we want to access in Db
        ICollection<NationalPark> GetNationalParks(); //We use ICollection to Get all nationalParks
        NationalPark GetNationalParkById(int nationalParkId); // GetBy Id
        bool NationalParkExists(string name);//Check by name if Exist
        bool NationalParkExists(int id);//Check by Id if Exist
        bool CreateNationalPark(NationalPark nationalPark);//while creating NationalPark we also pass it object class i.e 'nationalPark'
        bool UpdateNationalPark(NationalPark nationalPark);
        bool DeleteNationalPark(NationalPark nationalPark);//you can delete based on Id or object
        bool Save();
    }
}
