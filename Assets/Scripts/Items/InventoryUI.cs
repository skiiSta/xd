using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public GameObject inventoryPanel; // El panel del inventario
    public Transform itemsParent; // El contenedor de los íconos de los objetos
    public GameObject itemSlotPrefab; // Prefabricado de la ranura del inventario

    private Inventory playerInventory;

    private void Start()
    {
        playerInventory = FindObjectOfType<Inventory>();
        UpdateInventoryUI();
    }

    // Actualiza la interfaz gráfica del inventario
    public void UpdateInventoryUI()
    {
        foreach (Transform child in itemsParent)
        {
            Destroy(child.gameObject); // Limpiar los elementos anteriores
        }

        foreach (Item item in playerInventory.items)
        {
            GameObject itemSlot = Instantiate(itemSlotPrefab, itemsParent);
            itemSlot.GetComponentInChildren<Image>().sprite = item.icon;
            itemSlot.GetComponentInChildren<Text>().text = item.itemName;
        }
    }

    // Mostrar o esconder el inventario
    public void ToggleInventory()
    {
        inventoryPanel.SetActive(!inventoryPanel.activeSelf);
    }
}
