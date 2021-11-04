using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSnowBehaviour : MonoBehaviour
{
    public ParticleSystem psNormal;
    public ParticleSystem psLeft;
    public ParticleSystem psRight;
    public ParticleSystem stars;

    private void Awake()
    {
    }

    private void Start()
    {
        WindField.changeDirectionOfSnow += OnChangeBehaviour;
        OnChangeBehaviour(0);
    }

    private void OnEnable()
    {
        DeactivateParticles();
    }

    private void Update()
    {
        transform.position = new Vector3(transform.position.x, Camera.main.transform.position.y + 20);
    }

    private void OnDestroy()
    {
        WindField.changeDirectionOfSnow -= OnChangeBehaviour;
    }

    public void OnChangeBehaviour(int direction)
    {
        DeactivateParticles();

        switch (direction)
        {
            case 0:
                psNormal.gameObject.SetActive(true);
                break;
            case 1:
                psRight.gameObject.SetActive(true);
                break;
            case -1:
                psLeft.gameObject.SetActive(true);
                break;
        }
    }
    private void DeactivateParticles()
    {
        psLeft.gameObject.SetActive(false);
        psNormal.gameObject.SetActive(false);
        psRight.gameObject.SetActive(false);
        //stars.gameObject.SetActive(false);
    }
}
