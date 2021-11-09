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
    }
}
