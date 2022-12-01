using UnityEngine;
using System.Collections.Generic;

public class BlockGenerator : MonoBehaviour
{
    [SerializeField] List<GameObject> allTheBlocks = new List<GameObject>();
    [SerializeField] int GenerateTImes;
    BoxCollider2D myBoxCollider;
    float blockSize;
    [SerializeField] LayerMask layerMask;


    private void Start()
    {
        myBoxCollider = GetComponent<BoxCollider2D>();
        blockSize = allTheBlocks[0].GetComponent<BoxCollider2D>().size.x;
        for (int i = 0; i < GenerateTImes; i++)
        {
            GenerateNewBlock();
        }
    }

    public void GenerateNewBlock()
    {
        var index = Random.Range(0, allTheBlocks.Count - 1);
        var location = GetGeneratePOS();

        while (HasObject(location))
        {
            location = GetGeneratePOS();
        }
        var block = Instantiate(allTheBlocks[index], location, Quaternion.identity);
        block.GetComponent<Block>().SetGenerator(this);
    }
    private Vector3 GetGeneratePOS()
    {
        float maxX = myBoxCollider.bounds.max.x;
        float maxY = myBoxCollider.bounds.max.y;
        float minX = myBoxCollider.bounds.min.x;
        float minY = myBoxCollider.bounds.min.y;
        float randY = Random.Range(minY + 1, maxY - 1);
        float randX = Random.Range(minX + 1, maxX - 1);
        return new Vector2(randX, randY);
    }

    private bool HasObject(Vector2 location)
    {
        Collider2D[] hitTargets = Physics2D.OverlapCircleAll(location, blockSize, layerMask);
        if (hitTargets.Length != 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}