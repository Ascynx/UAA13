using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class FicherSauvegarde : MonoBehaviour
{
    private static readonly string FILE_PATH = @".\gameFiles\";

    [SerializeField]
    private Sauvegarde _sauvegardeActuelle;

    [SerializeField]
    private bool _saveActionLocked;

    public Sauvegarde Data
    {
        get { return _sauvegardeActuelle; }
        private set {
            _sauvegardeActuelle = value;
            value.SetParent(this);
        }
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
            Debug.LogWarning("Essayé de lancer une deuxième action sur le fichier de sauvegarde actuel alors que le fichier est déja lock.");
            return null;
        }

        _saveActionLocked = true;
        Jeu.Instance.saveIconControl.SetActive();
        return Task.Run(() => callBack())
            .ContinueWith((result) => {
            _saveActionLocked = false;
            Jeu.Instance.saveIconControl.SetInactive();
        }, TaskScheduler.FromCurrentSynchronizationContext());
    }

    /// <summary>
    /// Vérifie si le fichier de sauvegarde existe
    /// </summary>
    /// <param name="slot"></param>
    /// <returns></returns>
    public bool VerifieSauvegarde(string slot)
    {
        return ExistsFile("save-" + slot + ".json");
    }

    /// <summary>
    /// merci d'utiliser Sauvegarde#SauvegardeFichier(string) et Sauvegarde#LoadFichier(string) pour sauvegarder et charger (sauf si vous ne voulez pas de mise à jour de données)
    /// </summary>
    /// <param name="slot"></param>
    /// <returns></returns>
    public Task SaveSauvegarde(string slot)
    {
        return SaveJsonObject("save-" + slot + ".json", _sauvegardeActuelle);
    }

    /// <summary>
    /// merci d'utiliser Sauvegarde#SauvegardeFichier(string) et Sauvegarde#LoadFichier(string) pour sauvegarder et charger (sauf si vous ne voulez pas de mise à jour de données)
    /// </summary>
    /// <param name="slot"></param>
    /// <returns></returns>
    public Task LoadSauvegarde(string slot)
    {
        return ReadJsonObject("save-" + slot + ".json", _sauvegardeActuelle).ContinueWith((resultat) => { _sauvegardeActuelle.Slot = slot; });
    }

    public Task DeleteSauvegarde(string slot)
    {
        return DeleteFileTask("save-" + slot + ".json");
    }

    private Task SaveJsonObject(string fileName, MonoBehaviour obj)
    {
        string jsonObj = JsonUtility.ToJson(obj);
        return QueueTask(() => SaveFile(fileName, jsonObj));
    }

    private Task ReadJsonObject<T>(string fileName, T obj)
    {
        return QueueTask(
            () =>
            {
                string jsonData = ReadFile(fileName);
                JsonUtility.FromJsonOverwrite(jsonData, obj);
            }
        );
    }

    private Task DeleteFileTask(string fileName)
    {
        return QueueTask(() => DeleteFile(fileName));
    }

    private bool ExistsFile(string fileName)
    {
        string file = @FILE_PATH + @fileName;
        lock (this)
        {
            try
            {
                return File.Exists(file);
            }
            catch (IOException e)
            {
                Debug.LogError("Erreur pendant la vérification d'un fichier de sauvegarde " + e.Message + " " + e.StackTrace);
                return false;
            }
        }
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
                }
                else
                {
                    fileWriteStream = File.OpenWrite(file);
                }
                //Ré-écrire fichier si existe déja, écrire sinon

                byte[] byteFileContent = Encoding.UTF8.GetBytes(fileContent);

                fileWriteStream.Write(byteFileContent);
                fileWriteStream.Close();
            }
            catch (IOException e)
            {
                Debug.LogError("Erreur pendant l'écriture d'un fichier de sauvegarde " + e.Message + " " + e.StackTrace);
                return false;
            }
        }
        Debug.Log("Fichier de sauvegarde " + fileName + " sauvegardé avec succès.");
        return true;
    }

    private string ReadFile(string fileName)
    {
        string file = @FILE_PATH + @fileName;
        lock (this)
        {
            try
            {
                if (!File.Exists(file))
                {
                    return null;
                }

                return File.ReadAllText(file);
            }
            catch (IOException e)
            {
                Debug.LogError("Erreur pendant la lecture d'un fichier de sauvegarde " + e.Message + " " + e.StackTrace);
            }
        }

        return null;
    }

    private bool DeleteFile(string fileName)
    {
        string file = @FILE_PATH + @fileName;
        lock (this)
        {
            try
            {
                FileInfo fileInfo = new(file);
                if (!fileInfo.Exists)
                {
                    return false;
                }
                fileInfo.Delete();
                fileInfo.Refresh();
                while (fileInfo.Exists)
                {
                    System.Threading.Thread.Sleep(100); // Attendre que le fichier soit supprimé
                    fileInfo.Refresh();
                }
                return true;
            }
            catch (IOException e)
            {
                Debug.LogError("Erreur pendant la suppression d'un fichier de sauvegarde " + e.Message + " " + e.StackTrace);
                return false;
            }
        }
    }
}
