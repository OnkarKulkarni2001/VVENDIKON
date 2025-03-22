// Modified Item.cs
using UnityEngine;

[CreateAssetMenu(menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    // Add this
    [Header("UI Settings")]
    public Sprite icon;
    public string itemName;
    public string description;
    public int price;



    // Rest of your existing fields
   // [SerializeField] private ItemType type;
    //[SerializeField] public ItemCore itemCore;    // Commented this out as I was getting circular dependency in inspector - by Onkar
   // [SerializeField] public ItemRarityConfiguration raritySO;
}