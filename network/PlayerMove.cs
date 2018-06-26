using UnityEngine;
using UnityEngine.Networking;

public class PlayerMove : NetworkBehaviour
{
    public GameObject bulletPrefab;
   // public GameObject camera;

    [Command]
    void CmdFire()
    {
        // This [Command] code is run on the server!

        // create the bullet object locally
        var bullet = (GameObject)Instantiate(
             bulletPrefab,
             transform.position - transform.forward,
             Quaternion.identity);

        bullet.GetComponent<Rigidbody>().velocity = transform.forward * 20;

        // spawn the bullet on the clients
        NetworkServer.Spawn(bullet);

        // when the bullet is destroyed on the server it will automaticaly be destroyed on clients
        Destroy(bullet, 2.0f);
    }

    void Update()
    {
        //Camera.main.transform.position = new Vector3(this.transform.position.x, 30, this.transform.position.z);
        if (!isLocalPlayer)
            return;
        if (Input.GetKey(KeyCode.W))
        {
            this.GetComponent<Rigidbody>().velocity = this.transform.forward * 3;
        }

        if (Input.GetKey(KeyCode.S))
        {
            this.GetComponent<Rigidbody>().velocity = this.transform.forward * -3;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            CmdFire();
        }

        float offsetX = Input.GetAxis("Horizontal");//获取水平轴的增量，控制玩家的转向
                                                     //user.turn(offsetX);
        float x = this.transform.localEulerAngles.y + offsetX * 5;
        float y = this.transform.localEulerAngles.x;
        this.transform.localEulerAngles = new Vector3(y, x, 0);
    }
}
