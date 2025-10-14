using UnityEngine;

[CreateAssetMenu(fileName = "NewTablet", menuName = "CookAnything/Tablet")]
public class TabletData : ScriptableObject
{
    public string mealName;
    public starValue Geschmack;
    public starValue Konsistenz;
    public starValue Temperatur;
}
