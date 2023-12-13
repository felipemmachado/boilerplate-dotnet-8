using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Models.FileService;

public record FileObjectInfo(string Key, long Size, DateTime LastModified);