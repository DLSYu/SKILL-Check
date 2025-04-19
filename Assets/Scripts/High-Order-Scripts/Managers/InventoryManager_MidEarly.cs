using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager_MidEarly : MonoBehaviour
{
    public static InventoryManager_MidEarly Instance;

    [Header("Gem Tracking")]
    [SerializeField] private List<Gem_MidEarly> gems = new List<Gem_MidEarly>();
    [SerializeField] private int requiredGems;

    [Header("SWBST Tracking")]
    private Dictionary<SWBSTSlot_MidEarly.SlotType, Gem_MidEarly> swbstGems = new Dictionary<SWBSTSlot_MidEarly.SlotType, Gem_MidEarly>();

    public int CollectedGemsCount => gems.Count;
    public List<Gem_MidEarly> GetGems() => new List<Gem_MidEarly>(gems);

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void AddGemToInventory(Gem_MidEarly gem)
    {
        // Prevent duplicate gem types
        // Strict check for duplicates
        if (!gems.Exists(g => g.Type == gem.Type))
        {
            gems.Add(gem);
            CheckDoorUnlock();
            Debug.Log($"Added {gem.Type} gem to inventory. Total: {gems.Count}");
        }
        else
        {
            Debug.LogWarning($"Already have {gem.Type} gem");
        }
    }

    public void MoveToSWBST(Gem_MidEarly gem)
    {
        var type = (SWBSTSlot_MidEarly.SlotType)Enum.Parse(
            typeof(SWBSTSlot_MidEarly.SlotType),
            gem.Type.ToString()
        );
        swbstGems[type] = gem;
        gems.Remove(gem);
    }

    public void ReturnFromSWBST(Gem_MidEarly gem)
    {
        var type = (SWBSTSlot_MidEarly.SlotType)Enum.Parse(
            typeof(SWBSTSlot_MidEarly.SlotType),
            gem.Type.ToString()
        );
        swbstGems.Remove(type);
        gems.Add(gem);
    }

    public bool IsSWBSTComplete()
    {
        var requiredTypes = new List<SWBSTSlot_MidEarly.SlotType> {
            SWBSTSlot_MidEarly.SlotType.Somebody,
            SWBSTSlot_MidEarly.SlotType.Wanted,
            SWBSTSlot_MidEarly.SlotType.But,
            SWBSTSlot_MidEarly.SlotType.So,
            SWBSTSlot_MidEarly.SlotType.Then
        };

        foreach (var type in requiredTypes)
        {
            if (!swbstGems.ContainsKey(type)) return false;
        }
        return true;
    }

    private void CheckDoorUnlock()
    {
        if (gems.Count >= requiredGems)
        {
            Door_MidEarly door = FindObjectOfType<Door_MidEarly>();
            if (door != null) door.EnableDoorInteraction();
        }
    }
}
