using UnityEngine;

public class TrooperData : MonoBehaviour
{
    /*[HideInInspector]*/ public bool targetInShootingRange;
    /*[HideInInspector]*/ public bool targetSeen;

    [HideInInspector] public bool attacking;
    [HideInInspector] public bool onReload;

    public Transform target;
}
