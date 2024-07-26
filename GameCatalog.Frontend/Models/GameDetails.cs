using System.ComponentModel.DataAnnotations;

namespace GameCatalog.Frontend.Models;

public class GameDetails
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Surely the game has a name?")]
    [StringLength(50, ErrorMessage = "Name cannot be more than 50 characters long.")]
    public required string Name { get; set; }

    [Required(ErrorMessage = "Genre is required.")]
    [RegularExpression(@"(\d+)", ErrorMessage = "Please select a genre.")]
    public int GenreId { get; set; }

    [Range(1, 100, ErrorMessage = "Only prices between 1 and 100 are accepted.")]
    public decimal Price { get; set; }
    public DateOnly ReleaseDate { get; set; }
}
