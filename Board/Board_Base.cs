using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board_Base
{
    int m_boardWidth;
    public int BoardWidth { get { return m_boardWidth; } }
    int m_boardHeight;
    public int BoardHeight { get { return m_boardHeight; } }

    int[,] m_tileIndexes;
    public int[,] TileIndexes { get { return m_tileIndexes; } }

    private Board_Base(BoardBuilder builder)
    {
        m_boardWidth = builder._width;
        m_boardHeight = builder._height;
        m_tileIndexes = builder._tileIndexs;
    }

    public static BoardBuilder Builder()
    {
        return new BoardBuilder();
    }

    public class BoardBuilder
    {
        public int _width;
        public int _height;
        public int[,] _tileIndexs;


        public BoardBuilder SetWidth(int width)
        {
            _width = width;
            return this;
        }

        public BoardBuilder SetHeight(int height)
        {
            _height = height;
            return this;
        }

        public BoardBuilder SetTileIndexs(int[,] tileIndexs)
        {
            _tileIndexs = tileIndexs;
            return this;
        }

        public Board_Base BoardBuild()
        {
            return new Board_Base(this);
        }
    }
}
