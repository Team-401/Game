using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenBoltScript : MonoBehaviour
{
    public float velocity;
    public int damage;

    private Vector3 _castDir;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position += _castDir * velocity * Time.deltaTime;
    }

    public void Setup(Vector3 castDir)
    {
        this._castDir = castDir;
        transform.eulerAngles = new Vector3(0, 0, GetAngleFromVectorFloat(castDir));
        Destroy(gameObject, 5f);
    }

    static float GetAngleFromVectorFloat(Vector3 dir)
    {
        dir = dir.normalized;
        float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (n < 0)
        {
            n += 360;
        }

        return n+180;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PrototypeHero hero = collision.GetComponent<PrototypeHero>();
        if (hero != null)
        {
            hero.TakeDamage(damage);
        } else if (collision.GetComponent<Sensor_Prototype>() != null)
        {
            hero.TakeDamage(damage);
        }
        Destroy(gameObject);
    }
}
