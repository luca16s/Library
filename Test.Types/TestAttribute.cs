namespace Test.Types;

using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

using Xunit;

[ExcludeFromCodeCoverage]
public class TestAttribute : FactAttribute
{
    public TestAttribute(
        string charsToReplace = "_",
        string replacementChars = " ",
        [CallerMemberName] string testMethodName = ""
    )
    {
        if (charsToReplace != null)
            base.DisplayName = testMethodName?.Replace(charsToReplace, replacementChars);
    }
}
