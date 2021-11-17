///////////////////////////////////////////////////////////////////////////////
// ARGUMENTS
///////////////////////////////////////////////////////////////////////////////

var target = Argument("target", "Default");
var configuration = Argument("configuration", "Release");
var solutionFolder = "./";
DirectoryPath outputFolder = MakeAbsolute(Directory("./output"));

///////////////////////////////////////////////////////////////////////////////
// SETUP / TEARDOWN
///////////////////////////////////////////////////////////////////////////////

Setup(ctx =>
{
	// Executed BEFORE the first task.
	Information("Running tasks...");
});

Teardown(ctx =>
{
    // Executed AFTER the last task.
    Information("Finished running tasks.");
});

///////////////////////////////////////////////////////////////////////////////
// TASKS
///////////////////////////////////////////////////////////////////////////////

Task("Clean")
    .Does(() =>
    {
        CleanDirectory(outputFolder);
    });

Task("Restore")
    .Does(() =>
    {
        DotNetRestore(solutionFolder);
    });

Task("Build")
    .IsDependentOn("Clean")
    .IsDependentOn("Restore")
    .Does(() =>
    {
        DotNetBuild(solutionFolder, new DotNetBuildSettings
        {
            Configuration = configuration,
            NoRestore = true
        });
    });

Task("Test")
    .IsDependentOn("Build")
    .Does(() =>
    {
        DotNetTest(solutionFolder, new DotNetTestSettings
        {
            Configuration = configuration,
            NoRestore = true,
            NoBuild = true,
        });
    });

Task("Publish")
    .IsDependentOn("Test")
    .Does(() =>
    {
        DotNetPublish($"{solutionFolder}/Gateways.WebApi/Gateways.WebApi.csproj", new DotNetPublishSettings
        {
            Configuration = configuration,
            OutputDirectory	= outputFolder,
            NoRestore = true,
            NoBuild = true,
        });
    });

Task("Default")
    .IsDependentOn("Publish");

RunTarget(target);