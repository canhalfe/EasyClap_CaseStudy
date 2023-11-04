using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeController : MonoBehaviour
{
    GameManager gameManager;
    public float swipeSpeed = 15f;
    public float forwardSpeed = 3.6f;
    public float xPosClamp = 2f;

    Vector3 _mouse_pos_start;
    Vector3 _mouse_pos;

    float start_position_x;
    float swipexValue;

    //bool move;
    private void Start()
    {
        gameManager = GameManager.Instance;
    }
    void FixedUpdate()
    {
        if (!gameManager.gameStart) return;

        transform.position += forwardSpeed * Time.deltaTime * transform.forward;

        Swipe();
    }
    private void Swipe()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _mouse_pos_start = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10f));
            
            start_position_x = transform.position.x;
        }

        if (Input.GetMouseButton(0))
        {
            _mouse_pos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10f));
            //Debug.Log(_mouse_pos.x);
            swipexValue = ((_mouse_pos.x - _mouse_pos_start.x)) + start_position_x;

            swipexValue = Mathf.Clamp(swipexValue, -xPosClamp, xPosClamp);

            Vector3 pos = transform.position;
            pos.x = swipexValue;

            transform.position = Vector3.Lerp(transform.position, pos, swipeSpeed * Time.deltaTime);
        }
    }
}