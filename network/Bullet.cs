using UnityEngine;

public class Bullet : MonoBehaviour
{

    private void Start()
    {
        this.transform.position = new Vector3(this.transform.position.x, 1f,this.transform.position.z+1f);
    }
    void OnCollisionEnter(Collision collision)
    {
        var hit = collision.gameObject;
        var hitPlayer = hit.GetComponent<Combat>();
        if (hitPlayer != null)
        {
            // Subscribe and Publish model may be good here!
            var combat = hit.GetComponent<Combat>();
            combat.TakeDamage(20);

            Destroy(gameObject);
        }
    }
}
