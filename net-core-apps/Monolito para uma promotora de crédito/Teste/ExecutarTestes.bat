dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=cobertura
dotnet reportgenerator "-reports:coverage.cobertura.xml" "-targetdir:RelatorioTestes"
start .\RelatorioTestes\index.htm