using UnityEngine;


public enum ItemType
{
    Plastic,
    Glass,
    Metal
}


public class Container : MonoBehaviour
{
    [SerializeField] public ItemType acceptedItem;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
