using UnityEngine;

public class WorldManager : MonoBehaviour 
{
    public GameObject WoodenHomeDoor;

    public void OnEnable()
    {
        WorldEvents.OnWoodenHomeDoorOpened += OpenWoodenHomeDoor;
    }

    public void OnDisable() 
    { 
        WorldEvents.OnWoodenHomeDoorOpened -= OpenWoodenHomeDoor;
    }


    public void OpenWoodenHomeDoor()
    {
        WoodenHomeDoor.SetActive(false);
    }

}
