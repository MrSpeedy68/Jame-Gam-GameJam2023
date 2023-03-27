using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PurchaseManager : MonoBehaviour
{
    private Player player;
    private PlayerUI playerUi;

    public Text moneyText;
    public Text inventoryText;
    private int currentInventorySize = 0;

    void Start()
    {
        player = FindObjectOfType<Player>();
        UpdateUI();
    }

    void UpdateUI()
    {
        moneyText.text = "Money: " + currentMoney;
        inventoryText.text = "Inventory: " + currentInventorySize + "/" + MaxInventorySize;
    }

    public void PurchaseItem(Item item)
    {
        if (coinSystem.currentMoney >= item.cost && currentInventorySize < MaxInventorySize)
        {
            currentMoney -= item.cost;
            currentInventorySize++;
            UpdateUI();
            Debug.Log("Purchased " + item.name);
        }
        else
        {
            Debug.Log("Not enough money or inventory space.");
        }
    }


}
