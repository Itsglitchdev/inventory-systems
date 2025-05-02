using UnityEngine;

[System.Serializable]
public class ElementalShopItemData
{
    public int id;
    public ElementType shopElementType;
    public string shopItemName;
    public ItemType shopItemType;
    public string shopItemDescription;
    public string shopItemStats;
    public int shopItemPrice;
}

[System.Serializable]
public class ElementalInventoryItemData
{
    public int id;
    public ElementType inventoryElementType;
    public string inventoryItemName;
    public ItemType inventoryItemType;
    public string inventoryItemDescription;
    public bool isEquipped;
}