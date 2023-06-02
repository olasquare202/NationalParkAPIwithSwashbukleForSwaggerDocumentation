using NationalParkAPI.Dtos;
using System.ComponentModel.DataAnnotations;
using static NationalParkAPI.Models.Trail;

namespace NationalParkAPI.Models.Dtos
{
    public class TrailDto
    {
        
       //public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public double Distance { get; set; }
       
        public DifficultyType Difficulty { get; set; }//A variable to store d DifficultyType
        [Required]
        public int NationalParkId { get; set; }//bcos d Trail is associted with NationalPark
        
        public NationalParkDto NationalPark { get; set; }//Bind d foreignKey to NationalPark table in d DB

    }
}
