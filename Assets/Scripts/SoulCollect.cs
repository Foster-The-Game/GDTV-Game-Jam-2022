using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulCollect : MonoBehaviour
{
    public List<GameObject> soulActive;
    public GameObject toSpawn;
    public GameObject tileMap;
    int numberToSpawn = 0;

    [SerializeField] AudioClip soulCollectSFX;
    [SerializeField] int pointsForSouls = 1;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            FindObjectOfType<GameSession>().AddToSouls(pointsForSouls);
            AudioSource.PlayClipAtPoint(soulCollectSFX, Camera.main.transform.position);
            spawnObject();
            gameObject.SetActive(false);
            Destroy(gameObject);
        }

        if (other.tag == "Enemy")
        {
            //spawnObject();
            gameObject.SetActive(false);
            Destroy(gameObject);
        }

    }

    public void spawnObject()
    {
        CompositeCollider2D cC = tileMap.GetComponent<CompositeCollider2D>();
        float screenX, screenY;
        Vector2 pos;
        numberToSpawn = soulActive.Count;

        for (int i = 0; i < numberToSpawn; i++)
        {
            screenX = Random.Range(cC.bounds.min.x, cC.bounds.max.x);
            screenY = Random.Range(cC.bounds.min.y, cC.bounds.max.y);
            pos = new Vector2(screenX, screenY);
            Instantiate(toSpawn, pos, toSpawn.transform.rotation);
        }

    }
}
