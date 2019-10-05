FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["SocioLiftPay/SocioLiftPay.csproj", "SocioLiftPay/"]
RUN dotnet restore "SocioLiftPay/SocioLiftPay.csproj"
COPY . .
WORKDIR "/src/SocioLiftPay"
RUN dotnet build "SocioLiftPay.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "SocioLiftPay.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "SocioLiftPay.dll"]
