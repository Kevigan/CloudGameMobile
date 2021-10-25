using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelPart : MonoBehaviour
{
    [SerializeField] private Transform[] positions;


    private void Awake()
    {

    }

    private void OnEnable()
    {
        SpawnClouds();
    }

    private void Start()
    {
        SpawnClouds();
    }

    public void SpawnClouds()
    {
        foreach (Transform pos in positions)
        {
            GameObject cloud = CloudPooler.Instance.SpawnFromPool(RandomCloud().ToString(), pos.position, Quaternion.identity);
            cloud.GetComponentInChildren<Cloud>().gameObject.SetActive(true);
            cloud.transform.parent = gameObject.transform;
            cloud.transform.position = new Vector3(pos.transform.position.x - CloudPosition(), pos.transform.position.y);
        }
    }

    private float CloudPosition()
    {
        float x = Random.Range(0f, 8f + 1f);
        return x;
    }

    private CloudNames RandomCloud()
    {
        CloudNames tag = 0;
        int randomNumber = Random.Range(0, 10);
        if (randomNumber <= 7)
        {
            tag = CloudNames.WhiteCloud;
        }
        else
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