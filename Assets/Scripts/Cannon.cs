using System.Collections;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    private Player player = null;
    [SerializeField] private Transform playerSetPosition;
    [SerializeField] private Transform playerTopPosition;
    [SerializeField] private Transform playerLandingPoint;

    private bool moving = false;

    /// <summary>
    /// 発射準備or飛ぶ
    /// </summary>
    /// <param name="player"></param>
    public void InteractAlternate(Player player)
    {
        //if (moving)
        //{
        //    return;
        //}

        if (this.player == null)
        {
            this.player = player;
            this.player.transform.position = playerSetPosition.position;
        }
        else
        {
            moving = true;
            StartCoroutine(Move(player.transform, playerSetPosition, playerTopPosition, playerLandingPoint, 3));
        }
    }
    private IEnumerator Move(Transform current, Transform start, Transform top, Transform end, float moveTime)
    {
        float timer = 0f;
        float diff = Vector3.Distance(top.position, end.position);
        float delta = diff * (Time.deltaTime / moveTime);
        while (true)
        {
            start.position = Vector3.Lerp(start.position, top.position, delta);
            top.position = Vector3.Lerp(top.position, end.position, delta);
            timer += Time.deltaTime;
            current.position = Vector3.Lerp(start.position, top.position, delta);
            yield return null;
            if (timer > moveTime)
            {
                break;
            }
        }

    }
}
