using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHorizontalMoving : MonoBehaviour
{
    GameManager gameManager;
    [Header("Movement")]
    [SerializeField] float speed;
    [SerializeField] float forwardSpeed;
    [SerializeField] float horizontalLimit;

    private float mZCoord;
    private Vector3 mOffset;

    private void Start()
    {
        gameManager = GameManager.Instance;
    }
    void FixedUpdate()
    {
        if (gameManager.gameStart)
        {
            transform.Translate(Vector3.forward * forwardSpeed * Time.fixedDeltaTime); // forward

            if (Input.GetMouseButtonDown(0))
            {
                mZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;

                mOffset = gameObject.transform.position - GetMouseAsWorldPoint();
            }

            if (Input.GetMouseButton(0))
            {
                Vector3 endPos = new Vector3((GetMouseAsWorldPoint().x + mOffset.x), transform.position.y, transform.position.z);

                transform.position = Vector3.Lerp(transform.position, endPos, speed * Time.fixedDeltaTime);

                transform.position =
                    new Vector3(Mathf.Clamp(transform.position.x, -horizontalLimit, horizontalLimit), transform.position.y, transform.position.z);
                // horizontal limit 
            }
        }
        
    }

    private Vector3 GetMouseAsWorldPoint()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = mZCoord;

        // Convert it to world points
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }
}