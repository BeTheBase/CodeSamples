using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Babylonian;
public interface IGameManger
{
    public GameObject GetPlayer(int id);
    public void ReceiveNewPlayerPosition(Vector3 playerPosition, int id);
}

public class UpdatePlayerPosition : MonoBehaviour //GenericSingleton<UpdatePlayerPosition, IGameManger>, IGameManger
{
    public GameObject player;
    public GameObject OtherPlayer;
    public LevelLoader LevelLoader;
    private Dictionary<int, Vector3> playerPositions = new Dictionary<int, Vector3>();
    public List<BaseProjectile> ActiveProjectiles = new List<BaseProjectile>();
    bool client = true;

    private static UpdatePlayerPosition instance;
    public static UpdatePlayerPosition Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<UpdatePlayerPosition>();
                if (instance == null)
                {
                   // GameObject obj = new GameObject();
                    //obj.hideFlags = HideFlags.HideAndDontSave;
                    //instance = obj.AddComponent<UpdatePlayerPosition>();
                }
            }
            return instance;
        }
    }
    public virtual void Awake()
    {

        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void NextLevel(int index)
    {
        Debug.Log("Next level");

        LevelLoader.LoadNextLevel();
    }

    public void SyncProjectiles(Vector3 projectilePosition, int id)
    {

        //incoming projectile spawned from client so now on all other connected also spawn this projectile
        var projectile = ActiveProjectiles.Find(g => g.GetInstanceID().Equals(id));
        if (projectile!=null)
        {
            if(projectile.transform.position != projectilePosition)
            {
                projectile.transform.position = projectilePosition;
            }
        }
        else
        {
            if(ServerBehaviour.Instance!=null)
            {
                if(OtherPlayer.GetComponent<Babylonian.PlayerAttack>()!=null)
                {
                    Debug.Log("Attack"+projectilePosition);
                    OtherPlayer.GetComponent<Babylonian.PlayerAttack>().enabled = true;
                    OtherPlayer.GetComponent<Babylonian.PlayerAttack>().OnPlayerAttack(projectilePosition.z);        
                }
                else
                {
                    Debug.Log("Can't find component");
                }

            }
            else
            {
                Debug.LogError("CLIENT HAS to Update proj");
                player.GetComponent<PlayerBase>().OnPlayerAttack(projectilePosition.z);
            }
        }
        /*
        if (ActiveProjectiles[id] != null)
        {
            if (!ActiveProjectiles[id].gameObject.activeSelf && ActiveProjectiles[id] != null)
            {
                //the incoming projectile from id = not currently active in scene
                ActiveProjectiles[id].gameObject.SetActive(true);
            }
            else
            {
                //oke se we have a nullable index or index out of range because we can't find the incoming projectile in our active list
                //thats cool we can also spawn one from our object pool and put it in the activelist
                //ObjectPool.Instance.SpawnFromPool<BaseProjectile>(projectilePosition)

            }
        }*/
    }

    public void ReceiveNewPlayerPosition(Vector3 playerPosition, int id)
    {
        //wat gaan we doen:
        /*
         * Server host
         * Client connect
         * there is a connection
         * play game
         * player 1 gets controlled bij host(server)
         * player 2 gets controlled bij incomign connection(client)
         * --
         * incomging id=='playerX'
         * dus:
         * id = 1 == player1
         * id = 2 == player2
         * nu moet de update van data aangepast worden naar het verschil tussen client en server
         * 
         * 
         */


        if(id==1)
        {
            //Player 1 moved so we update now 
            //incoming playerposition is de positie van speler 1 ( aka komt binnen vanaf server )
            if (player.transform.position != playerPosition)
            {
                Vector3 tmpPosition = player.transform.position;
                player.transform.position = Vector3.Lerp(tmpPosition, playerPosition, 5*Time.deltaTime);
                //Dti werkt Debug.LogWarning("FAKA opsotie:" + playerPosition);
            }
            /*
            if (ServerBehaviour.Instance == null)
            {
                //incoming playerposition is de positie van speler 1 ( aka komt binnen vanaf server )
                if (player.transform.position != playerPosition)
                {
                    player.transform.position = playerPosition;
                    //Dti werkt Debug.LogWarning("FAKA opsotie:" + playerPosition);
                }
            }
            else
            {
                OtherPlayer.transform.position = playerPosition; 
            }*/
        }
        if(id==2)
        {
            //Debug.Log("Player2: = " + OtherPlayer + "with id: " + id.ToString());

            //Player2 moved so update now
            if (OtherPlayer.transform.position != playerPosition)
            {
                Vector3 tmpPosition = OtherPlayer.transform.position;
                OtherPlayer.transform.position = Vector3.Lerp(tmpPosition, playerPosition, 5 * Time.deltaTime);
            }
            /*
            if(ServerBehaviour.Instance != null)
            {
                //incomgin playerposition is de positie van speler 2 ( aka komt binnen vanaf client )
                if (OtherPlayer.transform.position != playerPosition)
                    OtherPlayer.transform.position = playerPosition;


               //dt werkt Debug.LogWarning("FAKA opsotie:" + playerPosition);

            }
            else
            {
                //oke so we are the client
                player.transform.position = playerPosition;
            }*/
        }

        /*
        if (id == 2)
        {
            //incomgin playerpositionupdate is not from PLayer1 so we need to update 
            //Both client and server have there own player1 
            if (OtherPlayer.GetComponent<SendPlayerPosition>().Id == id)
                OtherPlayer.transform.position = playerPosition;
            else 
                player.transform.position = playerPosition;
        }
        else if(id == 1)
        {
            if(OtherPlayer.GetComponent<SendPlayerPosition>().Id != id)
            {
                OtherPlayer.transform.position = playerPosition;
            }
            else
            {
                player.transform.position = playerPosition;
            }    
            //the incoming playerposition is from the current controlable player so do nothing
        }
        
        if (!client)
        {
            OtherPlayer.transform.position = playerPosition;//StartCoroutine(LerpToNewPositin(OtherPlayer.transform.position, playerPosition, true));
        }
        else
            player.transform.position = playerPosition;//StartCoroutine(LerpToNewPositin(player.transform.position, playerPosition, false));
        */
    }

    private IEnumerator LerpToNewPositin(Vector3 oldPosition, Vector3 newPosition, bool v)
    {
        float timeElapsed = 0;

        while (timeElapsed < 1)
        {
            if(v)
                OtherPlayer.transform.position = Vector3.Lerp(oldPosition, newPosition, timeElapsed / 1);
            else
                player.transform.position = Vector3.Lerp(oldPosition, newPosition, timeElapsed / 1);
            timeElapsed += Time.deltaTime;
            yield return null;

        }

        if (v)
            OtherPlayer.transform.position = newPosition;
        else
            player.transform.position = newPosition;
    }
}
