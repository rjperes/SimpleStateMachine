# GitHub Actions Workflows

## Publish NuGet Package

This workflow automates the process of building and publishing the SimpleStateMachine NuGet package.

### Trigger Methods

#### 1. Automatic (Tag-based)
The workflow automatically runs when you push a version tag:
```bash
git tag v1.0.0
git push origin v1.0.0
```

Tags must follow the pattern `v*.*.*` (e.g., v1.0.0, v2.1.3)

#### 2. Manual (Workflow Dispatch)
You can also trigger the workflow manually from the GitHub Actions UI:
1. Navigate to Actions tab
2. Select "Publish NuGet Package"
3. Click "Run workflow"
4. Enter the version number
5. Click "Run workflow" button

### Workflow Steps

1. **Checkout code** - Retrieves the repository code
2. **Setup .NET** - Installs .NET 9.0 SDK
3. **Restore dependencies** - Restores NuGet packages
4. **Build** - Builds the solution in Release configuration
5. **Run tests** - Runs unit tests if they exist
6. **Extract version** - Extracts version from tag or manual input
7. **Pack** - Creates the NuGet package with specified version
8. **Publish** - Publishes to NuGet.org using API key
9. **Upload artifacts** - Uploads the .nupkg file as workflow artifact

### Required Secrets

- `NUGET_API_KEY` - Your NuGet.org API key
  - Get it from https://www.nuget.org/account/apikeys
  - Add it in repository Settings → Secrets and variables → Actions

### Security

- The workflow uses explicit permissions (`contents: read`) to limit GITHUB_TOKEN scope
- API key is stored securely as a GitHub secret
- The `--skip-duplicate` flag prevents accidental republishing of existing versions
