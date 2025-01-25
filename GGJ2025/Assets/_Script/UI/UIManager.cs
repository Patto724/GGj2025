using System;
using UnityEngine;
using TMPro;
using System.Collections;

public class UIManager : MonoBehaviour
{

    public GameObject safeLock;
    [SerializeField] TMP_Text dialogText;
    
    Coroutine setDialogCoroutine;

    public static UIManager instance;

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
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            safeLock.SetActive(false);
        }
    }

    public void SetDialogText(string text)
    {
        if (setDialogCoroutine != null)
        {
            StopCoroutine(setDialogCoroutine);
        }
        setDialogCoroutine = StartCoroutine(ShowDialogText(text));
    }

    IEnumerator ShowDialogText(string text)
    {
        dialogText.text = text;
        yield return new WaitForSeconds(2f);
        dialogText.text = "";
    }

    
}
