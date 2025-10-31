using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject flagPolePrefab;

    public Profile player1Profile;
    public Profile player2Profile;

    public Transform spawn1;
    public Transform spawn2;
    public Transform flagSpawn1;
    public Transform flagSpawn2;

    private List<PlayerController> players;
    private UIManager UIManager;

    private void OnEnable()
    {
        PlayerController.OnPlayerWin += PlayerWin;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        players = new List<PlayerController>();
        Canvas UI = GetComponentInChildren<Canvas>();
        UIManager = UI.GetComponent<UIManager>();
        
        GameObject p1 = Instantiate(playerPrefab, spawn1.position, Quaternion.identity);
        PlayerController controller1 = p1.GetComponent<PlayerController>();
        controller1.Profile = player1Profile;
        p1.name = "Player 1";

        players.Add(controller1);

        GameObject p2 = Instantiate(playerPrefab, spawn2.position, Quaternion.identity);
        PlayerController controller2 = p2.GetComponent<PlayerController>();
        controller2.Profile = player2Profile; 
        p2.name = "Player 2";

        players.Add(controller2);

        GameObject f1 = Instantiate(flagPolePrefab, flagSpawn1.position, Quaternion.identity);
        var manager1 = f1.GetComponent<FlagPoleManager>();
        manager1.Profile = player1Profile;
        f1.name = "FlagPole1";

        GameObject f2 = Instantiate(flagPolePrefab, flagSpawn2.position, Quaternion.identity);
        var manager2 = f2.GetComponent<FlagPoleManager>();
        manager2.Profile = player2Profile;
        f2.name = "FlagPole2";

    }

    void PlayerWin(GameObject winner)
    {
        Debug.Log("Player wins");
        foreach(var player in players)
        {
            player.DisableInputs();
            UIManager.DisplayWinMessage(winner.name);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
