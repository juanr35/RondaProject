using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mono.Data.Sqlite;
using System.Data;
using System;

//Clase estatica en donde se guardan los datos de registro: el nombre de los jugadores y la id de la partida
public static class StaticPlayers {
	public static string p1 { get; set; }
	public static string p2 { get; set; }
	public static int IdPartida { get; set; }
}

public class DBconection : MonoBehaviour {

	public GameObject Input1, Input2;

	void _InsertarJugador(string player, ref IDbConnection dbconn){

		//Si existe el jugador, por default no existe
		bool exist = false;
		
		IDbCommand dbcmd_read = dbconn.CreateCommand (); //Comando Lectura
		IDbCommand dbcmd_write = dbconn.CreateCommand (); //Comando Escritura

		//Consultas
		string sqlQuery_ins = "INSERT INTO Jugador ('Nickname') VALUES('" + player + "')";
		string sqlQuery_sel = "SELECT Nickname FROM Jugador where Nickname = '" + player + "'";

		string sqlQuery_points_ins = "INSERT INTO PuntuacionJugador ('NicknameJugador') VALUES('" + player + "')";

		dbcmd_read.CommandText = sqlQuery_sel;

		IDataReader reader = dbcmd_read.ExecuteReader();

		//Mirar si el jugador ya esta en la bd
		while (reader.Read ()) {
			if (reader.IsDBNull(0)) {
				Debug.Log ("Entre a la condicion");
			} else {
				Debug.Log ("El jugador " + player + " ya esta en la base de datos");
				//Si está, entonces existe
				exist = true;
			}
		}

		reader.Close ();
		reader = null;

		dbcmd_read.Dispose();
		dbcmd_read = null;

		//Si ya existe el jugador, ignora esto
		if (exist == false) {
			dbcmd_write.CommandText = sqlQuery_ins;
			dbcmd_write.ExecuteNonQuery ();

			dbcmd_write.CommandText = sqlQuery_points_ins;
			dbcmd_write.ExecuteNonQuery ();

			Debug.Log ("Jugador" + player + " ha sido introducido correctamente");
		}

		dbcmd_write.Dispose();
		dbcmd_write = null;

	}

	void _InsertarMatchup(string player1, string player2, ref IDbConnection dbconn){

		bool exist = false;

		IDbCommand dbcmd_read = dbconn.CreateCommand (); //Comando Lectura
		IDbCommand dbcmd_write = dbconn.CreateCommand (); //Comando Escritura
		string sqlQuery_ins = "INSERT INTO MatchUp ('NicknameJugador', 'Rival') VALUES('" + player1 + "', '" + player2 + "')";
		string sqlQuery_sel = "SELECT NicknameJugador, Rival FROM MatchUp where NicknameJugador = '" + player1 + "' and" +
			" Rival = '" + player2 + "'";

		dbcmd_read.CommandText = sqlQuery_sel;

		IDataReader reader = dbcmd_read.ExecuteReader();

		while (reader.Read ()) {
			if (reader.IsDBNull(0)) {
				Debug.Log ("Entre a la condicion");
			} else {
				Debug.Log ("El matchup ya esta en la base de datos");
				exist = true;
			}
		}

		reader.Close ();
		reader = null;

		dbcmd_read.Dispose();
		dbcmd_read = null;

		if (exist == false) {
			dbcmd_write.CommandText = sqlQuery_ins;
			dbcmd_write.ExecuteNonQuery ();
			Debug.Log ("El matchup " + player1 + " vs " + player2 + " ha sido introducido correctamente");
		}

		dbcmd_write.Dispose();
		dbcmd_write = null;

	}

	void _NuevaPartida(ref IDbConnection dbconn){

		IDbCommand dbcmd_read = dbconn.CreateCommand (); //Comando Lectura
		IDbCommand dbcmd_write = dbconn.CreateCommand (); //Comando Escritura
		string sqlQuery_ins = "INSERT INTO Partida (Ganador, Perdedor, Fecha) VALUES('TempG', 'TempP', '" + DateTime.Now + "')";

		dbcmd_write.CommandText = sqlQuery_ins;
		dbcmd_write.ExecuteNonQuery ();

		dbcmd_write.Dispose();
		dbcmd_write = null;


		string sqlQuery_sel = "SELECT * FROM Partida ORDER BY Id DESC LIMIT 1";

		dbcmd_read.CommandText = sqlQuery_sel;

		IDataReader reader = dbcmd_read.ExecuteReader();

		while (reader.Read ()) {
			StaticPlayers.IdPartida = reader.GetInt32(0);
		}

	}

	void _InsertarParticipacion (string player, ref IDbConnection dbconn){
		IDbCommand dbcmd_write = dbconn.CreateCommand (); //Comando Escritura
		string sqlQuery_ins = "INSERT INTO Participa (NicknameJugador, IdPartida) VALUES('" + player + "', '" + StaticPlayers.IdPartida + "')";

		dbcmd_write.CommandText = sqlQuery_ins;
		dbcmd_write.ExecuteNonQuery ();

		dbcmd_write.Dispose();
		dbcmd_write = null;
	}

	public void _sqlite () {
		Debug.Log ("--------------------------");
		string conn = "URI=file:" + Application.dataPath + "/Plugins/Ronda.db";
		IDbConnection dbconn;
		dbconn = (IDbConnection)new SqliteConnection (conn);
		dbconn.Open ();

		string player1 = Input1.GetComponent<Text> ().text;
		string player2 = Input2.GetComponent<Text> ().text;

		//Guarda el nombre de los jugadores para poder usarlos en otra escena
		StaticPlayers.p1 = player1;
		StaticPlayers.p2 = player2;

		//Inserciones iniciales

		_InsertarJugador (player1, ref dbconn);
		_InsertarJugador (player2, ref dbconn);
		_InsertarMatchup (player1, player2, ref dbconn);
		_NuevaPartida (ref dbconn);
		_InsertarParticipacion (player1, ref dbconn);
		_InsertarParticipacion (player2, ref dbconn);

		dbconn.Close();
		dbconn = null;

	}
}
