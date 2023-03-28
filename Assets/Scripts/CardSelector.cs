using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardSelector : MonoBehaviour
{
    private Player player;
    private int currentSelectedCard = 0;
    [SerializeField] private List<Card> cardList; // List of existing cards
    [SerializeField] private List<Button> buttonList; // List of buttons to assign the selected cards to
    [SerializeField] private TMPro.TMP_Dropdown dropdown;

    private List<Card> selectedCards = new List<Card>(5);

    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        SelectCards();

        // Assign the selected cards to the buttons
        for (int i = 0; i < selectedCards.Count; i++)
        {
            buttonList[i].image.sprite = selectedCards[i].cardSprite;
        }
    }

    void SelectCards()
    {
        for (int i = 0; i < buttonList.Count; i++)
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



    public void PurchaseCard(int index)
    {
        if(player.score >= selectedCards[index].cardPrice)
        {
            player.RemoveScore(selectedCards[index].cardPrice);
            player.cardDeck[currentSelectedCard] = selectedCards[index];
            UpdateCards();
            
           
        }
        
    }

    public void OnCardNumberSelected(int index)
    {
        currentSelectedCard = dropdown.value;
    }
    
    public void CloseUI()
    {
        GetComponentInParent<Canvas>().enabled = false;
    }

    private void UpdateCards()
    {
        player.SendCardUpdate();
    }
}
