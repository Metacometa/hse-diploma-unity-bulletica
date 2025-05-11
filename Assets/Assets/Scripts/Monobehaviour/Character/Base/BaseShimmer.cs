using System.Collections;
using UnityEngine;

public class BaseShimmer : MonoBehaviour, IShimmerable
{
    private Color[] startColors;

    public SpriteRenderer[] sprites;
    

    private bool onShimmer;

    void Awake()
    {
        sprites = GetComponentsInChildren<SpriteRenderer>();
        startColors = new Color[sprites.Length];

        for (int colors = 0; colors < sprites.Length; ++colors)
        {
            startColors[colors] = new Color(0, 0, 0);
        }
    }

    public void ShimmerManager()
    {
        if (!onShimmer)
        {
            StartCoroutine(Shimmer());
        }
    }

    public IEnumerator Shimmer()
    {
        onShimmer = true;

        for (int i = 0; i < sprites.Length; ++i)
        {
            startColors[i] = sprites[i].color;
        }

        ChangeColor();

        yield return new WaitForSeconds(0.2f);

        RestoreColor();

        onShimmer = false;
    }

    void ChangeColor()
    {
        foreach (var sprite in sprites)
        {
            sprite.color = new Color(1, 0, 0, 1); ;
        }
    }

    public void RestoreColor()
    {
        for (int i = 0; i < sprites.Length; ++i)
        {
            sprites[i].color = startColors[i];
        }
    }
}
