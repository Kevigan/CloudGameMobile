using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelPart : MonoBehaviour
{
    [SerializeField] private Transform[] positions;

    private void Awake()
    {

    }

    private void Start()
    {
        foreach (Transform pos in positions)
        {
            var tagCloud = (CloudNames)Random.Range(0, 2 + 1);
            var cloudPos = Random.Range(0.5f, 8.5f + 1);
            GameObject cloud = CloudPooler.Instance.SpawnFromPool(tagCloud.ToString(), pos.position, Quaternion.identity);
            cloud.GetComponentInChildren<Cloud>().gameObject.SetActive(true);
            cloud.transform.position = new Vector3(pos.localPosition.x - cloudPos, pos.transform.position.y);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
public enum CloudNames
{
    RedCloud,
    WhiteCloud,
    GreenCloud,
    BlackCloud,
    BlueCloud,
    YellowCloud
}