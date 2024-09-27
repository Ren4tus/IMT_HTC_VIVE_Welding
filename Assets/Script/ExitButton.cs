using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitButton : MonoBehaviour
{
    public GameObject Menu;
    public void OnClickExit()
    {
        Menu.SetActive(false);
    }
}
