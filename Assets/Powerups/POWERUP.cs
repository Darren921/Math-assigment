using Platformer.Mechanics;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class POWERUP : MonoBehaviour
{
    /*****************************/
    /* these are shown in the inspector in unity */
    /* this lets you have a dropdown in the inspector */
    public enum PowerupType
    {
        TRANSLATE = 0,
        ROTATE,
        SCALE,
        PointAndShoot
        

    };

    [SerializeField] Collidables _collidables;
    [SerializeField] PowerupType PType;

    [SerializeField] public float Speed;

    [SerializeField] Vector3 deltaVector;

    [SerializeField] Transform PlayerPos;

    [SerializeField] public GameObject Player;


    [SerializeField] Vector3 offset; // offset for children object positions
    /*****************************/


    bool isON = false; // is this powerup active?

    public float returnSpeed()
    {
        return Speed;
    }
    /*****************************/
    /* utility functions you may need or want */
    /* utility function to get a list of children of a given game object */
    public List<GameObject> GetChildren(GameObject obj)
    {
        Debug.Log("GetChildren");
        List<GameObject> children = new List<GameObject>();
        foreach (Transform child in obj.transform)
        {
            children.Add(child.gameObject);
        }
        return children;
    }

    /* utility function to get a list of ALL children of a given game object */
    public List<GameObject> GetAllChildren(GameObject obj)
    {
        List<GameObject> children = GetChildren(obj);
        for (var i = 0; i < children.Count; i++)
        {
            List<GameObject> moreChildren = GetChildren(children[i]);
            for (var j = 0; j < moreChildren.Count; j++)
            {
                children.Add(moreChildren[j]);
            }
        }
        return children;
    }

    /* utility function to get a list of ALL children of a given game object with a particular NAME */
    public List<GameObject> FindChildrenWithName(string nam, GameObject obj)
    {
        List<GameObject> children = GetAllChildren(obj);
        List<GameObject> results = new List<GameObject>();
        for (var i = 0; i < children.Count; i++)
        {
            if (children[i].name == nam)
                results.Add(children[i].gameObject);
        }
        return results;
    }
    /*****************************/

    /* feel free to use/modify/change all of these */
    /* there are more than one way to solve this problem */
    void applyPowerUp(POWERUP p)
    {
        if (p)
        {
            switch (p.PType)
            {

                case PowerupType.TRANSLATE:

                    break;
                case PowerupType.ROTATE:
                    /* do something here */
                    break;
                case PowerupType.SCALE:
                    /* do something here */
                    break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isON = !isON;

            LayerMask mask = LayerMask.GetMask("PowerUp");
            RaycastHit2D raycastHit = Physics2D.GetRayIntersection(Camera.main.ScreenPointToRay(Input.mousePosition),Mathf.Infinity,mask);
          
        
    
            // player triggered this
            // make a small thumbnail here and add as a child to the player

            // make a clone of ourselves

            // change the name of the object, you may wish to use something different 
            // to denote the different powerups 
            if (PType == PowerupType.TRANSLATE && raycastHit.transform.name == "Cookie")
            {
                var obj = Instantiate(this, PlayerPos);
                string childname = "Childname";
                obj.name = childname;
                obj.tag = "Translate";
                // remove all powerup component scripts from the clone 
                // otherwise you will have an infinite loop and it will crash your PC
                Destroy(obj.GetComponent<POWERUP>());
                // how many children are already attached to the player?
                // you may wish to use a specific powerup name to see how many powerups are already applied
                // hint: think of only having 1 type of powerup shown, maybe you need to do something here or before
                int numChildren = FindChildrenWithName(childname, Player).Count;

                // set the position of the child based on how many already exist
                obj.transform.localPosition = new Vector3(offset.x * numChildren, 0.25f, 0f);

                // set the scale to be small
                obj.transform.localScale /= 5f;

            }
            if (PType == PowerupType.ROTATE &&  raycastHit.transform.name == "Donut")
            {
                var obj = Instantiate(this, PlayerPos);
                string childname = "Childname";
                obj.name = childname;
                obj.tag = "Rotate";
                // remove all powerup component scripts from the clone 
                // otherwise you will have an infinite loop and it will crash your PC
                Destroy(obj.GetComponent<POWERUP>());

                // how many children are already attached to the player?
                // you may wish to use a specific powerup name to see how many powerups are already applied
                // hint: think of only having 1 type of powerup shown, maybe you need to do something here or before
                int numChildren = FindChildrenWithName(childname, Player).Count;

                // set the position of the child based on how many already exist
                obj.transform.localPosition = new Vector3(offset.x * numChildren, 0.25f, 0f);

                // set the scale to be small
                obj.transform.localScale /= 5f;

            }
            if (PType == PowerupType.SCALE && raycastHit.transform.name == "GummyBear")
            {
                var obj = Instantiate(this, PlayerPos);
                string childname = "Childname";
                obj.name = childname;
                obj.tag = "Scale";
                // remove all powerup component scripts from the clone 
                // otherwise you will have an infinite loop and it will crash your PC
                Destroy(obj.GetComponent<POWERUP>());

                // how many children are already attached to the player?
                // you may wish to use a specific powerup name to see how many powerups are already applied
                // hint: think of only having 1 type of powerup shown, maybe you need to do something here or before
                int numChildren = FindChildrenWithName(childname, Player).Count;

                // set the position of the child based on how many already exist
                obj.transform.localPosition = new Vector3(offset.x * numChildren, 0.25f, 0f);


                // set the scale to be small
                obj.transform.localScale /= 5f;
            }

                if (PType == PowerupType.PointAndShoot && raycastHit.transform.name == "Licorice")
                {
                    var obj = Instantiate(this, PlayerPos);
                    string childname = "Childname";
                    obj.name = childname;
                    obj.tag = "PointandShoot";
                    // remove all powerup component scripts from the clone 
                    // otherwise you will have an infinite loop and it will crash your PC
                    Destroy(obj.GetComponent<POWERUP>());

                    // how many children are already attached to the player?
                    // you may wish to use a specific powerup name to see how many powerups are already applied
                    // hint: think of only having 1 type of powerup shown, maybe you need to do something here or before
                    int numChildren = FindChildrenWithName(childname, Player).Count;

                    // set the position of the child based on how many already exist
                    obj.transform.localPosition = new Vector3(offset.x * numChildren, 0.25f, 0f);


                    // set the scale to be small
                    obj.transform.localScale /= 5f;
                    
            }
            
            if (isON)
            {
                // apply powerup to the object this script is attached to
                // you may wish to do this elsewhere
                applyPowerUp(this);
            }
        }

    }


   
}