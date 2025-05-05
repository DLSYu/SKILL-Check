using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] private List<GemInterface> gems;

    public void addGemToInventory(GemInterface gem)
    {
        Debug.Log("Adding gem to inventory");
        gems.Add(gem);
    }

    public List<GemInterface> getGems()
    {
        return gems;
    }
}