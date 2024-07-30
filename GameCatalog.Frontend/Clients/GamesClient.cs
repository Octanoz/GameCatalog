using GameCatalog.Frontend.Models;

namespace GameCatalog.Frontend.Clients;

public class GamesClient
{

    private readonly List<GameSummary> games =
    [
        new(){ Id = 1, Name = "Street Fighter II", Genre = "Fighting", Price = 19.99M, ReleaseDate = new(1992, 7, 15) },
        new(){ Id = 2, Name = "Final Fantasy XIV", Genre = "Roleplaying", Price = 59.99M, ReleaseDate = new(2010,9, 30) },
        new(){ Id = 3, Name = "FIFA 23", Genre = "Sports", Price = 69.99M, ReleaseDate = new(2022, 9, 27) }
    ];

    public GameSummary[] GetGames() => games.ToArray();

    public void AddGame(GameDetails game)
    {
        GameSummary gameSummary = new()
        {
            Id = games.Count + 1,
            Name = game.Name,
            Genre = ((Genre)game.GenreId).ToString().Replace("_", " & "),
            Price = game.Price,
            ReleaseDate = game.ReleaseDate
        };

        games.Add(gameSummary);
    }

    public GameDetails GetGame(int id)
    {
        GameSummary game = GetGameSummaryById(id);

        int genreId = GetGenreId(game.Genre);

        return new() { Id = game.Id, Name = game.Name, GenreId = genreId, Price = game.Price, ReleaseDate = game.ReleaseDate };
    }

    public void UpdateGame(GameDetails updatedGame)
    {
        GameSummary existingGame = GetGameSummaryById(updatedGame.Id);

        existingGame.Genre = ConvertEnumNameToGenre(((Genre)updatedGame.GenreId).ToString());
        existingGame.Price = updatedGame.Price;
    }

    public void DeleteGame(int id)
    {
        var game = GetGameSummaryById(id);
        games.Remove(game);
    }

    private GameSummary GetGameSummaryById(int id) => games.Find(x => x.Id == id)
                    ?? throw new ArgumentNullException($"No game found with id: [{id}]");

    static string ConvertEnumNameToGenre(string genre) => genre.Replace(" & ", "_");
    static string ConvertGenreToEnumName(string genre) => genre.Replace(" & ", "_");
    static int GetGenreId(string genre) => Enum.GetNames<Genre>().ToList().IndexOf(ConvertGenreToEnumName(genre)) + 1;

}
