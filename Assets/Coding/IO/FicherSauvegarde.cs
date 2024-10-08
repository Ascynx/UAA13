using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using UnityEngine;

public class FicherSauvegarde
{
    //JsonUtility librairie Unity..
    private static readonly string FILE_PATH = "gameFiles/";

    private FicherSauvegarde()
    {

    }

    private void QueueTask(WaitCallback task)
    {
        ThreadPool.QueueUserWorkItem(task);
    }

    private void SaveFile(string fileName, string fileContent)
    {
        string file = FILE_PATH + fileName;
        lock (this)
        {
            try
            {
                if (!File.Exists(file))
                {
                    File.Create(file);
                }
                //Ré-écrire fichier si existe déja, écrire sinon

                File.WriteAllText(file, fileContent);
            } catch (IOException e)
            {
                Console.WriteLine("Erreur pendant l'écriture d'un fichier de sauvegarde " + e.Message + " " + e.StackTrace);
            }
        }
    }

    private string? ReadFile(string fileName)
    {
        string file = FILE_PATH + fileName;
        lock (this)
        {
            try
            {
                if (!File.Exists (file))
                {
                    return null;
                }

                return File.ReadAllText(file);
            } catch (IOException e)
            {
                Console.WriteLine("Erreur pendant la lecture d'un fichier de sauvegarde " + e.Message + " " + e.StackTrace);
            }
        }

        return null;
    }
}
