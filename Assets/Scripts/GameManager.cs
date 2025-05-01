using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    [Header("Buttons")]
    [SerializeField] private Button shopButton;
    [SerializeField] private Button inventoryButton;
    [SerializeField] private Button itemPrefabButton;

    [Header("Panels")]
    [SerializeField] private GameObject shopPanel;
    [SerializeField] private GameObject inventoryPanel;

    [Header("ButtonSpawn")]
    [SerializeField] private ElementType[] elementCategoryButtons;

    [Header("ButtonSpawnParent")]
    [SerializeField] private RectTransform shopButtonSpawnParent;
    [SerializeField] private RectTransform inventoryButtonSpawnParent;

    [Header("ItemsContainer")]
    [SerializeField] private RectTransform shopItemsContainer;

    [Header("Shop Items")]
    [SerializeField] private List<ElementalItemData> elementalItemData;
    [SerializeField] private GameObject shopItemPrefab;

    private List<GameObject> currentShopItems = new List<GameObject>();


    private void Awake()
    {
        shopPanel.SetActive(false);
        inventoryPanel.SetActive(false);
    }

    void Start()
    {
        Eventlistners();
        ShopElementSpawnerButtons();
        InventoryElementSpawnerButtons();
    }

    void Update()
    {

    }

    void Eventlistners()
    {
        shopButton.onClick.AddListener(OnShopButtonClicked);
        inventoryButton.onClick.AddListener(OnInventoryButtonClicked);
    }

    void OnShopButtonClicked()
    {
        if (inventoryPanel.activeSelf)
            inventoryPanel.SetActive(!inventoryPanel.activeSelf);

        shopPanel.SetActive(!shopPanel.activeSelf);
    }

    void OnInventoryButtonClicked()
    {
        if (shopPanel.activeSelf)
            shopPanel.SetActive(!shopPanel.activeSelf);

        inventoryPanel.SetActive(!inventoryPanel.activeSelf);
    }

    void ShopElementSpawnerButtons()
    {
        foreach (ElementType elementType in elementCategoryButtons)
        {
            Button button = Instantiate(itemPrefabButton, shopButtonSpawnParent.transform);
            button.GetComponentInChildren<TextMeshProUGUI>().text = elementType.ToString();

            ElementType capturedType = elementType;
            button.onClick.AddListener(() => LoadShopItemsByType(capturedType));
        }
    }

    void InventoryElementSpawnerButtons()
    {
        foreach (ElementType item in elementCategoryButtons)
        {
            Button button = Instantiate(itemPrefabButton, inventoryButtonSpawnParent.transform);
            button.GetComponentInChildren<TextMeshProUGUI>().text = item.ToString();

        }
    }

    public void LoadShopItemsByType(ElementType selectedType)
    {
        ClearShopItems();

        List<ElementalItemData> filteredItems = new List<ElementalItemData>();
        foreach (ElementalItemData item in elementalItemData)
        {
            if (item.elementType == selectedType)
            {
                filteredItems.Add(item);
            }
        }

        foreach (ElementalItemData item  in filteredItems)
        {
            GameObject newItem = Instantiate(shopItemPrefab, shopItemsContainer);
            currentShopItems.Add(newItem);

            ShopItemDisplay itemDisplay = newItem.GetComponent<ShopItemDisplay>();
            if (itemDisplay != null)
            {
                itemDisplay.SetupItem(item);
            }
            else
            {
                Debug.LogError("Shop item prefab is missing ShopItemDisplay component!");
            }
        }
       
    }

    private void ClearShopItems()
    {
        foreach (GameObject item in currentShopItems)
        {
            Destroy(item);
        }
        currentShopItems.Clear();
    }

}
