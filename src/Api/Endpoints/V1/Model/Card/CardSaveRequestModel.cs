using Domain.Entities.Shared;

namespace Api.Endpoints.V1.Model.Card;

public class CardSaveRequestModel
{
    public string CategoryId { get; set; } = default!;
    public List<AttributeItem> Items { get; set; } = new();


    public class AttributeItem
    {
        public string AttributeId { get; set; } = default!;

        public List<AttributeValueModel> Values { get; set; } = new();
    }
}