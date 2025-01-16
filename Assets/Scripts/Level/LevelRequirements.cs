using Unity.Hierarchy;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelRequirements", menuName = "Scriptable Objects/LevelRequirements")]
public class LevelRequirements : ScriptableObject
{
    public float Plastic;
    public float Metal;
    public float Glass;
    public float Humans;
    public float Animals;
    public float Time;
    public float CollectLimit;
    public float Total
    {
        get { return Plastic + Metal + Glass; }
    }
}
