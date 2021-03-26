dotnet sonarscanner begin /o:"jasonlwalker" /k:"JasonLWalker_Jlw.Utilities.Testing" /d:sonar.host.url="https://sonarcloud.io" /d:sonar.login=%JLW_SONAR_TOKEN% /d:sonar.cs.dotcover.reportsPaths="./dotCover.Unit.Output.html" 
dotnet restore
dotnet build --configuration Debug --no-restore
cd Jlw.Utilities.Testing.Tests
dotnet dotcover test -dcXML=dotCover.config.xml
cd ..
dotnet sonarscanner end /d:sonar.login=%JLW_SONAR_TOKEN%