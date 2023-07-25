using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Candy : MonoBehaviour, ICandy
{
    public int idCandy;

    private static Color selectedColor = new(0.5f, 0.5f, 0.5f, 1.0f);
    private static Candy previousSelected = null;

    [SerializeField] private Animator animator;

    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private bool isSelected = false;

    [SerializeField] private Vector2[] adjacentDirections = new Vector2[]
    {
        Vector2.up,
        Vector2.down,
        Vector2.right,
        Vector2.left
    };

    private Vector3 objetive;

    private void Awake()
    {
        if(spriteRenderer == null)
            spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        MovementCandy();
    }

    private void MovementCandy()
    {
        if (objetive == transform.position)
        {
            objetive = Vector3.zero;
        }

        if (objetive != Vector3.zero)
        {
            transform.position = Vector3.Lerp(transform.position, objetive, 5 * Time.deltaTime);
        }
    }

    public void SetEnabledAnimator(bool active)
    {
        animator.enabled = active;
    }

    public void SelectedCandy()
    {
        isSelected = !isSelected;
        spriteRenderer.color = selectedColor;
        previousSelected = gameObject.GetComponent<Candy>();
    }

    public void DeselectCandy()
    {
        isSelected = false;
        spriteRenderer.color = Color.white;
        previousSelected = null;
    }

    private void OnMouseDown()
    {
        SelectDeselectCandy();
    }

    private void SelectDeselectCandy()
    {
        if (spriteRenderer.sprite == null ||
            BoardManager.sharedInstance.isShifting) return;

        if (isSelected)
        {
            DeselectCandy();
        }
        else
        {
            if (previousSelected == null)
            {
                SelectedCandy();
            }
            else
            {
                if (CanSwipe())
                {
                    SwappingCandy(previousSelected.gameObject);
                    previousSelected.DeselectCandy();
                }
                else
                {
                    previousSelected.DeselectCandy();
                    SelectedCandy();
                }
            }
        }
    }

    public void SwappingCandy(GameObject newCandy)
    {
        var compoCandy = newCandy.GetComponent<Candy>();

        if (spriteRenderer.sprite == newCandy.GetComponent<SpriteRenderer>().sprite) return;

        objetive = newCandy.transform.position;

        compoCandy.objetive = transform.position;
    }

    private GameObject GetNeighbor(Vector2 direction)
    {
        RaycastHit2D hit2D = Physics2D.Raycast(transform.position, direction);

        if(hit2D.collider != null)
        {
            return hit2D.collider.gameObject;
        }

        return null;
    }

    private List<GameObject> GetAllNeightbors()
    {
        List<GameObject> neightbors = new ();

        foreach (Vector2 direction in adjacentDirections)
        {
            neightbors.Add(GetNeighbor(direction));
        }

        return neightbors;
    }

    private bool CanSwipe()
    {
        return GetAllNeightbors().Contains(previousSelected.gameObject);
    }
}

