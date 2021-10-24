using System.Collections;
using System.Collections.Generic;
using UnityEngine; 
public class PlayerControllerRigidBody : MonoBehaviour
{
    public PlayGoundManager manager;

    Rigidbody rb;
    public float speed = 2f;
    public float RotationSpeed = 30f;
    float newRotY = 0;
    public float jumpPower = 3;
    public GameObject PrefabBellet;
    public GameObject gunPosition;

    public bool hasGun;
    public float gunPower;
    public float gunCooldown=1f;
    public float gunCooldownCount;
    public int bulletCount = 0;
    public int coinCount = 0;

    public AudioSource audioCoin;
    public AudioSource audioFire;


    public float ver;
    public float hor;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if(manager == null)
        {
            manager = FindObjectOfType<PlayGoundManager>();
        }
        if(PlayerPrefs.HasKey("CoinCount"))
        {
            coinCount = PlayerPrefs.GetInt("CoinCount");
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        /*
        if (Input.GetKey(KeyCode.UpArrow))
        {
            rb.AddForce(0, 0, -speed, ForceMode.VelocityChange);
            newRotY = 0;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            rb.AddForce(0, 0, speed,ForceMode.VelocityChange);
            newRotY = -180;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            rb.AddForce(speed, 0, 0, ForceMode.VelocityChange);
            newRotY = -90;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            rb.AddForce(-speed, 0, 0, ForceMode.VelocityChange);
            newRotY = 90;
        }
        if (Input.GetButtonDown("Jump"))
        {
            rb.AddForce(0, jumpPower, 0, ForceMode.Impulse);
        }*/

        float horizontal = Input.GetAxis("Horizontal") * speed;
        float vertical = Input.GetAxis("Vertical") * speed;
        rb.AddForce(-horizontal,0,-vertical,ForceMode.VelocityChange);
        if(horizontal > 0)
        {
            newRotY = 90;
        }
        else if(horizontal < 0)
        {
            newRotY = -90;
        }

        if(vertical > 0)
        {
            newRotY = 0;
        }
        else if (vertical < 0)
        {
            newRotY = 180;
        }

        if (Input.GetButtonDown("Fire1") && bulletCount > 0 && (gunCooldownCount >= gunCooldown))
        {
            gunCooldownCount = 0;
            bulletCount--;
            audioFire.Play();
            manager.SetTextBullet(bulletCount);
            GameObject bullet = Instantiate(PrefabBellet, gunPosition.transform.position, gunPosition.transform.rotation);
            bullet.GetComponent<Rigidbody>().AddForce(transform.forward * -gunPower, ForceMode.Impulse);
            /*Rigidbody bRb = bullet.GetComponent<Rigidbody>();
            bRb.AddForce(transform.forward * gunPower, ForceMode.Impulse);*/
            Destroy(bullet,3f);
            
        }
            gunCooldownCount += Time.fixedDeltaTime;
        

        transform.rotation = Quaternion.Lerp(
                                                Quaternion.Euler(0, newRotY, 0),
                                                transform.rotation,
                                                Time.deltaTime * RotationSpeed);
    }

    private void OnTriggerEnter(Collider other)
    {
        print(other.gameObject.name);
        if (other.gameObject.tag == "Collectable")
        {
            Destroy(other.gameObject);
            coinCount++;
            manager.SetTextCoin(coinCount);
            audioCoin.Play();
            PlayerPrefs.SetInt("CoinCount", coinCount);
        }
        if (other.gameObject.name == "Gun Trigger")
        {
            hasGun = true;
            bulletCount += 10;
            Destroy(other.gameObject);
            manager.SetTextBullet(bulletCount);

        }
    }
}
