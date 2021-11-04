using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelPart : MonoBehaviour
{
    [SerializeField] private Transform[] positions;
    private bool started = false;

    private Vector3 screen = Vector3.zero;

    private void Awake()
    {

    }

    private void OnEnable()
    {
        foreach (Transform pos in positions)
        {
            screen = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height));
            pos.position = new Vector3(screen.x, pos.position.y);
        }

        if (started) SpawnClouds();
    }

    private void Start()
    {
        SpawnClouds();
        started = true;
    }

    public void SpawnClouds()
    {
        int i = 0;
        foreach (Transform pos in positions)
        {
            GameObject cloud = CloudPooler.Instance.SpawnFromPool(RandomCloud(i).ToString(), pos.position, Quaternion.identity);
            cloud.GetComponentInChildren<Cloud>().gameObject.SetActive(true);
            cloud.transform.parent = gameObject.transform;
            cloud.transform.position = new Vector3(CloudPosition(), pos.transform.position.y);
        }
    }

    private float CloudPosition()
    {
        float num = Mathf.Abs(Camera.main.transform.position.x);
        float x = Random.Range(-screen.x - num, screen.x);
        return x;
    }

    private CloudNames RandomCloud(int i)
    {
        CloudNames tag = 0;
        int randomNumber = Random.Range(0, 10);
        if (randomNumber <= 7)
        {
            tag = CloudNames.WhiteCloud;
        }
        else if(i % 2 == 0)
        {
            tag = (CloudNames)Random.Range(1, 3 + 1);
        }

        return tag;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
public enum CloudNames
{
    WhiteCloud,
    RedCloud,
    GreenCloud,
    BlackCloud,
    BlueCloud,
    YellowCloud
}