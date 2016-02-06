using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour 
{

    public float m_fSpeed = 1;

    public Vector3 m_down;

    public bool m_bHorizontal = true;

    public KeyCode m_KeyUp;
    public KeyCode m_KeyDown;
    public KeyCode m_KeyRight;
    public KeyCode m_KeyLeft;
    public KeyCode m_KeyAttack;

    public float m_jumpTime = 1;
    public AnimationCurve m_jumpAnim;

    public GameObject m_Sword;

    public Transform m_SwordRight;
    public Transform m_SwordLeft;
    public Transform m_attackPosLeft;
    public Transform m_attackPosRight;


    private bool m_bJumping = false;
    private bool m_bDucking = false;

    private Vector3 m_curPos;

    private Animator m_animator;
    private SpriteRenderer m_sprite;

    private bool m_bDead = false;

	// Use this for initialization

	void Awake () 
    {
        m_animator = GetComponent<Animator>();
        m_sprite = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () 
    {

        if (m_bDead)
            return;

        Vector3 velocity = Vector3.zero;

        if (m_bHorizontal)
        {
            if (Input.GetKey(m_KeyLeft))
            {
                velocity += transform.right * -1;
                m_sprite.flipX = true;
            }
            if (Input.GetKey(m_KeyRight))
            {
                velocity += transform.right;
                m_sprite.flipX = false;
            }

            if (Input.GetKey(m_KeyUp) && !m_bJumping && !m_bDucking)
            {
                if (m_down == Vector3.down)
                    StartCoroutine(doJump());
                else if (m_down == Vector3.up)
                    StartCoroutine(doDuck());

            }
            if (Input.GetKey(m_KeyDown) && !m_bJumping && !m_bDucking)
            {
                if (m_down == Vector3.up)
                    StartCoroutine(doJump());
                else if (m_down == Vector3.down)
                    StartCoroutine(doDuck());
            }
        }
        else
        {
            if (Input.GetKey(m_KeyUp))
            {
                velocity += transform.right * -1;
                m_sprite.flipX = true;
            }
            if (Input.GetKey(m_KeyDown))
            {
                velocity += transform.right;
                m_sprite.flipX = false;
            }

            if (Input.GetKey(m_KeyRight) && !m_bJumping && !m_bDucking)
            {
                if (m_down == Vector3.left)
                    StartCoroutine(doJump());
                else if (m_down == Vector3.right)
                    StartCoroutine(doDuck());
            }
            if (Input.GetKey(m_KeyLeft) && !m_bJumping && !m_bDucking)
            {
                if (m_down == Vector3.right)
                    StartCoroutine(doJump());
                else if (m_down == Vector3.left)
                    StartCoroutine(doDuck());
            }
        }

        if (!m_bDucking && Input.GetKeyDown(m_KeyAttack))
        {
            m_animator.SetTrigger("PlayerAttack");
            StartCoroutine(moveSword());

        }

        m_curPos = transform.position + velocity.normalized * Time.deltaTime * m_fSpeed;

        if(!m_bDucking)
            GetComponent<Rigidbody2D>().MovePosition(transform.position + velocity.normalized * Time.deltaTime * m_fSpeed);

        if (velocity.magnitude > 0.1)
            m_animator.SetBool("PlayerWalk", true);
        else
            m_animator.SetBool("PlayerWalk", false);

	}

    IEnumerator doJump()
    {
        m_bJumping = true;

        m_animator.SetTrigger("PlayerJump");

        float time = 0;

        Vector3 startPos = transform.position;

        while (time < m_jumpTime)
        {

            Vector3 newPos = m_curPos;

            if(m_bHorizontal)
                newPos.y = (startPos + m_down * -1 * m_jumpAnim.Evaluate(time / m_jumpTime)).y;
            else
                newPos.x = (startPos + m_down * -1 * m_jumpAnim.Evaluate(time / m_jumpTime)).x;

            GetComponent<Rigidbody2D>().MovePosition(newPos);

            time += Time.deltaTime;

            yield return 0;
        }

        m_bJumping = false;
    }

    IEnumerator doDuck()
    {
        m_bDucking = true;

        m_animator.SetTrigger("PlayerJump");

        yield return new WaitForSeconds(1.0f);

        m_bDucking = false;
    }

    IEnumerator moveSword()
    {
        m_Sword.GetComponent<BoxCollider2D>().enabled = true;

        float time = 0;

        Vector2 startOffset = GetComponent<BoxCollider2D>().offset;

        while (time < 0.3f)
        {
            time += Time.deltaTime;

            if (m_sprite.flipX)
            {
                GetComponent<BoxCollider2D>().offset = startOffset + new Vector2(Vector3.Lerp(Vector3.zero, m_attackPosLeft.localPosition, time / 0.3f).x, 0);
            }
            else
            {
                GetComponent<BoxCollider2D>().offset = startOffset + new Vector2(Vector3.Lerp(Vector3.zero, m_attackPosRight.localPosition, time / 0.3f).x, 0);
            }

            yield return 0;
        }


        Vector3 startPos = m_Sword.transform.localPosition;
        time = 0;
        while (time < 0.4f)
        {

            time += Time.deltaTime;

            if (m_sprite.flipX)
            {
                m_Sword.transform.Rotate(Vector3.forward, Time.deltaTime * 250f);
                m_Sword.transform.localPosition = Vector3.Lerp(startPos, m_SwordLeft.localPosition, time / 0.4f);
            }
            else
            {
                m_Sword.transform.Rotate(Vector3.forward, Time.deltaTime * 250f * -1);
                m_Sword.transform.localPosition = Vector3.Lerp(startPos, m_SwordRight.localPosition, time / 0.4f);
            }


            yield return 0;
        }

        m_Sword.GetComponent<BoxCollider2D>().enabled = false;

        m_Sword.transform.localPosition = startPos;
        m_Sword.transform.localRotation = Quaternion.identity;


        time = 0;

        while (time < 0.3f)
        {
            time += Time.deltaTime;

            if (m_sprite.flipX)
            {
                GetComponent<BoxCollider2D>().offset = startOffset + new Vector2(Vector3.Lerp(m_attackPosLeft.localPosition, Vector3.zero, time / 0.3f).x, 0);
            }
            else
            {
                GetComponent<BoxCollider2D>().offset = startOffset + new Vector2(Vector3.Lerp(m_attackPosRight.localPosition, Vector3.zero, time / 0.3f).x, 0);
            }

            yield return 0;
        }
    }

    public void Die()
    {

        if (m_bDucking)
            return;

        m_bDead = true;

        StopAllCoroutines();
        m_animator.SetBool("PlayerDie", true);

        GetComponent<BoxCollider2D>().enabled = false;
        m_Sword.GetComponent<BoxCollider2D>().enabled = false;
    }
}
