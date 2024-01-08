namespace Application.Common.Configs;
public class EmailConfig
{
    public string ApiKey { get; set; } = string.Empty;
    public string ForgotPasswordId { get; set; } = string.Empty;
    public string FirstAccessId { get; set; } = string.Empty;
    public string UrlFront { get; set; } = string.Empty;
    public string FromName { get; set; } = string.Empty;
    public string FromEmail { get; set; } = string.Empty;
}
