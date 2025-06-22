using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GemInventoryPrefab : MonoBehaviour
{
    private int id;
    private string type;
    private string gemName;
    private string description;
    private bool isColorless = false;

    [SerializeField] private GameObject gemImage;

    [SerializeField] private Sprite gemRedImage;

    [SerializeField] private Sprite gemOrangeImage;

    [SerializeField] private Sprite gemGreenImage;

    [SerializeField] private Sprite gemBlueImage;

    [SerializeField] private Sprite gemPurpleImage;
    [SerializeField] private Sprite gemColorlessImage;

    [SerializeField]
    private GameObject gemHighlight;

    Button button;




    private void Awake()
    {
        button = GetComponent<Button>();
        if (button != null)
            button.onClick.AddListener(OnItemClicked);
    }

    public void setType(string type, string isColorless)
    {
        this.type = type;

        Debug.Log(isColorless);

        if (isColorless == "True")
        {
            gemImage.GetComponent<Image>().sprite = gemColorlessImage;
            this.isColorless = true;
            return;
        }

        switch (type)
        {
            case "Somebody":
                gemImage.GetComponent<Image>().sprite = gemBlueImage;
                break;
            case "Wanted":
                gemImage.GetComponent<Image>().sprite = gemGreenImage;
                break;
            case "But":
                gemImage.GetComponent<Image>().sprite = gemOrangeImage;
                break;
            case "So":
                gemImage.GetComponent<Image>().sprite = gemPurpleImage;
                break;
            case "Then":
                gemImage.GetComponent<Image>().sprite = gemRedImage;
                break;
            default:
                gemImage.GetComponent<Image>().sprite = gemRedImage;
                break;

        }
    }

    public void setId(int id)
    {
        this.id = id;
    }

    public void setDescription(string description)
    {
        this.description = description;
    }
    public void setName(string name)
    {
        this.gemName = name;
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
        if (UIManagerTemplate.Instance != null)
        {
            UIManagerTemplate.Instance.updateInventoryGemSelectedText(this.gemName, this.type, this.description, this.isColorless);
            UIManagerTemplate.Instance.inventoryGemHighlight(this.id);
        }
        else if (UIManager_Early.Instance != null)
        {
            UIManager_Early.Instance.updateInventoryGemSelectedText(this.gemName, this.type, this.description, this.isColorless);
            UIManager_Early.Instance.inventoryGemHighlight(this.id);
        }

    }
}
