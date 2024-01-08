using System.Diagnostics.CodeAnalysis;

namespace Application.Common.Models.FileService;


[ExcludeFromCodeCoverage]
public record FileObject(string FileURL, FileObjectInfo Information);