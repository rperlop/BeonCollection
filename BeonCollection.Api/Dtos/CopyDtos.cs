using BeonCollection.Api.Models;

namespace BeonCollection.Api.Dtos;

public record CreateCopyDto(
    int EditionId,
    Contents Contents,
    Condition Condition,
    string? Notes);

public record CopyDto(
    int Id,
    int EditionId,
    Contents Contents,
    Condition Condition,
    string? Notes);