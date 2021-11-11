using System;

public class Body : GridObject
{
    [NonSerialized] public Tile previousTile;

    public void MoveToTile(Tile tile)
    {
        if (tile == null) { return; }
        transform.position = tile.worldPosition;
        Tile temp = currentTile;
        previousTile = temp;
        currentTile = tile;

        previousTile.walkable = true;
        currentTile.walkable = false;
        previousTile.currentObjects.Remove(gameObject);
        currentTile.currentObjects.Add(gameObject);
    }
}
