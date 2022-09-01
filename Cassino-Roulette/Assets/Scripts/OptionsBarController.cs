using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsBarController : MonoBehaviour
{
    public Image image;

    public Vector3 offset;
    Vector3 imagePos, mousePos, desiredPos;

    public float swiftness;
    float imageXsize, imageYsize;

    // Start is called before the first frame update
    void Start()
    {
        imagePos = image.GetComponent<RectTransform>().position;
        desiredPos = imagePos;
        desiredPos.y  -= 55f;

        imageXsize = image.GetComponent<RectTransform>().rect.width  / 2;
        imageYsize = image.GetComponent<RectTransform>().rect.height / 2;
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = Input.mousePosition;

        if (mousePos.x < imagePos.x + imageXsize + offset.x && mousePos.x > imagePos.x - imageXsize - offset.x &&
            mousePos.y < imagePos.y + imageYsize + offset.y && mousePos.y > imagePos.y - imageYsize - offset.y)
        {
            //  Lerp position to slide in on screen
            transform.position = Vector3.Lerp(transform.position, desiredPos, swiftness * Time.deltaTime);

            //  Also lerping the offset
            offset.y = Mathf.Lerp(offset.y, 55f, swiftness * Time.deltaTime);
        }
        else
        {
            //  Lerp position to slide out from screen
            transform.position = Vector3.Lerp(transform.position, imagePos, swiftness * Time.deltaTime);

            //  Also lerping the offset
            offset.y = Mathf.Lerp(offset.y, 0f, swiftness * Time.deltaTime);
        }
    }
}
