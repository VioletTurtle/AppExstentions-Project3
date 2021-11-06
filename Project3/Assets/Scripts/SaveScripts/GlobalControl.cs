using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalControl : MonoBehaviour
{
    public static GlobalControl Instance;

    public List<ItemSlot> inventoryList;
    public float masterVolume;
    public float backgroundMusicVolume;
    public float SFXVolume;
    public float resolutionHeight;
    public float resolutionWidth;
    public bool isWindowed;

    void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }
}
