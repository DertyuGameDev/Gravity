using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public float EjectionForce;
    public Transform spawnPoint;
    public GameObject obj;
    public void shoot() {
      GameObject t =  Instantiate(obj, spawnPoint.position, Quaternion.identity);
      t.GetComponent<Rigidbody>()?.AddForce(spawnPoint.forward*EjectionForce,ForceMode.Impulse);
    }
}
