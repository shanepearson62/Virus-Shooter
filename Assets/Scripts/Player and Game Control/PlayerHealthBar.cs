using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{
    public Slider _health;
    // Start is called before the first frame update
    void Start()
    {
        _health.value = PlayerScript.playerHealth;
    }

    // Update is called once per frame
    void Update()
    {
        _health.value = PlayerScript.playerHealth;
    }
}
