using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NationalParkAPI.Models
{
    public class Trail
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public double Distance { get; set; }
        public enum DifficultyType { Easy, Moderate, Difficult, Expert }
        public DifficultyType Difficulty { get; set; }//A variable to store d DifficultyType
        [Required]
        public int NationalParkId { get; set; }//bcos d Trail is associted with NationalPark
        [ForeignKey("NationalParkId")]//Foreign Key reference to add National park
        public NationalPark NationalPark { get; set; }//Bind d foreignKey to NationalPark table in d DB
        public DateTime Created { get; set; }

    }
}
