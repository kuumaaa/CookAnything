using UnityEngine;

public class Object : MonoBehaviour
{
    [SerializeField] private string name;
    [SerializeField] private int eigenschaft1;
    [SerializeField] private int eigenschaft2;
    [SerializeField] private int eigenschaft3;
    [SerializeField] private int eigenschaft4;
    [SerializeField] private string beschreibung;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public string GetName()
    {
        return name;
    }

    public ObjectData GetObjectData()
    {
        ObjectData data = ScriptableObject.CreateInstance<ObjectData>();
        
        data.objectName = name;
        data.eigenschaft1 = eigenschaft1;
        data.eigenschaft2 = eigenschaft2;
        data.eigenschaft3 = eigenschaft3;
        data.eigenschaft4 = eigenschaft4;
        data.beschreibung = beschreibung;
        
        return data;
    } 
    
    
}
