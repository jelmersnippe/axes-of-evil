using System;

[Serializable]
public class Tile
{
    public bool walkable { get; private set; }

    public Tile(bool walkable)
    {
        this.walkable = walkable;
    }
}
