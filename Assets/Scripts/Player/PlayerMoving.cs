using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoving : MonoBehaviour, IMove
{
    public int Speed { get; set; }
    public float XMin, XMax, ZMin, ZMax;
    public float Tilt;

    void Start()
    {
        Speed = 10;
    }

    private void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Rigidbody Player = GetComponent<Rigidbody>();

        Player.velocity = new Vector3(horizontal, 0, vertical) * Speed;

        float newPosX = Mathf.Clamp(Player.position.x, XMin, XMax);
        float newPosZ = Mathf.Clamp(Player.position.z, ZMin, ZMax);
        float newPosY = Player.position.y;

        Player.position = new Vector3(newPosX, newPosY, newPosZ);

        Player.rotation = Quaternion.Euler(Player.velocity.z * Tilt, 0, -Player.velocity.x * Tilt);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Speed *= 2;
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            Speed /= 2;
        }
        

    }

    public void Move()
    {
        throw new System.NotImplementedException();
    }
}
