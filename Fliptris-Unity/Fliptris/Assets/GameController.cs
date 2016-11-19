using UnityEngine;
using System.Collections;
using fliptris.core;
using System.Linq;

public class GameController : MonoBehaviour
{
    public int score = 0;
    public int width = 10;
    public int height = 10;

    public GameObject tilePrefab;
    public Sprite border1;
    public Sprite border2;
    
    private Tile[,] tiles;
    private Board board;
    private float moveTimer = 0f;
    private float speed = 0.75f;
    private float speedDelta = 0f;

    public new AudioSource audio;
    public AudioClip moveSound;
    public AudioClip stuckSound;
    public AudioClip removalSound;

    public void Start ()
    {
        board = new Board(width, height);

        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        tiles = new Tile[board.Width, board.Height];

        for (int y = 0; y < board.Height + 2; y++)
        {
            AddBorderTile(-1, y - 1);
            AddBorderTile(board.Width, y - 1);
        }

        for (int x = 1; x < board.Width + 1; x++)
        {
            AddBorderTile(x - 1, -1);
            AddBorderTile(x - 1, board.Height);
        }

        Camera.main.transform.position = new Vector3(board.Width / 2f - 0.5f, board.Height / 2f - 0.5f, -10);
        Camera.main.orthographicSize = board.Width + 1;

        for (int y = 0; y < board.Height; y++)
        {
            for (int x = 0; x < board.Width; x++)
            {
                var tileObject = (GameObject)Instantiate(tilePrefab, new Vector3(x, y, 0), Quaternion.identity);
                var tile = tileObject.GetComponent<Tile>();

                tiles[x, y] = tile;
            }
        }
    }

    private void AddBorderTile(int x, int y)
    {
        var borderObject = new GameObject("Border");
        borderObject.transform.position = new Vector3(x, y, 0);

        var renderer = borderObject.AddComponent<SpriteRenderer>();
        renderer.color = new Color(0.8f, 1, 0.3f);
        if (y % 2 == 0)
            renderer.sprite = border1;
        else
            renderer.sprite = border2;
    }

    public void Update()
    {
        moveTimer -= Time.deltaTime;

        if (moveTimer < 0f)
        {
            int dx = 0;
            int dy = -1;

            switch (Input.deviceOrientation)
            {
                case DeviceOrientation.Portrait:
                    dx = 0;
                    dy = -1;
                    break;
                case DeviceOrientation.PortraitUpsideDown:
                    dx = 0;
                    dy = 1;
                    break;
                case DeviceOrientation.LandscapeLeft:
                    dx = -1;
                    dy = 0;
                    break;
                case DeviceOrientation.LandscapeRight:
                    dx = 1;
                    dy = 0;
                    break;
            }

            var result = board.Move(dx, dy);
            moveTimer = speed - speedDelta;

            if (result.DidMove)
            {
                audio.PlayOneShot(moveSound);
                speedDelta += 0.01f;
            }

            if (result.GotStuck)
            {
                audio.PlayOneShot(stuckSound);
                speedDelta = 0f;
            }

            foreach (var removedPart in result.RemovedParts)
            {
                tiles[removedPart.X, removedPart.Y].PlayRemovalEffect();
            }

            if (result.RemovedParts.Any())
                audio.PlayOneShot(removalSound);

            for (int y = 0; y < board.Height; y++)
            {
                for (int x = 0; x < board.Width; x++)
                {
                    tiles[x, y].SetValue(board.State[x, y]);
                }
            }
        }        
    }
}
