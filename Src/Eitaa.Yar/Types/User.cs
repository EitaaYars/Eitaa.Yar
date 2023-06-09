﻿using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;

namespace Eitaa.Yar.Types;

/// <summary>
/// This object represents an Eitaa user or bot.
/// </summary>
[JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
public class User
{
    /// <summary>
    /// Unique identifier for this user or bot
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public long Id { get; set; }

    /// <summary>
    /// <see langword="true"/>, if this user is a bot
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public bool IsBot { get; set; }

    /// <summary>
    /// User's or bot’s first name
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public string FirstName { get; set; } = default!;

    /// <summary>
    /// Optional. User's or bot’s last name
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public string? LastName { get; set; }

    /// <summary>
    /// Optional. User's or bot’s username
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public string? Username { get; set; }

    /// <summary>
    /// Optional. <see langword="true"/>, if the bot can be invited to groups. Returned only in <see cref="Requests.GetMeRequest"/>
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public bool? CanJoinGroups { get; set; }

    /// <summary>
    /// Optional. <see langword="true"/>, if privacy mode is disabled for the bot. Returned only in <see cref="Requests.GetMeRequest"/>
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public bool? CanReadAllGroupMessages { get; set; }

    /// <summary>
    /// Optional. <see langword="true"/>, if the bot supports inline queries. Returned only in <see cref="Requests.GetMeRequest"/>
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public bool? SupportsInlineQueries { get; set; }

    /// <inheritdoc/>
    public override string ToString() =>
        $"{(Username is null ? $"{FirstName}{LastName?.Insert(0, " ")}" : $"@{Username}")} ({Id})";
}
