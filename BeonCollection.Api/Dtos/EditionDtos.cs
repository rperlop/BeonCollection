using BeonCollection.Api.Models;

namespace BeonCollection.Api.Dtos;

public record CreateEditionDto(
    int GameId,
    Platform Platform,
    Region Region,
    EditionType Type,
    int? Year);

public record EditionDto(
    int Id,
    int GameId,
    Platform Platform,
    Region Region,
    EditionType Type,
    int? Year);