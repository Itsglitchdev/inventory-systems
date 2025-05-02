using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance { get; private set; }

    [Header("Buttons")]
    [SerializeField] private Button shopButton;
    [SerializeField] private Button inventoryButton;
    [SerializeField] private Button prefabButton;

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
    [SerializeField] private RectTransform inventoryItemsContainer;

    [Header("Shop Items")]
    [SerializeField] private List<ElementalShopItemData> shopElementalItemData;
    [SerializeField] private GameObject shopItemPrefab;

    [Header("Inventory Items")]
    [SerializeField] private List<ElementalInventoryItemData> inventoryElementalItemData;
    [SerializeField] private GameObject inventoryItemPrefab;


    private List<GameObject> currentShopItems = new List<GameObject>();
    private List<GameObject> currentInventoryItems = new List<GameObject>();


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

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
            Button button = Instantiate(prefabButton, shopButtonSpawnParent.transform);
            button.GetComponentInChildren<TextMeshProUGUI>().text = elementType.ToString();

            ElementType capturedType = elementType;
            button.onClick.AddListener(() => LoadShopItemsByType(capturedType));
        }
    }

    void InventoryElementSpawnerButtons()
    {
        foreach (ElementType elementType in elementCategoryButtons)
        {
            Button button = Instantiate(prefabButton, inventoryButtonSpawnParent.transform);
            button.GetComponentInChildren<TextMeshProUGUI>().text = elementType.ToString();

            ElementType capturedType = elementType;
            button.onClick.AddListener(() => LoadInventoryItemsByType(capturedType));

        }
    }

    private void LoadShopItemsByType(ElementType selectedType)
    {
        ClearShopItems();

        List<ElementalShopItemData> filteredItems = new List<ElementalShopItemData>();

        foreach (ElementalShopItemData item in shopElementalItemData)
        {
            if (item.shopElementType == selectedType)
            {
                filteredItems.Add(item);
            }
        }

        foreach (ElementalShopItemData item in filteredItems)
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

    private void LoadInventoryItemsByType(ElementType selectedType)
    {
        ClearInventoryItems();

        List<ElementalInventoryItemData> filteredItems = new List<ElementalInventoryItemData>();

        foreach (ElementalInventoryItemData item in inventoryElementalItemData)
        {
            if (item.inventoryElementType == selectedType)
            {
                filteredItems.Add(item);
            }
        }

        foreach (ElementalInventoryItemData item in filteredItems)
        {
            GameObject newItem = Instantiate(inventoryItemPrefab, inventoryItemsContainer);
            currentInventoryItems.Add(newItem);

            InventoryItemDisplay itemDisplay = newItem.GetComponent<InventoryItemDisplay>();
            if (itemDisplay != null)
            {
                itemDisplay.SetupItem(item);
            }
            else
            {
                Debug.LogError("Inventory item prefab is missing InventoryItemDisplay component!");
            }
        }
    }

    public void BuyItem(ElementalShopItemData itemData)
    {
        // Add item to inventory
        ElementalInventoryItemData inventoryItem = new ElementalInventoryItemData
        {
            id = itemData.id,
            inventoryElementType = itemData.shopElementType,
            inventoryItemName = itemData.shopItemName,
            inventoryItemType = itemData.shopItemType,
            inventoryItemDescription = itemData.shopItemDescription,
            isEquipped = false
        };
        inventoryElementalItemData.Add(inventoryItem);

        if (inventoryPanel.activeSelf)
        {
            LoadInventoryItemsByType(inventoryItem.inventoryElementType);
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

    private void ClearInventoryItems()
    {
        foreach (GameObject item in currentInventoryItems)
        {
            Destroy(item);
        }
        currentInventoryItems.Clear();
    }

}
