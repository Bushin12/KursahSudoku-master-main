using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ArrayConverter
{
    public static int[] To1DArray(int[,] input)
    {
        int rows = input.GetLength(0);
        int cols = input.GetLength(1);
        int[] output = new int[rows * cols];
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                output[i * cols + j] = input[i, j];
            }
        }
        return output;
    }

    public static int[,] To2DArray(int[] input, int rows, int cols)
    {
        int[,] output = new int[rows, cols];
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                output[i, j] = input[i * cols + j];
            }
        }
        return output;
    }

    public static bool[] To1DBoolArray(bool[,] input)
    {
        int rows = input.GetLength(0);
        int cols = input.GetLength(1);
        bool[] output = new bool[rows * cols];
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                output[i * cols + j] = input[i, j];
            }
        }
        return output;
    }

    public static bool[,] To2DBoolArray(bool[] input, int rows, int cols)
    {
        bool[,] output = new bool[rows, cols];
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                output[i, j] = input[i * cols + j];
            }
        }
        return output;
    }
}
