using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    [Header("General")]
    public static BoardManager sharedInstance;
    public List<GameObject> prefabs = new ();
    public GameObject currentBackground;
    public int xSize, ySize;
    public float paddingX, paddingY;

    private int _idx = 1;

    [SerializeField] private GameObject[,] candies;

    private GameObject currentCandy;
    [SerializeField] private GameObject selectedCandy;
    [SerializeField] private GameObject parentObjCandies;

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
                    currentBackground.transform.rotation, transform
                );
                newCandy.name = string.Format("Candy[{0}]-[{1}]", x, y);

                currentCandy = RandomCandy(newCandy, x, y);
                candies[x, y] = currentCandy;
            }
        }
    }

    private GameObject RandomCandy(GameObject currentObject, int x, int y)
    {
        do
        {
            _idx = Random.Range(0, prefabs.Count);
        } while ((x > 0 && _idx == candies[x - 1, y].GetComponent<Candy>().idCandy) ||
                (y > 0 && _idx == candies[x, y - 1].GetComponent<Candy>().idCandy));

        currentCandy = prefabs[_idx];

        GameObject objectCandy = Instantiate(currentCandy,
            currentObject.transform.position, Quaternion.identity, parentObjCandies.transform);

        Candy objCandy = objectCandy.GetComponent<Candy>();

        objCandy.idCandy = _idx;

        return objectCandy;
    }
}
