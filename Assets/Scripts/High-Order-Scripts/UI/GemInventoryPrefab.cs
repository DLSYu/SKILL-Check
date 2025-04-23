using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GemInventoryPrefab : MonoBehaviour
{
    string type;
    string description;

    Button button;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


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


    public void OnItemClicked()
    {
        // Call a central method to update the UI
        UIManager.Instance.updateInventoryGemSelectedText(this.type, this.description);
    }
}
