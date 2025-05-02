using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryItemDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI itemNameText;
    [SerializeField] private TextMeshProUGUI typeText;
    [SerializeField] private TextMeshProUGUI effectText;
    [SerializeField] private Button euipButton;

    public void SetupItem(ElementalInventoryItemData data)
    {
        if (itemNameText != null)
            itemNameText.text = data.inventoryItemName;

        if (typeText != null)
            typeText.text = data.inventoryItemType.ToString();

        if (effectText != null)
            effectText.text = data.inventoryItemDescription;

        if (euipButton != null)
        {
            euipButton.onClick.RemoveAllListeners();
            euipButton.onClick.AddListener(OnEuipButtonClicked);

        }
    }
    
    private void OnEuipButtonClicked()
    {
        // Here you would implement the equipping logic
        Debug.Log("Equipping item: " + itemNameText.text);
    }
    
}