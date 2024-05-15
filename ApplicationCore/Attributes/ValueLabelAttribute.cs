namespace ApplicationCore.Attributes;
[AttributeUsage(AttributeTargets.Property)]
public class ValueLabelAttribute : Attribute
{
    public readonly string FieldName;

    public ValueLabelAttribute(string fieldName)
    {
        FieldName = fieldName;
    }
}
