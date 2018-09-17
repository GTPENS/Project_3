using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class CoreManager : MonoBehaviour {
    

    [SerializeField] GameObject prefabSky;
    [SerializeField] List<GameObject> listOfPrefabBackgroundCloud;
    [SerializeField] List<GameObject> listOfPrefabCloud;
    [SerializeField] GameObject prefabPlayer;
    //[SerializeField] GameObject prefabDrone;
    [SerializeField] Text txtAccInput;

    private List<GameObject> listOfBackgroundObjects = new List<GameObject>();
    private List<GameObject> listOfObstacle = new List<GameObject>();

    private bool isPlaying = false;

    #region Enum
    private const int ENUM_ID_SKY = 0;
    private const int ENUM_ID_CLOUD_A = 1;
    private const int ENUM_ID_CLOUD_B = 2;
    private const int ENUM_ID_CLOUD_C = 3;
    #endregion
    // Use this for initialization
    void Start () {
        isPlaying = true;
        Debug.Log("Start");
        DOTween.Init(true, true, LogBehaviour.Default);
        attachPlayer(Vector3.zero);
        attachBackgroundObjects(new Vector3(0, -7.8f, 0), ENUM_ID_SKY, 15);
        StartCoroutine("delayedVictory");
        StartCoroutine("onFrequentlySpawnClouds");
	}
	
	// Update is called once per frame
	void Update () {
        updatePlayer();
        updateBackgroundObjects();
	}

    //UPDATE FUNCTIONS
    private void updatePlayer(){
        getPlayer().update();
    }

    private void OnGUI() {
        GUI.skin.label.fontSize = GUI.skin.box.fontSize = GUI.skin.button.fontSize = 32;

        GUI.Box(new Rect(10, 10, 400, 100), string.Format("{0:0.000}", Input.acceleration));
    }

    private void updateObstacle(){
        for (int i = 0; i < listOfObstacle.Count; i++){
            //listOfObstacle[i].update();
        }
    }

    private void updateBackgroundObjects() {
        for (int i = 0; i < getNumberOfBackgroundObjects(); i++) {
            if(getBackgroundObject(i).transform.position.y>=getBackgroundObjectScript(i).getBoundsSize().y/2 + Camera.main.orthographicSize) {
                Destroy(getBackgroundObject(i));
                listOfBackgroundObjects.RemoveAt(i);
            }
        }
    }

    private void checkColision(){

    }

    private IEnumerator onFrequentlySpawnClouds() {
        while (isPlaying) {
            Debug.Log("Spawn clouds");
            yield return new WaitForSecondsRealtime(UnityEngine.Random.Range(3f, 6f));
            attachBackgroundObjects(new Vector3(UnityEngine.Random.Range(-5f, 5f), -8), (int)Mathf.Floor(UnityEngine.Random.Range(1f, 3f)), 5f);
        }
    }

    private IEnumerator delayedVictory() {
        yield return new WaitForSecondsRealtime(60f);
        isPlaying = false;
    }

    #region Player Attacher Remover
    private void attachPlayer(Vector3 _position) {
        prefabPlayer = Instantiate(prefabPlayer, _position, Quaternion.identity);
    }

    private Player getPlayer() {
        return prefabPlayer.GetComponentInChildren<Player>();
    }
    #endregion

    #region Obstacle Attacher Remover
    /*
    private void attachObstacle(Vector3 _position, int _id = 1) {
        GameObject newObstacle;
        switch (_id) {
            case 1:
                newObstacle = Instantiate(prefabDrone, _position, Quaternion.identity);
                break;
            default:
                newObstacle = Instantiate(prefabDrone, _position, Quaternion.identity);
                break;
        }
        listOfObstacle.Add(newObstacle.GetComponent<Obstacle>());
    }

    private void removeObstacle(int _index) {
        Destroy(listOfObstacle[_index].gameObject);
        listOfObstacle.RemoveAt(_index);
    }
    */
    #endregion

    #region Background Objects Attacher Remover
    private void attachBackgroundObjects(Vector3 _position, int _id, float _crossingScreenDuration) {
        GameObject newBackgroundObject;
        switch (_id) {
            case ENUM_ID_SKY:
                newBackgroundObject = Instantiate(prefabSky) as GameObject;
                newBackgroundObject.transform.position = _position;
                newBackgroundObject.AddComponent<ParallaxBackgroundObject>();
                newBackgroundObject.GetComponent<ParallaxBackgroundObject>().StayInScreen = true;
                break;
            case ENUM_ID_CLOUD_A:
                newBackgroundObject = Instantiate(listOfPrefabBackgroundCloud[0]) as GameObject;
                newBackgroundObject.transform.position = _position;
                newBackgroundObject.AddComponent<ParallaxBackgroundObject>();
                newBackgroundObject.GetComponent<ParallaxBackgroundObject>().StayInScreen = false;
                break;
            case ENUM_ID_CLOUD_B:
                newBackgroundObject = Instantiate(listOfPrefabBackgroundCloud[1]) as GameObject;
                newBackgroundObject.transform.position = _position;
                newBackgroundObject.AddComponent<ParallaxBackgroundObject>();
                newBackgroundObject.GetComponent<ParallaxBackgroundObject>().StayInScreen = false;
                break;
            case ENUM_ID_CLOUD_C:
                newBackgroundObject = Instantiate(listOfPrefabBackgroundCloud[2]) as GameObject;
                newBackgroundObject.transform.position = _position;
                newBackgroundObject.AddComponent<ParallaxBackgroundObject>();
                newBackgroundObject.GetComponent<ParallaxBackgroundObject>().StayInScreen = false;
                break;
            default:
                newBackgroundObject = Instantiate(listOfPrefabBackgroundCloud[0]) as GameObject;
                newBackgroundObject.transform.position = _position;
                newBackgroundObject.AddComponent<ParallaxBackgroundObject>();
                newBackgroundObject.GetComponent<ParallaxBackgroundObject>().StayInScreen = false;
                break;
        }
        newBackgroundObject.GetComponent<ParallaxBackgroundObject>().Duration = _crossingScreenDuration;
        Debug.Log(newBackgroundObject.GetComponent<ParallaxBackgroundObject>().StayInScreen);
        if (_id==ENUM_ID_SKY)
            newBackgroundObject.transform.DOMoveY(newBackgroundObject.GetComponent<Renderer>().bounds.size.y / 2 - Camera.main.orthographicSize, _crossingScreenDuration, false);
        else
            newBackgroundObject.transform.DOMoveY(newBackgroundObject.GetComponent<Renderer>().bounds.size.y / 2 + Camera.main.orthographicSize, _crossingScreenDuration, false);
        listOfBackgroundObjects.Add(newBackgroundObject);
    }
    private int getNumberOfBackgroundObjects() {
        return listOfBackgroundObjects.Count;
    }
    private GameObject getBackgroundObject(int _id) {
        return listOfBackgroundObjects[_id];
    }
    private ParallaxBackgroundObject getBackgroundObjectScript(int _id) {
        return listOfBackgroundObjects[_id].GetComponent<ParallaxBackgroundObject>();
    }
    #endregion

}
