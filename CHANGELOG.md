# Changelog

All notable changes to this project are documented here.

## 1.4.0

- Renamed the public package identity to `UmbrellaFrame.EnumMeta`.
- Added UmbrellaFrame-branded assembly and root namespace metadata.
- Renamed project files and folders to use the `UmbrellaFrame.` prefix consistently.
- Added a package icon and repository logo asset.
- Updated documentation and samples to use the `UmbrellaFrame.EnumMeta.Core` namespace.

## 1.3.0

- Added cached enum metadata lookup to avoid repeated reflection work.
- Added `StatusMetadata` and `InfoMetadata` convenience models.
- Added `GetStatusMetadata`, `TryGetStatusMetadata`, `GetInfoMetadata`, `TryGetInfoMetadata`, and `GetEnumInfoOrDefault`.
- Added XML documentation generation for NuGet consumers.
- Expanded test coverage for missing metadata, undefined enum values, null lookups, fallback info, and typed metadata models.
- Removed database dependency from the core package and test suite.
- Added release-oriented package metadata.

## 1.2.0

- Added safe `TryGetEnumStatus` and `TryGetEnumInfos` APIs.
- Improved exceptions for missing enum metadata.
- Added `InfoAttribute` and `InfoType` support.

## 1.0.0

- Initial enum status metadata support.
