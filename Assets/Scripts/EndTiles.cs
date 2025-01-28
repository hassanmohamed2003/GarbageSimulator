using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(Tilemap))]
[RequireComponent(typeof(TilemapCollider2D))]
public class endtiles : MonoBehaviour
{
    private Tilemap tilemap;

    void Start()
    {
        tilemap = GetComponent<Tilemap>();

        if (tilemap == null)
        {
            Debug.LogError("Geen Tilemap-component gevonden op dit GameObject!");
            return;
        }

        BoundsInt bounds = tilemap.cellBounds;
        for (int x = bounds.xMin; x < bounds.xMax; x++)
        {
            for (int y = bounds.yMin; y < bounds.yMax; y++)
            {
                Vector3Int pos = new Vector3Int(x, y, 0);

                if (tilemap.HasTile(pos))
                {
                    if (IsEdgeTile(pos))
                    {
                        tilemap.SetColliderType(pos, Tile.ColliderType.Grid);
                    }
                    else
                    {
                        tilemap.SetColliderType(pos, Tile.ColliderType.None);
                    }
                }
            }
        }
    }

    bool IsEdgeTile(Vector3Int pos)
    {
        Vector3Int[] neighbors = new Vector3Int[]
        {
            new Vector3Int(pos.x - 1, pos.y, 0),
            new Vector3Int(pos.x + 1, pos.y, 0),
            new Vector3Int(pos.x, pos.y - 1, 0),
            new Vector3Int(pos.x, pos.y + 1, 0)
        };

        foreach (var neighbor in neighbors)
        {
            if (!tilemap.HasTile(neighbor))
            {
                return true;
            }
        }

        return false;
    }
}
