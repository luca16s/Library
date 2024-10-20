namespace Test.Types;

using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

using Xunit.Sdk;

[ExcludeFromCodeCoverage]
[XunitTestCaseDiscoverer(
    "Xunit.Sdk.TheoryDiscoverer",
    "xunit.execution.{Platform}"
)]
public class CaseAttribute(
    string charsToReplace = "_",
    string replacementChars = " ",
    [CallerMemberName] string testMethodName = ""
) : TestAttribute(
    charsToReplace,
    replacementChars,
    testMethodName
)
{ }
