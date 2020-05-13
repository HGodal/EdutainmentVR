using System.Collections.Generic;
using UnityEngine;
using VRTK.Prefabs.Locomotion.Teleporters;
using Zinnia.Data.Type;
using UnityEngine.SceneManagement;
public class PauseMenuLogic : MonoBehaviour
{
    public TeleporterFacade teleporter; //this will actually do our teleportation
    public Transform playArea; //This is the area the teleportet moves
    public Transform headOrientation;
    public Transform pauseLocation; // here will the player be teleporteted to
    public Transform gameLocation; // Here will the player be teleported to when exit pausemenu

    protected bool inPauseMenu = false;

    public List<GameObject> pauseItems;
    public List<GameObject> gameItems;

    public GameObject teleportationRelease;
    public GameObject teleportationPress;

    public void SwitchRooms()
    {

        // makes if statement to see where the player is when pressing menu button
        TransformData teleportDestination = new TransformData(gameLocation);
        if (!inPauseMenu)
        {
            gameLocation.position = new Vector3(headOrientation.position.x, playArea.position.y, headOrientation.position.z);

            Vector3 right = Vector3.Cross(playArea.up, headOrientation.forward);
            Vector3 forward = Vector3.Cross(right, playArea.up);

            gameLocation.rotation = Quaternion.LookRotation(forward, playArea.up);

            teleportDestination = new TransformData(pauseLocation);
        }

        teleporter.Teleport(teleportDestination);
        inPauseMenu = !inPauseMenu;

        foreach (GameObject item in pauseItems)
        {
            item.SetActive(inPauseMenu);
        }

        foreach (GameObject item in gameItems)
        {
            item.SetActive(!inPauseMenu);
        }

        if (GameObject.Find("/RoomsAndVR/Logic/CountdownLogic"))
        {
            GameObject.Find("/RoomsAndVR/Logic/CountdownLogic").GetComponent<Countdown>().isPaused = inPauseMenu;
        }
    }
    public void ResetGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }

    public void SwitchTeleportationToPress(bool value)
    {
        teleportationRelease.SetActive(!value);
        teleportationPress.SetActive(value);
    }
    public void SwitchTeleportationToRelease(bool value)
    {
        SwitchTeleportationToPress(!value);
    }
}
