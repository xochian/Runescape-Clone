using UnityEngine;
using System.Collections;

using UnityEngine.Networking;

public class script_genLevel : NetworkBehaviour
{
    public int mapWidth, mapHeight;

    public int numberOfRooms;

    public int numberOfEnemyRooms;
    public int numberOfTreeRooms;
    public int numberOfMiningRooms;
    public int numberOfCraftingRooms;

    void Start()
    {
        if(isServer)
            script_LevelFactory.GenerateLevel(mapWidth, mapHeight, numberOfRooms, numberOfEnemyRooms, numberOfTreeRooms, numberOfMiningRooms, numberOfCraftingRooms);
        else
        {

        }
    }

    void Update()
    {
    }
}

public static class script_LevelFactory
{
    public static void GenerateLevel(int mapWidth, int mapHeight, int numberOfRooms, int enemyRooms, int treeRooms, int miningRooms, int craftingRooms )
    {
        int mapSize = mapHeight * mapWidth;

        //Generate RoomGrid
        RoomType[][] roomGrid = new RoomType[mapWidth][];
        bool[][] pathGrid = new bool[mapWidth][];

        for(int i = 0; i < mapWidth; ++i)
        {
            pathGrid[i] = new bool[mapHeight];
            roomGrid[i] = new RoomType[mapHeight];
        }

        //Generate Rooms
        int roomsGend = 0;
        int gendEnemyRooms = 0;
        int gendTreeRooms = 0;
        int gendMiningRooms = 0;
        int gendCraftingRooms = 0;
        int gendSpawnRooms = 0;

        int[] prevRoom = new int[] { -1, -1 };

        while (roomsGend < mapSize && roomsGend < numberOfRooms)
        {
            int[] curPos = Raindrop(mapWidth, mapHeight);

            //Generate Room
            if(roomGrid[curPos[0]][curPos[1]] == RoomType.None)
            {
                if(gendSpawnRooms <= 0)
                {
                    gendSpawnRooms++;
                    roomGrid[curPos[0]][curPos[1]] = RoomType.Spawn;
                }
                else if (enemyRooms > gendEnemyRooms)
                {
                    gendEnemyRooms++;
                    roomGrid[curPos[0]][curPos[1]] = RoomType.Enemy;
                }
                else if (treeRooms > gendTreeRooms)
                {
                    gendTreeRooms++;
                    roomGrid[curPos[0]][curPos[1]] = RoomType.Tree;
                }
                else if (miningRooms > gendMiningRooms)
                {
                    gendMiningRooms++;
                    roomGrid[curPos[0]][curPos[1]] = RoomType.Mine;
                }
                else if (craftingRooms > gendCraftingRooms)
                {
                    gendCraftingRooms++;
                    roomGrid[curPos[0]][curPos[1]] = RoomType.Crafting;
                }
                else
                    roomGrid[curPos[0]][curPos[1]] = RoomType.Plain;

                roomsGend++;

                //CREATE PATHS
                if(prevRoom[0] != -1)
                {
                    GeneratePath(pathGrid, prevRoom, curPos);
                }

                prevRoom = curPos;
            }
        }

        //Hook up the rooms
        //Starting from the spawn, go around it and chekc to see if everything is connected.
        ShredPaths(pathGrid, roomGrid);
        GeneratePaths(pathGrid, roomGrid);

        //Create Physical Rooms
        for(int x = 0; x < mapWidth; ++x)
        {
            for(int y = 0; y < mapHeight; ++y)
            {
                GenerateRoom(x, y, roomGrid[x][y]);
            }
        }
    }

    static void GeneratePath(bool[][] pathGrid, int[] posA, int[]posB)
    {
        int resX = posA[0];
        int resY = posA[1];


        while (resX != posB[0])
        {
            if (resX < posB[0])
                resX++;
            else if(resX > posB[0])
                resX--;

            pathGrid[resX][resY] = true;
        }

        while (resY != posB[1])
        {
            if (resY < posB[1])
                resY++;
            else if (resY > posB[1])
                resY--;

            pathGrid[resX][resY] = true;
        }
    }

    [Command]
    static void GenerateRoom(int x, int y, RoomType roomType)
    {
        //Debug.Log(roomType);
        GameObject g = null;

        if ((roomType.ToString()[0] == 'T' || roomType.ToString()[0] == 'L') && roomType != RoomType.Tree)
        {
            g = (GameObject)GameObject.Instantiate(Resources.Load("Rooms/" + roomType.ToString()[0] + "PathRoom"));
            g.transform.eulerAngles = new Vector3(0, -180 + ((int)char.GetNumericValue(roomType.ToString()[1]))*90,0);
            g.GetComponent<script_networkRotation>().rotation = g.transform.rotation;
        }
        else
            g = (GameObject)GameObject.Instantiate(Resources.Load("Rooms/" + roomType.ToString() + "Room"));

        g.transform.position = new Vector3(x*10, 0, y*10);

        NetworkServer.Spawn(g);
    }

