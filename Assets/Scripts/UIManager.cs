using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject objectInfoUI;
    [SerializeField] private TMP_Text objectNameText;
    [SerializeField] private TMP_Text objectDescriptionText;
    [SerializeField] private TMP_Text scoreBoardText;
    [SerializeField] private GameObject gameOverScreen;
    

    public void DisableObjectInfo()
    {
        objectInfoUI.SetActive(false);
    }

    public void UpdateObjectInfo(ObjectData data)
    {
        objectInfoUI.SetActive(true);
        objectNameText.text = "Press E to pick up " + data.objectName;
        //todo add the other properties
    }

    public void UpdateScore(int score)
    {
        scoreBoardText.text = "Score: " + score.ToString();
    }

    public void ActivateGameOverScreen()
    {
        gameOverScreen.SetActive(true);
    }
}
