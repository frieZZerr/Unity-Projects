using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Minimap : MonoBehaviour
{
    public PlayerManager player;
    public Image image;

    private void LateUpdate()
    {
        Vector3 newPosition = player.transform.position;
        newPosition.y = player.transform.position.y + 75f;
        transform.position = newPosition;

        image.transform.rotation = Quaternion.Euler(0f, 0f, player.transform.eulerAngles.y);
    }
}
