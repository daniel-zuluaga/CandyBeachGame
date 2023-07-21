using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoardManager : MonoBehaviour
{
    public static BoardManager sharedInstance;
    public List<Image> prefabs = new ();
    public GameObject currentCandy;
    public int xSize, ySize;

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

        Vector2 offset = currentCandy.GetComponent<BoxCollider>().size;
        CreateInitialBoard(offset);
    }

    private void CreateInitialBoard(Vector2 offset)
    {

    }
}
