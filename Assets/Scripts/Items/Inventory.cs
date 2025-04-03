using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<Item> items = new List<Item>();

    // Método para añadir un objeto al inventario
    public void AddItem(Item item)
    {
        items.Add(item);
        Debug.Log(item.itemName + " añadido al inventario.");
    }

    // Método para quitar un objeto del inventario
    public void RemoveItem(Item item)
    {
        if (items.Contains(item))
        {
            items.Remove(item);
            Debug.Log(item.itemName + " ha sido eliminado.");
        }
    }

    // Verificar si un objeto existe en el inventario
    public bool HasItem(Item item)
    {
        return items.Contains(item);
    }
}
