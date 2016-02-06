using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CheckForVictory : MonoBehaviour {

    public PlayerController m_player1Up;
    public PlayerController m_player1Down;
    public PlayerController m_player1Left;
    public PlayerController m_player1Right;

    public PlayerController m_player2Up;
    public PlayerController m_player2Down;
    public PlayerController m_player2Left;
    public PlayerController m_player2Right;

    private List<PlayerController> m_player1 = new List<PlayerController>();
    private List<PlayerController> m_player2 = new List<PlayerController>();

	// Use this for initialization
	void Awake () 
    {
        m_player1.Add(m_player1Up);
        m_player1.Add(m_player1Down);
        m_player1.Add(m_player1Left);
        m_player1.Add(m_player1Right);

        m_player2.Add(m_player2Up);
        m_player2.Add(m_player2Down);
        m_player2.Add(m_player2Left);
        m_player2.Add(m_player2Right);
	}
	
	// Update is called once per frame
	void Update () 
    {

        CheckPlayer1();
        CheckPlayer2();
        
	}

    void CheckPlayer1()
    {
        foreach (PlayerController player in m_player1)
        {
            if (!player.m_bDead)
                return;
        }

        Debug.Log("Player2 wins");
    }

    void CheckPlayer2()
    {
        foreach (PlayerController player in m_player2)
        {
            if (!player.m_bDead)
                return;
        }

        Debug.Log("Player1 wins");
    }
}
