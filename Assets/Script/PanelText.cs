using System.Collections.Generic;
using TMPro; // Import TextMeshPro namespace
using UnityEngine;
using UnityEngine.UI;

public class PanelText : MonoBehaviour
{
    public List<string> textList;  // List of texts to be displayed
    public TextMeshProUGUI tmpText;  // Reference to the TextMeshPro component
    private int currentTextIndex = 0;  // Index of the current text being displayed

    void Start()
    {
        if (tmpText == null)
        {
            Debug.LogError("TextMeshPro component is not assigned!");
            return;
        }

        if (textList.Count > 0)
        {
            DisplayText();
        }
        else
        {
            Debug.LogError("No text in the text list!");
        }
    }

    // Function to display the current text in the TMP component
    void DisplayText()
    {
        tmpText.text = textList[currentTextIndex];
    }

    // Function to go to the next text, to be assigned to a button
    public void NextText()
    {
        if (currentTextIndex < textList.Count - 1)
        {
            currentTextIndex++;
            DisplayText();
        }
        else
        {
            // If it's the last text, deactivate the panel
            gameObject.SetActive(false);
        }
    }
}
