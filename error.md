  Message d'erreur :
   System.Net.Http.HttpRequestException : Response status code does not indicate success: 500 (Internal Server Error).
  Arborescence des appels de procédure :
     at System.Net.Http.HttpResponseMessage.EnsureSuccessStatusCode()
   at ShoppingProject.UnitTests.IntegrationTests.ProductsIntegrationTests.SearchProducts_Should_Return_Results() in O:\Projets\vsts-agent-win-x64-4.266.2\_work\1\s\src\Tests\IntegrationTests\ProductsIntegrationTests.cs:line 85
--- End of stack trace from previous location ---
  Échoué ShoppingProject.UnitTests.IntegrationTests.ProductsIntegrationTests.CreateProduct_Should_Return_Success_When_Authorized [29 ms]
  Message d'erreur :
   System.Net.Http.HttpRequestException : Response status code does not indicate success: 500 (Internal Server Error).
  Arborescence des appels de procédure :
     at System.Net.Http.HttpResponseMessage.EnsureSuccessStatusCode()
   at ShoppingProject.UnitTests.IntegrationTests.ProductsIntegrationTests.AuthenticateAsAdminAsync() in O:\Projets\vsts-agent-win-x64-4.266.2\_work\1\s\src\Tests\IntegrationTests\ProductsIntegrationTests.cs:line 31
   at ShoppingProject.UnitTests.IntegrationTests.ProductsIntegrationTests.CreateProduct_Should_Return_Success_When_Authorized() in O:\Projets\vsts-agent-win-x64-4.266.2\_work\1\s\src\Tests\IntegrationTests\ProductsIntegrationTests.cs:line 100
--- End of stack trace from previous location ---
  Échoué ShoppingProject.UnitTests.IntegrationTests.IdentityIntegrationTests.Register_Then_Login_Should_Return_Token [92 ms]
  Message d'erreur :
   System.Net.Http.HttpRequestException : Response status code does not indicate success: 400 (Bad Request).
  Arborescence des appels de procédure :
     at System.Net.Http.HttpResponseMessage.EnsureSuccessStatusCode()
   at ShoppingProject.UnitTests.IntegrationTests.IdentityIntegrationTests.Register_Then_Login_Should_Return_Token() in O:\Projets\vsts-agent-win-x64-4.266.2\_work\1\s\src\Tests\IntegrationTests\IdentityIntegrationTests.cs:line 29
--- End of stack trace from previous location ---
Fichier de résultats : O:\Projets\vsts-agent-win-x64-4.266.2\_work\_temp\hp_DESKTOP-GH65CIA_2026-01-22_10_14_36.trx

Échoué!  - échec :    79, réussite :   102, ignorée(s) :     0, total :   181, durée : 55 s - ShoppingProject.Tests.dll (net8.0)

##[error]Error: The process 'O:\Projets\vsts-agent-win-x64-4.266.2\_work\_tool\dotnet\dotnet.exe' failed with exit code 1
Result Attachments will be stored in LogStore
Run Attachments will be stored in LogStore
##[warning].NET 5 has some compatibility issues with older Nuget versions(<=5.7), so if you are using an older Nuget version(and not dotnet cli) to restore, then the dotnet cli commands (e.g. dotnet build) which rely on such restored packages might fail. To mitigate such error, you can either: (1) - Use dotnet cli to restore, (2) - Use Nuget version 5.8 to restore, (3) - Use global.json using an older sdk version(<=3) to build
Info: Azure Pipelines hosted agents have been updated and now contain .Net 5.x SDK/Runtime along with the older .Net Core version which are currently lts. Unless you have locked down a SDK version for your project(s), 5.x SDK might be picked up which might have breaking behavior as compared to previous versions. You can learn more about the breaking changes here: https://docs.microsoft.com/en-us/dotnet/core/tools/ and https://docs.microsoft.com/en-us/dotnet/core/compatibility/ . To learn about more such changes and troubleshoot, refer here: https://docs.microsoft.com/en-us/azure/devops/pipelines/tasks/build/dotnet-core-cli?view=azure-devops#troubleshooting
##[error]Dotnet command failed with non-zero exit code on the following projects : [
  'O:\\Projets\\vsts-agent-win-x64-4.266.2\\_work\\1\\s\\src\\Tests\\ShoppingProject.Tests.csproj'
]
Async Command Start: Publish test results
Publishing test results to test run '2'.
TestResults To Publish 181, Test run id:2
Test results publishing 181, remaining: 0. Test run id: 2
Published Test Run : https://dev.azure.com/rostelpanoumassi/Azure%20Pipeline/_TestManagement/Runs?runId=2&_a=runCharts
Flaky failed test results are opted out of pass percentage
Async Command End: Publish test results
Finishing: Test with coverage
