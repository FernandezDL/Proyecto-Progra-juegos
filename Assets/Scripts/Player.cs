using System;
using System.Collections.Generic;
using Objects;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private List<Key> lostKeys = new List<Key>();
    private List<int> collectedKeys;
    
    public List<int> CollectedKeys => collectedKeys;
    
    public Action<int> onKeyPickedUp;

    private void Awake()
    {
        collectedKeys = new List<int>();
    }

    private void Start()
    {
        foreach (var key in lostKeys)
        {
            key.OnKeyPickedUp += ReceiveKey;
        }
    }

    private void ReceiveKey(int newKey)
    {
        if (!collectedKeys.Contains(newKey))
        {
            collectedKeys.Add(newKey);
            onKeyPickedUp?.Invoke(collectedKeys.Count);
        }
    }
}