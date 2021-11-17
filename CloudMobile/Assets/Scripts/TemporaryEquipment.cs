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
        if (hasTempEquipOn && GameManager.Main.GameState == GameState.Playing)
        {
            player.SetYForce(additionalSpeed);
        }
        if (GameManager.Main.GameState == GameState.LevelFinished) Destroy(gameObject);
    }

    IEnumerator Timer()
    {
        yield return new WaitForSeconds(3);
        hasTempEquipOn = false;
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<PlayerCharacter2D>() is PlayerCharacter2D player)
        {
            transform.parent = player.transform;
            transform.localPosition = player.JetPackPos.localPosition;
            hasTempEquipOn = true;
            StartCoroutine(Timer());
            player.SetInvincibleTimer(3);
            this.player = player;
        }
    }
}
