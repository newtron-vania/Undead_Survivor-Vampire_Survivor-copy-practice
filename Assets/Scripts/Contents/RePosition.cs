using UnityEngine;

public class RePosition : MonoBehaviour
{
    Collider2D coll;
    [SerializeField]

    private void Awake()
    {
        coll = GetComponent<Collider2D>();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Area"))
            return;

        float areaSize = collision.transform.GetComponent<BoxCollider2D>().size.x;

        Vector3 playerPos = Managers.Game.getPlayer().transform.position;
        Vector3 myPos = transform.position;


        float dirX = playerPos.x - myPos.x;
        float dirY = playerPos.y - myPos.y;

        float diffx = Mathf.Abs(dirX);
        float diffy = Mathf.Abs(dirY);

        dirX = dirX > 0 ? 1 : -1;
        dirY = dirY > 0 ? 1 : -1;

        if (coll.enabled)
        {
            transform.Translate(new Vector3(dirX, dirY, 0f).normalized * areaSize + new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0));
        }
    }
}
