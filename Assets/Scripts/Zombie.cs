using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour {


    public float speed;
    public int HP;
    public AudioClip onHitSound;
    private AudioSource audioSource;
    public Animator animator;
    public bool poisoned;

    // Use this for initialization
    void Start()
    {
        animator = this.GetComponent<Animator>();
        animator.SetFloat("Zombie Speed Multiplier", speed);
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = onHitSound;
    }
	
	// Update is called once per frame
	void Update ()
    {
		if(HP <= 0)
        {
            this.gameObject.transform.parent.GetComponentInParent<ManageDoor>().enemies.Remove(this.gameObject);
            Destroy(this.gameObject);
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Tear")
        {
            audioSource.Play();
        }
        //Debug.Log("Entered collider of: " + other.gameObject.tag);
        if (other.gameObject.tag == "Wall")
        {
            Vector3 rotation = transform.rotation.eulerAngles;
            transform.rotation = Quaternion.Euler(new Vector3(rotation.x, rotation.y + 180, rotation.z));
        }
        if(other.gameObject.tag == "Player")
        {
            GameObject.Find("Player").GetComponent<Player>().HP -= 30;
            GameObject.Find("Player").GetComponent<Player>().UpdateHPText();
        }
    }
}