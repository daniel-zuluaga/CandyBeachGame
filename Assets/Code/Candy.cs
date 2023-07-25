using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Candy : MonoBehaviour
{
    public int idCandy;

    private static Color selectedColor = new(0.5f, 0.5f, 0.5f, 1.0f);
    private static Candy previousSelected = null;

    [SerializeField] private Animator animator;

    [SerializeField] private SpriteRenderer image;
    [SerializeField] private bool isSelected = false;

    [SerializeField] private Vector2[] adjacentDirections = new Vector2[]
    {
        Vector2.up,
        Vector2.down,
        Vector2.right,
        Vector2.left
    };

    private void Awake()
    {
        if(image == null)
            image = GetComponent<SpriteRenderer>();
    }

    public void SetEnabledAnimator(bool active)
    {
        animator.enabled = active;
    }
}
