using UnityEngine;

[System.Serializable]
public class Item
{
    public string itemName; // Nombre del objeto
    public Sprite icon; // Icono del objeto
    public bool isUsable; // Determina si el objeto puede ser usado

    public Item(string name, Sprite icon, bool usable)
    {
        this.itemName = name;
        this.icon = icon;
        this.isUsable = usable;
    }
}
