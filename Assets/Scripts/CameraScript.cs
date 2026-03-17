using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public GameObject ink_guy;

    // Update is called once per frame
    void Update()
    {
        Vector3 position = transform.position;
        position.x = ink_guy.transform.position.x;
        position.y = ink_guy.transform.position.y;
        transform.position = position;
    }
}
