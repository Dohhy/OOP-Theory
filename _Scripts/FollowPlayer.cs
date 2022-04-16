using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    private Transform m_player;

    public float cameraSpeed;

    private Vector3 m_cameraOffet;


    private void Start()
    {
        m_player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        m_cameraOffet = transform.position;
    }
    private void LateUpdate()
    {
        float step = cameraSpeed * Time.deltaTime;
        transform.position = Vector3.Slerp(transform.position, m_player.position + m_cameraOffet, step);
        transform.position = new Vector3(transform.position.x, m_player.position.y + m_cameraOffet.y, m_player.position.z + m_cameraOffet.z);
    }
}
