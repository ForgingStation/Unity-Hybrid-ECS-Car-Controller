using Unity.Entities;
using Unity.Mathematics;

public struct Player_Drive_Component : IComponentData
{
    public float maxMotorTorque;
    public float maxBreakTorque;
    public float maxSteerAngle;
    public float maxSpeed;
    public float maxAcceleration;

    public float3 currentVelocity;
    public float currentSpeed;

    public float speedParameter;
    public float breakParameter;
    public float steerParameter;

}

