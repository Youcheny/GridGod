using UnityEngine;
using UnityEditor;
using System.Collections;

public class CsvUtil
{
   

    // Read data from CSV file
    public static ArrayList readData(string filepath, string filename)
    {
        AssetDatabase.ImportAsset(filepath);
        TextAsset csvFile = (TextAsset)Resources.Load(filename);
        char lineSeperater = '\n';
        char fieldSeperator = ',';
        string[] records = csvFile.text.Split(lineSeperater);
        ArrayList result = new ArrayList();
        foreach (string record in records)
        {
            string[] fields = record.Split(fieldSeperator);
            result.Add(fields);
            
        }
        return result;
    }
}