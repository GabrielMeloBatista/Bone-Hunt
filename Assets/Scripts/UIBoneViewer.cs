using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIBoneViewer : MonoBehaviour
{
    Vector3 firtPoint;
    Vector3 secondPoint;
    public float xAngle, yAngle, xAngleTemp, yAngleTemp, zoomOutMin, zoomOutMax;
    [SerializeField] Camera boneCamera;
    public float buffer = 1000;

    public void closeViewer()
    {
        Destroy(this);
    }

    void Start()
    {
        xAngle = 0;
        yAngle = 0;
        transform.rotation = Quaternion.Euler(yAngle, xAngle, 0);
        zoomOutMin = 0.06f;
        zoomOutMax = 0.2f;
        boneCamera = GameController.getInstance().getCamera();
    }

    void Update()
    {
        if (Input.touchCount == 2)
        {
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            float prevMagnitute = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float currentMagnitude = (touchZero.position - touchOne.position).magnitude;

            float diference = currentMagnitude - prevMagnitute;

            zoom(diference / buffer);
        }
        else if (Input.touchCount > 0)
        {
            // Quando se a toque, ve a posição inicial dele
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                firtPoint = Input.GetTouch(0).position;
                xAngleTemp = xAngle;
                yAngleTemp = yAngle;
            }
            // caso mova, verifica aonde ele esta indo
            if (Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                secondPoint = Input.GetTouch(0).position;
                // Ajusta os angulos baseados no movimento do toque
                xAngle = xAngleTemp + (secondPoint.x - firtPoint.x) * 180.0f / Screen.width;
                yAngle = yAngleTemp + (secondPoint.y - firtPoint.y) * 90.0f / Screen.height;

                // Baseado nisto, ele rotaciona o GameObject que tem este script
                transform.rotation = Quaternion.Euler(yAngle, xAngle, 0.0f);
            }
        }
    }

    void zoom(float increment)
    {
        boneCamera.orthographicSize = Mathf.Clamp(boneCamera.orthographicSize - increment, zoomOutMin, zoomOutMax);
    }
}
