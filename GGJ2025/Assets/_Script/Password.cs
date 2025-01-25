using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Password : MonoBehaviour
{
    
    public GameObject canvas;
    public TMP_Text[] digitTexts;        // Array of Text components to display each digit
    public Button[] buttonUp; // Buttons to increase digits
    public Button[] buttonDown; // Buttons to decrease digits
    private string currentPassword;
    public string correctCode = "1234"; // Correct 4-digit code
    private int[] currentDigits = new int[4]; // Array to store current digits
    [SerializeField] PickableScripableObject itemToGive;
    [SerializeField] AudioSource safeAudioSource;

    // Start is called before the first frame update
    void Start()
    {
        // Add listeners to the increase and decrease buttons
        for (int i = 0; i < 4; i++)
        {
            int index = i; // Capture the current digit index for button listeners

            // Increase the digit on button click
            buttonDown[i].onClick.AddListener(() => IncreaseDigit(index));

            // Decrease the digit on button click
            buttonUp[i].onClick.AddListener(() => DecreaseDigit(index));
            
        }

        // Add listener for the submit button
        //submitButton.onClick.AddListener(CheckCode);
        UpdateDisplay();
    }

    // Called when an increase button is clicked (Increase the digit)
    void IncreaseDigit(int index)
    {
        currentDigits[index] = (currentDigits[index] + 1) % 10; // Wrap around after 9 (0-9)
        UpdateDisplay();
        AutoCheckCode();
    }

    // Called when a decrease button is clicked (Decrease the digit)
    void DecreaseDigit(int index)
    {
        currentDigits[index] = (currentDigits[index] - 1 + 10) % 10; // Wrap around after 0 (9-0)
        UpdateDisplay();
        AutoCheckCode();
    }

    // Update the displayed digits on the UI
    void UpdateDisplay()
    {
        for (int i = 0; i < 4; i++)
        {
            digitTexts[i].text = currentDigits[i].ToString(); // Update the text for each digit
        }
    }

    // Called when submit button is clicked
    void AutoCheckCode()
    {
        string enteredCode = string.Join("", System.Array.ConvertAll(currentDigits, digit => digit.ToString()));

        if (enteredCode == correctCode)
        {
            // Correct code entered
            Debug.Log("Safe Unlocked!");
            Handheld.Vibrate();
            safeAudioSource.Play();
            Invoke(nameof(GiveItemToPlayer), 1.0f);
        }
    }

    void GiveItemToPlayer()
    {
        GameManager.instance.AddPickableObject(itemToGive);
        canvas.SetActive(false);
    }
}
