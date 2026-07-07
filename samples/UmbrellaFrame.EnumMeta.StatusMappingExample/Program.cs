using UmbrellaFrame.EnumMeta.Core;

var statusCodeFromDatabase = 2;
var status = (UserRegistrationStatus)statusCodeFromDatabase;

if (!status.TryGetStatusMetadata(out var metadata))
{
    Console.WriteLine("Unknown status code.");
    return;
}

Console.WriteLine($"{metadata.Type}: {metadata.Message}");

public enum UserRegistrationStatus
{
    [Status("User created successfully.", StatusType.Success)]
    Created = 0,

    [Status("Email address is already in use.", StatusType.Warning)]
    DuplicateEmail = 1,

    [Status("User information could not be verified.", StatusType.Error)]
    VerificationFailed = 2
}
