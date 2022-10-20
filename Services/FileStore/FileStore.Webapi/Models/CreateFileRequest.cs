namespace FileStore.Webapi.Models
{
    public record CreateFileRequest
    {
        public string FileName { get; set; }

        public byte[] Data{ get; set; }
    }
}
