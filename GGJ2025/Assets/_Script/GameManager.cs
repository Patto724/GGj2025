using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject mainPlayer;
    public GameObject crowbarGO_PC;
    public GameObject crowbarGO_Mobile;

    [SerializeField] float timeToSpawnBubble = 20f;

    public List<PickableScripableObject> pickableScripableObjects = new List<PickableScripableObject>();
    [Space(10)]
    [SerializeField] ParticleSystem bubbleParticle;
    public List<Transform> bubbleSpots = new List<Transform>();

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

    private void Start()
    {
        StartCoroutine(BubbleSpawnCycle());
    }

    public void AddPickableObject(PickableScripableObject pickableScripableObject)
    {
        pickableScripableObjects.Add(pickableScripableObject);
        if (pickableScripableObject.objectName == "Crowbar")
        {
            crowbarGO_PC.SetActive(true);
            crowbarGO_Mobile.SetActive(true);
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

    IEnumerator BubbleSpawnCycle()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeToSpawnBubble);
            int spotIndex = FindClosestBubbleSpot(mainPlayer.transform.position);
            bubbleParticle.transform.position = bubbleSpots[spotIndex].position;
            bubbleParticle.Play();
            yield return new WaitForSeconds(7f);
            bubbleParticle.Stop();
            timeToSpawnBubble -= timeToSpawnBubble * 0.1f;
            if(timeToSpawnBubble < 5f)
            {
                timeToSpawnBubble = 5f;
            }
        }
    }

    int FindClosestBubbleSpot(Vector3 playerPos)
    {
        float minDistance = Mathf.Infinity;
        int index = 0;
        for (int i = 0; i < bubbleSpots.Count; i++)
        {
            float distance = Vector3.Distance(playerPos, bubbleSpots[i].position);
            if (distance < minDistance && distance > 2.5f)
            {
                minDistance = distance;
                index = i;
            }
        }
        return index;
    }
}
