using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


//TODO: Arreglar el Dual wield

public class Player : MonoBehaviour
{
    public Font myFont;
    public Texture fullHeart;
    public Texture halveHeart;
    public Texture emptyHeart;
    public int HP = 8;
    public int score = 0;
    public float playerSpeed;
    public GameObject bullet;
    public float attackSpeed;
    public float bulletFlightDistance;
    public float CamR = 0.4f;
    public int numNuke = 0;
    public int stars = 0;
    private bool isDualWeilding = false;
    public bool isFreeze = false;
    public bool isPoison = false;
    public bool isHoming = false;
    public bool hasBossKey = false;
    public GameObject closestEnemy;
    public GameObject currentRoom;
#pragma warning disable CS0108 // Member hides inherited member; missing new keyword
    public GameObject eyes;
    public Camera playerCamera;
    public Camera mapCamera;
    public Text HPText;
    public AudioClip audioClip;
    public int startingPower = 20;
    public bool[] buffs;
    private AudioSource audioSource;
    private bool togglePause = false;
    private bool isDead = false;
    private bool toggleInventory = false;
    public bool isStar = false;
    private bool isShooting = false;
    private float hCamera = 0.0f;
    private Bullet bulletScript;

    // Use this for initialization
    void Start ()
    {
        playerCamera.enabled = true;
        mapCamera.enabled = false;
        bullet.gameObject.GetComponent<Renderer>().sharedMaterial.color = Color.blue;
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = audioClip;
        audioSource.Play();
        bulletScript = bullet.GetComponent<Bullet>();
        Cursor.lockState = CursorLockMode.Locked;

    }

