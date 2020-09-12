public partial class RegOptions
{
    /// <summary>
    /// OBS Remote IP Address
    /// </summary>
    public const string RemoteIP = "OBS_Remote_IP";
    /// <summary>
    /// OBS Remote Password
    /// </summary>
    public const string RemotePasswd = "OBS_Remote_Password"; 
}

/// <summary>
/// Live Streaming Options
/// </summary>
public partial class Streaming
{
    public const string Start = "StartStreaming";
    public const string Stop = "StopStreaming";
}

/// <summary>
/// Recording Options
/// </summary>
public partial class Recording
{
    public const string Start = "StartRecording";
    public const string Stop = "StopRecording";
    public const string Paused = "PausedRecording";
    public const string Resumed = "ResumedRecording";
}

/// <summary>
/// Replay Options
/// </summary>
public partial class Replay
{
    public const string Save = "SaveReplay";
}