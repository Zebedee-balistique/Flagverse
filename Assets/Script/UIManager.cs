using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI PlayerWinsText;

    void Start()
    {
        PlayerWinsText.gameObject.SetActive(false);
    }

    public void DisplayWinMessage(string playerName)
    {
        PlayerWinsText.text = $"{playerName} wins!";
        PlayerWinsText.gameObject.SetActive(true);
    }
}
