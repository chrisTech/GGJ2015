﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine.UI;


public class GameManager : MonoBehaviour {

    public Canvas MenuCanvas;
    public Canvas GameCanvas;

    public GameObject TooLateObject = null;

    public GameObject GameOverObj = null;
    
    public List<Transform> Levels;

    public int currentLevel = 0;

    public GameObject Player;
    public Player PlayerScript;
    public Animator PlayerAnimator;

    public List<AudioSource> levelSounds = new List<AudioSource>();

    public Camera cam;

    public float levelTimeLimit = 21f;

    private bool timeIsRunningOut = false;

    public GameObject VideoEnding = null;
    public GameObject VideoFenetre = null;

    public Animator kissBarbieAnimator = null;
    public Animator outRunAnimator = null;
    public Animator bigFireAnimator = null;
    public Animator ghostBusterAnimator = null;
    public Animator snakeAnimator = null;
    public Animator kameameaAnimator = null;
    public Animator boomAnimator = null;
    public Animator barbitchAnimator = null;

    public SpriteRenderer Monster;
    public SpriteRenderer Gremelins;

    public List<SpriteRenderer> MenuSprite = new List<SpriteRenderer>();

    public List<GameObject> multipleGremlins = new List<GameObject>();
    public List<GameObject> multipleMonkeys = new List<GameObject>();

    public GameObject LooserObj = null;

    public  void Awake()
    {
       // base.Init();
       // Player = GameObject.Find("Player");
       //// PlayerScript = Player.GetComponent<Player>();
       // PlayerAnimator = Player.GetComponent<Animator>();
      //  cam.enabled = false;
        if (VideoEnding == null)
            VideoEnding = GameObject.Find("EndVideo");
    }

    public void EnableMenuSprite(bool b)
    {
        foreach (var a in MenuSprite)
        {
            a.enabled = b;
        }
    }

    public void GoToGame(int playerIndex)
    {
        //Debug.Log("Player" + playerIndex);  
        //GameObject obj = Instantiate(Resources.Load("Player" + playerIndex)) as GameObject;
        //obj.name = "Player" + playerIndex;

        //Player = GameObject.Find("Player" + playerIndex);
        //PlayerAnimator = Player.GetComponent<Animator>();
        _playerIndex = playerIndex;
        DOTween.To(() => MenuCanvas.GetComponent<CanvasGroup>().alpha, x => MenuCanvas.GetComponent<CanvasGroup>().alpha = x, 0, 2f).OnComplete(EnableGameCanvas);
    }

    public void GoToMenu()
    {
        
        DOTween.To(() => GameCanvas.GetComponent<CanvasGroup>().alpha, x => GameCanvas.GetComponent<CanvasGroup>().alpha = x, 0f, 2f).OnComplete(EnableMenuCanvas);
    }

    private void EnableMenuCanvas()
    {
        MenuCanvas.enabled = true;
        GameCanvas.enabled = false;
        DOTween.To(() => MenuCanvas.GetComponent<CanvasGroup>().alpha, x => MenuCanvas.GetComponent<CanvasGroup>().alpha = x, 1f, 2f);
      //  cam.enabled = false;
    }

    public int _playerIndex = 0;

