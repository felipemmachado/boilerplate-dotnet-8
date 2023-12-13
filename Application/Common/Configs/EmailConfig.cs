using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Configs;
public class EmailConfig
{
    public string ApiKey { get; set; }
    public string ForgotPasswordId { get; set; }
    public string FirstAccessId { get; set; }
    public string UrlFront { get; set; }
    public string FromName { get; set; }
    public string FromEmail { get; set; }
}
