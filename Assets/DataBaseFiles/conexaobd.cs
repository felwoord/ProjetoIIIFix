using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
using MySql.Data;
using MySql.Data.MySqlClient;

public class conexaobd : MonoBehaviour {

	private string source;
	private MySqlConnection conexao;


	// Use this for initialization
	public void Startt () {
		source = "Server = localhost; " +         //htlm do site de host
		"Database = projeto3;" +
		"User ID = Felipe;" +
		"Pooling = false;" +
		"Password = 123456fe";
		ConectarBanco (source);
//		Listar ();
//		DesconectarBanco ();

		DontDestroyOnLoad (transform.gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void ConectarBanco(string _source){
		conexao = new MySqlConnection (source);
		conexao.Open ();

		Debug.Log ("Conectado");
	}

	public void DesconectarBanco(){
		conexao.Close ();
	}

	public void Listar(){
		MySqlCommand comando = conexao.CreateCommand ();
		comando.CommandText = "SELECT * FROM level";
		MySqlDataReader dados = comando.ExecuteReader ();

		while (dados.Read ()) {
			string nome = (string)dados["name"];
			Debug.Log ("level name: " + nome);
		}
		dados.Close ();
	}

	public void CheckAvl(int id){
		MySqlCommand comando = conexao.CreateCommand ();
		comando.CommandText = "SELECT idlevel from level where idlevel = " + id;
		MySqlDataReader dado = comando.ExecuteReader ();

		while (dado.Read ()) {
			int avl = (int)dado ["available"];
			Debug.Log (avl);
		}
		dado.Close ();
	}

	public bool CheckAvailable(int id){
		bool ret = false;
		MySqlCommand comando = conexao.CreateCommand ();
		comando.CommandText = "SELECT available FROM level where idlevel = " + id;
		MySqlDataReader dados = comando.ExecuteReader ();

		while (dados.Read ()) {
			bool avl = dados.GetBoolean("available");
//			Debug.Log ("level available: " + avl);
			ret = avl;
		}
		dados.Close ();
		return ret;
	}

	public void DeleteSave(){
		MySqlCommand comando = conexao.CreateCommand ();
		for (int i = 3; i <= 5; i++) {
			comando.CommandText = "update level set available = 0 where idlevel = " + i;
			MySqlDataReader dados = comando.ExecuteReader ();
			dados.Close ();
		}
	}

	public void OpenLevel(int id){
		MySqlCommand comando = conexao.CreateCommand ();
		comando.CommandText = "update level set available = 1 where idlevel = " + id;
		MySqlDataReader dados = comando.ExecuteReader ();
		dados.Close ();

	}
}
