using UnityEngine;

[CreateAssetMenu(fileName = "PickableScripableObject", menuName = "Scriptable Objects/PickableScripableObject")]
public class PickableScripableObject : ScriptableObject
{
    public string objectName;
    public Sprite objectSprite;
    public string objectDescription;
}
