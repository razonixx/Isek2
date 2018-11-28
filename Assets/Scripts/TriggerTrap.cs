using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerTrap : MonoBehaviour {

    public GameObject spikes;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            spikes.transform.Translate(new Vector3(0, 1f, 0));
            if(!collision.gameObject.GetComponent<Player>().isStar)
            {
                collision.gameObject.GetComponent<Player>().HP --;
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            spikes.transform.Translate(new Vector3(0, -1f, 0));
        }
    }
}
