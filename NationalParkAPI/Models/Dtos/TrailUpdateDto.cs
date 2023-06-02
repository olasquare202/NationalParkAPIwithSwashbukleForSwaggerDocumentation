using NationalParkAPI.Dtos;
using System.ComponentModel.DataAnnotations;
using static NationalParkAPI.Models.Trail;

namespace NationalParkAPI.Models.Dtos
{
    public class TrailUpdateDto
    {

        public int Id { get; set; }//We need Id to Update
        [Required]
        public string Name { get; set; }
        [Required]
        public double Distance { get; set; }

        public DifficultyType Difficulty { get; set; }//A variable to store d DifficultyType
        [Required]
        public int NationalParkId { get; set; }//bcos d Trail is associted with NationalPark



    }
}
