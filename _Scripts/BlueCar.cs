using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueCar : MainCar
{
    private Rigidbody m_carRb;

    private AudioSource m_audioSource;

    private float m_jumpForce = 2000;

    private bool m_isOnGround = true;

    private new int nextTime = 5;


    private void Start()
    {
        width = Screen.width / 4;
        height = Screen.height / 6;

        m_audioSource = GetComponent<AudioSource>();
        m_carRb = GetComponent<Rigidbody>();

        Physics.gravity = new Vector3(0, -25, 0);

        nextTime *= 60;

        m_audioSource.volume = Singleton.Instance.Volume;

        GameManager.gameManager.SetPowerBarValue(nextTime);
    }
    private void Update()
    {
        transform.Translate(Vector3.forward * carSpeed * Time.deltaTime, Space.World);
        Bounds();
        PowerCheck();
        Controls();
    }
    private new void Controls()
    {
        if (Input.touchCount > 0)
        {
            m_touch = Input.GetTouch(0);
            if (m_touch.phase == TouchPhase.Began) 
            {
                m_touchStartPos = m_touch.position;
            }
            if (m_touch.phase == TouchPhase.Moved) 
            {
                m_movedPosition = m_touch.position;
                if (m_movedPosition.x - m_touchStartPos.x > width && transform.position.x < 2) {
                    transform.position = new Vector3(transform.position.x + 5, transform.position.y, transform.position.z);
                    ResetTouch(m_touchStartPos, m_movedPosition);
                }
                if (m_movedPosition.x - m_touchStartPos.x < -width && transform.position.x > -2) {
                    transform.position = new Vector3(transform.position.x - 5, transform.position.y, transform.position.z);
                    ResetTouch(m_touchStartPos, m_movedPosition);
                }
                if (m_movedPosition.y - m_touchStartPos.y > height && isPowerActive) {
                    Power();
                    ResetTouch(m_touchStartPos, m_movedPosition);
                }
                if (m_movedPosition.y - m_touchStartPos.y < -height && !m_isOnGround) {
                    Stomp();
                    ResetTouch(m_touchStartPos, m_movedPosition);
                }
            }
            if (m_touch.phase == TouchPhase.Ended) {
                ResetTouch(m_touchStartPos, m_movedPosition);
            }
        }

    }
    private void FixedUpdate()
    {
        time++;
        GameManager.gameManager.FillPowerBar(time);
    }

    private void Stomp()
    {
        m_carRb.velocity = new Vector3(m_carRb.velocity.x, 0, m_carRb.velocity.z);
        m_carRb.AddForce(Vector3.down * m_jumpForce, ForceMode.Impulse);
    }
    protected override void PowerCheck()
    {
        if (time > nextTime) {
            isPowerActive = true;
        }
        if (time <= nextTime) {
            isPowerActive = false;
        }
    }
    protected override void Power()
    {
        time = 0;
        m_carRb.AddForce(Vector3.up * m_jumpForce, ForceMode.Impulse);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle")) {
            Time.timeScale = 0.4f;
            StartCoroutine(CrashCoroutine());
        }
        if (collision.gameObject.CompareTag("Ground")) {
            m_isOnGround = true;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground")) {
            m_isOnGround = false;
        }
    }
}
