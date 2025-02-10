using UnityEngine;

// Скрипт для описания предмета
[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/New Item")]
public class NewItem : ScriptableObject
{
    // Основные свойства предмета
    public string itemName; // Название предмета
    public Sprite icon; // Иконка предмета
    public int quantity; // Количество
    public GameObject prefab; // Префаб предмета
    public ItemType itemType; // Тип предмета

    /// <summary>
    /// Создает копию текущего предмета.
    /// Это полезно, если вы хотите создать независимый экземпляр предмета с такими же данными.
    /// </summary>
    /// <returns>Клон текущего предмета</returns>
    public NewItem Clone()
    {
        // Создаем новый экземпляр ScriptableObject
        NewItem newItem = ScriptableObject.CreateInstance<NewItem>();

        // Копируем все данные из текущего объекта в новый
        newItem.itemName = itemName;
        newItem.icon = icon;
        newItem.quantity = quantity;
        newItem.prefab = prefab;
        newItem.itemType = itemType;

        return newItem; // Возвращаем новый экземпляр
    }
}

// Перечисление для типа предметов
public enum ItemType
{
    Ammo,        // Патроны
    Weapon,      // Оружие
    Consumable,  // Расходуемый предмет
    Armor        // Броня
}
