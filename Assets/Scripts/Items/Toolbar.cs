using UnityEngine;

public class Toolbar : MonoBehaviour
{
    public Inventory inventory;  // Referencia al inventario

    // Diccionario para almacenar los íconos de los ítems.
    [System.Serializable]
    public class ItemIconPair
    {
        public string itemName;  // Nombre del ítem
        public Sprite itemIcon;  // Ícono del ítem
    }

    public ItemIconPair[] itemIcons;  // Array de pares nombre-ícono

    void Start()
    {
        // Asegurarse de que el inventario esté correctamente asignado
        if (inventory == null)
        {
            inventory = GetComponent<Inventory>();
        }
    }

    void Update()
    {
        // Control para recoger un ítem
        if (Input.GetKeyDown(KeyCode.E)) // 'E' para recoger objetos
        {
            // Crear un nuevo ítem "Plátano" y agregarlo al inventario
            Item newItem = CreateItem("Plátano");
            inventory.AddItem(newItem);
        }

        // Control para abrir el inventario
        if (Input.GetKeyDown(KeyCode.I)) // 'I' para abrir el inventario
        {
            FindObjectOfType<InventoryUI>().ToggleInventory();
        }
    }

    // Método para crear un ítem basado en su nombre
    Item CreateItem(string itemName)
    {
        // Obtener el icono correspondiente al nombre del ítem
        Sprite itemIcon = GetIcon(itemName);

        if (itemIcon == null)
        {
            Debug.LogError("No se encontró el icono para el ítem: " + itemName);
            return null;
        }

        // Crear y devolver el nuevo objeto Item
        return new Item(itemName, itemIcon, true);
    }

    // Método para obtener el ícono correspondiente a un nombre de ítem
    Sprite GetIcon(string itemName)
    {
        // Buscar el ícono que corresponda al nombre del ítem
        foreach (var pair in itemIcons)
        {
            if (pair.itemName == itemName)
            {
                return pair.itemIcon;
            }
        }

        // Si no se encuentra el ícono, retornar null
        Debug.LogError("Ícono no encontrado para el ítem: " + itemName);
        return null;
    }
}
