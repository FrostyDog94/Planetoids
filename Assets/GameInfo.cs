using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameInfo : MonoBehaviour
{
    public static GameInfo Instance { get; private set; }
    public Transform targetPlanet;
    public Transform currentPlanet;
    public TMP_Text text;
    public TMP_Text shuttleText;
    public List<Transform> planets;
    public GameObject player;
    

    // Start is called before the first frame update
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
      if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
