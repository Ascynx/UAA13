using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EntityManager : MonoBehaviour
{
    private List<GameObject> _mobEntities = new List<GameObject>();
    private List<GameObject> _itemEntities = new List<GameObject>();

    private Dictionary<int, Vector3> spawnPositions = new Dictionary<int, Vector3>();

    private void Awake()
    {
        _mobEntities.Clear();
        _itemEntities.Clear();

        MobAI[] mobEntities = Object.FindObjectsOfType<MobAI>();
        foreach (MobAI entity in mobEntities)
        {
            if (entity.gameObject.activeSelf)
            {
                _mobEntities.Add(entity.gameObject);
                spawnPositions.Add(entity.gameObject.GetInstanceID(), entity.transform.position);
            }
        }
        PickUpItem[] itemEntities = Object.FindObjectsOfType<PickUpItem>();
        foreach (PickUpItem entity in itemEntities)
        {
            if (entity.gameObject.activeSelf)
            {
                _itemEntities.Add(entity.gameObject);
                spawnPositions.Add(entity.gameObject.GetInstanceID(), entity.transform.position);
            }
        }
    }

    public void KillMobEntity(int instanceId)
    {
        GameObject entity = _mobEntities.FirstOrDefault(e => e.GetInstanceID() == instanceId);
        if (entity != null)
        {
            Destroy(entity);
            _mobEntities.Remove(entity);
        }
    }

    public void KillItemEntity(int instanceId)
    {
        GameObject entity = _itemEntities.FirstOrDefault(e => e.GetInstanceID() == instanceId);
        if (entity != null)
        {
            Destroy(entity);
            _itemEntities.Remove(entity);
        }
    }

    public void OnPreSave(Sauvegarde save)
    {
        save.PersistentEntityStates.Clear();
        foreach (GameObject entity in _mobEntities)
        {
            if (entity.activeInHierarchy)
            {
                continue;
            }
            save.PersistentEntityStates.Add(new EntityState(entity.GetInstanceID(), false, EntityState.EntityType.Mob, entity.transform.position));
        }
        foreach (GameObject entity in _itemEntities)
        {
            if (entity.activeInHierarchy)
            {
                continue;
            }
            save.PersistentEntityStates.Add(new EntityState(entity.GetInstanceID(), false, EntityState.EntityType.Item, entity.transform.position));
        }
    }

    public void OnPostLoad(Sauvegarde save)
    {
        //on résurectionne les mobs et items qui n'ont pas été sauvegardés comme morts
        foreach (GameObject mob in _mobEntities)
        {
            if (!save.PersistentEntityStates.Any((e) => e.type == EntityState.EntityType.Mob && e.instanceId == mob.GetInstanceID() && !e.isActive))
            {

                mob.SetActive(true);
            }
            //on remet la position de spawn
            if (spawnPositions.TryGetValue(mob.GetInstanceID(), out Vector3 spawnPos))
            {
                mob.transform.position = spawnPos;
            }
        }
        foreach (GameObject item in _itemEntities)
        {
            if (!save.PersistentEntityStates.Any((e) => e.type == EntityState.EntityType.Item && e.instanceId == item.GetInstanceID() && !e.isActive))
            {
                item.SetActive(true);
            }
            //on remet la position de spawn dans le cas ou ça aurait pu bouger.
            if (spawnPositions.TryGetValue(item.GetInstanceID(), out Vector3 spawnPos))
            {
                item.transform.position = spawnPos;
            }
        }

        //on désactive les mobs et items qui ont été sauvegardés comme morts
        foreach (var item in save.PersistentEntityStates)
        {
            if (!item.isActive)
            {
                if (item.type == EntityState.EntityType.Mob)
                {
                    GameObject entity = _mobEntities.FirstOrDefault(e => e.GetInstanceID() == item.instanceId);
                    if (entity != null)
                    {
                        entity.SetActive(false);
                    }
                }
                else if (item.type == EntityState.EntityType.Item)
                {
                    GameObject entity = _itemEntities.FirstOrDefault(e => e.GetInstanceID() == item.instanceId);
                    if (entity != null)
                    {
                        entity.SetActive(false);
                    }
                }
            }
        }
    }
}
