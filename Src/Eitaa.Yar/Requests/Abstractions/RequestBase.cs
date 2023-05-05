using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Eitaa.Yar.Requests.Abstractions;

/// <summary>
/// Represents an API request
/// </summary>
/// <typeparam name="TResponse">Type of result expected in result</typeparam>
[JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
public abstract class RequestBase<TResponse> : IRequest<TResponse>
{
    /// <inheritdoc />
    [JsonIgnore]
    public HttpMethod Method { get; }

    /// <inheritdoc />
    [JsonIgnore]
    public string MethodName { get; }

    /// <summary>
    /// Initializes an instance of request
    /// </summary>
    /// <param name="methodName">Bot API method</param>
    protected RequestBase(string methodName)
        : this(methodName, HttpMethod.Post)
    { }

    /// <summary>
    /// Initializes an instance of request
    /// </summary>
    /// <param name="methodName">Bot API method</param>
    /// <param name="method">HTTP method to use</param>
    protected RequestBase(string methodName, HttpMethod method) =>
        (MethodName, Method) = (methodName, method);

    /// <summary>
    /// Generate content of HTTP message
    /// </summary>
    /// <returns>Content of HTTP request</returns>
    public virtual HttpContent? ToHttpContent() =>
        new StringContent(
            content: JsonConvert.SerializeObject(this),
            encoding: Encoding.UTF8,
            mediaType: "application/json"
        );
}
