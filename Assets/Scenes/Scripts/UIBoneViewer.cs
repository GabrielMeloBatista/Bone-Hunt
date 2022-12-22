using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIBoneViewer: MonoBehaviour
{
    Vector3 firtPoint;
    Vector3 secondPoint;
    float xAngle;
    float yAngle;
    float xAngleTemp;
    float yAngleTemp;

    void Start()
    {
        xAngle= 0;
        yAngle= 0;
        this.transform.rotation= Quaternion.Euler(yAngle, xAngle, 0);
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            // Quando se a toque, ve a posição inicial dele
            if(Input.GetTouch(0).phase== TouchPhase.Began)
            {
                firtPoint = Input.GetTouch(0).position;
                xAngleTemp = xAngle;
                yAngleTemp = yAngle;
            }
            // caso mova, verifica aonde ele esta indo
            if(Input.GetTouch(0).phase== TouchPhase.Moved) {
                secondPoint = Input.GetTouch(0).position;
                // Ajusta os angulos baseados no movimento do toque
                xAngle = xAngleTemp + (secondPoint.x - firtPoint.x) * 180.0f / Screen.width;
                yAngle = yAngleTemp + (secondPoint.y - firtPoint.y) * 90.0f / Screen.height;
                
                // Baseado nisto, ele rotaciona o GameObject que tem este script
                this.transform.rotation = Quaternion.Euler(yAngle, xAngle, 0.0f);
            }
        }
    }
}
