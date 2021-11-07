using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemporaryEquipment : MonoBehaviour
{
    [SerializeField] private float additionalSpeed = 2f;
    [SerializeField] private int time = 3;
    private bool hasTempEquipOn = false;
    private PlayerCharacter2D player;

    private void FixedUpdate()
    {
            Debug.Log(hasTempEquipOn);
        if (hasTempEquipOn)
        {
            player.SetYForce(additionalSpeed);
            //player.SetForce(new Vector2(player.Velocity.x, additionalSpeed));
        }
    }

    IEnumerator Timer()
    {
        Debug.Log("start");
        yield return new WaitForSeconds(3);
        Debug.Log("end");
        hasTempEquipOn = false;
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<PlayerCharacter2D>() is PlayerCharacter2D player)
        {
            transform.parent = player.transform;
            hasTempEquipOn = true;
            StartCoroutine(Timer());
            this.player = player;
        }
    }
}
