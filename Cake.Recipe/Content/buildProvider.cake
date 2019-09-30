public interface ITagInfo
{
    bool IsTag { get; }

    string Name { get; }
}

public interface IRepositoryInfo
{
    string Branch { get; }

    string Name { get; }

    ITagInfo Tag { get; }
}

public interface IPullRequestInfo
{
    bool IsPullRequest { get; }
}

public interface IBuildInfo
{
    string Number { get; }
}

public interface IBuildProvider
{
    IRepositoryInfo Repository { get; }

    IPullRequestInfo PullRequest { get; }

    IBuildInfo Build { get; }

    void UploadArtifact(FilePath file);
}

public static IBuildProvider GetBuildProvider(ICakeContext context, BuildSystem buildSystem)
{
    if (IsRunningOnAzurePipelines || IsRunningOnAzurePipelinesHosted)
    {
        return new AzurePipelinesBuildProvider(buildSystem.TFBuild, context.Environment);
    }

    // always fallback to AppVeyor
    return new AppVeyorBuildProvider(buildSystem.AppVeyor);
}