        private void EnableGameCanvas()
    {
        MenuCanvas.enabled = false;
        GameCanvas.enabled = true;
        DOTween.To(() => GameCanvas.GetComponent<CanvasGroup>().alpha, x => GameCanvas.GetComponent<CanvasGroup>().alpha = x, 1f, 2f);
        levelTimeLimit = 21f;
        timeIsRunningOut = true;
      //  cam.enabled = true;
        EnableMenuSprite(false);
        EnableLevelSprites();
        ChangeLevelMusic();
        Debug.Log("Player" + _playerIndex);
        GameObject obj = Instantiate(Resources.Load("Player" + _playerIndex)) as GameObject;
        obj.name = "Player" + _playerIndex;

        Player = GameObject.Find("Player" + _playerIndex);
        PlayerAnimator = Player.GetComponent<Animator>();

    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void GoToPreviousLevel()
    {
        if (currentLevel >= 1)
        {
            timeIsRunningOut = false;
            PlayerAnimator.SetBool("Walking", true);
            currentLevel--;
        }
        else
        {
           
            return;
        }

        for (int i = 0; i < Levels.Count; i++)
        {
          
            float newX = Levels[i].localPosition.x + offsetTranslate;
            if (i == currentLevel)
            {

            
                Levels[i].DOLocalMoveX(newX, timeTranslate).OnComplete(StopPlayer);
            }
            else
            {
                
                Levels[i].DOLocalMoveX(newX, timeTranslate);
            }
        }
    }

    public void ChangeLevelMusic()
    {
        int i = 0;
        foreach (var a in levelSounds)
        {
            if (i != currentLevel)
                a.Stop();
            else
                a.Play();
            i++;
        }
    }

    public float timeTranslate = 5f;
    public float offsetTranslate =1024f;

    public void GoToNextLevel()
    {
      
        if (currentLevel < Levels.Count - 1)
        {
            

            timeIsRunningOut = false;
            if (PlayerAnimator == null)
                return;
        
            PlayerAnimator.SetBool("Walking", true);
            currentLevel++;
        }
        else
        {
            Debug.Log("Congratulations !!!");
            return;
        }

        for (int i = 0; i < Levels.Count; i++)
        {
           // var levelSprites = Levels[i].GetComponentsInChildren<SpriteRenderer>();
            float newX = Levels[i].localPosition.x - offsetTranslate;
            if (i == currentLevel)
            {

                //foreach (var ls in levelSprites)
                //{
                //    ls.enabled = true;
                //}
                Levels[i].DOLocalMoveX(newX, timeTranslate).OnComplete(StopPlayer);
            }
            else
            {
                //foreach (var ls in levelSprites)
                //{
                //    ls.enabled = false;
                //}
                Levels[i].DOLocalMoveX(newX, timeTranslate);
            }
        }
    }

    public void EnableLevelSprites()
    {
        for (int i = 0; i < Levels.Count; i++)
        {
            var levelSprites = Levels[i].GetComponentsInChildren<SpriteRenderer>();
       
            if (i == currentLevel)
            {

                foreach (var ls in levelSprites)
                {
                    ls.enabled = true;
                }
              
            }
            else
            {
                foreach (var ls in levelSprites)
                {
                    ls.enabled = false;
                }
             
            }
        }
    }

    public void DisableAllLevelSprites()
    {
        for (int i = 0; i < Levels.Count; i++)
        {
            var levelSprites = Levels[i].GetComponentsInChildren<SpriteRenderer>();

            foreach (var ls in levelSprites)
            {
                ls.enabled = false;
            }
        }
    }

    public Text TimeLeft;

    public void StopPlayer()
    {
        PlayerAnimator.SetBool("Walking", false);
        
        ChangeLevelMusic();
        EnableLevelSprites();
        levelTimeLimit = 20f;
        timeIsRunningOut = true;

    }

    private float xVelo = 0f;

    IEnumerator LevelTransition()
    {

        for (int i = 0; i < Levels.Count; i++)
        {
            Vector3 newPosCurrent = new Vector3(Levels[i].localPosition.x - 638f, Levels[i].localPosition.y, Levels[i].localPosition.z);
            //Levels[i].localPosition = Vector3.Lerp(Levels[i].localPosition, newPosCurrent, 2f);
            float newX = Mathf.SmoothDamp(Levels[i].localPosition.x, newPosCurrent.x, ref xVelo, 1f);
            
            Levels[i].Translate(new Vector3(newX, Levels[i].localPosition.y, Levels[i].localPosition.z));
           
            //float newX = Levels[i].localPosition.x;
            //float newNewX = Levels[i].localPosition.x - 638f;
            //DOTween.To(() => newX, x => newX = x, newNewX, 3f);
            //Levels[i].localPosition = new Vector3(newX, Levels[i].localPosition.y, Levels[i].localPosition.z);

        }
        yield return null;
    }

    void Update()
    {

        #region Input A
        if (Input.GetKeyDown(KeyCode.A) && levelTimeLimit > 0)
        {
            if (currentLevel == 0)
            {
               
                VideoFenetre.SetActive(true);
                var lol = VideoFenetre.GetComponent<lolili>();
                lol.InitLeBordel();
              
                if (lol.duration != 0 && !lol.once)
                {
                    Destroy(Player);
                    DisableAllLevelSprites();
                    lol.Play();
                    Invoke("RestartGame", lol.duration);
                }
            }
            else if (currentLevel == 1)
            {
                Debug.Log("Electrocution");
                PlayerAnimator.SetBool("Electrocuted", true);
                Invoke("GameOver", 3f);

            }
            else if (currentLevel == 2)
            {
                outRunAnimator.SetBool("OutRunning", true);
                //kissBarbieAnimator.SetBool("Kissing", true);
               // Invoke("DisableKissingBarbie", 1.5f);
                //Invoke("GameOver", 3f);
                //Invoke("RestartGame", 2f);
                Invoke("RestartGame", 5f);
            }
            else if (currentLevel == 3)
            {
                ghostBusterAnimator.SetBool("CalledGhostBuster", true);
                Monster.enabled = false;
                Invoke("DisableGhostBuster", 3.5f);
                Invoke("GoToNextLevel", 3.8f);
            }
            else if (currentLevel == 4)
            {
                Debug.Log("Sprite barbie tueuse + perso tombe");
                barbitchAnimator.SetBool("Barbitch", true);
                Invoke("GameOver", 3f);
            }
            else if (currentLevel == 5)
            {
                VideoFenetre.SetActive(true);
                var lol = VideoFenetre.GetComponent<lolili>();
                lol.InitLeBordel();

                if (lol.duration != 0 && !lol.once)
                {
                    Destroy(Player);
                    DisableAllLevelSprites();
                    lol.Play();
                    Invoke("RestartGame", lol.duration);
                }
            }
            else if (currentLevel == 6)
            {
                Debug.Log("multiplication gremelins");
                foreach (var g in multipleGremlins)
                {
                    g.SetActive(true);
                }
                Invoke("GameOver", 3f);
            }
            else if (currentLevel == 7)
            {
                GameOver();
            }
        }
        #endregion
        #region Input Z
        else if (Input.GetKeyDown(KeyCode.Z) && levelTimeLimit > 0)
        {
            if (currentLevel == 0)
            {
                GoToNextLevel();
            }
            else if (currentLevel == 1)
            {
                GoToNextLevel();
            }
            else if (currentLevel == 2)
            {
               // Pousse Barbie
                Debug.Log("Pousse Barbie");
                Invoke("GameOver", 3f);
               
                //RestartGame();
            
            }
            else if (currentLevel == 3)
            {
                Debug.Log("Boomerang");
                PlayerAnimator.SetBool("Boomerang", true);
                boomAnimator.SetBool("Boom", true);
                Invoke("TeteDecoupe", 2f);
                Invoke("GameOver", 3f);
            }
            else if (currentLevel == 4)
            {
                Debug.Log("Kameamea");
                kameameaAnimator.SetBool("Kameameaing", true);
                Invoke("DisableAllLevelSprites", 2.2f);
                Invoke("GoToNextLevel", 2.2f);
            }
            else if (currentLevel == 5)
            {
                // Money coming
                foreach (var m in multipleMonkeys)
                {
                    m.SetActive(true);
                }
                Invoke("GameOver", 3f);

            }
            else if (currentLevel == 6)
            {
                // resize gremlins
                Gremelins.gameObject.transform.DOScale(new Vector3(800f, 800f, 800f), 3f);
                Invoke("GameOver", 3f);
            }
            else if (currentLevel == 7)
            {
                Application.LoadLevel("retoto");
            }
        }
        #endregion
        #region Input E
        else if (Input.GetKeyDown(KeyCode.E) && levelTimeLimit > 0)
        {
            if (currentLevel == 0)
            {
                DisableAllLevelSprites();
                LooserObj.SetActive(true);
                Invoke("RestartGame", 3f);
               // GoToNextLevel();
            }
            else if (currentLevel == 1)
            {

            }
            else if (currentLevel == 2)
            {
                kissBarbieAnimator.SetBool("Kissing", true);
                Invoke("DisableKissingBarbie", 1.5f);
                Invoke("GoToNextLevel", 2f);
            }
            else if (currentLevel == 3)
            {
                bigFireAnimator.SetBool("OnFire", true);
                Invoke("GameOver", 3f);
            //    Invoke("RestartGame", 2f);
               
            }
            else if (currentLevel == 4)
            {
                snakeAnimator.SetBool("ShowSnake", true);
                Invoke("GameOver", 3f);
             //   Invoke("RestartGame", 2f);
            }
            else if (currentLevel == 5)
            {
                GoToNextLevel();
            }
            else if (currentLevel == 6)
            {
                GoToNextLevel();
            }
        }
        #endregion

        TimeLeft.text = levelTimeLimit.ToString("0");
        if (timeIsRunningOut)
            levelTimeLimit -= Time.deltaTime;
        if (levelTimeLimit <= 0)
        {
            timeIsRunningOut = false;
            DisableAllLevelSprites();
            TooLateObject.SetActive(true);
            Invoke("RestartGame", 3f);
           // Debug.Log("FUCK !!! You Lose");
          //  VideoEnding.SetActive(true);
            //var lol = VideoEnding.GetComponent<lolili>();
            //lol.InitLeBordel();
            //if (lol.duration != 0 && !lol.once)
            //{
            //    Destroy(Player);
            //    DisableAllLevelSprites();
            //    lol.Play();
            //    Invoke("RestartGame", lol.duration);
            //}
            
               
          //  Application.LoadLevel("main");
        }
    }

    private void TeteDecoupe()
    {
        PlayerAnimator.SetBool("Boomerang", false);
        PlayerAnimator.SetBool("Decoupe", true);
    }

    private void DisableKissingBarbie()
    {
        kissBarbieAnimator.SetBool("Kissing", false);
    }

    private void DisableGhostBuster()
    {
        ghostBusterAnimator.SetBool("CalledGhostBuster", false);
    }


    public void GameOver()
    {
        Destroy(Player);
        DisableAllLevelSprites();
         GameOverObj.SetActive(true);
        Invoke("RestartGame", 3f);
    }

    public void RestartGame()
    {
        Application.LoadLevel("main");
    }
}
