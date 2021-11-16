using UnityEngine;

public class SliceableBody : MonoBehaviour
{
    private Body body;
    public void Unlink()
    {
        transform.localScale = new Vector3(.5f, .5f, .5f);
        GetComponent<SpriteRenderer>().color = Color.blue;
        body.snake = null;
        body.previousTile = null;
        body.linked = false;
        GetComponent<GridCollider>().CollisionType = CollisionType.Active;
    }

    private void Awake()
    {
        body = GetComponent<Body>();
    }
}
