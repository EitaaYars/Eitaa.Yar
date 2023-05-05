using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;

namespace Eitaa.Yar.Types.Abstractions;

/// <summary>
/// Represents bot API response
/// </summary>
/// <typeparam name="TResult">Expected type of operation result</typeparam>
[JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
public class ApiResponse<TResult>
{
    /// <summary>
    /// Gets a value indicating whether the request was successful.
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public bool Ok { get; private set; }

    /// <summary>
    /// Gets the error message.
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public string? Description { get; private set; }

    /// <summary>
    /// Gets the error code.
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public int? ErrorCode { get; private set; }

    /// <summary>
    /// Gets the result object.
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public TResult? Result { get; private set; }

    /// <summary>
    /// Initializes an instance of <see cref="ApiResponse{TResult}"/>
    /// </summary>
    /// <param name="ok">Indicating whether the request was successful</param>
    /// <param name="result">Result object</param>
    /// <param name="errorCode">Error code</param>
    /// <param name="description">Error message</param>
    public ApiResponse(
        bool ok,
        TResult result,
        int errorCode,
        string description)
    {
        Ok = ok;
        ErrorCode = errorCode;
        Description = description;
        Result = result;
    }

    [JsonConstructor]
    private ApiResponse()
    { }
}
