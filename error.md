2026-01-22T19:43:43.8749808Z ##[section]Starting: Run ESLint
2026-01-22T19:43:43.8761433Z ==============================================================================
2026-01-22T19:43:43.8761563Z Task         : Command line
2026-01-22T19:43:43.8761640Z Description  : Run a command line script using Bash on Linux and macOS and cmd.exe on Windows
2026-01-22T19:43:43.8761795Z Version      : 2.250.1
2026-01-22T19:43:43.8761900Z Author       : Microsoft Corporation
2026-01-22T19:43:43.8762199Z Help         : https://docs.microsoft.com/azure/devops/pipelines/tasks/utility/command-line
2026-01-22T19:43:43.8762295Z ==============================================================================
2026-01-22T19:43:45.0668998Z Generating script.
2026-01-22T19:43:45.0762098Z Script contents: shell
2026-01-22T19:43:45.0771808Z npm run lint
2026-01-22T19:43:45.1029095Z ========================== Starting Command Output ===========================
2026-01-22T19:43:45.1291024Z ##[command]"C:\WINDOWS\system32\cmd.exe" /D /E:ON /V:OFF /S /C "CALL "O:\Projets\vsts-agent-win-x64-4.266.2\_work\_temp\1d851e62-6b4b-46b7-a2fd-34bca9a0a17e.cmd""
2026-01-22T19:43:46.1102735Z
2026-01-22T19:43:46.1104237Z > estore@0.0.0 lint
2026-01-22T19:43:46.1104656Z > eslint . --max-warnings 0
2026-01-22T19:43:46.1104830Z
2026-01-22T19:44:47.6759238Z
2026-01-22T19:44:47.6760693Z O:\Projets\vsts-agent-win-x64-4.266.2\_work\1\s\src\Presentation\Web\e2e\cart.spec.ts
2026-01-22T19:44:47.6761767Z    21:74  error  Unnecessary escape character: \"  no-useless-escape
2026-01-22T19:44:47.6764060Z    21:87  error  Unnecessary escape character: \"  no-useless-escape
2026-01-22T19:44:47.6764915Z    33:74  error  Unnecessary escape character: \"  no-useless-escape
2026-01-22T19:44:47.6765610Z    33:87  error  Unnecessary escape character: \"  no-useless-escape
2026-01-22T19:44:47.6766089Z    47:74  error  Unnecessary escape character: \"  no-useless-escape
2026-01-22T19:44:47.6766862Z    47:87  error  Unnecessary escape character: \"  no-useless-escape
2026-01-22T19:44:47.6767665Z    70:74  error  Unnecessary escape character: \"  no-useless-escape
2026-01-22T19:44:47.6768522Z    70:87  error  Unnecessary escape character: \"  no-useless-escape
2026-01-22T19:44:47.6769357Z    72:74  error  Unnecessary escape character: \"  no-useless-escape
2026-01-22T19:44:47.6770271Z    72:87  error  Unnecessary escape character: \"  no-useless-escape
2026-01-22T19:44:47.6770751Z    95:74  error  Unnecessary escape character: \"  no-useless-escape
2026-01-22T19:44:47.6771234Z    95:87  error  Unnecessary escape character: \"  no-useless-escape
2026-01-22T19:44:47.6771743Z   120:74  error  Unnecessary escape character: \"  no-useless-escape
2026-01-22T19:44:47.6772229Z   120:87  error  Unnecessary escape character: \"  no-useless-escape
2026-01-22T19:44:47.6772452Z
2026-01-22T19:44:47.6772968Z O:\Projets\vsts-agent-win-x64-4.266.2\_work\1\s\src\Presentation\Web\src\infrastructure\api\generated\shoppingProjectAPI.schemas.ts
2026-01-22T19:44:47.6774153Z   119:1  warning  Unused eslint-disable directive (no problems were reported from '@typescript-eslint/no-redeclare')
2026-01-22T19:44:47.6774465Z
2026-01-22T19:44:47.6774959Z O:\Projets\vsts-agent-win-x64-4.266.2\_work\1\s\src\Presentation\Web\src\infrastructure\persistence\AuthAPIService.ts
2026-01-22T19:44:47.6775858Z   63:13  error  'username' is assigned a value but never used  @typescript-eslint/no-unused-vars
2026-01-22T19:44:47.6776272Z
2026-01-22T19:44:47.6776770Z O:\Projets\vsts-agent-win-x64-4.266.2\_work\1\s\src\Presentation\Web\src\infrastructure\persistence\ProductAPIRepository.ts
2026-01-22T19:44:47.6777406Z   30:20  warning  Unexpected any. Specify a different type  @typescript-eslint/no-explicit-any
2026-01-22T19:44:47.6777661Z
2026-01-22T19:44:47.6778145Z O:\Projets\vsts-agent-win-x64-4.266.2\_work\1\s\src\Presentation\Web\src\presentation\features\admin\pages\AddProductPage.tsx
2026-01-22T19:44:47.6778725Z    90:29  error  JSX props should not use arrow functions  react/jsx-no-bind
2026-01-22T19:44:47.6781606Z   103:35  error  JSX props should not use arrow functions  react/jsx-no-bind
2026-01-22T19:44:47.6782119Z   115:41  error  JSX props should not use arrow functions  react/jsx-no-bind
2026-01-22T19:44:47.6782655Z   131:41  error  JSX props should not use arrow functions  react/jsx-no-bind
2026-01-22T19:44:47.6783161Z   155:49  error  JSX props should not use arrow functions  react/jsx-no-bind
2026-01-22T19:44:47.6785202Z   173:45  error  JSX props should not use arrow functions  react/jsx-no-bind
2026-01-22T19:44:47.6786324Z   197:41  error  JSX props should not use arrow functions  react/jsx-no-bind
2026-01-22T19:44:47.6787186Z   215:49  error  JSX props should not use arrow functions  react/jsx-no-bind
2026-01-22T19:44:47.6788345Z   245:41  error  JSX props should not use arrow functions  react/jsx-no-bind
2026-01-22T19:44:47.6788485Z
2026-01-22T19:44:47.6788772Z O:\Projets\vsts-agent-win-x64-4.266.2\_work\1\s\src\Presentation\Web\src\presentation\features\admin\pages\AdminDashboard.tsx
2026-01-22T19:44:47.6789113Z   124:25  error  JSX props should not use arrow functions  react/jsx-no-bind
2026-01-22T19:44:47.6789421Z   133:25  error  JSX props should not use arrow functions  react/jsx-no-bind
2026-01-22T19:44:47.6789745Z   142:25  error  JSX props should not use arrow functions  react/jsx-no-bind
2026-01-22T19:44:47.6790081Z   204:73  error  JSX props should not use arrow functions  react/jsx-no-bind
2026-01-22T19:44:47.6790426Z   216:81  error  JSX props should not use arrow functions  react/jsx-no-bind
2026-01-22T19:44:47.6790697Z   236:65  error  JSX props should not use arrow functions  react/jsx-no-bind
2026-01-22T19:44:47.6790903Z   283:57  error  JSX props should not use arrow functions  react/jsx-no-bind
2026-01-22T19:44:47.6790979Z
2026-01-22T19:44:47.6791177Z O:\Projets\vsts-agent-win-x64-4.266.2\_work\1\s\src\Presentation\Web\src\presentation\features\admin\pages\AuditLogsPage.tsx
2026-01-22T19:44:47.6791403Z   110:21  error  JSX props should not use arrow functions  react/jsx-no-bind
2026-01-22T19:44:47.6791609Z   120:21  error  JSX props should not use arrow functions  react/jsx-no-bind
2026-01-22T19:44:47.6791690Z
2026-01-22T19:44:47.6792012Z O:\Projets\vsts-agent-win-x64-4.266.2\_work\1\s\src\Presentation\Web\src\presentation\features\auth\hooks\useAuth.ts
2026-01-22T19:44:47.6792761Z   35:22  warning  Unexpected any. Specify a different type  @typescript-eslint/no-explicit-any
2026-01-22T19:44:47.6793292Z   49:22  warning  Unexpected any. Specify a different type  @typescript-eslint/no-explicit-any
2026-01-22T19:44:47.6793464Z
2026-01-22T19:44:47.6793812Z O:\Projets\vsts-agent-win-x64-4.266.2\_work\1\s\src\Presentation\Web\src\presentation\features\cart\components\BasketItem.tsx
2026-01-22T19:44:47.6794336Z   31:13  error  JSX props should not use arrow functions  react/jsx-no-bind
2026-01-22T19:44:47.6794886Z   45:13  error  JSX props should not use arrow functions  react/jsx-no-bind
2026-01-22T19:44:47.6795144Z
2026-01-22T19:44:47.6795655Z O:\Projets\vsts-agent-win-x64-4.266.2\_work\1\s\src\Presentation\Web\src\presentation\features\cart\hooks\useCart.ts
2026-01-22T19:44:47.6796316Z   35:22  warning  Unexpected any. Specify a different type  @typescript-eslint/no-explicit-any
2026-01-22T19:44:47.6796566Z
2026-01-22T19:44:47.6797091Z O:\Projets\vsts-agent-win-x64-4.266.2\_work\1\s\src\Presentation\Web\src\presentation\features\cart\pages\CartsPage.tsx
2026-01-22T19:44:47.6797698Z   147:23  error  JSX props should not use arrow functions  react/jsx-no-bind
2026-01-22T19:44:47.6798305Z   188:17  error  JSX props should not use arrow functions  react/jsx-no-bind
2026-01-22T19:44:47.6798746Z
2026-01-22T19:44:47.6799902Z O:\Projets\vsts-agent-win-x64-4.266.2\_work\1\s\src\Presentation\Web\src\presentation\features\product\components\ProductCard.tsx
2026-01-22T19:44:47.6800926Z   59:13  error  JSX props should not use arrow functions  react/jsx-no-bind
2026-01-22T19:44:47.6801396Z
2026-01-22T19:44:47.6803345Z O:\Projets\vsts-agent-win-x64-4.266.2\_work\1\s\src\Presentation\Web\src\presentation\features\product\hooks\useProducts.ts
2026-01-22T19:44:47.6844905Z    64:22  warning  Unexpected any. Specify a different type  @typescript-eslint/no-explicit-any
2026-01-22T19:44:47.6845908Z    83:22  warning  Unexpected any. Specify a different type  @typescript-eslint/no-explicit-any
2026-01-22T19:44:47.6846823Z   101:22  warning  Unexpected any. Specify a different type  @typescript-eslint/no-explicit-any
2026-01-22T19:44:47.6849252Z
2026-01-22T19:44:47.6850116Z O:\Projets\vsts-agent-win-x64-4.266.2\_work\1\s\src\Presentation\Web\src\presentation\features\product\pages\CategoryPage.tsx
2026-01-22T19:44:47.6851104Z    81:13  error  JSX props should not use arrow functions  react/jsx-no-bind
2026-01-22T19:44:47.6851953Z    86:13  error  JSX props should not use arrow functions  react/jsx-no-bind
2026-01-22T19:44:47.6852800Z   102:15  error  JSX props should not use arrow functions  react/jsx-no-bind
2026-01-22T19:44:47.6853643Z   107:15  error  JSX props should not use arrow functions  react/jsx-no-bind
2026-01-22T19:44:47.6854039Z
2026-01-22T19:44:47.6854860Z O:\Projets\vsts-agent-win-x64-4.266.2\_work\1\s\src\Presentation\Web\src\presentation\features\product\pages\ProductDetailPage.tsx
2026-01-22T19:44:47.6856144Z    46:25  error  JSX props should not use arrow functions  react/jsx-no-bind
2026-01-22T19:44:47.6856990Z   119:37  error  JSX props should not use arrow functions  react/jsx-no-bind
2026-01-22T19:44:47.6858032Z   127:37  error  JSX props should not use arrow functions  react/jsx-no-bind
2026-01-22T19:44:47.6858889Z   166:21  error  JSX props should not use arrow functions  react/jsx-no-bind
2026-01-22T19:44:47.6859243Z
2026-01-22T19:44:47.6860069Z O:\Projets\vsts-agent-win-x64-4.266.2\_work\1\s\src\Presentation\Web\src\presentation\features\product\pages\ProductListPage.tsx
2026-01-22T19:44:47.6861009Z   66:37  error  JSX props should not use arrow functions  react/jsx-no-bind
2026-01-22T19:44:47.6861386Z
2026-01-22T19:44:47.6862196Z O:\Projets\vsts-agent-win-x64-4.266.2\_work\1\s\src\Presentation\Web\src\presentation\shared\components\ErrorBoundary.tsx
2026-01-22T19:44:47.6863186Z   46:31  error  `'` can be escaped with `&apos;`, `&lsquo;`, `&#39;`, `&rsquo;`  react/no-unescaped-entities
2026-01-22T19:44:47.6864072Z   51:33  error  JSX props should not use arrow functions                         react/jsx-no-bind
2026-01-22T19:44:47.6864572Z
2026-01-22T19:44:47.6865350Z O:\Projets\vsts-agent-win-x64-4.266.2\_work\1\s\src\Presentation\Web\src\presentation\shared\components\Header.tsx
2026-01-22T19:44:47.6866268Z   125:23  error  JSX props should not use arrow functions  react/jsx-no-bind
2026-01-22T19:44:47.6867142Z   133:23  error  JSX props should not use arrow functions  react/jsx-no-bind
2026-01-22T19:44:47.6868169Z   144:17  error  JSX props should not use arrow functions  react/jsx-no-bind
2026-01-22T19:44:47.6869017Z   170:25  error  JSX props should not use arrow functions  react/jsx-no-bind
2026-01-22T19:44:47.6869387Z
2026-01-22T19:44:47.6870197Z O:\Projets\vsts-agent-win-x64-4.266.2\_work\1\s\src\Presentation\Web\src\presentation\shared\components\ThemeToggle.tsx
2026-01-22T19:44:47.6871099Z   16:13  error  JSX props should not use arrow functions  react/jsx-no-bind
2026-01-22T19:44:47.6871467Z
2026-01-22T19:44:47.6872237Z O:\Projets\vsts-agent-win-x64-4.266.2\_work\1\s\src\Presentation\Web\src\presentation\shared\pages\NotFoundPage.tsx
2026-01-22T19:44:47.6873285Z   19:41  error  `'` can be escaped with `&apos;`, `&lsquo;`, `&#39;`, `&rsquo;`  react/no-unescaped-entities
2026-01-22T19:44:47.6874234Z   19:62  error  `'` can be escaped with `&apos;`, `&lsquo;`, `&#39;`, `&rsquo;`  react/no-unescaped-entities
2026-01-22T19:44:47.6874624Z
2026-01-22T19:44:47.6875441Z O:\Projets\vsts-agent-win-x64-4.266.2\_work\1\s\src\Presentation\Web\src\presentation\store\slices\preferencesSlice.ts
2026-01-22T19:44:47.6876424Z   48:15  error  'isInitialized' is assigned a value but never used  @typescript-eslint/no-unused-vars
2026-01-22T19:44:47.6878004Z   53:15  error  'isInitialized' is assigned a value but never used  @typescript-eslint/no-unused-vars
2026-01-22T19:44:47.6878921Z   58:15  error  'isInitialized' is assigned a value but never used  @typescript-eslint/no-unused-vars
2026-01-22T19:44:47.6879860Z   63:15  error  'isInitialized' is assigned a value but never used  @typescript-eslint/no-unused-vars
2026-01-22T19:44:47.6881912Z   68:15  error  'isInitialized' is assigned a value but never used  @typescript-eslint/no-unused-vars
2026-01-22T19:44:47.6882847Z   73:15  error  'isInitialized' is assigned a value but never used  @typescript-eslint/no-unused-vars
2026-01-22T19:44:47.6883795Z   78:15  error  'isInitialized' is assigned a value but never used  @typescript-eslint/no-unused-vars
2026-01-22T19:44:47.6884213Z
2026-01-22T19:44:47.6884946Z O:\Projets\vsts-agent-win-x64-4.266.2\_work\1\s\src\Presentation\Web\src\services\queryClient.ts
2026-01-22T19:44:47.6885858Z   25:36  warning  Unexpected any. Specify a different type  @typescript-eslint/no-explicit-any
2026-01-22T19:44:47.6886801Z   56:24  warning  Unexpected any. Specify a different type  @typescript-eslint/no-explicit-any
2026-01-22T19:44:47.6887179Z
2026-01-22T19:44:47.6888385Z O:\Projets\vsts-agent-win-x64-4.266.2\_work\1\s\src\Presentation\Web\src\services\signalRService.ts
2026-01-22T19:44:47.6889318Z    12:50  warning  Unexpected any. Specify a different type  @typescript-eslint/no-explicit-any
2026-01-22T19:44:47.6890275Z    15:43  warning  Unexpected any. Specify a different type  @typescript-eslint/no-explicit-any
2026-01-22T19:44:47.6891179Z    18:45  warning  Unexpected any. Specify a different type  @typescript-eslint/no-explicit-any
2026-01-22T19:44:47.6892092Z    21:42  warning  Unexpected any. Specify a different type  @typescript-eslint/no-explicit-any
2026-01-22T19:44:47.6893153Z    22:48  warning  Unexpected any. Specify a different type  @typescript-eslint/no-explicit-any
2026-01-22T19:44:47.6894069Z    26:7   warning  Unexpected console statement              no-console
2026-01-22T19:44:47.6894878Z    58:9   warning  Unexpected console statement              no-console
2026-01-22T19:44:47.6895660Z    64:9   warning  Unexpected console statement              no-console
2026-01-22T19:44:47.6896450Z    69:9   warning  Unexpected console statement              no-console
2026-01-22T19:44:47.6897612Z    74:9   warning  Unexpected console statement              no-console
2026-01-22T19:44:47.6898701Z    79:9   warning  Unexpected console statement              no-console
2026-01-22T19:44:47.6899935Z    85:9   warning  Unexpected console statement              no-console
2026-01-22T19:44:47.6900828Z    90:9   warning  Unexpected console statement              no-console
2026-01-22T19:44:47.6901777Z   102:7   warning  Unexpected console statement              no-console
2026-01-22T19:44:47.6902592Z   123:7   warning  Unexpected console statement              no-console
2026-01-22T19:44:47.6903499Z   130:43  warning  Unexpected any. Specify a different type  @typescript-eslint/no-explicit-any
2026-01-22T19:44:47.6904584Z   145:36  warning  Unexpected any. Specify a different type  @typescript-eslint/no-explicit-any
2026-01-22T19:44:47.6905641Z   166:38  warning  Unexpected any. Specify a different type  @typescript-eslint/no-explicit-any
2026-01-22T19:44:47.6906820Z   181:35  warning  Unexpected any. Specify a different type  @typescript-eslint/no-explicit-any
2026-01-22T19:44:47.6908095Z   188:41  warning  Unexpected any. Specify a different type  @typescript-eslint/no-explicit-any
2026-01-22T19:44:47.6908574Z
2026-01-22T19:44:47.6909285Z O:\Projets\vsts-agent-win-x64-4.266.2\_work\1\s\src\Presentation\Web\src\test\infrastructure\persistence\ProductAPIRepository.test.ts
2026-01-22T19:44:47.6910178Z   37:28  warning  Unexpected any. Specify a different type  @typescript-eslint/no-explicit-any
2026-01-22T19:44:47.6910545Z   50:28  warning  Unexpected any. Specify a different type  @typescript-eslint/no-explicit-any
2026-01-22T19:44:47.6911568Z   64:29  warning  Unexpected any. Specify a different type  @typescript-eslint/no-explicit-any
2026-01-22T19:44:47.6911947Z   67:62  warning  Unexpected any. Specify a different type  @typescript-eslint/no-explicit-any
2026-01-22T19:44:47.6912111Z
2026-01-22T19:44:47.6912434Z O:\Projets\vsts-agent-win-x64-4.266.2\_work\1\s\src\Presentation\Web\src\test\presentation\features\product\hooks\useProducts.test.tsx
2026-01-22T19:44:47.6913779Z   24:12  error    Component definition is missing display name  react/display-name
2026-01-22T19:44:47.6914164Z   35:38  warning  Unexpected any. Specify a different type      @typescript-eslint/no-explicit-any
2026-01-22T19:44:47.6914761Z   56:38  warning  Unexpected any. Specify a different type      @typescript-eslint/no-explicit-any
2026-01-22T19:44:47.6914924Z
2026-01-22T19:44:47.6915211Z O:\Projets\vsts-agent-win-x64-4.266.2\_work\1\s\src\Presentation\Web\src\test\setup.ts
2026-01-22T19:44:47.6915670Z   34:6  warning  Unexpected any. Specify a different type  @typescript-eslint/no-explicit-any
2026-01-22T19:44:47.6915836Z
2026-01-22T19:44:47.6916151Z O:\Projets\vsts-agent-win-x64-4.266.2\_work\1\s\src\Presentation\Web\src\types\global.d.ts
2026-01-22T19:44:47.6916550Z    5:13  error  An empty interface declaration allows any non-nullish value, including literals like `0` and `""`.
2026-01-22T19:44:47.6917959Z - If that's what you want, disable this lint rule with an inline comment or configure the 'allowInterfaces' rule option.
2026-01-22T19:44:47.6918590Z - If you want a type meaning "any object", you probably want `object` instead.
2026-01-22T19:44:47.6919167Z - If you want a type meaning "any value", you probably want `unknown` instead  @typescript-eslint/no-empty-object-type
2026-01-22T19:44:47.6919839Z   17:11  error  'ImportMeta' is defined but never used                                                                                                                                                                                                                                                                                                                                                    @typescript-eslint/no-unused-vars
2026-01-22T19:44:47.6920146Z
2026-01-22T19:44:47.7194291Z ✖ 103 problems (66 errors, 37 warnings)
2026-01-22T19:44:47.7195233Z   0 errors and 1 warning potentially fixable with the `--fix` option.
2026-01-22T19:44:47.7195499Z
2026-01-22T19:44:47.9086071Z ##[error]Cmd.exe exited with code '1'.
2026-01-22T19:44:47.9709287Z ##[section]Finishing: Run ESLint
