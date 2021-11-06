using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindField : MonoBehaviour
{
    public delegate void OnChangeWindDirection(int direction);
    public static event OnChangeWindDirection changeDirectionOfSnow;

    public Vector2 windDirection = Vector2.zero;
    private int direction;
    public float windSpeed = 5f;
    public float windTime = 3f;

    private bool windActive = false;

    //public ParticleSystem psNormal;
    //public ParticleSystem psLeft;
    //public ParticleSystem psRight;

    private void Awake()
    {
    }

    private void Start()
    {
        GameManager.activateWindField += ActivateWindField;
    }

    private void Update()
    {
    }

    private void FixedUpdate()
    {
        if (GameManager.Main.GameState != GameState.Playing) return;
        transform.position = new Vector3(transform.position.x, Camera.main.transform.position.y + 3);
    }

    private void OnDestroy()
    {
        GameManager.activateWindField -= ActivateWindField;
    }

    public void ActivateWindField()
    {
        // int[] a = { -1 , 1 };
        //int num = Random.Range(0, 2);
        //windDirection = new Vector2(a[num], 0);
        //OnChangeBehaviour(num);
        //windActive = true;
        if (changeDirectionOfSnow != null && GameManager.Main.windDirection > 0)
        {
            //changeDirectionOfSnow();
            windActive = true;
        }
        else if (changeDirectionOfSnow != null && GameManager.Main.windDirection < 0)
        {
            windActive = true;
            //changeDirectionOfSnow();
        }

        StartCoroutine(DeactivateWindField());
    }

    IEnumerator DeactivateWindField()
    {
        yield return new WaitForSeconds(windTime);
        if (changeDirectionOfSnow != null)
        {
            changeDirectionOfSnow(0);
            windActive = false;
        }
    }

    

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerCharacter2D>() is PlayerCharacter2D player)
        {
            if (GameManager.Main.GameState != GameState.LevelFinished && windActive)
            {
                //Vector3 direction = new Vector3(windDirection.x, player.Velocity.y);
                player.transform.position += new Vector3(GameManager.Main.windDirection * windSpeed, player.Velocity.y) * Time.fixedDeltaTime;
            }
        }
    }
}
