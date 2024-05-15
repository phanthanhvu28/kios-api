namespace ApplicationCore.Attributes;
[AttributeUsage(AttributeTargets.Property)]
internal class ParentColumnAttribute : Attribute
{
    public readonly string FieldName;

    public ParentColumnAttribute(string fieldName)
    {
        FieldName = fieldName;
    }
}
