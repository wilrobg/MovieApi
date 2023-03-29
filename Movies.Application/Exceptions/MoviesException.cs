using System.Net;

namespace Movies.Application.Exceptions;

public class MoviesException : Exception
{
	public HttpStatusCode HttpStatusCode { get; }

	public MoviesException(HttpStatusCode statusCode, string message) : base(message) 
	{
		HttpStatusCode = statusCode;
	}
}
