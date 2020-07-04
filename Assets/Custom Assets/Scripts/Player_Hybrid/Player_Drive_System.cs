using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class Player_Drive_System : SystemBase
{
    protected override void OnUpdate()
    {
        float hAxis = Input.GetAxis("Horizontal");
        float vAxis = Input.GetAxis("Vertical");
        bool spaceKey = Input.GetKey("space");

        Entities.ForEach((ref Player_Drive_Component pdc) => {
            pdc.currentSpeed = math.sqrt(math.pow(pdc.currentVelocity.x, 2) +
                                        math.pow(pdc.currentVelocity.y, 2) +
                                        math.pow(pdc.currentVelocity.z, 2));
            pdc.speedParameter = pdc.maxMotorTorque * pdc.maxAcceleration * vAxis * (1 - (pdc.currentSpeed / pdc.maxSpeed));
            pdc.steerParameter = pdc.maxSteerAngle * hAxis;
            if (spaceKey)
            {
                pdc.breakParameter = pdc.maxBreakTorque * (pdc.currentSpeed / pdc.maxSpeed) * pdc.maxAcceleration;
            }
            else
            {
                pdc.breakParameter = 0;
            }
            
        }).Run();

    }
}
