using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;

public class FicherSauvegarde: MonoBehaviour
{
    private static readonly string FILE_PATH = @".\gameFiles\";

    [SerializeField]
    private Sauvegarde _sauvegardeActuelle;

    private bool _saveActionLocked;

    public Sauvegarde Data {
        get { return _sauvegardeActuelle; }
        private set { _sauvegardeActuelle = value; value.SetParent(this); }
    }

    private FicherSauvegarde()
    {  
    }
    private void Awake()
    {
        Data = this.AddComponent<Sauvegarde>();
    }


    public void SetChildData(Sauvegarde sauvegarde)
    {
        Data = sauvegarde;
    }

    private Task QueueTask(Action callBack)
    {
        if (_saveActionLocked)
        {
            Debug.Log("Essayé de lancer une deuxième action sur le fichier de sauvegarde actuel alors que le fichier est déja lock.");
            return null;
        }

        _saveActionLocked = true;
        return Task.Run(() => callBack()).ContinueWith((result) => { _saveActionLocked = false; });
    }

    public Task SaveSauvegarde(string slot)
    {
       return SaveJsonObject("save-" + slot + ".json", _sauvegardeActuelle);
    }

    public Task LoadSauvegarde(string slot)
    {
        return ReadJsonObject("save-" + slot + ".json", _sauvegardeActuelle).ContinueWith((resultat) => { _sauvegardeActuelle.Slot = slot; });
    }

    private Task SaveJsonObject(string fileName, MonoBehaviour obj)
    {
        string jsonObj = JsonUtility.ToJson(obj);
        return QueueTask(() => SaveFile(fileName, jsonObj));
    }

    private Task ReadJsonObject<T>(string fileName, T obj) {
        return QueueTask(
            () =>
            {
                string jsonData = ReadFile(fileName);
                JsonUtility.FromJsonOverwrite(jsonData, obj);
            }
        );
    }

    private bool SaveFile(string fileName, string fileContent)
    {
        string file = @FILE_PATH + @fileName;
        lock (this)
        {
            try
            {

                FileStream fileWriteStream;

                DirectoryInfo fileInfo = new(file);
                if (!fileInfo.Exists)
                {
                    if (!fileInfo.Parent.Exists)
                    {
                        Directory.CreateDirectory(fileInfo.Parent.FullName);
                    }
                    fileWriteStream = File.Create(fileInfo.FullName);
                } else
                {
                    fileWriteStream = File.OpenWrite(file);
                }
                //Ré-écrire fichier si existe déja, écrire sinon

                byte[] byteFileContent = Encoding.UTF8.GetBytes(fileContent);

                fileWriteStream.Write(byteFileContent);
                fileWriteStream.Close();
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
