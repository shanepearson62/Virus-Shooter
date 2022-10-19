using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BulletNumberLeft : MonoBehaviour
{
    public TextMeshProUGUI textmeshPro;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        textmeshPro.SetText(PlayerScript.bulletNumber + "");
    }
}
