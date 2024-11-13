using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SearchBarScript : MonoBehaviour
{
    public TMP_InputField Input;
    // Start is called before the first frame update
    void Start()
    {
        Input.interactable = false;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void RevealField()
    {
        Input.interactable = true;


    }
}
