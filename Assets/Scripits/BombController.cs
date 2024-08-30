using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using UnityEngine.Tilemaps;

public class BombController : MonoBehaviour
{
  [Header ("Bomb")]
  public AudioSource  audioSource;
  //audioSource = GetComponent<AudioSource>();
    public GameObject bombPrefab;
    public KeyCode inputKey = KeyCode.Space;
    public float bombFuseTime=5f;
    public int bombAmount = 1;
    //[ SerializeField] private Text BombText;
   
    private int bombsRemaining ;
    [Header("Explosion")]
      public Explosion explosionPrefab;
      public float explosionDuration=1f;
      public int explosionReduis=1;
      public LayerMask explosionLayerMask ;
[Header ("Destructible")]
public Tilemap destructibleTiles;
public Destructible destructiblePrefab;
    private void OnEnable()
    {
     bombsRemaining=bombAmount;
    }
    private void Update()
    {
      if (bombsRemaining >0 && Input.GetKeyDown(inputKey) )
      {
        StartCoroutine(PlaceBomb());
      }
    }
      private IEnumerator PlaceBomb()
      {
        Vector2 position=transform.position;
        position.x= Mathf.Round(position.x);
        position.y= Mathf.Round(position.y);
        GameObject bomb = Instantiate(bombPrefab,position,Quaternion.identity);
        bombsRemaining--;
        yield return new WaitForSeconds(bombFuseTime);
         if (audioSource != null) {
        audioSource.Play(); // Play the explosion sound
    }
  
        position=bomb.transform.position;
        position.x= Mathf.Round(position.x);
        position.y= Mathf.Round(position.y);
         Explosion explosion = Instantiate(explosionPrefab,position,Quaternion.identity);
         explosion.SetActiveRenderer(explosion.start);
         explosion.DestroyAfterSeconds(explosionDuration);
         Destroy(explosion.gameObject,explosionDuration);
         Explode(position ,Vector2.up,explosionReduis);
        Explode(position ,Vector2.down,explosionReduis);
        Explode(position ,Vector2.left,explosionReduis);
        Explode(position ,Vector2.right,explosionReduis);
        Destroy(bomb);
        bombsRemaining++;

      }
      private void Explode(Vector2 position,Vector2 direction,int length)
      {

        if(length<=0){
        
          return;
        }
        position+=direction;
        if(Physics2D.OverlapBox(position, Vector2.one/2f,0f,explosionLayerMask)){
          ClearDestructible(position);
          return;
        }
        Explosion explosion=Instantiate(explosionPrefab,position,Quaternion.identity);
        explosion.SetActiveRenderer(  length>1?explosion.middle:explosion.end);
        explosion.SetDirection(direction);
        explosion.DestroyAfterSeconds(explosionDuration);
        
        Explode(position,direction,length-1);

    
}
private void ClearDestructible(Vector2 position){
  Vector3Int cell =destructibleTiles.WorldToCell(position);
  TileBase tile=destructibleTiles.GetTile(cell);
  if(tile!=null){
    Instantiate(destructiblePrefab ,position,Quaternion.identity);
    destructibleTiles.SetTile(cell,null);
  }
}
public void AddBomb(){
  bombAmount++;
  
  bombsRemaining++;
}
//[SerializeField] private Text bombsRemainingText;
// private void OnTriggerEnter2D(Collider2D collision)
// {
//     if (collision.gameObject.CompareTag("Bomb"))
//     {
//        // bombAmount++;
//         //bombsRemaining++;
//         AddBomb();
//         UpdateBombsRemainingText();
//     }
// }

// private void UpdateBombsRemainingText()
// {
//     bombsRemainingText.text = "Bombs: " + bombsRemaining + "/" +  bombAmount;
// }
}