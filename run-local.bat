echo off
echo Limpando cache...
dotnet clean
echo Buscando packages...
dotnet restore
cd CadProcessosApp
echo Iniciando a aplicacao...
dotnet watch run --no-hot-reload