using UnityEngine;

public abstract class Base_Item : MonoBehaviour
{
    public Transform target { get; set; } = null;

    public abstract void OnItemEvent(PlayerStat player);

    protected virtual void OnTriggerEnter2D(Collider2D col)
    {
        if (col.transform.tag == "Player")
        {
            PlayerStat playerStat = col.GetComponent<PlayerStat>();
            OnItemEvent(playerStat);
            Managers.Resource.Destroy(gameObject);
        }
    }
}
