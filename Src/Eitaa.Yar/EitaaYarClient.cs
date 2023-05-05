using System;
using System.Net;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Threading;
using Eitaa.Yar.Requests.Abstractions;
using Eitaa.Yar.Extensions;
using Eitaa.Yar.Exceptions;
using Eitaa.Yar.Types.Abstractions;

namespace Eitaa.Yar;

/// <summary>
/// Http client responsible for sending http requests to EitaaYar Api.
/// </summary>
public class EitaaYarClient : IEitaaYarClient
{
    readonly string _token;
    readonly HttpClient _httpClient;

    /// <inheritdoc />
    public TimeSpan Timeout
    {
        get => _httpClient.Timeout;
        set => _httpClient.Timeout = value;
    }

    /// <inheritdoc />
    public IExceptionParser ExceptionsParser { get; set; } = new DefaultExceptionParser();

    /// <summary>
    /// Create a new <see cref="EitaaYarClient"/> instance.
    /// </summary>
    /// <param name="token"></param>
    /// <param name="httpClient">A custom <see cref="HttpClient"/></param>
    /// <exception cref="ArgumentException">
    /// Thrown if <paramref name="token"/> format is invalid
    /// </exception>
    public EitaaYarClient(
        string token,
        HttpClient? httpClient = null)
    {
        _token = token ?? throw new ArgumentNullException(nameof(token));
        _httpClient = httpClient ?? new HttpClient();
    }

    /// <inheritdoc />
    public virtual async Task<TResponse> MakeRequestAsync<TResponse>(
        IRequest<TResponse> request,
        CancellationToken cancellationToken = default)
    {
        if (request is null) { throw new ArgumentNullException(nameof(request)); }

        var url = $"https://eitaayar.ir/api/{_token}/{request.MethodName}";

        var httpRequest = new HttpRequestMessage(method: request.Method, requestUri: url)
        {
            Content = request.ToHttpContent()
        };

        using var httpResponse = await SendRequestAsync(
            httpClient: _httpClient,
            httpRequest: httpRequest,
            cancellationToken: cancellationToken
        ).ConfigureAwait(false);

        if (httpResponse.StatusCode != HttpStatusCode.OK)
        {
            var failedApiResponse = await httpResponse
                .DeserializeContentAsync<ApiResponse>(
                    guard: response =>
                        response.ErrorCode == default ||
                        // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
                        response.Description is null
                )
                .ConfigureAwait(false);

            throw ExceptionsParser.Parse(failedApiResponse);
        }

        var apiResponse = await httpResponse
            .DeserializeContentAsync<ApiResponse<TResponse>>(
                guard: response => response.Ok == false ||
                                   response.Result is null
            )
            .ConfigureAwait(false);

        return apiResponse.Result!;

        [MethodImpl(methodImplOptions: MethodImplOptions.AggressiveInlining)]
        static async Task<HttpResponseMessage> SendRequestAsync(
            HttpClient httpClient,
            HttpRequestMessage httpRequest,
            CancellationToken cancellationToken)
        {
            HttpResponseMessage? httpResponse;
            try
            {
                httpResponse = await httpClient
                    .SendAsync(request: httpRequest, cancellationToken: cancellationToken)
                    .ConfigureAwait(continueOnCapturedContext: false);
            }
            catch (TaskCanceledException exception)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    throw;
                }

                throw new RequestException(message: "Request timed out", innerException: exception);
            }
            catch (Exception exception)
            {
                throw new RequestException(
                    message: "Exception during making request",
                    innerException: exception
                );
            }

            return httpResponse;
        }
    }

}
