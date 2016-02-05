using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {


    public float m_fSpeed = 10;
    public Vector3 m_direction;
    public float m_maxLifeTime = 100;

    private float m_currentTime = 0;

	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
        transform.position += m_direction * Time.deltaTime * m_fSpeed;
        m_currentTime += Time.deltaTime;

        if (m_currentTime > m_maxLifeTime)
            Destroy(this.gameObject);
	}

    void OnTriggerEnter2D(Collider2D coll)
    {
        Destroy(this.gameObject);
    }
}
