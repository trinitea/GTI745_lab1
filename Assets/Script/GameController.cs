using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class GameController : MonoBehaviour
{
    public Flag CurrentPlayerFlag { get; private set; }

    public uint NbWinX { get; private set; }
    public uint NbWinO { get; private set; }

    public uint NbRemainingTiles { get; private set; }

    private bool GameSet { get; set; }

    [SerializeField]
    private Text TextScore;
    [SerializeField]
    private Text TextVictory;
    
    [SerializeField]
    private Image NextFlagIcon;

    [SerializeField]
    private List<Tile> tiles;
    
    [SerializeField]
    private DialogHelper DialogComponent;

    public AudioClip JingleWin;
    public AudioClip JingleDraw;
    public AudioClip JinglePiece;

    private AudioSource audioSource;

    void Awake()
    {
        Utility.DialogComponent = DialogComponent;
        audioSource = GetComponent<AudioSource>();
    }

    // Use this for initialization
    void Start ()
    {
        GameSet = true;
        TextVictory.enabled = false;

        CurrentPlayerFlag = Flag.None;

        if (tiles.Count != 9)
        {
            Debug.LogError("9 tiles required");
        }

        TextScore.text = "X:0 vs O:0";

        DialogComponent.ShowPlayerOptionDialog(InitNewMatch);
    }

    private void InitNewMatch(Flag startingFlag)
    {
        GameSet = false;
        NbRemainingTiles = 9;

        foreach (Tile tile in tiles)
        {
            tile.SetFlag(Flag.None);
        }

        CurrentPlayerFlag = startingFlag != Flag.None ? startingFlag : Flag.X; // can be set as random, X always starting or last Winner

        NextFlagIcon.sprite = FlagProperties.Instance.GetSpriteFor(CurrentPlayerFlag);
        NextFlagIcon.color = FlagProperties.Instance.GetColorFor(CurrentPlayerFlag);

        NextFlagIcon.GetComponent<Mask>().showMaskGraphic = true;
    }

    public void RestartPressed()
    {
        if(GameSet)
        {
            DoRestart();
        }
        else
        {
            DialogComponent.ShowConfirmDialog("Recommencer la partie ?", DoRestart);
        }
    }

    private void DoRestart(bool restart = true)
    {
        if(restart)
        {
            DialogComponent.ShowPlayerOptionDialog(InitNewMatch);
        }
    }

    public void ExitPressed()
    {
        DialogComponent.ShowConfirmDialog("Voulez-vous quitter ?", DoQuit);
    }

    private void DoQuit(bool quit = true)
    {
        if(quit)
        {
            Application.Quit();
        }
    }

    public void TilePressed(Tile tile)
    {
        if (tile.flag != Flag.None || GameSet)
        {
            return;
        }
        
        audioSource.PlayOneShot(JinglePiece);

        tile.SetFlag(CurrentPlayerFlag);
        NbRemainingTiles--;

        Flag flag = TestWinScenario();

        if (flag != Flag.None)
        {
            GameSet = true;

            audioSource.PlayOneShot(JingleWin);

            NextFlagIcon.GetComponent<Mask>().showMaskGraphic = false;

            TextVictory.enabled = true;
            TextVictory.text = flag.ToString() + " a gagné !";

            if (flag == Flag.X) NbWinX += 1;
            else NbWinO += 1;

            TextScore.text = String.Format("X:{0} vs O:{1}", NbWinX, NbWinO);
        }
        else if (NbRemainingTiles == 0)
        {
            GameSet = true;

            audioSource.PlayOneShot(JingleDraw);

            NextFlagIcon.GetComponent<Mask>().showMaskGraphic = false;

            TextVictory.enabled = true;
            TextVictory.text = "Match nul";
        }
        else
        {
            NextRound();
        }
    }

    private void NextRound()
    {
        CurrentPlayerFlag = CurrentPlayerFlag == Flag.X ? Flag.O : Flag.X;
        NextFlagIcon.sprite = FlagProperties.Instance.GetSpriteFor(CurrentPlayerFlag);
        NextFlagIcon.color = FlagProperties.Instance.GetColorFor(CurrentPlayerFlag);
    }

    private Flag TestWinScenario()
    {
        //horizontal line (3)
        for(int i = 0; i < 3; i ++)
        {
            int index = 3 * i;
            if (tiles[index].flag == tiles[index+1].flag 
                && tiles[index].flag == tiles[index+2].flag
                && tiles[index].flag != Flag.None)
            {
                return tiles[index].flag;
            }
        }

        // vertical line (3)
        for (int i = 0; i < 3; i++)
        {
            int index = i;
            if (tiles[index].flag == tiles[index + 3].flag
                && tiles[index].flag == tiles[index + 6].flag
                && tiles[index].flag != Flag.None)
            {
                return tiles[index].flag;
            }
        }

        // cross lines (2)
        if (tiles[0].flag == tiles[4].flag
                && tiles[0].flag == tiles[8].flag
                && tiles[0].flag != Flag.None)
        {
            return tiles[0].flag;
        }

        if (tiles[2].flag == tiles[4].flag
                && tiles[2].flag == tiles[6].flag
                && tiles[2].flag != Flag.None)
        {
            return tiles[2].flag;
        }

        return Flag.None;
    }
}
