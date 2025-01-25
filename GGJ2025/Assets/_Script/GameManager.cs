using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject crowbarGO;
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
        if (pickableScripableObject.objectName == "Crowbar")
        {
            crowbarGO.SetActive(true);
        }
    }

    public bool HaveThisItem(string itemName)
    {
        foreach (var item in pickableScripableObjects)
        {
            if (item.objectName == itemName)
            {
                return true;
            }
        } return false;
    }
}
