using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI itemNameText;
    [SerializeField] private TextMeshProUGUI typeText;
    [SerializeField] private TextMeshProUGUI effectText;
    [SerializeField] private TextMeshProUGUI priceText;
    [SerializeField] private Button buyButton;

    private ElementalShopItemData itemData;

    public void SetupItem(ElementalShopItemData data)
    {
        itemData = data;
        
        if (itemNameText != null)
            itemNameText.text = data.shopItemName;
            
        if (typeText != null)
            typeText.text = data.shopItemType.ToString();
            
        if (effectText != null)
            effectText.text = data.shopItemDescription;
            
        if (priceText != null)
            priceText.text = data.shopItemPrice.ToString();
            
        if (buyButton != null)
        {
            buyButton.onClick.RemoveAllListeners();
            buyButton.onClick.AddListener(OnBuyButtonClicked);
        }
    }
    
    private void OnBuyButtonClicked()
    {
        // Here you would implement the purchase logic
        Debug.Log($"Buying item: {itemData.shopItemName} for {itemData.shopItemPrice} coins");
        
        // Example: Call the BuyItem method from the GameManager
        GameManager.Instance.BuyItem(itemData);
    }
}