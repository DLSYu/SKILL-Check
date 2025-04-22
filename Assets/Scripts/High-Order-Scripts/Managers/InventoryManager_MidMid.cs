using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager_MidMid : MonoBehaviour
{
    [SerializeField] private List<Gem_MidMid> gems;

    public void addGemToInventory(Gem_MidMid gem)
    {
        Debug.Log("Adding gem to inventory");
        gems.Add(gem);
    }

    public List<Gem_MidMid> getGems()
    {
        return gems;
    }
}
