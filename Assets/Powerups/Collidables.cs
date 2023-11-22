using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class Collidables : MonoBehaviour
{

    bool iscollided;
    bool isTranslating;
    bool isRotating;
    bool isScaling;
   public bool isReverse;
    bool isFiring;


    [SerializeField] GameObject _Player; 
    [SerializeField] Rigidbody2D _Bullet;
    private float force = 50;


    private void OnTriggerEnter2D(UnityEngine.Collider2D collision)
    {
        var Powerup = GameObject.Find("Cookie").GetComponent<POWERUP>();
         var Powerups = Powerup.GetAllChildren(_Player);
        var Stack = new Stack<GameObject>(Powerups);
       
        var lastPower = Stack.Peek();
        print (Stack.Peek().tag);
        if (collision.transform.CompareTag("Bullet"))
        {
            Destroy(gameObject);
        }
        if (collision.transform.CompareTag("Player"))
        {
            iscollided = false;
            if (Powerup.FindChildrenWithName("Childname", Powerup.Player).Count >= 1)
            {
                if(lastPower.tag == "Translate")
                {
                    iscollided = true;
                    isTranslating = true;
                    Destroy(lastPower);
                }
                if (lastPower.tag == "Rotate")
                {
                    iscollided = true;
                    isRotating = true;
                    Destroy(lastPower);
                }
                if (lastPower.tag == "Scale")
                {

                    isScaling = true;
                    iscollided = true;

                    Destroy(lastPower);
                }
                if (lastPower.tag == "PointandShoot")
                {
                     isFiring = true;
                    iscollided = true;
                    Destroy(lastPower);
                }



            }
                
        }
    }

    private void Update()
    {
        StartCoroutine(ApplyAndStop());

    }
    private IEnumerator ApplyAndStop()
    {
        var ReverseR = GameObject.Find("ReverseR").GetComponent<Reverse>();
        var ReverseS = GameObject.Find("ReverseSpeed").GetComponent<Reverse>();
        var Powerup = GameObject.Find("Cookie").GetComponent<POWERUP>();
        if (isTranslating == true && GameObject.Find("Wall1").GetComponent<Collidables>().returnIsCollided() == true)
        {
            if(ReverseS.isReverse != true)
            {
                GameObject.Find("Wall1").transform.position += Vector3.right * Powerup.returnSpeed();

            }
            else
            {
                GameObject.Find("Wall1").transform.position += Vector3.left * Powerup.returnSpeed();
            }

            yield return new WaitForSeconds(3) ;
            isTranslating =false   && GameObject.Find("Wall1").GetComponent<Collidables>().returnIsCollided() == false;

        }
        if (isRotating == true && GameObject.Find("Wall1").GetComponent<Collidables>().returnIsCollided() == true)
        {
            if (ReverseR.isReverse != true)
            {
                transform.Rotate(0f, 0f, 90f * Powerup.returnSpeed());

            }
            else
            {
                transform.Rotate(0f, 0f, 90f * -Powerup.returnSpeed());
            }

            yield return new WaitForSeconds(10);
            isRotating = false && GameObject.Find("Wall1").GetComponent<Collidables>().returnIsCollided() == false;

        }
        if (isScaling == true && GameObject.Find("Wall1").GetComponent<Collidables>().returnIsCollided() == true)
        {
            GameObject.Find("Wall1").transform.localScale += new Vector3(-0f, -0.1f, 0f);
            yield return new WaitForSeconds(3);
            isScaling = false && GameObject.Find("Wall1").GetComponent<Collidables>().returnIsCollided() == false;

        }
        
        if (isFiring == true && GameObject.Find("Wall1").GetComponent<Collidables>().returnIsCollided() == true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                
                    Rigidbody2D rb = Instantiate(_Bullet, _Player.transform.position + new Vector3(0.5f, 0, 0), transform.rotation);
                    rb.AddForce(force * Vector3.right, ForceMode2D.Impulse);
                    Destroy(rb.gameObject, 5);
                yield return new WaitForSeconds(1f);
                isFiring = false && GameObject.Find("Wall1").GetComponent<Collidables>().returnIsCollided() == false;
            }
            
        }



    }


    public bool returnIsCollided()
    {
        return iscollided;
    }
}
