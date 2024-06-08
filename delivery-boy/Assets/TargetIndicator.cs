using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetIndicator : MonoBehaviour
{
    [SerializeField]
    public GameObject Target=null;
    [SerializeField]
    public GameObject character;
    void Update(){
        if(character == null){
            transform.position = character.transform.position;
        }
        if (Target!=null){
            var dir = Target.transform.position - transform.position;
            var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }
}
