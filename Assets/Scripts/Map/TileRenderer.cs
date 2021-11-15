using System.Collections;
using UnityEngine;

public class TileRenderer : MonoBehaviour
{
    private Color defaultColor;
    private SpriteRenderer spriteRenderer;
    
    public void ColorForSeconds(Color color, float time)
    {
        spriteRenderer.color = color;
        StartCoroutine(DefaultColor(time));
    }

    private IEnumerator DefaultColor(float time)
    {
        yield return new WaitForSeconds(time);
        spriteRenderer.color = defaultColor;
    }

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        defaultColor = spriteRenderer.color;
    }
}
