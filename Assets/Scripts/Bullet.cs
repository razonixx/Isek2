using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Player player;
    public int power;
    public bool freeze = false;
    public bool poison = false;

    // Use this for initialization
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        Destroy(this.gameObject, 2);
        GetComponent<Rigidbody>().AddRelativeForce(new Vector3(0, 0, GameObject.Find("Player").GetComponent<Player>().bulletFlightDistance), ForceMode.Impulse);
        power = player.startingPower;
        freeze = player.isFreeze;
        poison = player.isPoison;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer != 11)
        {
            switch (other.gameObject.tag)
            {
                case "Room":
                    break;
                case "Zombie":
                    other.gameObject.GetComponent<Zombie>().HP -= power;
                    if(freeze)
                    {
                        other.gameObject.GetComponent<Zombie>().animator.speed = 0;
                    }   
                    if(poison)
                    {
                        other.gameObject.GetComponent<Zombie>().poisoned = true;
                    }
                    break;
                case "Imp":
                    other.gameObject.GetComponentInChildren<Imp>().HP -= power * 100;
                    break;
                case "Skeleton":
                    other.gameObject.GetComponentInChildren<Skeleton>().HP -= power;
                    if (freeze)
                    {
                        other.gameObject.GetComponentInChildren<Skeleton>().animator.speed = 0;
                        other.gameObject.GetComponentInChildren<Skeleton>().playerIsInRoom = false;
                    }
                    break;
                case "UnityChan":
                    other.gameObject.GetComponentInChildren<UnityChan>().HP -= (power/2);
                    if (freeze)
                    {
                        other.gameObject.GetComponent<UnityChan>().animator.speed = 0;
                        other.gameObject.GetComponent<UnityChan>().playerIsInRoom = false;
                    }
                    break;
                default:
                    Debug.Log("Trigger enter with: " + other.gameObject.name);
                    break;
            }
            if (other.gameObject.tag == "Room")
            {

            }
            else
            {
                Destroy(this.gameObject);
            }
        }
    }
}
