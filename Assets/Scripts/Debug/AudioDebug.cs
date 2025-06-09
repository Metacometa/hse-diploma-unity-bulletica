using UnityEngine;

public class AudioDebug : MonoBehaviour
{
    void Update()
    {
        AudioSource[] sources = GetComponentsInChildren<AudioSource>();

        foreach (AudioSource source in sources)
        {
            string parentName = "null";

            if (source.transform.parent != null)
            {
                parentName = source.transform.parent.gameObject.name;
            }

            Debug.Log($"AudioSource: {source.name}, {source.playOnAwake}, gameO: {parentName}");
        }
    }
}