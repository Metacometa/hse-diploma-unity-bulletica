using System.Collections;
using UnityEngine;

public class BaseShimmer : MonoBehaviour, IShimmerable
{
    private Color startColor;
    private SpriteRenderer sprite;

    private bool onShimmer;

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    public void ShimmerManager()
    {
        if (!onShimmer)
        {
            ChangeColor();

            StartCoroutine(Shimmer());
        }
    }

    public IEnumerator Shimmer()
    {
        onShimmer = true;
        yield return new WaitForSeconds(0.2f);

        RestoreColor();

        onShimmer= false;
    }

    void ChangeColor()
    {
        startColor = sprite.color;
        sprite.color = new Color(1, 0, 0, 1);
    }

    void RestoreColor()
    {
        sprite.color = startColor;
    }
}
