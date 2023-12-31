using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBounce : MonoBehaviour
{
    public BallMouvement ballMouvement;
    public ScoreManager scoreManager;
    public GameObject hitSFX;
    private void Bounce(Collision2D collision)
    {
        Vector3 ballPosition = transform.position;
        Vector3 racketPosition = collision.transform.position;
        float racketHeight = collision.collider.bounds.size.y;
        float positionX;
        if (collision.gameObject.name == "Player 1")
        {
            positionX = 1;
        }
        else { positionX = -1; }
        float positionY = (ballPosition.y + racketPosition.y) / racketHeight;
        ballMouvement.IncreaseHitCounter();
        ballMouvement.MoveBall(new Vector2(positionX, positionY));
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name =="Player 1" || collision.gameObject.name == "Player 2")
        { Bounce (collision); }
        else if (collision.gameObject.name=="Right Border")
        { scoreManager.Player1Goal();
            ballMouvement.player1Start = false;
            StartCoroutine(ballMouvement.Launch());
                }
        else if (collision.gameObject.name == "Left Border")
        { scoreManager.Player2Goal();
            ballMouvement.player1Start = true;
            StartCoroutine(ballMouvement.Launch());
        }
        Instantiate(hitSFX, transform.position, transform.rotation);
    }
}
