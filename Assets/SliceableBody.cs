using UnityEngine;

public class SliceableBody : MonoBehaviour
{
    private Body body;
    private Color defaultColor;
    [SerializeField] private Color deadColor;
    private SpriteRenderer _spriteRenderer;
    public void Unlink()
    {
        body.snake = null;
        body.previousTile = null;
        body.linked = false;
    }

    private void Update()
    {
        if (body.linked)
        {
            transform.localScale = new Vector3(.8f, .8f, .8f);
            GetComponent<SpriteRenderer>().color = defaultColor;
        }
        else
        {
            transform.localScale = new Vector3(.6f, .6f, .6f);
            GetComponent<SpriteRenderer>().color = deadColor;
        }
    }

    private void Awake()
    {
        body = GetComponent<Body>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        defaultColor = _spriteRenderer.color;
    }
}
