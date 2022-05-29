using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
using Mono.Data.Sqlite;

using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DBManager : MonoBehaviour
{
    private string dbName = "URI=file:GameInfo.db";
    [SerializeField] private InputField enterName;
    [SerializeField] private GameObject contentBox;
    [SerializeField] private Button btnSaveSlot;
    [SerializeField] private GameObject panelPlayerResults;
    private GameObject saveVariables;
    private int cod; //ROWID de la partida en la BBDD
    //public string numScene;
    // Start is called before the first frame update
    void Start()
    {
        CreateDB();
        //Ninguna partida iniciada
        saveVariables = GameObject.FindWithTag("SaveVariables");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreateDB()
    {
        using(var connection = new SqliteConnection(dbName))
        {
            connection.Open();

            using(var command = connection.CreateCommand())
            {
                //Se usa el campo oculto ROWID como identificador y clave primaria
                command.CommandText = "CREATE TABLE IF NOT EXISTS PARTIDA(Nombre VARCHAR2(10), EscenaActual VARCHAR2(20) ,Tiempo1 VARCHAR2(10) DEFAULT '-', Tiempo2 VARCHAR2(10) DEFAULT '-', Tiempo3 VARCHAR2(10) DEFAULT '-')";
                command.ExecuteNonQuery();
                //command.CommandText = "CREATE TABLE IF NOT EXISTS PROGRESO(IDPartida VARCHAR2(10), FOREIGN KEY(IDPartida) REFERENCES PARTIDA(IDPartida))"; //Añadir variables de progreso, por ahora para pruebas
                //command.ExecuteNonQuery();
                command.CommandText = "CREATE TABLE IF NOT EXISTS PUZLE_RECETA(CodigoSecreto VARCHAR2(4), Nombre VARCHAR2(20), ING1 VARCHAR2(20), ING2 VARCHAR2(20), ING3 VARCHAR2(20), ING4 VARCHAR2(20))";
                command.ExecuteNonQuery();
            }

            connection.Close();

        }
    }

    public void AddPartida()
    {
        using(var connection = new SqliteConnection(dbName))
        {
            connection.Open();

            using(var command = connection.CreateCommand())
            {
                command.CommandText = "INSERT INTO PARTIDA(Nombre, EscenaActual) VALUES('"+enterName.text.Trim()+"','Inicio')";
                command.ExecuteNonQuery();

                //Registra el ROWID de la nueva partida en el singleton para guardar posteriormente
                command.CommandText = "SELECT last_insert_rowid()";
                command.ExecuteNonQuery();

                using(IDataReader reader = command.ExecuteReader())
                {
                    while(reader.Read())
                    {
                        saveVariables.GetComponent<SaveVariables>().cod = reader.GetInt32(0);
                    }
                }

            }

            connection.Close();
        }

    }

    //Guardado automatico que se ejecuta al principio de cada escena
    public void guardarPartida(int id)
    {
        string sceneName = SceneManager.GetActiveScene().name;

        using(var connection = new SqliteConnection(dbName))
        {
            connection.Open();

            using(var command = connection.CreateCommand())
            {
                //command.CommandText = "INSERT INTO PARTIDA(Nombre) VALUES('"+enterName.text.Trim()+"')";
                command.CommandText = "UPDATE PARTIDA SET EscenaActual = '"+sceneName+"' WHERE ROWID = "+id;
                command.ExecuteNonQuery();
            }

            connection.Close();
        }
    }

    public void guardarTiempo(int id, string numScene, string finishTime)
    {
        string sceneName = SceneManager.GetActiveScene().name;

        using (var connection = new SqliteConnection(dbName))
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                //command.CommandText = "INSERT INTO PARTIDA(Nombre) VALUES('"+enterName.text.Trim()+"')";
                command.CommandText = "UPDATE PARTIDA SET Tiempo"+numScene+" = '" + finishTime + "' WHERE ROWID = " + id;
                command.ExecuteNonQuery();
            }

            connection.Close();
        }
    }


    public void cargarPartidaSeleccionada()
    {
        saveVariables.GetComponent<SaveVariables>().cod = int.Parse(gameObject.transform.GetChild(0).GetComponent<Text>().text);
        try
        {
            SceneManager.LoadSceneAsync(gameObject.transform.GetChild(2).GetComponent<Text>().text);
        }
        catch(Exception ex)
        {
            Debug.Log("No se puede cargar la escena");
            Debug.Log(ex.Message);
        }
    }

    public void cargarListaPartidas()
    {
        using(var connection = new SqliteConnection(dbName))
        {
            connection.Open();

            using(var command = connection.CreateCommand())
            {
                //command.CommandText = "SELECT ROWID, Nombre, EscenaActual FROM PARTIDA";
                command.CommandText = "SELECT ROWID, Nombre, EscenaActual FROM PARTIDA WHERE EscenaActual NOT LIKE 'Finalizada'";

                using(IDataReader reader = command.ExecuteReader())
                {

                    //string cod;
                    //string nom;
                    //string prog;

                    //Limpiar resultados anteriores
                    foreach(Transform child in contentBox.transform)
                    {
                        GameObject.Destroy(child.gameObject);
                    }

                    //Leer resultados de la select
                    while(reader.Read())
                    {
                        Debug.Log("Carga");
                        //Crear boton por cada resultado
                        var button = Instantiate<Button>(btnSaveSlot);
                        //Asignar texto a los botones
                        /*Debug.Log(reader.GetDataTypeName(0).ToString());
                        Debug.Log(reader.GetDataTypeName(1).ToString());
                        Debug.Log(reader.GetDataTypeName(2).ToString());
                        Debug.Log(reader.GetInt32(0).ToString());
                        Debug.Log(reader.GetString(1));
                        Debug.Log(reader.GetString(2));*/
                        button.transform.GetChild(0).GetComponent<Text>().text = reader.GetInt32(0).ToString();
                        button.transform.GetChild(1).GetComponent<Text>().text = reader.GetString(1);
                        button.transform.GetChild(2).GetComponent<Text>().text = reader.GetString(2);
                        //Coloca el boton en el content del scrollView
                        button.transform.SetParent(contentBox.transform, false);
                    }
                }
            }
        }
    }

    public void cargarListaResultados()
    {
        using (var connection = new SqliteConnection(dbName))
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                //command.CommandText = "SELECT ROWID, Nombre, EscenaActual FROM PARTIDA";
                command.CommandText = "SELECT Nombre, Tiempo1, Tiempo2, Tiempo3 FROM PARTIDA";
                //command.CommandText = "SELECT ROWID, Nombre, Tiempo1, Tiempo2, Tiempo3 FROM PARTIDA WHERE EscenaActual LIKE 'Finalizada'";

                using (IDataReader reader = command.ExecuteReader())
                {

                    //string cod;
                    //string nom;
                    //string prog;

                    //Limpiar resultados anteriores
                    foreach (Transform child in contentBox.transform)
                    {
                        GameObject.Destroy(child.gameObject);
                    }

                    //Leer resultados de la select
                    while (reader.Read())
                    {
                        Debug.Log("Carga");
                        //Crear boton por cada resultado
                        var panelResults = Instantiate<GameObject>(panelPlayerResults);
                        //Asignar texto a los botones
                        /*Debug.Log(reader.GetDataTypeName(0).ToString());
                        Debug.Log(reader.GetDataTypeName(1).ToString());
                        Debug.Log(reader.GetDataTypeName(2).ToString());
                        Debug.Log(reader.GetInt32(0).ToString());
                        Debug.Log(reader.GetString(1));
                        Debug.Log(reader.GetString(2));*/
                        panelResults.transform.GetChild(0).GetComponent<Text>().text = reader.GetString(0);
                        panelResults.transform.GetChild(1).GetComponent<Text>().text = reader.GetString(1);
                        panelResults.transform.GetChild(2).GetComponent<Text>().text = reader.GetString(2);
                        panelResults.transform.GetChild(3).GetComponent<Text>().text = reader.GetString(3);
                        //Coloca el boton en el content del scrollView
                        panelResults.transform.SetParent(contentBox.transform, false);
                    }
                }
            }
        }
    }

}
