using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject objectInfoUI;
    [SerializeField] private TMP_Text objectNameText;
    [SerializeField] private StarsUI starsUIOne;
    [SerializeField] private StarsUI starsUITwo;
    [SerializeField] private StarsUI starsUIThree;
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
        starsUIOne.InitializeStars(data.Geschmack,"Mild","Funky",true);
        starsUITwo.InitializeStars(data.Konsistenz,"Weich","Hart",true);
        starsUIThree.InitializeStars(data.Temperatur,"Kalt","Heiß",true);
        
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
