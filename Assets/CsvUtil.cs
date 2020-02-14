using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System;

public class CsvUtil
{
    // Read data from CSV file
    public static string[,] readData(string filepath, string filename)
    {
        AssetDatabase.ImportAsset(filepath);
        TextAsset csvFile = (TextAsset)Resources.Load(filename);
        if (csvFile == null)
        {
            return new string[0, 0];
        }
        char lineSeperater = '\n';
        char fieldSeperator = ',';
        string[] records = csvFile.text.Split(lineSeperater);
        ArrayList result = new ArrayList();
        foreach (string record in records)
        {
            string[] fields = record.Split(fieldSeperator);
            for (int i = 0; i < fields.Length; ++i)
            {
                fields[i] = fields[i].Replace("\r", string.Empty);
            }
            result.Add(fields);
        }
        return TrimData(result);
    }

    private static string[,] TrimData(ArrayList data)
    {
        if (data.Count == 0 || ((string[])data[0]).Length == 0)
        {
            return new string[0, 0];
        }
        int top = Int32.MaxValue;
        int bottom = 0;
        int left = Int32.MaxValue;
        int right = 0;
        bool noData = true;
        HashSet<string> ignore = new HashSet<string> {
            "",
            "\r"
        };
        for (int i = 0; i < data.Count; ++i)
        {
            for (int j = 0; j < ((string[])data[i]).Length; ++j)
            {
                string text = ((string[])data[i])[j];
                if (!ignore.Contains(text))
                {
                    top = Math.Min(top, i);
                    bottom = Math.Max(bottom, i);
                    left = Math.Min(left, j);
                    right = Math.Max(right, j);
                    noData = false;
                }
            }
        }
        if (noData)
        {
            return new string[0, 0];
        }
        string[,] trimmedData = new string[bottom - top + 1, right - left + 1];
        for (int i = top; i <= bottom; ++i)
        {
            for (int j = left; j <= right; ++j)
            {
                trimmedData[i - top, j - left] = ((string[])data[i])[j];
            }
        }
        return trimmedData;
    }
}