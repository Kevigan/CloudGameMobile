using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraFollower : MonoBehaviour
{
    [SerializeField] private Transform target;
    private bool followNegative = false;
    [SerializeField] private Color colorStart;
    [SerializeField] private Color colorEnd;
    [SerializeField] private float desiredDuration = 5f;
    [SerializeField] private GameObject starsPrefab;
    private float elapsedTime;
    private bool starsActive = false;
    private bool startLerp = false;

    // Start is called before the first frame update
    void Start()
    {
        DamageColider.onPlayerFalling += ChangeBool;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Main.actualHeight / GameManager.Main.endHeight > 0.5 && !starsActive)
        {
            startLerp = true;
            starsActive = true;
            starsPrefab.SetActive(true);
        }
        if (startLerp)
        {
            elapsedTime += Time.deltaTime;
            float percentageComplete = elapsedTime / desiredDuration;

            Camera.main.backgroundColor = Color.Lerp(colorStart, colorEnd, percentageComplete);
        }

    }
    IEnumerator StartColorLerp()
    {
        yield return new WaitForSeconds(0);

    }

    private void LateUpdate()
    {
        if (target.position.y > transform.position.y && GameManager.Main.GameState == GameState.Playing)
        {
            transform.position = new Vector3(transform.position.x, target.position.y, transform.position.z);
        }
        else if (followNegative)
        {
            transform.position = new Vector3(transform.position.x, target.position.y, transform.position.z);
        }
    }

    public void ChangeBool()
    {
        followNegative = !followNegative;
    }
}
