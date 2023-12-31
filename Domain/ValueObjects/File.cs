﻿using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Domain.ValueObjects;
public class UploadFile : ValueObject
{
    public string ResourceName { get; set; } = null!;
    public string ResourceKey { get; set; } = null!;
    public bool IsPrivate { get; set; }
    public UploadFile() { }
    public UploadFile(string resourceName, string resourceKey, bool isPrivate = true)
    {
        ResourceName = resourceName;
        ResourceKey = resourceKey;
        IsPrivate = isPrivate;
    }

    public void UpdateURL(string url)
    {
        ResourceKey = url;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return ResourceKey;
        yield return ResourceName;
    }
}
