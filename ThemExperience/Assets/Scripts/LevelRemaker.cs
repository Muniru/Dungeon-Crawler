using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelRemaker : MonoBehaviour {

    public GameObject needed;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        Instantiate(needed);
    }
}
