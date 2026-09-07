# syntax=docker/dockerfile:1

# ---------- build ----------
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Restore first (better layer caching)
COPY BookQuotesApp.sln ./
COPY src/BookQuotesApp.Api/BookQuotesApp.Api.csproj src/BookQuotesApp.Api/
RUN dotnet restore src/BookQuotesApp.Api/BookQuotesApp.Api.csproj

# Build + publish
COPY src/ src/
RUN dotnet publish src/BookQuotesApp.Api/BookQuotesApp.Api.csproj \
    -c Release -o /app --no-restore

# ---------- runtime ----------
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app
COPY --from=build /app ./

ENV ASPNETCORE_ENVIRONMENT=Production
# Render overrides this via $PORT; EXPOSE is just documentation.
EXPOSE 8080

ENTRYPOINT ["dotnet", "BookQuotesApp.Api.dll"]
