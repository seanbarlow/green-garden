# green-garden

## cert

`powershell
dotnet dev-certs https -ep $env:USERPROFILE\.aspnet\https\aspnetapp.pfx -p 'green-garden-cert'
dotnet dev-certs https --trust``

## run

`powershell
docker run --rm -it -p 32777:80 -p 32790:443 -e ASPNETCORE_URLS="https://+;http://+" -e ASPNETCORE_HTTPS_PORT=32780 -e ASPNETCORE_ENVIRONMENT=Production -e ASPNETCORE_Kestrel__Certificates__Default__Password="green-garden-cert" -e ASPNETCORE_Kestrel__Certificates__Default__Path=/https/aspnetapp.pfx -v #env:USERPROFILE\.aspnet\https:/https/ --name gg-live ggserver`
