using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton : MonoBehaviour
{
    public static Singleton Instance { get; private set; }

    private float m_volume;
    public float Volume
    {
        get
        {
            return m_volume;
        }
        set
        {
            m_volume = value;
        }
    }


    private void Awake()
    {
        if (Instance != null) {
            Destroy(gameObject);
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
