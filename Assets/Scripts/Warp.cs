using System;
using System.Collections;
using UnityEngine;

public class Warp : MonoBehaviour
{


    [SerializeField] private Transform warpPoint = null;
    private float speed = 10;

    private void Start()
    {
        Player.Instance.OnWarped += Instance_OnWarped; ;
    }

    private void Instance_OnWarped(object sender, EventArgs e)
    {
        Player.Instance.SetPlayerState(PlayerState.UnableToMove);
        StartCoroutine(MoveDown(Player.Instance));
    }

    private IEnumerator MoveDown(Player player)
    {
        float time = 0;
        Vector3 startPos = warpPoint.position;
        player.transform.position = startPos;

        while (true)
        {
            time += Time.deltaTime;
            float y = speed * time;
            player.transform.position = new Vector3(startPos.x, startPos.y - y, startPos.z);
            if (player.transform.position.y <= 0f)
            {
                player.SetPlayerState(PlayerState.EnableToMove);
                player.transform.position = new Vector3(startPos.x, 0, startPos.z);
                break;
            }
            yield return null;
        }
    }

}
