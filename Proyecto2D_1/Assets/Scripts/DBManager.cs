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

    [SerializeField] private GameObject laptopPuzzle;

    private GameObject saveVariables;
    private int cod; //ROWID de la partida en la BBDD
    //public string numScene;



    // Start is called before the first frame update
    void Start()
    {
        if(gameObject.tag == "DB")
        {
            CreateDB();
            //Ninguna partida iniciada
        }
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
                command.CommandText = "CREATE TABLE IF NOT EXISTS PUZLE_MENU(CodigoSecreto VARCHAR2(4) PRIMARY KEY, ING1 VARCHAR2(20), ING2 VARCHAR2(20), ING3 VARCHAR2(20), ING4 VARCHAR2(20))";
                command.ExecuteNonQuery();

            }

            connection.Close();
            fillMenuTable();
        }
    }

    public void fillMenuTable()
    {
        using (var connection = new SqliteConnection(dbName))
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                //Se usa el campo oculto ROWID como identificador y clave primaria
                command.CommandText = "SELECT COUNT(*) FROM PUZLE_MENU";

                using (IDataReader reader = command.ExecuteReader())
                {

                    //Leer resultados de la select
                    //while (reader.Read())
                    //{
                    reader.Read();
                    Debug.Log("Hay "+ reader.GetInt32(0).ToString()+" menus en la tabla");
                    if(reader.GetInt32(0) == 0)
                    {
                        connection.Close();
                        cargarMenus();
                    }
                    //}
                }
            }

            connection.Close();

        }
    }

    public void cargarMenus()
    {

        string[] listaIng1 = {"Merluza","Atún","Salmón","Dorada","Lubina"};
        string[] listaIng2 = { "Cerdo", "Pollo", "Buey", "Ternera", "Cordero" };
        string[] listaIng3 = { "Manzana", "Plátano", "Naranja", "Melocotón", "Kiwi" };
        string[] listaIng4 = { "Natillas", "Tiramisú", "Flan", "Helado", "Tarta" };

        using (var connection = new SqliteConnection(dbName))
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                int codMenu = 2000;
                foreach(string ing1 in listaIng1)
                {
                    foreach (string ing2 in listaIng2)
                    {
                        foreach (string ing3 in listaIng3)
                        {
                            foreach (string ing4 in listaIng4)
                            {
                                command.CommandText = "INSERT OR IGNORE INTO PUZLE_MENU VALUES('" + codMenu.ToString() + "','"+ing1+ "','" + ing2 + "','" + ing3 + "','" + ing4 + "')";
                                command.ExecuteNonQuery();
                                codMenu = codMenu + 3;
                            }
                        }
                    }
                }

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

                        panelResults.transform.GetChild(0).GetComponent<Text>().text = reader.GetString(0);
                        panelResults.transform.GetChild(1).GetComponent<Text>().text = reader.GetString(1);
                        panelResults.transform.GetChild(2).GetComponent<Text>().text = reader.GetString(2);
                        panelResults.transform.GetChild(3).GetComponent<Text>().text = reader.GetString(3);
                        //Coloca el boton en el content del scrollView
                        panelResults.transform.SetParent(contentBox.transform, false);
                    }
                }
            }

            connection.Close();

        }
    }

    public void buscarMenuPorIngredientes()
    {
        string ing1 = panelPlayerResults.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Text>().text;
        string ing2 = panelPlayerResults.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<Text>().text;
        string ing3 = panelPlayerResults.transform.GetChild(0).GetChild(2).GetChild(0).GetComponent<Text>().text;
        string ing4 = panelPlayerResults.transform.GetChild(0).GetChild(3).GetChild(0).GetComponent<Text>().text;

        using (var connection = new SqliteConnection(dbName))
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                //command.CommandText = "SELECT ROWID, Nombre, EscenaActual FROM PARTIDA";
                command.CommandText = "SELECT CodigoSecreto FROM PUZLE_MENU WHERE ING1 LIKE '"+ing1+"' AND ING2 LIKE '"+ing2+"' AND ING3 LIKE '"+ing3+"' AND ING4 LIKE '"+ing4+"'";

                using (IDataReader reader = command.ExecuteReader())
                {
                    //Leer resultados de la select
                    reader.Read();

                    Debug.Log(reader.GetString(0));

                    panelPlayerResults.transform.GetChild(1).GetComponent<Text>().text = "Código: #"+reader.GetString(0);

                }
            }

            connection.Close();
        }
    }

    public string getRandomMenuCode()
    {
        string[] listaIng1 = { "Merluza", "Atún", "Salmón", "Dorada", "Lubina" };
        string[] listaIng2 = { "Cerdo", "Pollo", "Buey", "Ternera", "Cordero" };
        string[] listaIng3 = { "Manzana", "Plátano", "Naranja", "Melocotón", "Kiwi" };
        string[] listaIng4 = { "Natillas", "Tiramisú", "Flan", "Helado", "Tarta" };

        string ingMenu1 = listaIng1[UnityEngine.Random.Range(0, 5)];
        string ingMenu2 = listaIng2[UnityEngine.Random.Range(0, 5)];
        string ingMenu3 = listaIng3[UnityEngine.Random.Range(0, 5)];
        string ingMenu4 = listaIng4[UnityEngine.Random.Range(0, 5)];

        laptopPuzzle.GetComponent<Laptop>().ing1 = ingMenu1;
        laptopPuzzle.GetComponent<Laptop>().ing2 = ingMenu2;
        laptopPuzzle.GetComponent<Laptop>().ing3 = ingMenu3;
        laptopPuzzle.GetComponent<Laptop>().ing4 = ingMenu4;

        string menuCode = "";

        using (var connection = new SqliteConnection(dbName))
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                //command.CommandText = "SELECT ROWID, Nombre, EscenaActual FROM PARTIDA";
                command.CommandText = "SELECT CodigoSecreto FROM PUZLE_MENU WHERE ING1 LIKE '" + ingMenu1 + "' AND ING2 LIKE '" + ingMenu2 + "' AND ING3 LIKE '" + ingMenu3 + "' AND ING4 LIKE '" + ingMenu4 + "'";

                using (IDataReader reader = command.ExecuteReader())
                {
                    //Leer resultados de la select
                    reader.Read();

                    Debug.Log(reader.GetString(0));
                    menuCode = reader.GetString(0);
                }
            }

            connection.Close();
            return menuCode;
        }
    }

}
