using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TwitchBrawl
{
    public class SimpleUnit : MonoBehaviour
    {

        public bool lToR;
        public float power;
        
        public string unitName;
        public Text nameTextObject;


        // Use this for initialization
        void Start()
        {

        }

        public void OnSpawn(bool spawnLToR, string spawnName, float spawnpPower)
        {
            lToR = spawnLToR;
            unitName = spawnName;
            nameTextObject.text = unitName;
            power = spawnpPower;

            float scaleFactor = GetScaleFactor(power);
            transform.localScale = GetScaleVector(power);

            if (lToR)
            {
                gameObject.layer = 8;
            }
            else
            {
                gameObject.layer = 9;
            }
        }

        public void TakeDamage(float damage)
        {
            StartCoroutine(TakeDamageDelayed(damage));
        }

        IEnumerator TakeDamageDelayed(float damage)
        {
            yield return null;
            Debug.Log(unitName + "took " + damage + " damage");
            power -= damage;
            if (power <= 0)
            {
                power = 0;
                StartCoroutine(destroyObjectDelayed());
            }
            else
            {
                transform.localScale = GetScaleVector(power);
            }

        }

        IEnumerator destroyObjectDelayed()
        {
            yield return null;
            GameObject.Destroy(this.gameObject);
        }

        Vector3 GetScaleVector(float power)
        {
            float scaleFactor = GetScaleFactor(power);
            return new Vector3(scaleFactor, scaleFactor, scaleFactor);
        }

        float GetScaleFactor(float power)
        {
            return Mathf.Pow(power, 0.25f) * 0.1f ;
        }

        void OnCollisionEnter(Collision col)
        {
            if ((lToR && col.collider.gameObject.layer == 9) || (!lToR && col.collider.gameObject.layer == 8))
            {
                //Debug.Log("Collision!");

                col.collider.gameObject.GetComponent<SimpleUnit>().TakeDamage(power);
            }

            if ((col.collider.gameObject.layer == 10))
            {
                col.collider.gameObject.GetComponent<Hitpoints>().DealDamage(1);
                GameObject.Destroy(this.gameObject);
            }
        }

        // Update is called once per frame
        void Update()
        {

            float movement = 0;

            if (lToR)
            {
                movement = GameVariables.Instance.unitSpeed * Time.deltaTime;
            }
            else
            {
                movement = GameVariables.Instance.unitSpeed * -1 * Time.deltaTime;
            }

            transform.Translate(new Vector3(movement, 0, 0));
        }
    }
}