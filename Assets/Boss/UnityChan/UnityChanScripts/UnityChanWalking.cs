using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnityChanWalking : MonoBehaviour {
    private GameObject player;
    private Animator animator;
	// Use this for initialization
	void Start () {
        player = GameObject.Find("Player");
        animator = this.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        float dist = Vector3.Distance(transform.position, player.transform.position);
        transform.LookAt(player.transform);
        transform.Translate(Vector3.forward * Time.deltaTime * 5);
        animator.SetFloat("distance", dist);

        if (dist < 2)
        {
            transform.gameObject.AddComponent<UnityChanAttacking>();
            Destroy(this);
        }
    }
}
