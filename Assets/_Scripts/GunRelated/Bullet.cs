using UnityEngine;
[RequireComponent(typeof(Rigidbody) )]
public class Bullet : MonoBehaviour
{
    private float time;
    private float lifeTime = 2.0f;
    private void OnEnable()
    {
        time = 0;
    }
    private void Update()
    {
        //if doesn't hit something then return pool when lifetime is over
        time += Time.deltaTime;
        if (time > lifeTime)
            ReturnMeToPool();
    }
    private void OnCollisionEnter(Collision collision)
    {
        DecalManager.Instance.SpawnDecal(collision.gameObject, collision.GetContact(0).point, collision.GetContact(0).normal);
        if (collision.transform.root.CompareTag(Tags.enemy))
        {
            var ai = collision.gameObject.GetComponentInParent<ZombieAI>();
            if(ai != null)
                ai.GetDemage(GunManager.Instance.activeGun.damage);
        }
        ReturnMeToPool();
    }
    private void ReturnMeToPool()
    {
        ObjectPool.Instance.ReturnBulletToPool(gameObject);
    }
}