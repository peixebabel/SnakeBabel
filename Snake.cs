using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static Player;

public class Snake : MonoBehaviour {

    Vector2 dir = Vector2.right;
    Vector2 lastdir = Vector2.right;
    List<Transform> tail = new List<Transform>();
    bool ate = false;
    public GameObject tailPrefab;
    public Text pontos;
    

    // Use this for initialization
    void Start () {
        Player.points = 0;
        // Move the Snake every 100ms
        InvokeRepeating("Move", 0.1f, 0.1f);    
    }
   
    // Update is called once per Frame
    void Update() {
        // Move in a new Direction?
        lastdir = dir;
        if (Input.GetKey(KeyCode.RightArrow))
            dir = Vector2.right;
        else if (Input.GetKey(KeyCode.DownArrow))
            dir = -Vector2.up;    // '-up' means 'down'
        else if (Input.GetKey(KeyCode.LeftArrow))
            dir = -Vector2.right; // '-right' means 'left'
        else if (Input.GetKey(KeyCode.UpArrow))
            dir = Vector2.up;
    }
   
    void Move() {
        // Save current position (gap will be here)
        Vector2 v = transform.position;
        // Move head into new direction (now there is a gap)
        if (Vector2.Angle(lastdir, dir) != 180){
            transform.Translate(dir);
            // Ate something? Then insert new Element into gap
            if (ate) {
                // Load Prefab into the world
                GameObject g =(GameObject)Instantiate(tailPrefab,
                                                    v,
                                                    Quaternion.identity);

                // Keep track of it in our tail list
                tail.Insert(0, g.transform);

                Player.points += 1;
                pontos.text = "" + Player.points;

                // Reset the flag
                ate = false;
            }
            // Do we have a Tail?
            else if (tail.Count > 0) {
                // Move last Tail Element to where the Head was
                tail.Last().position = v;

                // Add to front of list, remove from the back
                tail.Insert(0, tail.Last());
                tail.RemoveAt(tail.Count-1);
            }
        }
    }
    void OnTriggerEnter2D(Collider2D coll) {
        
        if (coll.tag == "Food"){
            // Get longer in next Move call
            ate = true;
        
            // Remove the Food
            Destroy(coll.gameObject);
        }
        // Collided with Tail or Border
        else {
            
            SceneManager.LoadScene("EndScene");
        
        }
    }

    void OnCollisionEnter2D(Collision2D coll){

        if (coll.gameObject.tag == "Player"){
            SceneManager.LoadScene("EndScene");
        }

    }
    
}