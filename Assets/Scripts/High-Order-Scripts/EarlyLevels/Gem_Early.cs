using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Has comparing methods for early leveld rag and drop
public class Gem_Early : GemInterface
{

    public override void Interact()
    {
        audioSource.PlayOneShot(gemSound);
        uiManager.openGemCanvas(gemDescription, gemType.ToString(), gemName);

        if (transform.parent == null || transform.parent.GetComponent<SWBSTSlot>() == null)
        {
            gameObject.SetActive(false);
            inventoryManager.addGemToInventory(this);
        }
    }

    //--------------- METHODS FOR EARLY LEVEL GEM MATCHING MECHANIC ------------------//

    public bool compareGemType(Gem_Early otherGem)
    {
        if (otherGem.gemType == this.gemType)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool checkIfGemTypeInList(List<GemInterface> gemList)
    {
        foreach (Gem_Early gem in gemList)
        {
            if (compareGemType(gem))
            {
                return true;
            }
        }
        return false;
    }

    public Gem_Early getGemFromGemType(List<GemInterface> gemList)
    {
        foreach (Gem_Early gem in gemList)
        {
            if (compareGemType(gem))
            {
                return gem;
            }
        }
        return null;
    }

    public void copyGemData(GemInterface otherGem)
    {
        this.gemType = otherGem.Type;
        this.gemDescription = otherGem.GemDescription;
    }
}
