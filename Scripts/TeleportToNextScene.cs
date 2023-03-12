using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportToNextScene : MonoBehaviour
{
    public SceneTransitionManager teleport;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "Player")
        {
            teleport.GoToScene(1);
        }
    }
}
