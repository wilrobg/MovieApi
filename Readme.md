# .NET 7 API with PostgreSQL and Redis

This is a sample .NET 7 API that use PostgreSQL and Redis services.

## Getting Started

### Prerequisites

Before you can run this API, you need to have the following installed on your machine:

* [.NET 7 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/7.0)
* [PostgreSQL](https://www.postgresql.org/)
* [Redis](https://redis.io/)

## Running the API

To run the API, you need to set the connection string for PostgreSQL and Redis in the `appsettings.json` file:

```json
{
  "ConnectionStrings": {
    "ConnectionString": "host=host.docker.internal;port=5431;database=movies_db;username=postgres;password=postgres;Pooling=true"
  },
  "Cache": {
    "HostName": "host.docker.internal",
    "PortNumber": "6379",
    "Password": "eYVX7EwVmmxKPCDmwMtyKVge8oLd2t81"
  }
}

```

### Docker Compose

It can be dockerized the services by running the docker-compose attached Movie.API.

## Built With

* [.NET 7 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/7.0)
* [PostgreSQL](https://www.postgresql.org/)
* [Redis](https://redis.io/)