using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActivateBuff : MonoBehaviour {
    private Player player;
    public GameObject bullet;
    public Text buffText;

    private Bullet bulletScript;
    private Renderer bulletRenderer;
	// Use this for initialization
	void Start () {
        player = GameObject.Find("Player").GetComponent<Player>();
        bulletScript = bullet.GetComponent<Bullet>();
        bulletRenderer = bullet.GetComponent<Renderer>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    
    public void ActBuff(string buff)
    {
        switch(buff)
        {
            case "Health":
                player.HP += 2;
                buffText.text = "Found a heart!";
                StartCoroutine(SendMessage());
                break;

            case "Attack":
                player.startingPower += 10;
                bulletRenderer.sharedMaterial.color = Color.red;
                buffText.text = "Damage increased by 10!";
                StartCoroutine(SendMessage());
                break;

            case "Freeze":
                player.isFreeze = true;
                bulletRenderer.sharedMaterial.color = Color.white;
                buffText.text = "Freezing shots!";
                StartCoroutine(SendMessage());
                break;

            case "Poison":
                player.isPoison = true;
                bulletRenderer.sharedMaterial.color = Color.green;
                buffText.text = "Poison shots!";
                StartCoroutine(SendMessage());
                break;

            case "Homing":
                player.isHoming = true;
                bulletRenderer.sharedMaterial.color = Color.magenta;
                buffText.text = "Homing shots!";
                StartCoroutine(SendMessage());
                break;

            case "BossKey":
                player.hasBossKey = true;
                buffText.text = "You find an old key...";
                StartCoroutine(SendMessage());
                break;

            case "AtkSpeed":
                player.attackSpeed -= 10;
                buffText.text = "Attack speed increased!";
                StartCoroutine(SendMessage());
                break;

            case "Distance":
                player.bulletFlightDistance += 5;
                buffText.text = "Bullet flight distance increased!";
                StartCoroutine(SendMessage());
                break;

            case "Nuke":
                player.numNuke += 1;
                buffText.text = "You got a nuke! Press I to use it";
                StartCoroutine(SendMessage());
                break;

            case "Star":
                player.stars += 1;
                buffText.text = "You got a star! Press I to use it";
                StartCoroutine(SendMessage());
                break;
        }
    }

    IEnumerator SendMessage()
    {
        yield return new WaitForSeconds(3);
        buffText.text = "";
    }
}
