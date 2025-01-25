using UnityEngine;
using TMPro;
using System.Collections;

public class UIManager : MonoBehaviour
{
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
