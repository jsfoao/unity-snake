using System;
using UnityEngine;

public class SliceableBody : MonoBehaviour
{
    private Body body;
    public void Unlink()
    {
        body.snake = null;
        body.previousTile = null;
        body.linked = false;
    }

    private void Update()
    {
        transform.localScale = !body.linked ? new Vector3(.6f, .6f, .6f) : new Vector3(.8f, .8f, .8f);
    }

    private void Awake()
    {
        body = GetComponent<Body>();
    }
}
