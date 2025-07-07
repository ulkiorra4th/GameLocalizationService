# Используем официальный образ .NET 8 SDK для сборки
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Копируем файлы решения и проектов
COPY GameLocalization.sln ./
COPY GameLocalization/ ./GameLocalization/
COPY GameLocalization.Application/ ./GameLocalization.Application/
COPY GameLocalization.Domain/ ./GameLocalization.Domain/
COPY GameLocalization.Common/ ./GameLocalization.Common/
COPY GameLocalization.Infrastructure.Mapping/ ./GameLocalization.Infrastructure.Mapping/
COPY GameLocalization.Persistence.Postgres/ ./GameLocalization.Persistence.Postgres/

# Восстанавливаем зависимости
RUN dotnet restore GameLocalization/GameLocalization.csproj

# Публикуем проект (Release)
RUN dotnet publish GameLocalization/GameLocalization.csproj -c Release -o /app/publish

# Используем официальный рантайм-образ .NET 8 для запуска
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

# Открываем порт (по умолчанию 80)
EXPOSE 8080

# Запускаем приложение
ENTRYPOINT ["dotnet", "GameLocalization.dll"] 