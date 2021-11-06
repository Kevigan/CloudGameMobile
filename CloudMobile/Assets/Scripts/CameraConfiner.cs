using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraConfiner : MonoBehaviour
{
    [SerializeField] private Scrollbar hoehenMeter;
    [SerializeField] private Transform startPoint;
    [SerializeField] private Transform endPoint;

    [SerializeField] private PlayerCharacter2D player;
    [SerializeField] private float timer = 5;
    [SerializeField] private float katapultForce = 15f;

    private bool timerStarted = false;
    private bool justExited = false;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartTimer());
    }
    private void FixedUpdate()
    {
        if (timerStarted && GameManager.Main.GameState != GameState.Death)
        {
            transform.position += new Vector3(0, 1, 0) * Time.deltaTime;
            HandleHoehenmeter();
        }
    }

    private void HandleHoehenmeter()
    {
        hoehenMeter.value = ((transform.position.y - startPoint.transform.position.y) / (endPoint.transform.position.y - startPoint.transform.position.y));
    }

    IEnumerator StartTimer()
    {
        yield return new WaitForSeconds(timer);
        timerStarted = true;
    }

    
}
