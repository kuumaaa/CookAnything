using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject objectInfoUI;
    [SerializeField] private TMP_Text objectNameText;
    [SerializeField] private TMP_Text objectDescriptionText;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    public void DisableObjectInfo()
    {
        objectInfoUI.SetActive(false);
    }

    public void UpdateObjectInfo(string info)
    {
        objectInfoUI.SetActive(true);
        objectNameText.text = "Press E to pick up " + info;
    }
}
