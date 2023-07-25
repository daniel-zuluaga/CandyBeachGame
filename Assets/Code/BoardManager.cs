using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    public static BoardManager sharedInstance;
    public List<GameObject> prefabs = new ();
    public GameObject currentBackground;
    [SerializeField] private GameObject parentObjCandies;
    public int xSize, ySize;
    public float paddingX, paddingY;

    private GameObject currentCandy;
    private int _idx = 1;

    [SerializeField] private GameObject[,] candies;

    public bool isShifting { get; set; }

    private void Start()
    {
        CreateInstance();
    }

    private void CreateInstance()
    {
        if (sharedInstance == null)
        {
            sharedInstance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        Vector2 offset = currentBackground.GetComponent<SpriteRenderer>().size;
        offset.x += paddingX;
        offset.y += paddingY;
        CreateInitialBoard(offset);
    }

    private void CreateInitialBoard(Vector2 offset)
    {
        candies = new GameObject[xSize, ySize];

        float startX = transform.position.x;
        float startY = transform.position.y;

        for (int x = 0; x < xSize; x++)
        {
            for (int y = 0; y < ySize; y++)
            {
                GameObject newCandy = Instantiate(currentBackground,
                    new Vector3(startX + (offset.x * x),
                    startY + (offset.y * y), 0),
                    currentBackground.transform.rotation
                );
                newCandy.name = string.Format("Candy[{0}]-[{1}]", x, y);

                //var validateHoriVerti = (x > 0 && _idx == candies[x - 1, y].GetComponent<Candy>().idCandy);
                //var validateVertiHori = (y > 0 && _idx == candies[x, y - 1].GetComponent<Candy>().idCandy);

                _idx = Random.Range(0, prefabs.Count);

                currentCandy = RandomCandy(newCandy, _idx);
                candies[x, y] = currentCandy;

                candies[x, y].transform.parent = parentObjCandies.transform;
            }
        }
    }

    private GameObject RandomCandy(GameObject currentObject, int idx)
    {
        currentObject.transform.parent = transform;


        currentCandy = prefabs[idx];

        GameObject objectCandy = Instantiate(currentCandy,
            currentObject.transform.position, Quaternion.identity, parentObjCandies.transform);

        Candy objCandy = objectCandy.GetComponent<Candy>();

        objCandy.idCandy = idx;

        return objectCandy;
    }
}
