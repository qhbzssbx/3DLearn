using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct HotUpdateProgressChangeEvent
{
    public string PackageName;

    public float Progress;

    public int TotalDownloadCount;

    public int CurrentDownloadCount;

    public long TotalDownloadBytes;

    public long CurrentDownloadBytes;
}
