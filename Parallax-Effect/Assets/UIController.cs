using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Slider slider;
    public CameraMovement cameraMovement;

    Vector2 mousePos;
    Vector2 sliderPos;
    [SerializeField]
    Vector2 offset;
    CanvasGroup canvasGroup;

    private void Start()
    {
        float sliderX = slider.GetComponent<RectTransform>().position.x;
        float sliderY = slider.GetComponent<RectTransform>().position.y;
        sliderPos = new Vector2(sliderX, sliderY);

        canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0.15f;
    }

    private void FixedUpdate()
    {
        //  Getting mouse position
        mousePos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

        //  Fading the slider in and out
        float sliderXsize = slider.GetComponent<RectTransform>().rect.width/2;
        float sliderYsize = slider.GetComponent<RectTransform>().rect.height/2;

        if (mousePos.x < sliderPos.x+sliderXsize+offset.x && mousePos.x > sliderPos.x-sliderXsize-offset.x &&
            mousePos.y < sliderPos.y+sliderYsize+offset.y && mousePos.y > sliderPos.y-sliderYsize-offset.y)
        {
            if (canvasGroup.alpha < 1)
                canvasGroup.alpha += 2.5f * Time.fixedDeltaTime;
        }
        else
        {
            if (canvasGroup.alpha > 0.15f)
                canvasGroup.alpha -= 2.5f * Time.fixedDeltaTime;
        }
    }

    public void OnSliderChange()
    {
        cameraMovement.cameraSpeed = slider.value;
    }
}
