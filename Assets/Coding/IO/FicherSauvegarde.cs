using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using UnityEngine;
using UnityEngine.Rendering.UI;

public class FicherSauvegarde
{
    private static readonly string FILE_PATH = @".\gameFiles\";
    private static readonly FicherSauvegarde INSTANCE = new FicherSauvegarde();

    private Sauvegarde sauvegardeActuelle;

    public Sauvegarde Data {
        get { return sauvegardeActuelle; }
        private set { sauvegardeActuelle = value; value.SetParent(this); }
    }

    public static FicherSauvegarde GetInstance()
    {
        return INSTANCE;
    }
         
    private FicherSauvegarde()
    {
    }

    public void SetChildData(Sauvegarde sauvegarde)
    {
        Data = sauvegarde;
    }

    private void QueueTask<TState>(Action<TState> callBack, TState state, bool preferLocal)
    {
        ThreadPool.QueueUserWorkItem(callBack, state, preferLocal);
    }

    public void SaveSauvegarde(string slot)
    {
        SaveJsonObject("save-" + slot + ".json", sauvegardeActuelle);
    }

    public void LoadSauvegarde(string slot)
    {
        ReadJsonObject("save-" + slot + ".json", sauvegardeActuelle);
    }

    private void SaveJsonObject(string fileName, MonoBehaviour obj)
    {
        string jsonObj = JsonUtility.ToJson(obj);
        QueueTask((b) => SaveFile(fileName, jsonObj), false, true);
    }

    private void ReadJsonObject<T>(string fileName, T obj) {
        QueueTask(
            (b) =>
            {
                string jsonData = ReadFile(fileName);
                JsonUtility.FromJsonOverwrite(jsonData, obj);
            },
            false,
            true
        );
    }

    private bool SaveFile(string fileName, string fileContent)
    {
        string file = @FILE_PATH + @fileName;
        lock (this)
        {
            try
            {

                DirectoryInfo fileInfo = new DirectoryInfo(file);
                if (!fileInfo.Exists)
                {
                    if (!fileInfo.Parent.Exists)
                    {
                        Directory.CreateDirectory(fileInfo.Parent.FullName);
                    }
                    File.Create(fileInfo.FullName);
                }
                //Ré-écrire fichier si existe déja, écrire sinon

                File.WriteAllText(file, fileContent); // Sharing violation on path
            } catch (IOException e)
            {
                Debug.LogError("Erreur pendant l'écriture d'un fichier de sauvegarde " + e.Message + " " + e.StackTrace);
                return false;
            }
        }

        return true;
    }

    private string ReadFile(string fileName)
    {
        string file = FILE_PATH + fileName;
        lock (this)
        {
            try
            {
                if (!File.Exists(file))
                {
                    return null;
                }

                return File.ReadAllText(file);
            } catch (IOException e)
            {
                Debug.LogError("Erreur pendant la lecture d'un fichier de sauvegarde " + e.Message + " " + e.StackTrace);
            }
        }

        return null;
    }
}
