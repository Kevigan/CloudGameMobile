using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingScore : MonoBehaviour
{
    [SerializeField] private Animator ani;
    [SerializeField] private TextMesh score;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, 1.5f);
        score.text = GameManager.Main.floatingScoreText.ToString();
        // ani.Play("playFloatingText");
    }
}
