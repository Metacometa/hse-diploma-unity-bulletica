using System.Collections;
using UnityEngine;

public class BaseShimmer : MonoBehaviour, IShimmerable
{
    public Color[] startColors;

    public SpriteRenderer[] sprites;
    
    private bool onShimmer;

    private BaseProfile profile;

    void Awake()
    {
        profile = GetComponent<BaseCharacter>().profile;

        sprites = GetComponentsInChildren<SpriteRenderer>();
        startColors = new Color[sprites.Length];

        for (int colors = 0; colors < sprites.Length; ++colors)
        {
            startColors[colors] = new Color(0, 0, 0);
        }
    }

    public void ShimmerManager()
    {
        if (!profile) { return; }

        if (!onShimmer)
        {
            StartCoroutine(Shimmer());
        }
    }

    public IEnumerator Shimmer()
    {
        float timeChunk = profile.invincibilityTime / profile.shimmerTime;

        for (int i = 0; i < sprites.Length; ++i)
        {
            startColors[i] = sprites[i].color;
        }

        onShimmer = true;

        for (int time = 0; time < profile.shimmerTime; ++time)
        {
            ChangeColor();

            yield return new WaitForSeconds(timeChunk / 2);

            RestoreColor();

            yield return new WaitForSeconds(timeChunk / 2);
        }

        onShimmer = false;
    }

    void ChangeColor()
    {
        foreach (var sprite in sprites)
        {
            Color temp = profile.shimmerColor;
            sprite.color = new Color(temp.r, temp.g, temp.b, 1);
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
