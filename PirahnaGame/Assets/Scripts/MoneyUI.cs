using System.Collections;
using UnityEngine;
using TMPro;

public class MoneyUI : MonoBehaviour
{
    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI changeText;
    public GameObject moneyManager;
    private float changeDuration = 1.2f;
    private int previousMoney = 0;
    private Color originalColor;

    private void Start()
    {
        if (moneyManager != null)
        {
            int totalMoney = moneyManager.GetComponent<MoneyScript>().totalMoney;
            previousMoney = totalMoney >= 0 ? totalMoney : 0;
            moneyText.text = "$: " + previousMoney;
        }

        if (moneyText == null || changeText == null)
        {
            Debug.LogError("No Money UI Text elements assigned.");
        }

        changeText.gameObject.SetActive(false);

        
        originalColor = moneyText.color;
    }

    private void Update()
    {
        UpdateMoneyUI();
    }

    public void UpdateMoneyUI()
    {
        if (moneyManager != null)
        {
            int currentMoney = moneyManager.GetComponent<MoneyScript>().totalMoney;
            moneyText.text = "$: " + (currentMoney >= 0 ? currentMoney : 0).ToString();

            int difference = currentMoney - previousMoney;
            if (difference != 0)
            {
                ShowMoneyChange(difference);
                previousMoney = currentMoney;
            }
        }
    }

    public void ShowMoneyChange(int amount)
    {
        changeText.text = (amount >= 0 ? "+" : "") + amount.ToString();
        changeText.gameObject.SetActive(true);
        StartCoroutine(HideChangeTextAfterDelay());
    }

    private IEnumerator HideChangeTextAfterDelay()
    {
        yield return new WaitForSeconds(changeDuration);
        changeText.gameObject.SetActive(false);
    }

    // Call this method when you want to change the color to red (if cannot afford)
    public IEnumerator SetMoneyTextRedForSeconds(float duration)
    {
        moneyText.color = Color.red;
        yield return new WaitForSeconds(duration);
        moneyText.color = originalColor;
    }
}
