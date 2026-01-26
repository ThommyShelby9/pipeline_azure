Starting: Deploy to App Service
==============================================================================
Task         : Azure Web App
Description  : Deploy an Azure Web App for Linux or Windows
Version      : 1.266.0
Author       : Microsoft Corporation
Help         : https://aka.ms/azurewebapptroubleshooting
==============================================================================
Got service connection details for Azure App Service:'securefintech-api'
Package deployment using ZIP Deploy initiated.
Deploy logs can be viewed at https://securefintech-api-staging.scm.azurewebsites.net/api/deployments/2ad98eb5-cb99-4dba-ac6a-d1c938c1a1e3/log
Successfully deployed web package to App Service.
Successfully added release annotation to the Application Insight : appi-ydzybb4cow2gg
Successfully updated deployment History at https://securefintech-api-staging.scm.azurewebsites.net/api/deployments/1041769180714360
App Service Application URL: https://securefintech-api-staging.azurewebsites.net
Finishing: Deploy to App Service

Starting: Run smoke tests
==============================================================================
Task         : PowerShell
Description  : Run a PowerShell script on Linux, macOS, or Windows
Version      : 2.266.0
Author       : Microsoft Corporation
Help         : https://docs.microsoft.com/azure/devops/pipelines/tasks/utility/powershell
==============================================================================
Generating script.
========================== Starting Command Output ===========================
"C:\WINDOWS\System32\WindowsPowerShell\v1.0\powershell.exe" -NoLogo -NoProfile -NonInteractive -ExecutionPolicy Unrestricted -Command ". 'O:\Projets\vsts-agent-win-x64-4.266.2\_work\_temp\cc29ebee-cf71-4bb2-af2b-0a05d839c030.ps1'"
========================================
Running smoke tests against https://securefintech-api-staging.azurewebsites.net
Environment: staging
========================================

Test 1: Health endpoint (/health)
  [FAIL] Health endpoint failed: Le serveur distant a retourné une erreur : (404) Introuvable.

Test 2: API availability (/swagger)
  [INFO] Swagger UI not available - may be disabled in production

Test 3: Static files (index.html)
  [PASS] Static files served correctly (HTTP 200)

Test 4: React app loaded (checking for 'root' element)
  [FAIL] React app root element not found in response

Test 5: Health UI endpoint (/health-ui)
  [INFO] Health UI not available: Le serveur distant a retourné une erreur : (404) Introuvable.

========================================
Some smoke tests failed!
##[error]PowerShell exited with code '1'.
Finishing: Run smoke tests

