using System.ComponentModel.DataAnnotations;

namespace Rk.FileStore.Domain
{
    public class FileData: BaseEntity
    {
        public FileData(string sha256Key, byte[] data)
        {
            Sha256Key = sha256Key;
            Data = data;
        }
        [Required]
        [StringLength(256)]
        public string  Sha256Key { get; set; }

        public byte[] Data { get; set; }
    }
}
