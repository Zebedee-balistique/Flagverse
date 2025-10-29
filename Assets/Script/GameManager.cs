using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject flagPrefab;

    public Profile player1Profile;
    public Profile player2Profile;

    public Transform spawn1;
    public Transform spawn2;
    public Transform flagSpawn1;
    public Transform flagSpawn2;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameObject p1 = Instantiate(playerPrefab, spawn1.position, Quaternion.identity);
        var controller1 = p1.GetComponent<PlayerController>();
        controller1.Profile = player1Profile;
        p1.name = "Player1";

        GameObject p2 = Instantiate(playerPrefab, spawn2.position, Quaternion.identity);
        var controller2 = p2.GetComponent<PlayerController>();
        controller2.Profile = player2Profile; 
        p2.name = "Player2";

        GameObject f1 = Instantiate(flagPrefab, flagSpawn1.position, Quaternion.identity);
        var manager1 = f1.GetComponent<FlagManager>();
        manager1.Profile = player1Profile;
        f1.name = "Flag1";

        GameObject f2 = Instantiate(flagPrefab, flagSpawn2.position, Quaternion.identity);
        var manager2 = f2.GetComponent<FlagManager>();
        manager2.Profile = player1Profile;
        f2.name = "Flag2"; 

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
