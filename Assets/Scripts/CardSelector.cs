using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardSelector : MonoBehaviour
{
    [SerializeField] private List<GameObject> cardList; // List of existing cards
    [SerializeField] private List<Button> buttonList; // List of buttons to assign the selected cards to
    [SerializeField] private int pp;

    private List<GameObject> selectedCards = new List<GameObject>(); // List of selected cards

    void Start()
    {
        SelectCards();

        // Assign the selected cards to the buttons
        for (int i = 0; i < buttonList.Count; i++)
        {
            buttonList[i].image.sprite = selectedCards[i].GetComponent<SpriteRenderer>().sprite;
        }
    }

    void SelectCards()
    {
        for (int i = 0; i < 5; i++)
        {
            int randomIndex = Random.Range(0, cardList.Count); // Get a random index from the list of cards

            if (!selectedCards.Contains(cardList[randomIndex]))
            {
                selectedCards.Add(cardList[randomIndex]); // Add the card to the list of selected cards
            }
            else
            {
                i--; // If the card has already been selected, try again
            }
        }
    }
}
