using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<PickableScripableObject> pickableScripableObjects = new List<PickableScripableObject>();

    public static GameManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddPickableObject(PickableScripableObject pickableScripableObject)
    {
        pickableScripableObjects.Add(pickableScripableObject);
    }
}
