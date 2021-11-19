using System;
using UnityEngine;

public class SliceableBody : MonoBehaviour
{
    private Body body;
    private Color defaultColor;
    [SerializeField] private Color unlinkedColor;
    private SpriteRenderer _spriteRenderer;
    public void Unlink()
    {
        body.snake = null;
        body.previousTile = null;
        body.linked = false;
        
        // Visuals
        HandleRendering();
    }

    private void HandleRendering()
    {
        transform.localScale = new Vector3(.6f, .6f, .6f);
        _spriteRenderer.color = unlinkedColor;
    }

    private void Awake()
    {
        body = GetComponent<Body>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        unlinkedColor = body.snake == null ? Color.grey : body.snake.color;
        unlinkedColor.a = 0.5f;
    }
}
