namespace BeonCollection.Api.Dtos;

public record CreateGameDto(string Title);

public record GameDto(int Id, string Title, List<EditionDto> Editions);