    // Update is called once per frame
    void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !isDead && !toggleInventory)
        {
            togglePause = !togglePause;
        }
        if (Input.GetKeyDown(KeyCode.I) && !isDead && !togglePause)
        {
            toggleInventory = !toggleInventory;
        }
        if (!togglePause && !isDead && !toggleInventory)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            if (hasBossKey)
            {
                audioSource.Stop();
            }
            if (Input.GetMouseButton(0) && !isShooting)
            {
                Shoot();
                StartCoroutine(ShootCR());
                isShooting = true;
            }
            if (Input.GetMouseButtonUp(0) && isShooting)
            {
                StopAllCoroutines();
                isShooting = false;
            }
            if (Input.GetKeyDown(KeyCode.M))
            {
                playerCamera.enabled = !playerCamera.enabled;
                mapCamera.enabled = !mapCamera.enabled;
            }
            SetPlayerPosition();
            hCamera += CamR * Input.GetAxis("Mouse X");
            transform.eulerAngles = new Vector3(0.0f, hCamera, 0.0f);
            mapCamera.transform.position = new Vector3(this.transform.position.x, mapCamera.transform.position.y, this.transform.position.z);
            Cursor.lockState = CursorLockMode.Locked;
            if(HP <= 0)
            {
                isDead = true;
            }
        }
        if(togglePause && Input.GetKey(KeyCode.U) && Input.GetKey(KeyCode.N) && Input.GetKey(KeyCode.C))
        {
            SceneManager.LoadScene("Locomotion");
        }
    }
    void SetPlayerPosition()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        transform.Translate(new Vector3(horizontal * Time.deltaTime * playerSpeed, 0, vertical * Time.deltaTime * playerSpeed));
    }

    void CreateBullet()
    {
        if (isHoming && currentRoom != null)
        {
            float distance = 100;
            List<GameObject> enemies = currentRoom.transform.gameObject.GetComponent<ManageDoor>().enemies;
            foreach (GameObject enemy in enemies)
            {
                float distanceToEnemy = Vector3.Distance(this.transform.position, enemy.transform.position);
                if (distance > distanceToEnemy)
                {
                    distance = distanceToEnemy;
                    closestEnemy = enemy;
                }
            }
        }
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

    private void OnGUI()
    {
        Overlay();

        HandleHearts();

        if (togglePause)
        {
            PauseMenu();
        }

        if (isDead)
        {
            DeathMenu();
        }

        if (toggleInventory)
        {
            Inventory();
        }
    }

    private void Overlay()
    {
        GUIStyle overlayText = new GUIStyle(GUI.skin.label);
        overlayText.font = myFont;
        overlayText.fontSize = 35;

        if (isStar)
        {
            GUI.Label(new Rect(Screen.width / 4, Screen.height / 2 - 100, 400, 150), "You are invincible!", overlayText);
        }

        //GUI.Label(new Rect(150, 30, 200, 50), "HP: " + HP, overlayText);
        GUI.Label(new Rect(Screen.width - 200, 30, 200, 150), "Score       " + score, overlayText);
    }
    private void HandleHearts()
    {
        switch(HP)
        {
            case 1:
                GUI.DrawTexture(new Rect(30, 30, 30, 30), halveHeart);
                break;
            case 2:
                GUI.DrawTexture(new Rect(30, 30, 30, 30), fullHeart);
                break;
            case 3:
                GUI.DrawTexture(new Rect(30, 30, 30, 30), fullHeart);
                GUI.DrawTexture(new Rect(65, 30, 30, 30), halveHeart);
                break;
            case 4:
                GUI.DrawTexture(new Rect(30, 30, 30, 30), fullHeart);
                GUI.DrawTexture(new Rect(65, 30, 30, 30), fullHeart);
                break;
            case 5:
                GUI.DrawTexture(new Rect(30, 30, 30, 30), fullHeart);
                GUI.DrawTexture(new Rect(65, 30, 30, 30), fullHeart);
                GUI.DrawTexture(new Rect(100, 30, 30, 30), halveHeart);
                break;
            case 6:
                GUI.DrawTexture(new Rect(30, 30, 30, 30), fullHeart);
                GUI.DrawTexture(new Rect(65, 30, 30, 30), fullHeart);
                GUI.DrawTexture(new Rect(100, 30, 30, 30), fullHeart);
                break;
            case 7:
                GUI.DrawTexture(new Rect(30, 30, 30, 30), fullHeart);
                GUI.DrawTexture(new Rect(65, 30, 30, 30), fullHeart);
                GUI.DrawTexture(new Rect(100, 30, 30, 30), fullHeart);
                GUI.DrawTexture(new Rect(135, 30, 30, 30), halveHeart);
                break;
            case 8:
                GUI.DrawTexture(new Rect(30, 30, 30, 30), fullHeart);
                GUI.DrawTexture(new Rect(65, 30, 30, 30), fullHeart);
                GUI.DrawTexture(new Rect(100, 30, 30, 30), fullHeart);
                GUI.DrawTexture(new Rect(135, 30, 30, 30), fullHeart);
                break;
            case 9:
                GUI.DrawTexture(new Rect(30, 30, 30, 30), fullHeart);
                GUI.DrawTexture(new Rect(65, 30, 30, 30), fullHeart);
                GUI.DrawTexture(new Rect(100, 30, 30, 30), fullHeart);
                GUI.DrawTexture(new Rect(135, 30, 30, 30), fullHeart);
                GUI.DrawTexture(new Rect(170, 30, 30, 30), halveHeart);
                break;
            case 10:
                GUI.DrawTexture(new Rect(30, 30, 30, 30), fullHeart);
                GUI.DrawTexture(new Rect(65, 30, 30, 30), fullHeart);
                GUI.DrawTexture(new Rect(100, 30, 30, 30), fullHeart);
                GUI.DrawTexture(new Rect(135, 30, 30, 30), fullHeart);
                GUI.DrawTexture(new Rect(170, 30, 30, 30), fullHeart);
                break;
            case 11:
                GUI.DrawTexture(new Rect(30, 30, 30, 30), fullHeart);
                GUI.DrawTexture(new Rect(65, 30, 30, 30), fullHeart);
                GUI.DrawTexture(new Rect(100, 30, 30, 30), fullHeart);
                GUI.DrawTexture(new Rect(135, 30, 30, 30), fullHeart);
                GUI.DrawTexture(new Rect(170, 30, 30, 30), fullHeart);
                GUI.DrawTexture(new Rect(205, 30, 30, 30), halveHeart);
                break;
            case 12:
                GUI.DrawTexture(new Rect(30, 30, 30, 30), fullHeart);
                GUI.DrawTexture(new Rect(65, 30, 30, 30), fullHeart);
                GUI.DrawTexture(new Rect(100, 30, 30, 30), fullHeart);
                GUI.DrawTexture(new Rect(135, 30, 30, 30), fullHeart);
                GUI.DrawTexture(new Rect(170, 30, 30, 30), fullHeart);
                GUI.DrawTexture(new Rect(205, 30, 30, 30), fullHeart);
                break;
        }
    }
    private void PauseMenu()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        GUIStyle boxStyle = new GUIStyle(GUI.skin.box);
        boxStyle.fontSize = 130;
        boxStyle.font = myFont;

        GUIStyle itemStyle = new GUIStyle(GUI.skin.button);
        itemStyle.fontSize = 48;
        itemStyle.font = myFont;

        GUI.Box(new Rect(0, 0, Screen.width, Screen.height), "Paused", boxStyle);

        if (GUI.Button(new Rect(Screen.width / 2 - Screen.width / 12, Screen.height / 2, 300, 60), "Resume", itemStyle))
        {
            togglePause = !togglePause;
        }
        if (GUI.Button(new Rect(Screen.width / 2 - Screen.width / 12, Screen.height / 2 + Screen.height / 6, 300, 60), "Restart", itemStyle))
        {
            SceneManager.LoadScene("Splashscreen");
        }
        if (GUI.Button(new Rect(Screen.width / 2 - Screen.width / 12, Screen.height / 2 + Screen.height / 3, 300, 60), "Quit", itemStyle))
        {
            Application.Quit();
        }
    }
    private void DeathMenu()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        GUIStyle boxStyle = new GUIStyle(GUI.skin.box);
        boxStyle.fontSize = 130;
        boxStyle.font = myFont;

        GUIStyle itemStyle = new GUIStyle(GUI.skin.button);
        itemStyle.fontSize = 48;
        itemStyle.font = myFont;

        GUI.Box(new Rect(0, 0, Screen.width, Screen.height), "You Died", boxStyle);
        if (GUI.Button(new Rect(Screen.width / 2 - Screen.width / 12, Screen.height / 2 + Screen.height / 6, 300, 60), "Restart", itemStyle))
        {
            SceneManager.LoadScene("Splashscreen");
        }
        if (GUI.Button(new Rect(Screen.width / 2 - Screen.width / 12, Screen.height / 2 + Screen.height / 3, 300, 60), "Quit", itemStyle))
        {
            Application.Quit();
        }
    }
    private void Inventory()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        GUIStyle boxStyle = new GUIStyle(GUI.skin.box);
        boxStyle.fontSize = 130;
        boxStyle.font = myFont;

        GUIStyle itemStyle = new GUIStyle(GUI.skin.button);
        itemStyle.fontSize = 48;
        itemStyle.font = myFont;

        GUI.Box(new Rect(0, 0, Screen.width, Screen.height), "Inventory", boxStyle);

        
        if (GUI.Button(new Rect(Screen.width / 36, Screen.height / 2 - Screen.height / 4, 1825, 60), "Nuke: Kills all enemies in the current room. You have: " + numNuke + " nukes.", itemStyle)
            && numNuke > 0)
        {
            int killed = 0;
            List<GameObject> enemies = currentRoom.transform.gameObject.GetComponent<ManageDoor>().enemies;
            foreach (GameObject enemy in enemies)
            {
                Destroy(enemy);
                killed++;
            }
            currentRoom.transform.gameObject.GetComponent<ManageDoor>().enemyCount = 0;
            if (killed != 0)
            {
                numNuke--;
            }
        }
        if (GUI.Button(new Rect(Screen.width / 36, Screen.height / 2 + Screen.height / 4, 1825, 60), "Star: Makes you invulnerable for 10 seconds. You have: " + stars + " stars.", itemStyle)
            && stars > 0)
        {
            isStar = true;
            StartCoroutine(StartStar());
            stars--;
        }
    }

    IEnumerator ShootCR()
    {
        while(true)
        {
            yield return new WaitForSeconds(.01f * attackSpeed);
            Shoot();
        }
    }

    IEnumerator StartStar()
    {
        while (true)
        {
            yield return new WaitForSeconds(10);
            isStar = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == 8 && !isStar)
        {
            HP -= 2;
        }
    }
}
