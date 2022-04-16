using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCar : MonoBehaviour
{
    protected int carSpeed = 20;
    private int m_slowSpeed = 5;
    private int m_boostSpeed = 45;
    private int m_boostTime = 5;
    private int m_defaultCarSpeed;

    private AudioSource m_audioSource;

    protected bool isPowerActive;

    protected int time;
    protected int nextTime = 10;

    #region Touch Variables
    protected Touch m_touch;
    protected Vector2 m_touchStartPos;
    protected Vector2 m_movedPosition;

    protected int width;
    protected int height;
    #endregion


    private void Start()
    {
        width = Screen.width / 4;
        height = Screen.height / 6;

        m_defaultCarSpeed = carSpeed;

        nextTime *= 60;

        m_audioSource = GetComponent<AudioSource>();
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

    protected void Controls()
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
            }
            if (m_touch.phase == TouchPhase.Ended) {
                ResetTouch(m_touchStartPos, m_movedPosition);
            }
        }
    }
    protected void ResetTouch(Vector2 value1, Vector2 value2)
    {
        value1 = Vector2.zero;
        value2 = Vector2.zero;
    }
    protected virtual void PowerCheck()
    {
        if (time > nextTime) {
            isPowerActive = true;
        }
        if (time <= nextTime) {
            isPowerActive = false;
        }
    }
    private void FixedUpdate()
    {
        time++;
        GameManager.gameManager.FillPowerBar(time);
    }
    protected virtual void Power()
    {
        time = 0;
        StartCoroutine(SlowDownCoroutine());
    }
    private IEnumerator SlowDownCoroutine()
    {
        carSpeed = m_slowSpeed;
        yield return new WaitForSecondsRealtime(m_boostTime / 3);
        StartCoroutine(BoostCoroutine());
    }
    private IEnumerator BoostCoroutine()
    {
        carSpeed = m_boostSpeed;
        yield return new WaitForSecondsRealtime(m_boostTime);
        carSpeed = m_defaultCarSpeed;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle")) {
            Time.timeScale = 0.4f;
            StartCoroutine(CrashCoroutine());
        }
    }
    protected IEnumerator CrashCoroutine()
    {
        yield return new WaitForSecondsRealtime(2.5f);
        carSpeed = 0;
        GameManager.gameManager.GameOver();
    }
    protected void Bounds()
    {
        if (transform.position.x > 3.0f)
        {
            transform.position = new Vector3(3.0f, transform.position.y, transform.position.z);
        }
        if (transform.position.x < -3.0f)
        {
            transform.position = new Vector3(-3.0f, transform.position.y, transform.position.z);
        }
    }
}
