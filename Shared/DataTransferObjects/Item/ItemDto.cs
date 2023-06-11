namespace Shared.DataTransferObjects.Item;

public record ItemDto(int Id, string Name, 
    int MaxDurability, int Price, 
    float Weight, string SecretItemDescription, 
    string ItemDescription, int ItemCategoryId);
