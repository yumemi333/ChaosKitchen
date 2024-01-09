using System.Collections;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    private Player player = null;
    [SerializeField] private Transform playerSetPosition;
    [SerializeField] private Transform playerTopPosition;
    [SerializeField] private Transform playerLandingPoint;
    [SerializeField] private float moveTime = 2.5f;


    /// <summary>
    /// 発射準備or飛ぶ
    /// </summary>
    /// <param name="player"></param>
    public void InteractAlternate(Player player)
    {
        if (this.player == null)
        {
            this.player = player;
            player.transform.eulerAngles = playerSetPosition.rotation.eulerAngles;
            this.player.transform.position = playerSetPosition.position;
        }
        else
        {
            StartCoroutine(BezierCurve(player.transform, playerSetPosition, playerTopPosition, playerLandingPoint));
           // StartCoroutine(ProjectileMotion(player.transform));
        }
    }

    //MEMO: 下に落ちる時もっと勢い良くしたいので、加速度を追加してもいいかも
    private IEnumerator BezierCurve(Transform current, Transform start, Transform top, Transform end)
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
            if (timer > moveTime)
            {
                //player.transform.eulerAngles = Vector3.zero;
                break;
            }
            yield return null;
        }

    }
    float gravity = 9.8f;//重力加速度
    [SerializeField] private float degree = 30f;//角度
    [SerializeField] private float v0 = 10; //初速度
    /// <summary>
    /// 重力を用いた斜方投射での移動
    /// </summary>
    /// <param name="ballTransform"></param>
    /// <returns></returns>
    private IEnumerator ProjectileMotion(Transform ballTransform)
    {
        float time = 0;
        // 開始位置
        Vector3 startPos = ballTransform.position;
        while (true)
        {
            time += Time.deltaTime;
            float vx = Mathf.Cos(Mathf.Deg2Rad * degree) * v0 * time;
            float vy = (Mathf.Sin(Mathf.Deg2Rad * degree) * v0) * time - (gravity / 2) * Mathf.Pow(time, 2);
            ballTransform.position = new Vector3(startPos.x - vx, startPos.y + vy, ballTransform.position.z);
            // y方向への移動がなくなったら
            if (vy <= 0)
            {
                //player.transform.eulerAngles = Vector3.zero;
                ballTransform.position = new Vector3(ballTransform.position.x, 0, ballTransform.position.z);
                yield break;
            }
            yield return null;
        }
    }

}
