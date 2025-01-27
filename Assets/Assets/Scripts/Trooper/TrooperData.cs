using System.Collections.Generic;
using UnityEngine;

public class TrooperData : MonoBehaviour
{
    /*[HideInInspector]*/ public bool targetInShootingRange;
    /*[HideInInspector]*/ public bool targetSeen;

    [HideInInspector] public bool attacking;

    [HideInInspector] public bool canMove = true;

    public Transform target;
}
