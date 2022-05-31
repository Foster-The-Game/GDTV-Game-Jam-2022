using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hell_Bat : MonoBehaviour
{
    public Transform player;
    //public Transform soul;
    [SerializeField] private LayerMask whatIsEnemy;
    [SerializeField] private Transform attackCheck;
    public Rigidbody2D rb;
    public Animator animator;

    public float maxHealth = 1f;
    public float currentHealth;
    public float enemyDisable = 1f;
    public static int attackDamage = 4;
    public float attackRadius = .75f;
    public float playerRadius = 1.75f;
    public float soulRadius = 3f;
    public float moveSpeed = 5f;
    public float toPlayerSpeed = 40f;
    public bool upDown = false;

    public List<GameObject> enemyToSpawn;
    public GameObject toSpawn;
    public GameObject tileMap;
    int numberToSpawn = 0;

    public float minPosY;
    public float maxPosY;
    public float minPosX;
    public float maxPosX;
    public float travelDist = 5f;
    bool moveTrigger = true;

    [SerializeField] int pointsForSouls = attackDamage/2;

    void Start()
    {
        currentHealth = maxHealth;
        
        minPosY = transform.position.y - travelDist;
        maxPosY = transform.position.y + travelDist;
        minPosX = transform.position.x - travelDist;
        maxPosX = transform.position.x + travelDist;
    }

    public void TakeDamage(float attackDamage)
    {
        currentHealth -= attackDamage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Attack()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackCheck.position, attackRadius, whatIsEnemy);

        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Player>().TakeDamage(attackDamage);
        }
    }

    void Die()
    {
        //animator.SetBool("IsDying", true);
        GetComponent<Collider2D>().enabled = false;
        StartCoroutine(EnemyDisable());

    }
    IEnumerator EnemyDisable()
    {
        yield return new WaitForSecondsRealtime(enemyDisable);
        spawnObject();
        gameObject.SetActive(false);
        Destroy(gameObject);
    }

    public void spawnObject()
    {
        CompositeCollider2D cC = tileMap.GetComponent<CompositeCollider2D>();
        float screenX, screenY;
        Vector2 pos;
        numberToSpawn = enemyToSpawn.Count;

        FindObjectOfType<GameSession>().AddToSouls(pointsForSouls);

        for (int i = 0; i < numberToSpawn; i++)
        {
            screenX = Random.Range(cC.bounds.min.x, cC.bounds.max.x);
            screenY = Random.Range(cC.bounds.min.y, cC.bounds.max.y);
            pos = new Vector2(screenX, screenY);
            Instantiate(toSpawn, pos, toSpawn.transform.rotation);
        }

    }

    private void Update()
    {
        
        //if (Vector2.Distance(soul.position, rb.position) <= soulRadius)
        //{
        //    Vector2 target = new Vector2(soul.position.x, soul.position.y);
        //    Vector2 newPos = Vector2.MoveTowards(rb.position, target, moveSpeed * Time.fixedDeltaTime);
        //    rb.MovePosition(newPos);
        //}
        if (Vector2.Distance(player.position, rb.position) <= playerRadius)
        {
            Vector2 target = new Vector2(player.position.x, player.position.y);
            Vector2 newPos = Vector2.MoveTowards(rb.position, target, toPlayerSpeed * Time.fixedDeltaTime);
            rb.MovePosition(newPos);
            
        }
        else
        {
            if (upDown)
            {
                if (transform.position.y <= minPosY)
                {
                    moveTrigger = true;
                }
                if (transform.position.y >= maxPosY)
                {
                    moveTrigger = false;
                }
                if (moveTrigger)
                {
                    transform.position = new Vector2(transform.position.x, transform.position.y + moveSpeed * Time.fixedDeltaTime);
                }
                else
                {
                    transform.position = new Vector2(transform.position.x, transform.position.y - moveSpeed * Time.fixedDeltaTime);
                }
            }
            else
            {
                if (transform.position.x <= minPosX)
                {
                    moveTrigger = true;
                }
                if (transform.position.x >= maxPosX)
                {
                    moveTrigger = false;
                }
                if (moveTrigger)
                {
                    transform.position = new Vector2(transform.position.x + moveSpeed * Time.fixedDeltaTime, transform.position.y);
                }
                else
                {
                    transform.position = new Vector2(transform.position.x - moveSpeed * Time.fixedDeltaTime, transform.position.y);
                }
            }
        }
        
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackCheck.position, attackRadius);
        Gizmos.DrawWireSphere(attackCheck.position, playerRadius);
        Gizmos.DrawWireSphere(attackCheck.position, soulRadius);
    }
}
