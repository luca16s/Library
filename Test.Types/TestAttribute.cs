namespace Test.Types;

using System.Runtime.CompilerServices;

using Xunit;

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
