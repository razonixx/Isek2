using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//TODO: Arreglar el Dual wield

public class Player : MonoBehaviour
{
    public int HP = 100;
    public float playerSpeed;
    public GameObject bullet;
    public float attackSpeed;
    public float bulletFlightDistance;
    public float CamR = 0.4f;
    public bool isDualWeilding = false;
    public bool isFreeze = false;
    public bool isPoison = false;
    public bool hasBossKey = false;
#pragma warning disable CS0108 // Member hides inherited member; missing new keyword
    public GameObject eyes;
    public Camera playerCamera;
    public Camera mapCamera;
    public Text HPText;
    public AudioClip audioClip;
    public int startingPower = 20;
    private AudioSource audioSource;

    private bool isShooting = false;
    private float hCamera = 0.0f;
    private Bullet bulletScript;

    // Use this for initialization
    void Start ()
    {
        UpdateHPText();
        playerCamera.enabled = true;
        mapCamera.enabled = false;
        bullet.gameObject.GetComponent<Renderer>().sharedMaterial.color = Color.blue;
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = audioClip;
        audioSource.Play();
        bulletScript = bullet.GetComponent<Bullet>();
    }

    // Update is called once per frame
    void Update ()
    {
     
        SetPlayerPosition();
        if(Input.GetMouseButton(0) && !isShooting)
        {
            Shoot();
            StartCoroutine(ShootCR());
            isShooting = true;
        }
        if(Input.GetMouseButtonUp(0) && isShooting)
        {
            StopAllCoroutines();
            isShooting = false;
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            playerCamera.enabled = !playerCamera.enabled;
            mapCamera.enabled = !mapCamera.enabled;
        }

        hCamera += CamR * Input.GetAxis("Mouse X");
        transform.eulerAngles = new Vector3(0.0f, hCamera, 0.0f);
        mapCamera.transform.position = new Vector3(this.transform.position.x, mapCamera.transform.position.y, this.transform.position.z);
    }
    void SetPlayerPosition()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        transform.Translate(new Vector3(horizontal * Time.deltaTime * playerSpeed, 0, vertical * Time.deltaTime * playerSpeed));
    }

    public void UpdateHPText()
    {
        HPText.text = "Life: " + HP;
    }

    void CreateBullet()
    {
        Instantiate(bullet, eyes.transform.position, transform.rotation);
    }

    void CreateBullet(float xDisplacement)
    {
        Instantiate(bullet, eyes.transform.position + new Vector3(xDisplacement, 0, 0), transform.rotation);
    }

    void Shoot()
    {
        if (isDualWeilding)
        {
            CreateBullet(-.2f);
            CreateBullet(.2f);
        }
        else
            CreateBullet();
    }

    IEnumerator ShootCR()
    {
        while(true)
        {
            yield return new WaitForSeconds(.01f * attackSpeed);
            Shoot();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == 8)
        {
            HP -= 40;
            UpdateHPText();
        }
    }
}
