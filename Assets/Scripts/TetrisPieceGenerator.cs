using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetrisPieceGenerator : MonoBehaviour
{
    public class TetrisPiece
    {
        public int size;
        public Vector2Int[] blocks;
        public int index;

        // Normalizes the piece to start at (0,0) and sorts the blocks
        public Vector2Int[] GetNormalizedBlocks()
        {
            Vector2Int[] normalizedBlocks = new Vector2Int[blocks.Length];
            Array.Copy(blocks, normalizedBlocks, blocks.Length);

            // Find the minimum x and y values
            int minX = int.MaxValue, minY = int.MaxValue;
            foreach (var block in normalizedBlocks)
            {
                if (block.x < minX) minX = block.x;
                if (block.y < minY) minY = block.y;
            }

            // Normalize the blocks to start at (0,0)
            for (int i = 0; i < normalizedBlocks.Length; i++)
            {
                normalizedBlocks[i] = new Vector2Int(normalizedBlocks[i].x - minX, normalizedBlocks[i].y - minY);
            }

            // Sort the blocks for consistent comparison
            Array.Sort(normalizedBlocks, (a, b) =>
            {
                if (a.x == b.x)
                    return a.y.CompareTo(b.y);
                return a.x.CompareTo(b.x);
            });

            foreach(Vector2Int vector in normalizedBlocks)
            {
                Debug.Log("Pos: " + vector.x + ", " + vector.y);
            }

            Debug.Log("----------------");

            return normalizedBlocks;
        }

        // Compares two pieces for shape equality
        public bool IsEqualShape(TetrisPiece other)
        {
            Vector2Int[] normalizedBlocks1 = GetNormalizedBlocks();
            Vector2Int[] normalizedBlocks2 = other.GetNormalizedBlocks();

            if (normalizedBlocks1.Length != normalizedBlocks2.Length)
                return false;

            for (int i = 0; i < normalizedBlocks1.Length; i++)
            {
                if (normalizedBlocks1[i] != normalizedBlocks2[i])
                    return false;
            }

            return true;
        }

    }

    public int input; // The input integer from 3 to 18

    [SerializeField]
    private LoadGridLayout gridSO;

    [SerializeField]
    private GameObject blockPrefab;

    private List<Transform> tetrisParents = new List<Transform>();

    public List<TetrisPiece> pieces = new List<TetrisPiece>();

    void Start()
    {
        input = gridSO.maxFriends;
        gridSO.tetrisCompleted = 0;

        pieces = GenerateTetrisPieces(input);
        foreach (TetrisPiece piece in pieces)
        {
            Debug.Log($"Piece of size {piece.size}:");
            foreach (Vector2Int block in piece.blocks)
            {
                Debug.Log(block);
            }
        }

        foreach(Transform t in transform)
        {
            tetrisParents.Add(t);
            Debug.Log(t.gameObject.name);
        }

        DisplayTetrisPieces(pieces);
    }

    List<TetrisPiece> GenerateTetrisPieces(int input)
    {
        int pieceCount = DeterminePieceCount(input);
        List<int> sizes = DistributeBlocks(input, pieceCount);
        List<TetrisPiece> pieces = new List<TetrisPiece>();

        foreach (int size in sizes)
        {
            pieces.Add(CreateTetrisPiece(size));
        }

        return pieces;
    }

    int DeterminePieceCount(int input)
    {
        if (input <= 6)
        {
            return 1;
        }
        else if (input <= 12)
        {
            return 2;
        }
        else
        {
            return 3;
        }
    }

    List<int> DistributeBlocks(int totalBlocks, int pieceCount)
    {
        List<int> sizes = new List<int>();

        while (pieceCount > 0)
        {
            Debug.Log("Totalblocks remaining: " + totalBlocks);

            if (totalBlocks >= pieceCount * 6)
            {
                sizes.Add(6);
                totalBlocks -= 6;
            }
            else if(pieceCount == 1)
            {
                sizes.Add(totalBlocks);
                totalBlocks -= totalBlocks;
            }
            else
            {
                int minSize = Mathf.Max(2, totalBlocks - ((pieceCount-1) * 6));
                int size = UnityEngine.Random.Range(minSize, 7);
                sizes.Add(size);
                totalBlocks -= size;
            }

            pieceCount--;
        }

        return sizes;
    }

    TetrisPiece CreateTetrisPiece(int size)
    {
        TetrisPiece piece = new TetrisPiece();
        piece.size = size;
        piece.blocks = new Vector2Int[size];
        Vector2Int currentPosition = Vector2Int.zero;
        piece.blocks[0] = currentPosition;

        Vector2Int lastDirection = Vector2Int.zero;
        HashSet<Vector2Int> occupiedPositions = new HashSet<Vector2Int> { currentPosition };

        for (int i = 1; i < size; i++)
        {
            Vector2Int newDirection = GetRandomDirection(lastDirection, currentPosition, occupiedPositions);
            currentPosition += newDirection;
            piece.blocks[i] = currentPosition;
            occupiedPositions.Add(currentPosition);
            lastDirection = newDirection;
        }

        return piece;
    }

    Vector2Int GetRandomDirection(Vector2Int lastDirection, Vector2Int currentPosition, HashSet<Vector2Int> occupiedPositions)
    {
        List<Vector2Int> possibleDirections = new List<Vector2Int>
        {
            Vector2Int.up,
            Vector2Int.down,
            Vector2Int.left,
            Vector2Int.right
        };

        if (lastDirection == Vector2Int.up)
        {
            possibleDirections.Remove(Vector2Int.down);
        }
        else if (lastDirection == Vector2Int.down)
        {
            possibleDirections.Remove(Vector2Int.up);
        }
        else if (lastDirection == Vector2Int.left)
        {
            possibleDirections.Remove(Vector2Int.right);
        }
        else if (lastDirection == Vector2Int.right)
        {
            possibleDirections.Remove(Vector2Int.left);
        }

        if(currentPosition.y == 4)
        {
            possibleDirections.Remove(Vector2Int.up);
        }

        possibleDirections.RemoveAll(direction => occupiedPositions.Contains(currentPosition + direction));

        if (possibleDirections.Count == 0)
        {
            throw new InvalidOperationException("No valid directions available to continue Tetris piece generation.");
        }

        return possibleDirections[UnityEngine.Random.Range(0, possibleDirections.Count)];
    }

    void DisplayTetrisPieces(List<TetrisPiece> pieces)
    {
        int index = 0;
        foreach(TetrisPiece tetris in pieces)
        {
            Vector2Int[] tempBlock = tetris.GetNormalizedBlocks();
            foreach (Vector2Int v2Int in tempBlock)
            {
                GameObject block = Instantiate(blockPrefab, tetrisParents[index]);
                block.transform.localPosition = new Vector3(v2Int.x * 30, v2Int.y * 30, 0);
                tetris.index = index;
            }
            index++;
        }
    }

    public bool UseTetrisPiece(TetrisPiece piece)
    {
        bool foundPiece = false;

        //Debug.Log("Pieces left: " + pieces.Count);

        if (pieces.Contains(piece))
        {
            //Debug.Log("Found piece, removing..." + tetrisParents[piece.index].gameObject.name);
            tetrisParents[piece.index].gameObject.SetActive(false);
            pieces.Remove(piece);
            gridSO.tetrisCompleted++;
            foundPiece = true;
        }

        return foundPiece;
    }
}