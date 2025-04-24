using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GemInventoryPrefab : MonoBehaviour
{
    private int id;
    private string type;
    private string description;

    [SerializeField]
    private GameObject gemHighlight;

    Button button;




    private void Awake()
    {
        button = GetComponent<Button>();
        if (button != null)
            button.onClick.AddListener(OnItemClicked);
    }

    public void setType(string type)
    {
        this.type = type;
    }

    public void setId(int id)
    {
        this.id = id;
    }

    public void setDescription(string description)
    {
        this.description = description;
    }

    public string getType()
    {
        return type;
    }

    public string getDescription()
    {
        return description;
    }

    public int getId()
    {
        return id;
    }

    public void setHighlight(bool value)
    {
        gemHighlight.SetActive(value);
    }


    public void OnItemClicked()
    {
        // Call a central method to update the UI
        UIManager.Instance.updateInventoryGemSelectedText(this.type, this.description);
        UIManager.Instance.inventoryGemHighlight(this.id);
    }
}
