
using UnityEngine;

public class Destructible : MonoBehaviour
{
   [Range(0f,1f)]
   public float distructionTime=1f;
   public float itemSpawnChance=0.2f;
   public GameObject [] spawnableItems;

    void Start()
    {
       Destroy(gameObject,distructionTime) ;
    }
    private void OnDestroy(){
      if(spawnableItems.Length>0&&Random.value<itemSpawnChance){
         int randomIndex = Random.Range(0,spawnableItems.Length);
         Instantiate(spawnableItems[randomIndex],transform.position,Quaternion.identity);
      }
    }

    
}
