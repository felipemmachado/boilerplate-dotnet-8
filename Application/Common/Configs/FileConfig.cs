using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Configs;
public class FileConfig
{
    public string AccessKeyId { get; set; } = null!;
    public string BucketName { get; set; } = null!;
    public string BucketUrl { get; set; } = null!;
    public string ServiceURL { get; set; } = null!;
    public string SecretKey { get; set; } = null!;
}
