using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyInBounds : MonoBehaviour
{
    private Transform m_player;

    public float destroyBounds;


    private void Start()
    {
        m_player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }
    private void Update()
    {
        if (m_player.position.z - transform.position.z > destroyBounds) {
            Destroy(gameObject);
        }
    }
}
