using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{   //Config params
    [SerializeField] AudioClip[] blockBreakSound;
    [SerializeField] GameObject blockSparklesFX;
    [SerializeField] Sprite[] hitSprites;

    private BlockGenerator blockGenerator;

    // cached reference
    Level level;
    GameStatus gameStatus;

    //Seriallizefiled for the debug purpose
    [SerializeField] int timesHit;

    private void Start()
    {
        level = FindObjectOfType<Level>();
        gameStatus = FindObjectOfType<GameStatus>();
    }


    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (tag == "Breakable Block")
        {
            HandleHit();
        }

        void HandleHit()
        {
            int maxHits = hitSprites.Length + 1;
            timesHit++;
            if (timesHit >= maxHits)
            {
                DestroyBlock();
            }
            else
            {
                loadNextSprite();
            }
        }
    }

    private void loadNextSprite()
    {
        int spriteIndex = timesHit - 1;
        if (hitSprites[spriteIndex] != null)
        {
            GetComponent<SpriteRenderer>().sprite = hitSprites[spriteIndex];
        }
        else
        {
            Debug.LogError("The block sprite is missing! " + gameObject.name);
        }
    }

    private void DestroyBlock()
    {
        gameStatus.AddToScore();
        AudioSource.PlayClipAtPoint(blockBreakSound[Random.Range(0, blockBreakSound.Length)], Camera.main.transform.position);
        Destroy(gameObject);
        level.BlockDestroyed();
        TriggerSparklesFX();
        blockGenerator.GenerateNewBlock();
    }

    private void TriggerSparklesFX()
    {
        GameObject sparkles = Instantiate(blockSparklesFX, transform.position, transform.rotation);
        Destroy(sparkles, 2f);
    }

    public void SetGenerator(BlockGenerator blockGenerator)
    {
        this.blockGenerator = blockGenerator;
    }


}
