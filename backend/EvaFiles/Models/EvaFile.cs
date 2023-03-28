using System.Text.Json.Serialization;

namespace EvaFiles.Models;

public class EvaFile
{
    public required string Id { get; set; } // Unique Id. Can be changed by the user, usually a Guid.
    [JsonIgnore]
    public string SeaVolume { get; set; } // SeaweedFS volume.
    [JsonIgnore]
    public string SeaHandle { get; set; } // SeaweedFS handle. 
    
    public required string Name { get; set; } // FileName, can be changed by user.
    public required string OriginalName { get; set; } // Original FileName.
    public required long Size { get; set; } // Size in bytes.

    public required long Duration { get; set; }
    
    public required long ExpireDate { get; set; }
    
    public long DownloadCount { get; set; }
    
    public long GetSizeInMb()
    {
        if (Size == 0) return Size;
        return Size / 1024 / 1024;
    }
}