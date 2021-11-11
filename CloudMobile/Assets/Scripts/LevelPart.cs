using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelPart : MonoBehaviour
{
    [SerializeField] private Transform[] cloudPositions;
    [SerializeField] private Transform[] obsticlePositions;

    [SerializeField] private GameObject obsticlePrefab;
    private bool started = false;

    private Vector3 screen = Vector3.zero;


    private void Start()
    {
        screen = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height));
        SpawnClouds();
        SpawnObsticles();
        started = true;
    }

    void Update()
    {
        if (Camera.main.transform.position.y > transform.position.y + 25)
        {
            Destroy(gameObject);
        }
    }

    public void SpawnObsticles()
    {
        foreach (Transform pos in obsticlePositions)
        {
            int num = Random.Range(0, 10);
            if (num >= 7)
            {
                GameObject obsticle = Instantiate(obsticlePrefab, pos.position, Quaternion.identity);
                obsticle.transform.position = new Vector3(CloudPosition(), pos.transform.position.y);
            }
        }
    }

    public void SpawnClouds()
    {
        int i = 0;
        foreach (Transform pos in cloudPositions)
        {
            GameObject cloud = CloudPooler.Instance.SpawnFromPool(RandomCloud(i).ToString(), pos.position, Quaternion.identity);
            cloud.GetComponentInChildren<Cloud>().gameObject.SetActive(true);
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
        else if (i % 2 == 0)
        {
            tag = (CloudNames)Random.Range(1, 3 + 1);
        }

        return tag;
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