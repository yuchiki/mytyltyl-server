namespace Yuchiki.MytyltylApi
{
    public class VersionResponse
    {
        public required DateTime StartedAt { get; init; }
        public required int MajorVersion { get; init; }
        public required int MinorVersion { get; init; }
        public required int PatchVersion { get; init; }

        public required string Suffix { get; init; }
        public string FullVerison => $"{MajorVersion}.{MinorVersion}.{PatchVersion}{((Suffix == "") ? "" : "-" + Suffix)}";
    }
}
