using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Models.FileService;


[ExcludeFromCodeCoverage]
public record FileObject(string FileURL, FileObjectInfo Information);