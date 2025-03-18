using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] private List<Gem> gems;

    public void addGemToInventory(Gem gem)
    {
        Debug.Log("Adding gem to inventory");
        gems.Add(gem);
    }

    public List<Gem> getGems()
    {
        return gems;
    }
}
