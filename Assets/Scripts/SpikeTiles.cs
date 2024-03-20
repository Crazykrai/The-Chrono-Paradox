using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SpikeTileDamage : MonoBehaviour
{
    public Tilemap spikeTilemap;
    public Sprite spikeTileSprite;
    public Rigidbody2D rb;
    public float dmg = 0.25f;

    void Update()
    {
        // Get the player's current world position
        Vector3 playerPosition = rb.position;

        // Convert player's world position to tile position
        Vector3Int playerTilePosition = spikeTilemap.WorldToCell(playerPosition);

        // Get the tile at the converted position
        TileBase tile = spikeTilemap.GetTile(playerTilePosition);

        Sprite tileSprite = (tile as Tile).sprite;
        // Check if the tile exists and its sprite matches the spike tile sprite
        if (tileSprite == spikeTileSprite)
        {
            // Player stepped on a spike tile, apply damage
            Hurt(0.25f);
        }
    }

    private void Hurt(float dmg)
    {
        PlayerStats.Instance.TakeDamage(dmg);
    }
}