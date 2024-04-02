using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatriceMeta<T>
{
    public T data;
    public int i;
    public int j;

    public MatriceMeta(T data, int i, int j)
    {
        this.data = data;
        this.i = i;
        this.j = j;
    }
}

public static class Helper
{

    public static T GetRandom<T>(this T[] arr)
    {
        
        return arr[Random.Range(0, arr.GetLength(0))];

    }


    public static bool IndexIsVailable<T>(this T[,] mat, int i, int j)
    {

        return i >= 0 && j >= 0 && i < mat.GetLength(0) && j < mat.GetLength(1);

    }

    public static List<MatriceMeta<T>> GetAllNeighbours<T>(this T[,] mat, int indexI, int indexJ)
    {

        List<MatriceMeta<T>> toReturn = new List<MatriceMeta<T>>();

        int[,] indexes = {
            { 0, -1 },
            { 0, 1},
            { -1, 0},
            { 1, 0}
        };

        int sizeX = indexes.GetLength(0);
        for (int w = 0; w < sizeX; w++)
        {

            int i = indexes[w,0] + indexI;
            int j = indexes[w,1] + indexJ;

            if (mat.IndexIsVailable(i, j))
            {
                toReturn.Add(new MatriceMeta<T>(mat[i,j], i, j));
            }

        }

        return toReturn;

          
    }

    public static List<MatriceMeta<T>> GetAllNeighbours<T>(this T[,] mat, ref bool[,] walkedIn, int i, int j)
    {

        List<MatriceMeta<T>> tempArr = GetAllNeighbours(mat, i, j);

        for (int index = tempArr.Count - 1; index >= 0; index--)
        {
            
            MatriceMeta<T> temp = tempArr[index];
            if (walkedIn[temp.i, temp.j])
            {
                tempArr.RemoveAt(index);
            }
            else
            {
                walkedIn[temp.i, temp.j] = true;
            }


        }

        return tempArr;


    }

}
