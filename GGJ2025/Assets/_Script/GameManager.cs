using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] PlayerControllerMobile playConMobile;
    [SerializeField] GyroCamController gyroCon;
    [SerializeField] GameObject GamePlayUIGroup;
    [SerializeField] Image titleImage;
    [SerializeField] GameObject titleGroup;
    bool clickTitle = false;

    [Space(10)]
    [SerializeField] GameObject mainPlayer;
    public GameObject crowbarGO_PC;
    public GameObject crowbarGO_Mobile;

    [Space(10)]
    [SerializeField] AudioSource voiceSource;
    [SerializeField] List<AudioClip> voiceClips = new List<AudioClip>();

    [Space(10)]
    [SerializeField] float timeToSpawnBubble = 20f;

    public List<PickableScripableObject> pickableScripableObjects = new List<PickableScripableObject>();
    [Space(10)]
    [SerializeField] ParticleSystem bubbleParticle;
    [SerializeField] AudioSource bubbleAudio;
    public List<Transform> bubbleSpots = new List<Transform>();

    public bool isStopBubbleInTime = false;

    [Space(10)]
    [SerializeField] PlayableDirector cutscenePlayer;
    [SerializeField] AudioSource ghostAudioSource;

    [SerializeField] List<Sprite> passwordSprites = new List<Sprite>();
    [SerializeField] List<GameObject> hintObjects = new List<GameObject>();

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
        SetupPasswordHint();
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
            UIManager.instance.spellGroup.SetActive(true);
            int spotIndex = FindClosestBubbleSpot(mainPlayer.transform.position);
            bubbleParticle.transform.position = bubbleSpots[spotIndex].position;
            bubbleParticle.Play();
            bubbleAudio.PlayOneShot(bubbleAudio.clip);
            yield return new WaitForSeconds(7f); 
            bubbleParticle.Stop();
            OnDisableBubble();
            float reduceTime = timeToSpawnBubble * 0.1f;
            if(reduceTime < 1f)
            {
                reduceTime = 1f;
            }
            timeToSpawnBubble -= reduceTime;
            if(timeToSpawnBubble < 5f)
            {
                timeToSpawnBubble = 5f;
            }

            if (!isStopBubbleInTime)// jump scare
            {
                var playerCon = mainPlayer.GetComponent<PlayerControllerMobile>();

                playerCon.enabled = false;
                playerCon.gyroCamController.enabled = false;
                playerCon.gameObject.transform.eulerAngles = new Vector3(0, 0, 0);
                playerCon.gyroCamController.gameObject.transform.eulerAngles = new Vector3(0, 0, 0);
                cutscenePlayer.Play();
                ghostAudioSource.Play();
                //Handheld.Vibrate();

                yield return new WaitForSeconds(3f);

                SceneManager.LoadScene(0);
            }

            isStopBubbleInTime = false;
        }
    }

    public void OnDisableBubble()
    {
        UIManager.instance.spellGroup.SetActive(false);
        bubbleAudio.Stop();
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

    void SetupPasswordHint()
    {
        for (int i = 0; i < 3; i++)
        {
            int randomIndex = Random.Range(0, hintObjects.Count);
            if (hintObjects[randomIndex].activeSelf == false)
            {
                hintObjects[randomIndex].SetActive(true);
                hintObjects[randomIndex].GetComponent<SpriteRenderer>().sprite = passwordSprites[i];
            }
            else
            {
                i--;
            }
        }
    }

    public void GameStart()
    {
        clickTitle = true;
        StartCoroutine(GameStartSequence());
    }

    IEnumerator GameStartSequence()
    {
        while(titleImage.color.a > 0)
        {
            titleImage.color = new Color(titleImage.color.r, titleImage.color.g, titleImage.color.b, titleImage.color.a - Time.deltaTime);
            yield return null;
        }

        titleGroup.SetActive(false);
        GamePlayUIGroup.SetActive(true);
        playConMobile.enabled = true;
        gyroCon.enabled = true;
        voiceSource.PlayOneShot(voiceClips[0]);

        StartCoroutine(BubbleSpawnCycle());
    }

    public void PlayVoice(int index)
    {
        voiceSource.PlayOneShot(voiceClips[index]);
    }

    public void GoodEnd()
    {
        UIManager.instance.EndGameGood();
    }
}
