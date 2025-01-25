using System;
using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{

    public GameObject safeLock;
    public GameObject howToPaper;
    [SerializeField] TMP_Text dialogText;

    [Space(10)]
    public GameObject spellGroup;
    public Image spellIcon;
    public Image spellCooldown;

    [Space(10)]
    public Image blackScreen;
    
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

    public void UseSpell()
    {
        //spellicon alpha = 0.10f
        spellIcon.color = new Color(spellIcon.color.r, spellIcon.color.g, spellIcon.color.b, 0.10f);
        spellCooldown.fillAmount = 0;

        spellIcon.gameObject.GetComponent<Button>().interactable = false;

        StartCoroutine(SpellCooldown());
    }

    IEnumerator SpellCooldown()
    {
        while (spellCooldown.fillAmount < 1)
        {
            spellCooldown.fillAmount += Time.deltaTime / 10;
            yield return null;
        }
        spellIcon.color = new Color(spellIcon.color.r, spellIcon.color.g, spellIcon.color.b, 1);
        spellCooldown.fillAmount = 0;

        spellIcon.gameObject.GetComponent<Button>().interactable = true;
    }

    public void EndGameGood()
    {
        StartCoroutine(EndGameGoodSequence());
    }

    IEnumerator EndGameGoodSequence()
    {
        while (blackScreen.color.a < 1)
        {
            blackScreen.color = new Color(blackScreen.color.r, blackScreen.color.g, blackScreen.color.b, blackScreen.color.a + Time.deltaTime);
            yield return null;
        }

        SceneManager.LoadScene(0);
    }
}
