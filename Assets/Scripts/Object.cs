using UnityEngine;

public class Object : MonoBehaviour
{
    [SerializeField] private string name = "hello world";
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
    
    
}
