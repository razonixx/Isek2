using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ActivateChest : MonoBehaviour
{

    public Transform lid, lidOpen, lidClose;    // Lid, Lid open rotation, Lid close rotation
    public float openSpeed = 5F;                // Opening speed
    public bool canClose;						// Can the chest be closed
    public string[] buffList;

    private Text text;
    private GameObject player;
    private bool buffGiven = false;
    public AudioClip audioClip;
    private AudioSource audioSource;
    public bool[] buffs;                        // Optimizar esto después. Cambiar atributo a private e igualarlo a buffs[] de jugador al principio
    private int buffNum;

    [HideInInspector]
    public bool _open;							// Is the chest open

    private void Start()
    {
        text = GameObject.Find("Interact Text").GetComponent<Text>();
        player = GameObject.Find("Player");
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = audioClip;
    }

    void Update()
    {
        if (_open)
        {
            ChestClicked(lidOpen.rotation);
        }
        else
        {
            ChestClicked(lidClose.rotation);
        }
        //Debug.Log((int)Mathf.Floor(Random.value * buffList.Length));
    }

    // Rotate the lid to the requested rotation
    void ChestClicked(Quaternion toRot)
    {
        if (lid.rotation != toRot)
        {
            lid.rotation = Quaternion.Lerp(lid.rotation, toRot, Time.deltaTime * openSpeed);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (!_open)
            {
                text.text = "Press E to open";
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (canClose) _open = !_open; else _open = true;
                if(!buffGiven)
                {
                    buffs = GameObject.Find("Player").GetComponent<Player>().buffs;
                    do
                    {
                        buffNum = (int)Mathf.Floor(Random.value * buffList.Length);
                        if (buffNum == 0 || buffNum == 1)
                            break;
                    } while (buffs[buffNum]);

                    string buff = buffList[buffNum];
                    player.GetComponent<ActivateBuff>().ActBuff(buff);
                    buffGiven = true;
                    GameObject.Find("Player").GetComponent<Player>().buffs[buffNum] = true;
                    audioSource.Play();
                }
                text.text = "";
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        text.text = "";
    }
}
