using Lya.Objects;

namespace LyaTests.Objects;

public class VariableTests
{
    [Test]
    public void SetValueTest()
    {
        var v1 = new Variable("test", VariableType.String, "testing");
        Assert.Multiple(() =>
        {
            Assert.That(v1.Value, Is.EqualTo("testing"));
            Assert.That(v1.SetValue("test"), Is.EqualTo(true));
            Assert.That(v1.Value, Is.EqualTo("test"));
            Assert.That(v1.SetValue(1), Is.EqualTo(false));
            Assert.That(v1.Value, Is.EqualTo("test"));
        });
    }

    [Test]
    public void IsTypeValidTest()
    {
        var v1 = new Variable("test", VariableType.String, "test");
        Assert.That(v1.IsTypeValid(), Is.True);
        v1.Type = VariableType.Bool;
        Assert.That(v1.IsTypeValid(), Is.False);
        v1.Type = VariableType.Float;
        Assert.That(v1.IsTypeValid(), Is.False);
        v1.Type = VariableType.Integer;
        Assert.That(v1.IsTypeValid(), Is.False);
        v1.Type = VariableType.Null;
        Assert.That(v1.IsTypeValid(), Is.False);
    }
}