Starting: Checkout ThommyShelby9/pipeline_azure@develop to s
==============================================================================
Task         : Get sources
Description  : Get sources from a repository. Supports Git, TfsVC, and SVN repositories.
Version      : 1.0.0
Author       : Microsoft
Help         : [More Information](https://go.microsoft.com/fwlink/?LinkId=798199)
==============================================================================
Syncing repository: ThommyShelby9/pipeline_azure (GitHub)
Prepending Path environment variable with directory containing 'git.exe'.
git version
git version 2.50.1.windows.1
git lfs version
git-lfs/3.4.0 (GitHub; windows amd64; go 1.20.6; git d06d6e9e)
git config --get remote.origin.url
git clean -ffdx
Removing artifacts/
Removing src/Presentation/API/wwwroot/
git reset --hard HEAD
HEAD is now at dbbe8a4 feat: replace SQL Server with PostgreSQL and add Redis and Service Bus
git sparse-checkout disable
git config gc.auto 0
git config core.longpaths true
git config --get-all http.https://github.com/ThommyShelby9/pipeline_azure.extraheader
git config --get-all http.extraheader
git config --get-regexp .*extraheader
git config --get-all http.proxy
git config http.version HTTP/1.1
git config --get-all remote.origin.promisor
git config --get-all remote.origin.partialclonefilter
git --config-env=http.extraheader=env_var_http.extraheader fetch --force --tags --prune --prune-tags --progress --no-recurse-submodules origin --depth=1  +dbbe8a48b18b58f4da6770c26c8e15ab36944cbc:refs/remotes/origin/dbbe8a48b18b58f4da6770c26c8e15ab36944cbc
remote: Total 0 (delta 0), reused 0 (delta 0), pack-reused 0 (from 0)
git --config-env=http.extraheader=env_var_http.extraheader fetch --force --tags --prune --prune-tags --progress --no-recurse-submodules origin --depth=1  +dbbe8a48b18b58f4da6770c26c8e15ab36944cbc
remote: Total 0 (delta 0), reused 0 (delta 0), pack-reused 0 (from 0)
From https://github.com/ThommyShelby9/pipeline_azure
 * branch            dbbe8a48b18b58f4da6770c26c8e15ab36944cbc -> FETCH_HEAD
git checkout --progress --force refs/remotes/origin/dbbe8a48b18b58f4da6770c26c8e15ab36944cbc
HEAD is now at dbbe8a4 feat: replace SQL Server with PostgreSQL and add Redis and Service Bus
Finishing: Checkout ThommyShelby9/pipeline_azure@develop to s

{
  "id": "1041769180714360",
  "status": 4,
  "status_text": "",
  "author_email": "",
  "author": "Microsoft.VisualStudio.Services.TFS",
  "deployer": "VSTS",
  "message": "{\"type\":\"Deployment\",\"commitId\":\"dbbe8a48b18b58f4da6770c26c8e15ab36944cbc\",\"buildId\":\"104\",\"buildNumber\":\"20260123.27\",\"repoProvider\":\"GitHub\",\"repoName\":\"ThommyShelby9/pipeline_azure\",\"collectionUrl\":\"https://dev.azure.com/rostelpanoumassi/\",\"teamProject\":\"9ceceb83-10af-40b2-8d91-aaf15e800ba1\",\"buildProjectUrl\":\"https://dev.azure.com/rostelpanoumassi/9ceceb83-10af-40b2-8d91-aaf15e800ba1\",\"repositoryUrl\":\"https://github.com/ThommyShelby9/pipeline_azure\",\"branch\":\"develop\",\"teamProjectName\":\"Azure Pipeline\",\"slotName\":\"staging\"}",
  "progress": "",
  "received_time": "2026-01-23T15:05:18.9157107Z",
  "start_time": "2026-01-23T15:05:18.9157107Z",
  "end_time": "2026-01-23T15:05:18.9157107Z",
  "last_success_end_time": "2026-01-23T15:05:18.9157107Z",
  "complete": true,
  "active": true,
  "is_temp": false,
  "is_readonly": true,
  "url": "https://securefintech-api-staging.scm.azurewebsites.net/api/deployments/1041769180714360",
  "log_url": "https://securefintech-api-staging.scm.azurewebsites.net/api/deployments/1041769180714360/log",
  "site_name": "securefintech-api",
  "build_summary": {
    "errors": [],
    "warnings": []
  }
}


[
  {
    "log_time": "2026-01-23T15:04:32.9193068Z",
    "id": "a3b18b86-0f3c-4e4d-895c-669c3ddd6f7b",
    "message": "Updating submodules.",
    "type": 0,
    "details_url": null
  },
  {
    "log_time": "2026-01-23T15:04:34.0927066Z",
    "id": "005a221e-92a0-4af1-8686-99ab9f431c9c",
    "message": "Preparing deployment for commit id '2ad98eb5-c'.",
    "type": 0,
    "details_url": null
  },
  {
    "log_time": "2026-01-23T15:04:34.452555Z",
    "id": "03ce9d5c-4e50-4178-b166-30ef04ead5c6",
    "message": "PreDeployment: context.CleanOutputPath False",
    "type": 0,
    "details_url": null
  },
  {
    "log_time": "2026-01-23T15:04:34.586177Z",
    "id": "e59b6624-9e29-4ae4-9c9a-338004f7d295",
    "message": "PreDeployment: context.OutputPath /home/site/wwwroot",
    "type": 0,
    "details_url": null
  },
  {
    "log_time": "2026-01-23T15:04:34.7464064Z",
    "id": "9344dad9-b7af-4fc5-954f-155c39e21b62",
    "message": "Generating deployment script.",
    "type": 0,
    "details_url": null
  },
  {
    "log_time": "2026-01-23T15:04:34.9325487Z",
    "id": "b0e943a5-afc6-4a7c-9ff0-9fc27557f623",
    "message": "Running deployment command...",
    "type": 0,
    "details_url": "https://securefintech-api-staging.scm.azurewebsites.net/api/deployments/2ad98eb5-cb99-4dba-ac6a-d1c938c1a1e3/log/b0e943a5-afc6-4a7c-9ff0-9fc27557f623"
  },
  {
    "log_time": "2026-01-23T15:04:54.0356221Z",
    "id": "7abc7d87-9bad-4613-856f-3977921f409f",
    "message": "Running post deployment command(s)...",
    "type": 0,
    "details_url": null
  },
  {
    "log_time": "2026-01-23T15:04:54.1683696Z",
    "id": "7eaaa7cc-8ecb-42f6-acf0-9ae44cad3b75",
    "message": "Triggering recycle (preview mode disabled).",
    "type": 0,
    "details_url": null
  },
  {
    "log_time": "2026-01-23T15:04:54.3330409Z",
    "id": "cc26a3a6-0999-4ad9-bf52-84862195942c",
    "message": "Deployment successful. deployer = VSTS_ZIP_DEPLOY deploymentPath = ZipDeploy. Extract zip.",
    "type": 0,
    "details_url": null
  }
]
