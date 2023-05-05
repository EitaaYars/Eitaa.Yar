using Eitaa.Yar.Requests.Abstractions;
using Eitaa.Yar.Types;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;

namespace Eitaa.Yar.Requests;

/// <summary>
/// A simple method for testing your bot’s auth token. Requires no parameters. Returns basic information
/// about the bot in form of a <see cref="User"/> object.
/// </summary>
[JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
public class GetMeRequest : ParameterlessRequest<User>
{
    /// <summary>
    /// Initializes a new request
    /// </summary>
    public GetMeRequest()
        : base("getMe")
    { }
}
