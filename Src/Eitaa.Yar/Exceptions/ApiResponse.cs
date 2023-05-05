using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;

namespace Eitaa.Yar.Exceptions;

/// <summary>
/// Represents failed API response
/// </summary>
[JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
public class ApiResponse
{
    /// <summary>
    /// Gets a value indicating whether the request was successful.
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public bool Ok { get; private set; }

    /// <summary>
    /// Gets the error message.
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public string Description { get; private set; }

    /// <summary>
    /// Gets the error code.
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public int ErrorCode { get; private set; }

    /// <summary>
    /// Initializes an instance of <see cref="ApiResponse"/>
    /// </summary>
    /// <param name="errorCode">Error code</param>
    /// <param name="description">Error message</param>
    public ApiResponse(
        int errorCode,
        string description)
    {
        ErrorCode = errorCode;
        Description = description;
    }
}
