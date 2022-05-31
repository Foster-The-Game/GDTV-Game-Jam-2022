using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    CircleCollider2D myBodyCollider;

    bool isAlive = true;
    public bool bank = false;
    float reloadTime = 1f;
    private static int soulsForBank;
    private static float directX;
    private static float directY;
    public float scytheSpeed = 10f;


    [SerializeField] GameObject scythe;
    [SerializeField] Transform attackPoint;

    public float currentHealth;
    int attackDamage = 1;

    void Start()
    {
        myBodyCollider = GetComponent<CircleCollider2D>();
    }

    void Update()
    {
        soulsForBank = GameSession.soulsCount;
        currentHealth = GameSession.soulsCount;
        if (!isAlive) { return; }

        if (myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Hazard")))
        {
            Die();
        }


        if (Input.GetButtonDown("Fire1"))
        {
            if (!bank)
            {
                Attack();
            }
            else
            {
                Bank();
            }

        }
    }

    void Bank()
    {
        FindObjectOfType<GameSession>().BankSouls(soulsForBank);
        //FindObjectOfType<GameSession>().TakeSouls(soulsForBank);
    }

    public void Attack()
    {   
        directX = Player_Movement.movementX;
        directY = Player_Movement.movementY;
        Instantiate(scythe, attackPoint.position, transform.rotation);
        scythe.GetComponent<Rigidbody2D>().velocity = new Vector2(directX * scytheSpeed, directY * scytheSpeed);
    }
    

    public void TakeDamage(int attackDamage)
    {


        //if (currentHealth > 3)
        //{
            FindObjectOfType<GameSession>().TakeSouls(attackDamage);
        //}
    }

    void Die()
    {

        isAlive = false;
        this.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        StartCoroutine(ReloadLevel());


        IEnumerator ReloadLevel()
        {
            yield return new WaitForSecondsRealtime(reloadTime);
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(currentSceneIndex);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Contains("PlatformMoving"))
        {
            this.transform.parent = collision.transform;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.name.Contains("PlatformMoving"))
        {
            this.transform.parent = null;
        }
    }
}
