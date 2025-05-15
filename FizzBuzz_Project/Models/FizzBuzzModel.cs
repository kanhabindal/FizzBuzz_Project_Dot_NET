using System.ComponentModel.DataAnnotations;


namespace FizzBuzz_Project.Models
{
    public class FizzBuzzModel
    {
        [Range(1, 1000, ErrorMessage = "Please enter a number between 1 and 1000.")]
        public int Number { get; set; }
    }
}
