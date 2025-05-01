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

    private ElementalItemData itemData;

    public void SetupItem(ElementalItemData data)
    {
        itemData = data;
        
        if (itemNameText != null)
            itemNameText.text = data.itemName;
            
        if (typeText != null)
            typeText.text = data.itemType.ToString();
            
        if (effectText != null)
            effectText.text = data.description;
            
        if (priceText != null)
            priceText.text = data.price.ToString();
            
        if (buyButton != null)
        {
            buyButton.onClick.RemoveAllListeners();
            buyButton.onClick.AddListener(OnBuyButtonClicked);
        }
    }
    
    private void OnBuyButtonClicked()
    {
        // Here you would implement the purchase logic
        Debug.Log($"Buying item: {itemData.itemName} for {itemData.price} coins");
        
        // You might want to call a method on your GameManager or another manager class
        // For example:
        // FindObjectOfType<GameManager>().BuyItem(itemData);
    }
}