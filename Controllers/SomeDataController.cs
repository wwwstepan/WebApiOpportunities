using Microsoft.AspNetCore.Mvc;

namespace NetCoreApp.Controllers;

/// <summary>
/// Controller, that can return some data
/// </summary>
[ApiController]
[Route("some-data")]
public class SomeDataController : ControllerBase
{
    /// <summary>
    /// Get user name by index
    /// </summary>
    /// <param name="index">index of user</param>
    /// <returns>User name (string)</returns>
    /// <returns></returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     Get /some-data/get-name?index=1
    ///
    /// </remarks>
    /// <response code="200">All ok</response>
    /// <response code="400">Some error</response>
    [HttpGet("get-name")]
    [Produces("text/plain")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<string> GetName(int index)
    {
        return index switch
        {
            0 => "Qwerty",
            1 => "Asdf",
            2 or 3 => "Zxcvbnm",
            _ => "Zero"
        };
    }

    /// <summary>
    /// Get country name
    /// </summary>
    /// <param name="prefix">Begin of country name</param>
    /// <returns>Country name (string)</returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     Get /some-data/get-country?prefix=Fin
    ///     
    ///     (returns "Finlandia")
    ///
    /// </remarks>
    /// <response code="200">All ok</response>
    /// <response code="400">Some error</response>
    [HttpGet("get-country")]
    [Produces("text/plain")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<string> GetCountry(string prefix)
    {
        return $"{prefix}landia";
    }

    /// <summary>
    /// Get some planet in Json format
    /// </summary>
    /// <returns>Planet attributes</returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     Get /some-data/get-planet
    ///     
    /// Now, returns Jupiter
    /// TODO: make real method, that read planet from database
    ///
    /// </remarks>
    /// <response code="200">All ok</response>
    /// <response code="400">Some error</response>
    [HttpGet("get-planet")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<Planet> GetPlanet()
    {
        return new Planet(
            "Jupiter",
            3511.12,
            19000,
            [
                new Satellite("Io"),
                new Satellite("Europa")
            ]
        );
    }

    /// <summary>
    /// Get planet radius
    /// </summary>
    /// <returns>radius (double type as string)</returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     Get /some-data/get-planet-r
    ///     with
    ///     {"Name":"Mars","Mass":2567.7,"Diameter":4244,"Satellites":[]}
    ///     in body
    ///     
    /// </remarks>
    /// <response code="200">All ok</response>
    /// <response code="400">Some error</response>
    [HttpPost("get-planet-r")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<double> GetPlanetRadius([FromBody] Planet planet)
    {
        return planet.Diameter / 2;
    }
}

public record Planet(string Name, double Mass, double Diameter, List<Satellite> Satellites);
public record Satellite(string Name);
