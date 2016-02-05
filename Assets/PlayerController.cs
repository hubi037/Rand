using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour 
{

	// Use this for initialization
	void Awake () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
        float vertical = Input.GetAxis("vertical");
        float horizontal = Input.GetAxis("horizontal");

        transform.position = transform.position + new Vector3(horizontal, vertical);
	}
}
