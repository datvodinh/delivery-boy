using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderTest : MonoBehaviour
{
    // Start is called before the first frame update
   void OnTriggerEnter(Collider other )
   {
    if (other.gameObject.tag == "vehicle")
    {
        print("ENTER");
    }
   }
 void OnTriggerStay(Collider other)
 {
    if (other.gameObject.tag == "vehicle")
    {
        print("STAY");
    }
 }

  void OnTriggerExit(Collider other)
  {
    if (other.gameObject.tag == "vehicle")
    {
        print("EXIT");
    }
  }
   

}
