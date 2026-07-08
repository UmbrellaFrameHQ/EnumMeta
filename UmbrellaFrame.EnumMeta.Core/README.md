<p align="center">
  <img src="https://raw.githubusercontent.com/UmbrellaFrameHQ/EnumMeta/master/assets/umbrellaframe-enummeta-icon.png" width="180" alt="UmbrellaFrame.EnumMeta logo" />
</p>

<h1 align="center">UmbrellaFrame.EnumMeta</h1>

<p align="center">
  <a href="https://www.nuget.org/packages/UmbrellaFrame.EnumMeta"><img src="https://img.shields.io/badge/NuGet-ready-004880?style=flat-square" alt="NuGet ready" /></a>
  <a href="LICENSE"><img src="https://img.shields.io/badge/License-MIT-blue.svg?style=flat-square" alt="MIT license" /></a>
  <a href="https://learn.microsoft.com/dotnet/standard/net-standard"><img src="https://img.shields.io/badge/.NET%20Standard-2.0-purple?style=flat-square" alt=".NET Standard 2.0" /></a>
</p>

**Language / Dil:** [English](#english) - [Turkce](#turkce)

---

## English

**Enum metadata layer for centralized status, message, and info management in .NET**

Zero database dependency - cached reflection - safe `Try...` APIs - typed metadata models.

### Installation

```bash
dotnet add package UmbrellaFrame.EnumMeta
```

### Quick Start

```csharp
using UmbrellaFrame.EnumMeta.Core;

public enum OperationStatus
{
    [Status("Operation completed.", StatusType.Success)]
    Completed = 0,

    [Status("Operation requires attention.", StatusType.Warning)]
    RequiresAttention = 1,

    [Status("Operation failed.", StatusType.Error)]
    Failed = 2
}

var metadata = OperationStatus.Failed.GetStatusMetadata();

Console.WriteLine(metadata.Message);
Console.WriteLine(metadata.IsError);
```

Safe boundary lookup:

```csharp
var status = (OperationStatus)externalStatusCode;

if (status.TryGetStatusMetadata(out var metadata))
{
    Console.WriteLine(metadata.Message);
}
```

### Custom Message Sources

EnumMeta does not own your localization source. Resolve messages from `.resx`, database, JSON, cache, or any service you prefer.

```csharp
var message = OperationStatus.Failed.GetLocalizedMessage(
    (status, culture) => myMessageProvider.GetMessage(status.ToString(), culture),
    "tr");
```

### Included APIs

| API | Description |
|---|---|
| `[Status]` | Status message and severity metadata |
| `[Info]` | Ordered display metadata |
| `GetStatusMetadata()` | Typed status metadata |
| `TryGetStatusMetadata(out StatusMetadata)` | Safe status metadata lookup |
| `GetInfoMetadata()` | Typed info metadata |
| `TryGetInfoMetadata(out InfoMetadata)` | Safe info metadata lookup |
| `GetEnumInfoOrDefault(InfoType, string)` | Info value with fallback |
| `GetResolvedMessage(Func<Enum, string>, string)` | Message from a caller-owned source |
| `GetLocalizedMessage(Func<Enum, CultureInfo, string>, string, string)` | Localized message from a caller-owned source |

---

## Turkce

**.NET icin merkezi enum durum, mesaj ve bilgi metadata katmani**

Veritabani bagimliligi yok - cache'li reflection - guvenli `Try...` API'leri - tiplenmis metadata modelleri.

### Kurulum

```bash
dotnet add package UmbrellaFrame.EnumMeta
```

### Hizli Baslangic

```csharp
using UmbrellaFrame.EnumMeta.Core;

public enum OdemeDurumu
{
    [Status("Odeme basariyla tamamlandi.", StatusType.Success)]
    Tamamlandi = 0,

    [Status("Odeme kontrol bekliyor.", StatusType.Warning)]
    KontrolBekliyor = 1,

    [Status("Odeme basarisiz oldu.", StatusType.Error)]
    Basarisiz = 2
}

var metadata = OdemeDurumu.Basarisiz.GetStatusMetadata();

Console.WriteLine(metadata.Message);
Console.WriteLine(metadata.IsError);
```

### Lisans

MIT (c) UmbrellaFrame
