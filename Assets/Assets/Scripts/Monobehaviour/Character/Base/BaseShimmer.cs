using System.Collections;
using UnityEngine;

public class BaseShimmer : MonoBehaviour, IShimmerable
{
    private Color startColor;
    private SpriteRenderer sprite;

    private bool onShimmer;

    void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        startColor = new Color(0,0,0);
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

        startColor = sprite.color;
        ChangeColor();

        yield return new WaitForSeconds(0.2f);

        RestoreColor();

        onShimmer = false;
    }

    void ChangeColor()
    {
        sprite.color = new Color(1, 0, 0, 1);
    }

    public void RestoreColor()
    {
        sprite.color = startColor;
    }
}
