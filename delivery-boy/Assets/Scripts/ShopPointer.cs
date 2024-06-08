using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopPointer : MonoBehaviour
{
    void Update()
    {
        var Target = GameObject.FindGameObjectsWithTag("Shop")[0];
        if (Target != null)
        {
            var dir = Target.transform.position - transform.position;
            var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }
}
