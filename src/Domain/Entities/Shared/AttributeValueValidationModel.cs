namespace Domain.Entities.Shared;

public class AttributeValueValidationModel
{
    public StringDataValidationModel? StringData { get; set; }

    public class StringDataValidationModel
    {
        public int? MinLength { get; set; }
        public int? MaxLength { get; set; }
    }
}