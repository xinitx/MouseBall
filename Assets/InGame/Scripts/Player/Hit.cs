using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Hit : MonoBehaviour
{
    public void hit()
    {
        FindObjectOfType<PlayerControl>().Start_Hit();
    }
    
}
