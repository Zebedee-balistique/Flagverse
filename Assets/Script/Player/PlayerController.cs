using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class PlayerController : MonoBehaviour
{
    //PUBLIC VARIABLES

    public Profile Profile; //To distinguish Player 1 from Player 2
    public float speed = 5f;

    public static event Action<PlayerController> OnPlayerWin; //Action for the end of the game


    //PRIVATE VARIABLES

    //Control handling
    private PlayerInputActions m_inputActions; //Get a local instance of the Input Actions
    private InputActionMap m_actionMap; //Distinguish Player 1's moveset from Player 2's
    private InputAction m_actionMove;
    private Vector2 m_moveInput;
    private Rigidbody2D m_rb;


    //Flag handling
    private bool m_eFlag; //Whether or not the player has the ennemy flag
    private bool m_sFlag; //Whether or not the player has its flag


    private void Awake()
    {
        m_rb = GetComponent<Rigidbody2D>();
    }

    private void OnDisable()
    {
        if (m_actionMap == null) return;

        m_actionMove.performed -= OnMove;
        m_actionMove.canceled -= OnMove;
        m_actionMap.Disable();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_inputActions = new PlayerInputActions();
        m_eFlag = false;
        m_sFlag = false;

        switch (Profile.ProfileNumber)
        {
            case 1:
                m_actionMap = m_inputActions.Player1;
                m_actionMove = m_inputActions.Player1.Move;
                break;
            
            case 2:
                m_actionMap = m_inputActions.Player2;
                m_actionMove = m_inputActions.Player2.Move;
                break;
            
            default:
                Debug.LogWarning("Wrong profile number, can't use");
                break;
        }

        if (m_actionMap == null) return;

        Debug.Log("Action Map activated");
        m_actionMap.Enable();
        m_actionMove.performed += OnMove;
        m_actionMove.canceled += OnMove;
    }

    // FixedUpdate is called once per tick of time to avoid framerate related issues
    void FixedUpdate()
    {
        m_rb.linearVelocity = m_moveInput * speed;
    }

    
    //Gets intel from the Player Action Map Move section
    private void OnMove(InputAction.CallbackContext ctx)
    {
        m_moveInput = ctx.ReadValue<Vector2>();
        //Debug.Log("OnMove Activated");
    }

    private void OnTriggerEnter2D(Collider2D Collision)
    {   

        GameObject collider = Collision.gameObject;
        Debug.Log("Entered in " + collider.name);

        if (collider.tag == "Location Tag")
        {
            Debug.Log("Encountered a location");
            LocationIdentifiers identifier = collider.GetComponent<LocationIdentifiers>();

            if (identifier == null)
            {
                Debug.Log("Location without identifier");
                return;
            }

            switch(identifier.Type)
            {
                case LocationType.FlagPole:
                    FlagPoleCollision(collider);
                    break;
                
                default:
                    break;
            }
        }
    }

    private void FlagPoleCollision(GameObject flagPole)
    {
        Debug.Log("Collision with a flag pole");
        
        FlagPoleManager manager = flagPole.GetComponent<FlagPoleManager>();

        if (manager == null)
        {
            Debug.Log("No manager on Flag Pole");
            return;
        }

        if (Profile.ProfileNumber == manager.Profile.ProfileNumber) //Self Flag Pole
        {
            if(m_sFlag) //I carry my flag
            {
                m_sFlag = false; //I put my flag
                manager.PutFlag(); //I tell the FlagPole
            }

            if(m_eFlag) //I carry the enemy flag
            {
                OnPlayerWin?.Invoke(this);
            }
        }
        else
        {
            if(!m_eFlag) //I don't have the enemy flag yet
            {
                m_eFlag = true;
                manager.TakeFlag();
            }
        }
    }

    public void DisableInputs()
    {
        m_actionMap.Disable();
    }
}