    static void ShredPaths(bool[][] pathGrid, RoomType[][] roomGrid)
    {
        if (roomGrid.Length <= 0)
            return;

        int width = roomGrid.Length;
        int height = roomGrid[0].Length;

        for (int x = 0; x < width; ++x)
        {
            for (int y = 0; y < height; ++y)
            {
                if (roomGrid[x][y] != RoomType.None)
                    pathGrid[x][y] = false;
            }
        }
    }

    //EnumParser
    public static T ParseEnum<T>(string value)
    {
        return (T)System.Enum.Parse(typeof(T), value, true);
    }

    static void GeneratePaths(bool[][] pathGrid, RoomType[][] roomGrid)
    {
        //Does it even exist?
        if (roomGrid.Length <= 0)
            return;

        int width = roomGrid.Length;
        int height = roomGrid[0].Length;

        for(int x = 0; x < width; ++x)
        {
            for(int y = 0; y < height; ++y)
            {
                if(pathGrid[x][y] == false)
                    continue;

                bool[] around = GetAround(pathGrid, roomGrid, x, y);

                int cols = 0;

                for(int i = 0; i < 4; ++i)
                {
                    if (around[i] == true)
                        cols++;
                }

                switch(cols)
                {
                    case 2:
                        if (around[0] == true && around[2] == true)
                            roomGrid[x][y] = RoomType.VerticalPath;
                        else if (around[1] == true && around[3] == true)
                            roomGrid[x][y] = RoomType.HorizontalPath;
                        else
                        {
                            for (int i = 0; i < 4; ++ i)
                            {
                                if(around[i] && around[(i+1)%4])
                                {
                                    roomGrid[x][y] = ParseEnum<RoomType>("L" + (i+1) + "Path");
                                    break;
                                }
                            }
                        }
                        break;
                    case 3:
                        for (int i = 0; i < 4; ++i)
                        {
                            if (!around[i])
                            {
                                roomGrid[x][y] = ParseEnum<RoomType>("T" + (i + 1) + "Path");
                                break;
                            }
                        }
                        break;
                    case 4:
                        roomGrid[x][y] = RoomType.CrossPath;
                        break;
                    default:
                        break;
                }
            }
        }
    }

    static bool[] GetAround(bool[][] pathGrid, RoomType[][] roomGrid, int x, int y)
    {
        bool[] returnMe = new bool[4];

        if(y < pathGrid[0].Length - 1 )
        {
            returnMe[0] = pathGrid[x][y + 1];

            if(!returnMe[0])
                returnMe[0] = (roomGrid[x][y + 1] != RoomType.None);
        }

        if (x < pathGrid.Length - 1)
        {
            returnMe[1] = pathGrid[x+1][y];

            if (!returnMe[1])
                returnMe[1] = (roomGrid[x+1][y] != RoomType.None);
        }

        if (y > 0)
        {
            returnMe[2] = pathGrid[x][y - 1];

            if (!returnMe[2])
                returnMe[2] = (roomGrid[x][y - 1] != RoomType.None);
        }

        if (x > 0)
        {
            returnMe[3] = pathGrid[x - 1][y];

            if (!returnMe[3])
                returnMe[3] = (roomGrid[x - 1][y] != RoomType.None);
        }

        return returnMe;
    }

    static int[] Raindrop(int width, int height)
    {
        return new int[] { Random.Range(0, width) , Random.Range(0,height) };
    }

    static int[] FindCollisionAroundPoint(int[][] roomGrid, int[] origin, int searchRadius)
    {
        //Does it even exist?
        if (roomGrid.Length <= 0)
            return null;

        int width = roomGrid.Length;
        int height = roomGrid[0].Length;
        int[] returnMe = new int[4];

        for(int i = -searchRadius; i > searchRadius; ++i)
        {
            for(int u = -searchRadius; u < searchRadius; ++u)
            {
                if (i == 0 && u == 0)
                    continue;

                int x = origin[0] + i;
                int y = origin[1] + u;

                if(( x < 0 || y < 0) || (x >= width || y >= height))
                {
                    continue;
                }
            }
        }

        return returnMe;
    }
}
