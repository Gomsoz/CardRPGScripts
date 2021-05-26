using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetupMapTiles
{
    Defines.Position m_startPosition;

    List<Sprite> m_terrains = new List<Sprite>();
    Transform m_terrainHolder;

    float m_gapBetweenTiles = 2.5f;

    public enum SpeciesOfTerrains
    {
        plains,
    }

    public void AwakeInit()
    {
        m_terrainHolder = new GameObject { name = "TerrainHolder" }.transform;
        LoadTiles(SpeciesOfTerrains.plains);
    }

    void LoadTiles(SpeciesOfTerrains terrain, int numOftiles = 4)
    {
        string name = System.Enum.GetName(typeof(SpeciesOfTerrains), terrain);
        for (int i = 0; i < numOftiles; i++)
        {
            Sprite sprite = Managers.Resources.Load<Sprite>($"Terrain/Tiles/{name}0{i}");
            if (sprite == null)
                continue;
            
            m_terrains.Add(sprite);
        }
    }

    public Transform[,] ConstructTiles(Board_Base board)
    {
        Defines.Position _startPos = new Defines.Position( (-board.BoardWidth / 2) * m_gapBetweenTiles,  (-board.BoardHeight / 2) * m_gapBetweenTiles);
        string _path = $"Prefabs/Tile/Tile";
        Transform[,] _returnBoard = new Transform[board.BoardWidth, board.BoardHeight];

        for(int i = 0; i < board.BoardWidth; i++)
        {
            for(int j = 0; j < board.BoardHeight; j++)
            {
                GameObject go = Managers.Resources.Instantiate(_path, m_terrainHolder);
                go.GetComponent<SpriteRenderer>().sprite = m_terrains[board.TileIndexes[i,j]];
                go.transform.position = new Vector2(_startPos.posX + (i * m_gapBetweenTiles), _startPos.posY + (j * m_gapBetweenTiles));
                _returnBoard[i, j] = go.transform;
            }
        }
        return _returnBoard;
    }
}
