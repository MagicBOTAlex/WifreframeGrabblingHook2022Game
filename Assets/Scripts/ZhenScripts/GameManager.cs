using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject Player;
    public GameObject Camera;

    public Texture2D crosshairImage;

    public int MyProperty { get; set; }

    // Start is called before the first frame update
    void Awake()
    {
        if (Instance is null) Instance = this; else Destroy(this);
    }

    private void OnGUI()
    {
        float xMin = (Screen.width / 2) - (crosshairImage.width / 2);
        float yMin = (Screen.height / 2) - (crosshairImage.height / 2);
        GUI.DrawTexture(new Rect(xMin, yMin, crosshairImage.width, crosshairImage.height), crosshairImage);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
