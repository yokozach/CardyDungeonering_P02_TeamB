using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlsMenu : MonoBehaviour
{
    [SerializeField] GameObject controlMenu;
    public void Controls()
    {
        controlMenu.SetActive(true);
    }
    public void Back()
    {
        controlMenu.SetActive(false);
    }
}
