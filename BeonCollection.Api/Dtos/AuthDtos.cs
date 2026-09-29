namespace BeonCollection.Api.Dtos;

public record LoginDto(string Username, string Password);

public record LoginResultDto(string Token);