using System;
using System.Threading;
using System.Threading.Tasks;
using Eitaa.Yar.Exceptions;
using Eitaa.Yar.Requests.Abstractions;

namespace Eitaa.Yar;

/// <summary>
/// A client interface to use the Eitaa Yar API
/// </summary>
public interface IEitaaYarClient
{
    /// <summary>
    /// Timeout for requests
    /// </summary>
    TimeSpan Timeout { get; set; }

    /// <summary>
    /// Instance of <see cref="IExceptionParser"/> to parse errors from Bot API into
    /// <see cref="ApiRequestException"/>
    /// </summary>
    /// <remarks>This property is not thread safe</remarks>
    IExceptionParser ExceptionsParser { get; set; }

    /// <summary>
    /// Send a request to Yar API
    /// </summary>
    /// <typeparam name="TResponse">Type of expected result in the response object</typeparam>
    /// <param name="request">API request object</param>
    /// <param name="cancellationToken"></param>
    /// <returns>Result of the API request</returns>
    Task<TResponse> MakeRequestAsync<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default);
}