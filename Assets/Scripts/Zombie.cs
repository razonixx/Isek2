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
    private int dot;
    private int currentHP;
    private Player player;

    // Use this for initialization
    void Start()
    {
        animator = this.GetComponent<Animator>();
        animator.SetFloat("Zombie Speed Multiplier", speed);
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = onHitSound;
        dot = 0;
        currentHP = HP;
        player = GameObject.Find("Player").GetComponent<Player>();

    }

    // Update is called once per frame
    void Update ()
    {
		if(HP <= 0)
        {
            player.score += 250;
            if (this.gameObject.transform.parent.GetComponentInParent<ManageDoor>() != null)
                this.gameObject.transform.parent.GetComponentInParent<ManageDoor>().enemies.Remove(this.gameObject);
            Destroy(this.gameObject);
        }
        if(currentHP != HP)
        {
        }

        if(poisoned)
        {
            StartCoroutine(DOT());
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
            if (!other.gameObject.GetComponent<Player>().isStar)
            {
                player.HP -= 2;
            }
        }
    }

    IEnumerator DOT()
    {
        while(poisoned) {
            HP -= 1;
            dot += 1;
            if(!(dot <= 25))
            {
                poisoned = false;
                dot = 0;
            }
            yield return new WaitForSecondsRealtime(5f);
        }

    }
}