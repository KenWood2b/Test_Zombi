using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/New Item")]
public class NewItem : ScriptableObject
{
    public string itemName;
    public Sprite icon;
    public int quantity;
    public GameObject prefab;
    public ItemType itemType;

    public NewItem Clone()
    {
        NewItem newItem = ScriptableObject.CreateInstance<NewItem>();

        newItem.itemName = itemName;
        newItem.icon = icon;
        newItem.quantity = quantity;
        newItem.prefab = prefab;
        newItem.itemType = itemType;

        return newItem;
    }
}

public enum ItemType
{
    Ammo,       
    Weapon,      
    Consumable,  
    Armor        
}
