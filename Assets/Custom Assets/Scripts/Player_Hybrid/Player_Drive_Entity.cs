using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

public class Player_Drive_Entity : MonoBehaviour
{
    public float maxSpeed;
    public float maxAcceleration;
    public float maxMotorTorque;
    public float maxBreakTorque;
    public float maxSteerAngle;
    private float currentSpeed;

    private EntityManager entitymamager;
    private Entity entity;
    private Rigidbody rb;
    private Drive_Bridge db;
    private float speedParameter;
    private float steerParameter;
    private float breakParameter;

    // Start is called before the first frame update
    void Start()
    {
        db = GetComponent<Drive_Bridge>();
        rb = GetComponentInParent<Rigidbody>();

        entitymamager = World.DefaultGameObjectInjectionWorld.EntityManager;
        EntityArchetype ea = entitymamager.CreateArchetype(
                             typeof(Player_Drive_Component)   
                             );
        entity = entitymamager.CreateEntity(ea);
        entitymamager.AddComponentData(entity, new Player_Drive_Component {
            maxAcceleration = maxAcceleration,
            maxSpeed = maxSpeed,
            maxMotorTorque = maxMotorTorque,
            maxBreakTorque = maxBreakTorque,
            maxSteerAngle = maxSteerAngle
         });

    }

    // Update is called once per frame
    void Update()
    {
        Player_Drive_Component pdc = entitymamager.GetComponentData<Player_Drive_Component>(entity);
        pdc.currentVelocity = rb.velocity;
        entitymamager.SetComponentData(entity, pdc);
        currentSpeed = pdc.currentSpeed;
        speedParameter = pdc.speedParameter;
        steerParameter = pdc.steerParameter;
        breakParameter = pdc.breakParameter;
        SetDriveParameters();
    }

    private void SetDriveParameters()
    {
        if (db != null)
        {
            db.steerParameter = steerParameter;
            db.breakParameter = breakParameter;
            if (currentSpeed < maxSpeed)
            {
                db.speedParameter = speedParameter;
            }
            else
            {
                db.speedParameter = 0;
            }
        }
    }
}
