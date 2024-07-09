using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceToStart : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0f;
    }

    public void PressToStart()
    {
        Time.timeScale = 1f;
        this.gameObject.SetActive(false);
    }
}
