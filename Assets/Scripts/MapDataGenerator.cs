using System.Collections;
using System.Collections.Generic;

public class MapDataGenerator
{
    public int[,] GetMapData(int width, int heigth, int bombsCount)
    {
        int[,] mapData = GenerateArray(width, heigth, true);

        SmoothVNCellularAutomata(mapData, true, 10);

        return mapData;
    }

    //Generate array
    public static int[,] GenerateArray(int width, int height, bool empty)
    {
        int[,] map = new int[width, height];

        for (int x = 0; x < map.GetUpperBound(0); x++)
        {
            for (int y = 0; y < map.GetUpperBound(1); y++)
            {
                if (empty)
                {
                    map[x, y] = 0;
                }
                else
                {
                    map[x, y] = 1;
                }
            }
        }
        return map;
    }

    static int GetVNSurroundingTiles(int[,] map, int x, int y, bool edgesAreWalls)
    {
        /* von Neumann Neighbourhood looks like this ('T' is our Tile, 'N' is our Neighbour)
        * 
        *   N 
        * N T N
        *   N
        *   
        */

        int tileCount = 0;

        //Keep the edges as walls
        if (edgesAreWalls && (x - 1 == 0 || x + 1 == map.GetUpperBound(0) || y - 1 == 0 || y + 1 == map.GetUpperBound(1)))
        {
            tileCount++;
        }

        //Ensure we aren't touching the left side of the map
        if (x - 1 > 0)
        {
            tileCount += map[x - 1, y];
        }

        //Ensure we aren't touching the bottom of the map
        if (y - 1 > 0)
        {
            tileCount += map[x, y - 1];
        }

        //Ensure we aren't touching the right side of the map
        if (x + 1 < map.GetUpperBound(0))
        {
            tileCount += map[x + 1, y];
        }

        //Ensure we aren't touching the top of the map
        if (y + 1 < map.GetUpperBound(1))
        {
            tileCount += map[x, y + 1];
        }

        return tileCount;
    }

    public static int[,] SmoothVNCellularAutomata(int[,] map, bool edgesAreWalls, int smoothCount)
    {
        for (int i = 0; i < smoothCount; i++)
        {
            for (int x = 0; x < map.GetUpperBound(0); x++)
            {
                for (int y = 0; y < map.GetUpperBound(1); y++)
                {
                    //Get the surrounding tiles
                    int surroundingTiles = GetVNSurroundingTiles(map, x, y, edgesAreWalls);

                    if (edgesAreWalls && (x == 0 || x == map.GetUpperBound(0) - 1 || y == 0 || y == map.GetUpperBound(1)))
                    {
                        //Keep our edges as walls
                        map[x, y] = 1;
                    }
                    //von Neuemann Neighbourhood requires only 3 or more surrounding tiles to be changed to a tile
                    else if (surroundingTiles > 2)
                    {
                        map[x, y] = 1;
                    }
                    else if (surroundingTiles < 2)
                    {
                        map[x, y] = 0;
                    }
                }
            }
        }
        //Return the modified map
        return map;
    }
}