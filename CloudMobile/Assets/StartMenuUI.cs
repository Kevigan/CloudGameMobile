using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartMenuUI : MonoBehaviour
{
    [SerializeField] private Text highScoreText; 
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("Car")) 
        Debug.Log("haskey");
            highScoreText.text = PlayerPrefs.GetInt("Car").ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
