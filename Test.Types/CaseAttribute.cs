namespace Test.Types;
using System.Runtime.CompilerServices;

using Xunit.Sdk;

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
