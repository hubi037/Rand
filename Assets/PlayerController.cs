using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour 
{

    public float m_fSpeed = 1;

    public Vector3 m_down;

    public bool m_bHorizontal = true;

    public KeyCode m_keyUp;
    public KeyCode m_KeyDown;
    public KeyCode m_KeyRight;
    public KeyCode m_KeyLeft;

    public float m_jumpTime = 1;
    public AnimationCurve m_jumpAnim;

    public bool m_bJumping = false;

	// Use this for initialization

	void Awake () 
    {
	}
	
	// Update is called once per frame
	void Update () 
    {

        Vector3 velocity = Vector3.zero;

        if (m_bHorizontal)
        {
            if (Input.GetKey(m_KeyLeft))
                velocity += transform.right * -1;
            if (Input.GetKey(m_KeyRight))
                velocity += transform.right;

            if (Input.GetKey(m_keyUp) && m_down == Vector3.down && !m_bJumping)
            {
                StartCoroutine(doJump());
            }
            if (Input.GetKey(m_KeyDown) && m_down == Vector3.up && !m_bJumping)
            {
                StartCoroutine(doJump());
            }
        }
        else
        {
            if (Input.GetKey(m_keyUp))
                velocity += transform.up;
            if (Input.GetKey(m_KeyDown))
                velocity += transform.up * -1;
        }

        GetComponent<Rigidbody2D>().MovePosition(transform.position + velocity.normalized * Time.deltaTime * m_fSpeed);

	}

    IEnumerator doJump()
    {
        m_bJumping = true;

        float time = 0;

        Vector3 startPos = transform.position;

        while (time < m_jumpTime)
        {
            GetComponent<Rigidbody2D>().MovePosition(startPos + m_down * -1 * m_jumpAnim.Evaluate(time / m_jumpTime));

            time += Time.deltaTime;

            yield return 0;
        }

        m_bJumping = false;
    }
}
