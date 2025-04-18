using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager_Early : MonoBehaviour
{
    public static InventoryManager_Early Instance;

    [Header("Gem Tracking")]
    [SerializeField] private List<Gem_Early> gems = new List<Gem_Early>();
    [SerializeField] private int requiredGems = 3;

    [Header("SWBST Tracking")]
    private Dictionary<SWBSTSlot.SlotType, Gem_Early> swbstGems = new Dictionary<SWBSTSlot.SlotType, Gem_Early>();

    public int CollectedGemsCount => gems.Count;
    public List<Gem_Early> GetGems() => new List<Gem_Early>(gems);

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void AddGemToInventory(Gem_Early gem)
    {
        if (!gems.Contains(gem))
        {
            gems.Add(gem);
            CheckDoorUnlock();
            Debug.Log($"Added {gem.Type} gem to inventory");
        }
    }

    public void MoveToSWBST(Gem_Early gem)
    {
        var type = (SWBSTSlot.SlotType)System.Enum.Parse(typeof(SWBSTSlot.SlotType), gem.Type.ToString());
        swbstGems[type] = gem;
        gems.Remove(gem);
        Debug.Log($"Moved {gem.Type} to SWBST line");
    }

    public void ReturnFromSWBST(Gem_Early gem)
    {
        var type = (SWBSTSlot.SlotType)System.Enum.Parse(typeof(SWBSTSlot.SlotType), gem.Type.ToString());
        swbstGems.Remove(type);
        gems.Add(gem);
        Debug.Log($"Returned {gem.Type} to inventory");
    }

    public bool IsSWBSTComplete()
    {
        var requiredTypes = new List<SWBSTSlot.SlotType> {
            SWBSTSlot.SlotType.Somebody,
            SWBSTSlot.SlotType.Wanted,
            SWBSTSlot.SlotType.But,
            SWBSTSlot.SlotType.So,
            SWBSTSlot.SlotType.Then
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
            Door_Early door = FindObjectOfType<Door_Early>();
            if (door != null) door.EnableDoorInteraction();
        }
    }
}
