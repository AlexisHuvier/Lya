using Lya.Utils;

namespace LyaTests.Utils;

public class VariableTypeTests
{
    [Test]
    public void Tests()
    {
        Assert.Multiple(() =>
        {
            Assert.That(VariableType.GetVariableTypeFromKeyword("int"), Is.EqualTo(VariableType.Integer));
            Assert.That(VariableType.GetVariableTypeFromKeyword("bool"), Is.EqualTo(VariableType.Bool));
            Assert.That(VariableType.GetVariableTypeFromKeyword("float"), Is.EqualTo(VariableType.Float));
            Assert.That(VariableType.GetVariableTypeFromKeyword("string"), Is.EqualTo(VariableType.String));
            Assert.That(VariableType.GetVariableTypeFromKeyword("bla"), Is.EqualTo(VariableType.Null));
            Assert.That(VariableType.String == VariableType.Bool, Is.False);
            Assert.That(VariableType.Float.Equals(VariableType.Bool), Is.False);
            Assert.That(VariableType.Float.Equals(null), Is.False);
            Assert.That(VariableType.Float?.Equals(VariableType.Float), Is.True);
            Assert.That(VariableType.Float != VariableType.Bool, Is.True);
            Assert.That(VariableType.Float?.GetHashCode(), Is.EqualTo("float".GetHashCode()));
            Assert.That(VariableType.Float?.ToString(), Is.EqualTo("float"));
        });
    }
}
