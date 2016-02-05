using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController2 : MonoBehaviour 
{

    public float m_fSpeed = 1;
    public GameObject m_Projectile;

    private List<KeyCode> m_keys = new List<KeyCode>();

    private Vector3 m_velocity;

	// Use this for initialization

    KeyCode up;
    KeyCode down;
    KeyCode left;
    KeyCode right;
    KeyCode shoot = KeyCode.LeftShift;

	void Awake () 
    {
	    m_keys.AddRange( new KeyCode[]{KeyCode.Keypad9, KeyCode.Keypad8, KeyCode.Keypad7,
                            KeyCode.Keypad6, KeyCode.Keypad5, KeyCode.Keypad4,
                            });


        Debug.Log(m_keys.Count);
	}
	
	// Update is called once per frame
	void Update () 
    {
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");

        Vector3 velocity = Vector3.zero;

        if (Input.GetKey(up))
            velocity += Vector3.up;
        if (Input.GetKey(down))
            velocity += Vector3.down;
        if (Input.GetKey(left))
            velocity += Vector3.left;
        if (Input.GetKey(right))
            velocity += Vector3.right;

        if(velocity != Vector3.zero)
            m_velocity = velocity.normalized;

        transform.position = transform.position + velocity.normalized * Time.deltaTime * m_fSpeed;

        if (Input.GetKeyDown(KeyCode.Space))
            RandomizeKeys();

        if(Input.GetKeyDown(shoot))
        {
            GameObject projectile = Instantiate(m_Projectile, transform.position + m_velocity, Quaternion.identity) as GameObject;
            projectile.GetComponent<Projectile>().m_direction = m_velocity;
        }
	}


    void RandomizeKeys()
    {

        List<KeyCode> tempList = new List<KeyCode>();
        tempList.AddRange(m_keys);


        /*
        up = tempList[Random.Range(0, tempList.Count)];
        tempList.Remove(up);
        down = tempList[Random.Range(0, tempList.Count)];
        tempList.Remove(down);*/

        left = tempList[Random.Range(0, tempList.Count)];
        tempList.Remove(left);
        right = tempList[Random.Range(0, tempList.Count)];
        tempList.Remove(right);
        shoot = tempList[Random.Range(0, tempList.Count)];
        tempList.Remove(shoot);
    }

}
