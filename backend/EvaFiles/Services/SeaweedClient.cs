namespace EvaFiles.Services;

public class SeaweedClient
{
    public class AssignResult
    {
        public int Count { get; set; }
        public string Fid { get; set; }
        public string Url { get; set; }
        public string PublicUrl { get; set; }
    }

    public class VolumeUploadResult
    {
        public string Name { get; set; }
        public ulong Size { get; set; }
        public string eTag { get; set; }
    }

    public class UploadResult
    {
        public string Id { get; set; }
        public string VolumeUrl { get; set; }
        public string PublicUrl { get; set; }
    }
    
    public const string ClientName = "seaweed_client";
    
    private readonly HttpClient _masterClient;
    private readonly HttpClient _volumeClient;
    
    public SeaweedClient(IHttpClientFactory factory)
    {
        _masterClient = factory.CreateClient(ClientName);
        _volumeClient = factory.CreateClient();
    }

    public async Task<UploadResult> UploadAsync(Stream data)
    {
        var assign = await AssignAsync();

        if (assign is null)
        {
            throw new Exception("Something happened while assigning new world to SeaweedFS");
        }

        var upload = await VolumeUploadAsync(assign, data);

        return new UploadResult()
        {
            Id = assign.Fid,
            VolumeUrl = assign.PublicUrl,
            PublicUrl = new Uri(new Uri("http://" + assign.PublicUrl), assign.Fid).ToString()
        };
    }

    public Task DeleteAsync(string url)
    {
        return _volumeClient.DeleteAsync(url);
    }

    private Task<AssignResult?> AssignAsync()
    {
        return _masterClient.GetFromJsonAsync<AssignResult>("/dir/assign");
    }

    private async Task<VolumeUploadResult?> VolumeUploadAsync(AssignResult assign, Stream data)
    {
        using var content = new MultipartFormDataContent();
        content.Add(new StreamContent(data));
        var response = await _volumeClient.PostAsync(new Uri(new Uri("http://" + assign.PublicUrl), assign.Fid), content);
        return await response.Content.ReadFromJsonAsync<VolumeUploadResult>();
    }
}
