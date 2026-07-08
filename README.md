<p align="center">
  <img src="https://raw.githubusercontent.com/UmbrellaFrameHQ/EnumMeta/master/assets/umbrellaframe-enummeta-icon.png" width="180" alt="UmbrellaFrame.EnumMeta logo" />
</p>

<h1 align="center">UmbrellaFrame.EnumMeta</h1>

<p align="center">
  <a href="https://www.nuget.org/packages/UmbrellaFrame.EnumMeta"><img src="https://img.shields.io/badge/NuGet-ready-004880?style=flat-square" alt="NuGet ready" /></a>
  <a href="https://github.com/UmbrellaFrameHQ/EnumMeta/actions/workflows/ci.yml"><img src="https://github.com/UmbrellaFrameHQ/EnumMeta/actions/workflows/ci.yml/badge.svg" alt="CI" /></a>
  <a href="LICENSE.txt"><img src="https://img.shields.io/badge/License-MIT-blue.svg?style=flat-square" alt="MIT license" /></a>
  <a href="https://learn.microsoft.com/dotnet/standard/net-standard"><img src="https://img.shields.io/badge/.NET%20Standard-2.0-purple?style=flat-square" alt=".NET Standard 2.0" /></a>
</p>

**Language / Dil:** [English](#english) - [Turkce](#turkce)

---

## English

**Enum metadata layer for centralized status, message, and info management in .NET**

Zero database dependency - cached reflection - safe `Try...` APIs - typed metadata models.

UmbrellaFrame.EnumMeta lets you decorate enum members with status and info attributes, then resolve messages, severity values, names, and descriptions from one centralized place. It is designed for API responses, stored procedure status codes, workflow outcomes, validation states, and enum-based domain results.

```text
UmbrellaFrame.EnumMeta
-> StatusAttribute, InfoAttribute, StatusType, InfoType
-> GetEnumStatus / TryGetEnumStatus
-> GetStatusMetadata / TryGetStatusMetadata
-> GetEnumInfo / GetEnumInfoOrDefault
-> GetInfoMetadata / TryGetInfoMetadata
```

### Design Philosophy

The package intentionally stays small. It does not try to become a result framework, ORM, validation framework, or localization system.

Its job is to keep enum-owned metadata close to the enum member that owns it. Boundary values can be unsafe, so every required lookup has a safe `Try...` alternative. Reflection is cached internally after the first lookup, keeping the public API simple without repeatedly scanning attributes.

### Installation

```bash
dotnet add package UmbrellaFrame.EnumMeta
```

### Quick Start

```csharp
using UmbrellaFrame.EnumMeta.Core;

public enum UserRegistrationStatus
{
    [Status("User created successfully.", StatusType.Success)]
    Created = 0,

    [Status("Email address is already in use.", StatusType.Warning)]
    DuplicateEmail = 1,

    [Status("User information could not be verified.", StatusType.Error)]
    VerificationFailed = 2
}

var status = UserRegistrationStatus.VerificationFailed;
var metadata = status.GetStatusMetadata();

Console.WriteLine(metadata.Message);
Console.WriteLine(metadata.IsError);
```

Boundary-safe usage:

```csharp
var status = (UserRegistrationStatus)statusCodeFromDatabase;

if (!status.TryGetStatusMetadata(out var metadata))
{
    return Results.Problem("Unknown status code returned by the database.");
}

return metadata.IsError
    ? Results.BadRequest(metadata.Message)
    : Results.Ok(metadata.Message);
```

### Info Metadata

```csharp
public enum AccountState
{
    [Info("Active", "The account can sign in and use the system.")]
    Active,

    [Info("Suspended", "The account is blocked until manual review.")]
    Suspended
}

var info = AccountState.Active.GetInfoMetadata();

Console.WriteLine(info.Name);
Console.WriteLine(info.Description);
```

Fallback reads:

```csharp
var description = AccountState.Active.GetEnumInfoOrDefault(
    InfoType.Description,
    "No description available.");
```

### Custom Message Sources

EnumMeta does not own your localization or message source. Resolve messages from any source you prefer: `.resx`, database, JSON, cache, or a remote localization service.

```csharp
var message = UserRegistrationStatus.VerificationFailed.GetLocalizedMessage(
    (status, culture) => myMessageProvider.GetMessage(status.ToString(), culture),
    "tr",
    "Mesaj bulunamadi.");
```

### Public API

| API | Description |
|---|---|
| `[Status("message", StatusType)]` | Adds status message and severity metadata |
| `[Info("name", "description")]` | Adds ordered display metadata |
| `GetEnumStatus()` | Reads required `StatusAttribute` |
| `TryGetEnumStatus(out StatusAttribute)` | Safe status attribute lookup |
| `GetStatusMetadata()` | Returns typed `StatusMetadata` |
| `TryGetStatusMetadata(out StatusMetadata)` | Safe typed status lookup |
| `GetEnumInfos()` | Reads required info entries |
| `TryGetEnumInfos(out string[])` | Safe info array lookup |
| `GetEnumInfo(InfoType)` | Reads one required info entry |
| `GetEnumInfoOrDefault(InfoType, string)` | Reads one info entry or fallback |
| `GetInfoMetadata()` | Returns typed `InfoMetadata` |
| `TryGetInfoMetadata(out InfoMetadata)` | Safe typed info lookup |
| `GetResolvedMessage(Func<Enum, string>, string)` | Resolves a message from a caller-owned source |
| `GetLocalizedMessage(Func<Enum, CultureInfo, string>, string, string)` | Resolves a localized message from a caller-owned source |
| `GetLocalizedMessage(ResourceManager, string)` | Reads resource text from a caller-provided resource manager |

### Runnable Example

```bash
dotnet run --project samples/UmbrellaFrame.EnumMeta.StatusMappingExample/UmbrellaFrame.EnumMeta.StatusMappingExample.csproj
```

Expected output:

```text
Error: User information could not be verified.
```

### Why This Package?

| Feature | EnumMeta | Switch statements | Full result framework |
|---|:---:|:---:|:---:|
| Enum-owned messages | Yes | Manual | Partial |
| Safe unknown-code handling | Yes | Manual | Yes |
| Cached attribute lookup | Yes | No | Depends |
| Zero database dependency | Yes | Yes | Yes |
| Small API surface | Yes | Yes | No |
| Typed metadata models | Yes | No | Depends |
| Works with numeric boundary codes | Yes | Manual | Yes |

### Development

```bash
dotnet restore
dotnet build
dotnet test
```

Create a release package:

```bash
dotnet build UmbrellaFrame.EnumMeta.Core/UmbrellaFrame.EnumMeta.Core.csproj -c Release
dotnet pack UmbrellaFrame.EnumMeta.Core/UmbrellaFrame.EnumMeta.Core.csproj -c Release --no-build
```

### License

MIT (c) UmbrellaFrame

---

## Turkce

**.NET icin merkezi enum durum, mesaj ve bilgi metadata katmani**

Veritabani bagimliligi yok - cache'li reflection - guvenli `Try...` API'leri - tiplenmis metadata modelleri.

UmbrellaFrame.EnumMeta, enum uyelerine status ve info attribute'lari ekleyerek mesaj, durum tipi, isim ve aciklama bilgilerini tek merkezden okumanizi saglar. API cevaplari, stored procedure durum kodlari, workflow sonuclari, validasyon durumlari ve enum tabanli domain sonuc akislari icin tasarlanmistir.

### Tasarim Felsefesi

Bu paket bilerek kucuk tutulur. Bir result framework, ORM, validasyon framework'u veya tam kapsamli localization sistemi olmaya calismaz.

Amaci, enum'a ait metadata'yi dogrudan enum uyesinin yaninda tutmaktir. Dis sistemlerden gelen durum kodlari guvensiz olabilecegi icin gerekli okuma metotlarinin guvenli `Try...` alternatifleri vardir. Reflection ilk okumadan sonra cache'lenir.

### Kurulum

```bash
dotnet add package UmbrellaFrame.EnumMeta
```

### Hizli Baslangic

```csharp
using UmbrellaFrame.EnumMeta.Core;

public enum SiparisDurumu
{
    [Status("Siparis basariyla olusturuldu.", StatusType.Success)]
    Olusturuldu = 0,

    [Status("Siparis onay bekliyor.", StatusType.Warning)]
    OnayBekliyor = 1,

    [Status("Siparis olusturulamadi.", StatusType.Error)]
    Olusturulamadi = 2
}

var durum = SiparisDurumu.Olusturulamadi;
var metadata = durum.GetStatusMetadata();

Console.WriteLine(metadata.Message);
Console.WriteLine(metadata.IsError);
```

Harici sistemlerden gelen kodlarda guvenli kullanim:

```csharp
var durum = (SiparisDurumu)veritabanindanGelenDurumKodu;

if (!durum.TryGetStatusMetadata(out var metadata))
{
    return Results.Problem("Bilinmeyen durum kodu dondu.");
}

return metadata.IsError
    ? Results.BadRequest(metadata.Message)
    : Results.Ok(metadata.Message);
```

### Info Metadata

```csharp
public enum HesapDurumu
{
    [Info("Aktif", "Hesap sisteme giris yapabilir.")]
    Aktif,

    [Info("Askida", "Hesap manuel inceleme bekliyor.")]
    Askida
}

var info = HesapDurumu.Aktif.GetInfoMetadata();

Console.WriteLine(info.Name);
Console.WriteLine(info.Description);
```

### Ozel Mesaj Kaynaklari

EnumMeta localization veya mesaj kaynagini sahiplenmez. Mesajlari istediginiz kaynaktan cozebilirsiniz: `.resx`, veritabani, JSON, cache veya uzak localization servisi.

```csharp
var mesaj = SiparisDurumu.Olusturulamadi.GetLocalizedMessage(
    (durum, culture) => mesajSaglayici.GetMessage(durum.ToString(), culture),
    "tr",
    "Mesaj bulunamadi.");
```

### Genel API

| API | Aciklama |
|---|---|
| `[Status("mesaj", StatusType)]` | Durum mesaji ve durum tipi ekler |
| `[Info("isim", "aciklama")]` | Sirali gorunum metadata'si ekler |
| `GetEnumStatus()` | Zorunlu status attribute'unu okur |
| `TryGetEnumStatus(out StatusAttribute)` | Guvenli status attribute okuma |
| `GetStatusMetadata()` | Tiplenmis `StatusMetadata` dondurur |
| `TryGetStatusMetadata(out StatusMetadata)` | Guvenli tiplenmis status okuma |
| `GetEnumInfo(InfoType)` | Tek bir info alanini okur |
| `GetEnumInfoOrDefault(InfoType, string)` | Info alanini veya fallback degeri dondurur |
| `GetInfoMetadata()` | Tiplenmis `InfoMetadata` dondurur |
| `TryGetInfoMetadata(out InfoMetadata)` | Guvenli tiplenmis info okuma |
| `GetResolvedMessage(Func<Enum, string>, string)` | Kullaniciya ait bir kaynaktan mesaj cozer |
| `GetLocalizedMessage(Func<Enum, CultureInfo, string>, string, string)` | Kullaniciya ait bir kaynaktan lokalize mesaj cozer |

### Neden Bu Paket?

| Ozellik | EnumMeta | Switch bloklari | Buyuk result framework |
|---|:---:|:---:|:---:|
| Enum uzerinde merkezi mesaj | Evet | Manuel | Kismen |
| Bilinmeyen kodlarda guvenli okuma | Evet | Manuel | Evet |
| Cache'li attribute okuma | Evet | Hayir | Degisir |
| Veritabani bagimliligi yok | Evet | Evet | Evet |
| Kucuk API yuzeyi | Evet | Evet | Hayir |
| Tiplenmis metadata modeli | Evet | Hayir | Degisir |

### Lisans

MIT (c) UmbrellaFrame
