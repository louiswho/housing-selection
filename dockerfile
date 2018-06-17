# STAGE - BUILD
FROM microsoft/aspnetcore-build:2.0 as build
WORKDIR /docker
COPY src/Housing.Selection.Context/*.csproj Housing.Selection.Context/
COPY src/Housing.Selection.Library/*.csproj Housing.Selection.Library/
COPY src/Housing.Selection.Service/*.csproj Housing.Selection.Service/
RUN dotnet restore *.Service
COPY src ./
RUN dotnet publish *.Service --no-restore -o ../www

# STAGE - DEPLOY
FROM microsoft/aspnetcore:2.0 as deploy
WORKDIR /webapi
COPY --from=build /docker/www ./
ENV ASPNETCORE_URLS=http://+:80/
EXPOSE 80
CMD [ "dotnet", "Housing.Selection.Service.dll" ]
