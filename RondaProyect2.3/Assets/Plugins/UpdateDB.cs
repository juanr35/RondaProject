using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mono.Data.Sqlite;
using System.Data;
using System;

//Variables Globales
 
public static class RondaStats{
	public static int Duracion{ get; set; }
	public static int Clicks{ get; set; }
	public static int Turnos{ get; set; }
	public static string Ganador{ get; set; }
	public static int IdPartida{ get; set; }
	public static bool enable { get; set;}

}

public static class PartidaStats{
	public static int Duracion{ get; set; }
	public static int Clicks{ get; set; }
	public static int Turnos{ get; set; }
	public static string Perdedor { get; set; }
	public static string Ganador{ get; set; }
	public static bool enable { get; set;}
	//Calculo del ganador, y suma datos de las rondas (MatchUp)
	public static int VictoriasP1{get;set;}
	public static int VictoriasP2{get; set;}
}

public static class Jugador1{
	public static int Clicks { get; set; }
	public static int TiempoDeJuego { get; set; }
}

public static class Jugador2{
	public static int Clicks { get; set; }
	public static int TiempoDeJuego { get; set; }
}

public static class CelulaStats{
	public static byte Desaparecidas { get; set; }
	public static byte Rojas { get; set; }
	public static byte Divididas { get; set; }
	public static byte Cardinalidad { get; set; }

}


public class UpdateDB : MonoBehaviour {

	void Start(){
		_VaciarPartidaStats ();
		_VaciarRondaStats ();
		_VaciarJugadorStats ();
		_VaciarCelulaStats ();

	}

	void Update(){
		if (RondaStats.enable == true) {
			_ActualizarBaseDatos (1);
		}
		if (PartidaStats.enable == true) {
			_ActualizarBaseDatos (2);
		}
	}

	void _InsertarCelulas(ref IDbConnection dbconn){

		IDbCommand dbcmd_write = dbconn.CreateCommand (); //Comando Escritura
		string sqlQuery_ins = "INSERT INTO Celula (Desaparecidas, Rojas, Divididas, Cardinalidad) " +
			"VALUES(" + CelulaStats.Desaparecidas + " , " + CelulaStats.Rojas + 
			" , " + CelulaStats.Divididas + ", " + CelulaStats.Cardinalidad + ")";

		dbcmd_write.CommandText = sqlQuery_ins;
		dbcmd_write.ExecuteNonQuery ();

		dbcmd_write.Dispose();
		dbcmd_write = null;
		
		Debug.Log ("Tabla de celulas actualizada");
	}

	void _InsertarRonda(ref IDbConnection dbconn){

		IDbCommand dbcmd_write = dbconn.CreateCommand (); //Comando Escritura
		string sqlQuery_ins = "INSERT INTO Ronda (Duracion, Clicks, Turnos, Ganador, IdPartida ) " +
		"VALUES('" + RondaStats.Duracion + "', '" + RondaStats.Clicks + "', '" + RondaStats.Turnos + "', '" + RondaStats.Ganador + "', '" + RondaStats.IdPartida + "')";

		//Suma de los datos de la Ronda a la partida

		if(RondaStats.Ganador == StaticPlayers.p1) {PartidaStats.VictoriasP1++;}
		if(RondaStats.Ganador == StaticPlayers.p2) {PartidaStats.VictoriasP2++;}
		PartidaStats.Duracion += RondaStats.Duracion;
		PartidaStats.Clicks += RondaStats.Clicks;
		PartidaStats.Turnos += RondaStats.Turnos;

		//-------------------------

		dbcmd_write.CommandText = sqlQuery_ins;
		dbcmd_write.ExecuteNonQuery ();
		Debug.Log ("Ronda de partida " + RondaStats.IdPartida + "En la base de datos introducida");
		_InsertarCelulas (ref dbconn);

		dbcmd_write.Dispose();
		dbcmd_write = null;
	}

	void _ActualizarPartida(ref IDbConnection dbconn){
		bool update_enable = true;
		if (PartidaStats.VictoriasP1 > PartidaStats.VictoriasP2) {
			PartidaStats.Ganador = StaticPlayers.p1;
			PartidaStats.Perdedor = StaticPlayers.p2;
		}
		if (PartidaStats.VictoriasP1 < PartidaStats.VictoriasP2) {
			PartidaStats.Ganador = StaticPlayers.p2;
			PartidaStats.Perdedor = StaticPlayers.p1;
		} 
		if (PartidaStats.VictoriasP1 == PartidaStats.VictoriasP2) {
			//Si la partida Empata, borrar todos los datos de las rondas y la partida en sí
			IDbCommand dbcmd_unfill = dbconn.CreateCommand (); //Comando Escritura

			string sqlQuery_del = "DELETE from Ronda Where IdPartida = " + StaticPlayers.IdPartida;
			dbcmd_unfill.CommandText = sqlQuery_del;
			dbcmd_unfill.ExecuteNonQuery ();

			sqlQuery_del = "DELETE from Partida where Id = " + StaticPlayers.IdPartida;
			dbcmd_unfill.CommandText = sqlQuery_del;
			dbcmd_unfill.ExecuteNonQuery ();

			Debug.Log ("Eliminada partida " + StaticPlayers.IdPartida + " por haber terminado en empate");

			dbcmd_unfill.Dispose();
			dbcmd_unfill = null;
			update_enable = false;
		}

		//Si la partida no empata, Actualizar estadisticas de la partida
		if (update_enable == true) {
			IDbCommand dbcmd_write = dbconn.CreateCommand (); //Comando Escritura
			string sqlQuery_ins = "UPDATE Partida " +
			                     "SET Duracion ='" + PartidaStats.Duracion + "', " +
			                     " Clicks ='" + PartidaStats.Clicks + "', " +
			                     " Turnos ='" + PartidaStats.Turnos + "', " +
			                     " Perdedor ='" + PartidaStats.Perdedor + "', " +
			                     " Ganador = '" + PartidaStats.Ganador + "' " +
			                     "WHERE Id = " + StaticPlayers.IdPartida;

			dbcmd_write.CommandText = sqlQuery_ins;
			dbcmd_write.ExecuteNonQuery ();
			Debug.Log ("Introducida partida" + StaticPlayers.IdPartida + "En la base de datos");

			dbcmd_write.Dispose ();
			dbcmd_write = null;


			_ActualizarMatchUp (ref dbconn);
			_ActualizarJugador (ref dbconn);

		}
	}
	void _ActualizarJugador (ref IDbConnection dbconn){

		float Puntuacion1 = (PartidaStats.VictoriasP1 + 1) / (PartidaStats.VictoriasP2 + 1);
		float Puntuacion2 = (PartidaStats.VictoriasP2 + 1) / (PartidaStats.VictoriasP1 + 1);

		IDbCommand dbcmd_write = dbconn.CreateCommand (); //Comando Escritura
		string sqlQuery_ins = "UPDATE Jugador " +
			"SET Clicks = Clicks +" + Jugador1.Clicks + ", " +
			" TiempoDeJuego = TiempoDeJuego + " + Jugador1.TiempoDeJuego + ", " +
			" Victorias = Victorias + " + PartidaStats.VictoriasP1 + ", " +
			" Derrotas = Derrotas + " + PartidaStats.VictoriasP2 + " " +
			"WHERE Nickname  = '" + StaticPlayers.p1 + "'";

		dbcmd_write.CommandText = sqlQuery_ins;
		dbcmd_write.ExecuteNonQuery ();

		sqlQuery_ins = "UPDATE PuntuacionJugador " +
			"SET Fecha = '" + DateTime.Now + "'," +
			" Puntuacion = Puntuacion + " + Puntuacion1 + " " +
			"WHERE NicknameJugador  = '" + StaticPlayers.p1 + "'";

		dbcmd_write.CommandText = sqlQuery_ins;
		dbcmd_write.ExecuteNonQuery ();

		Debug.Log ("Jugador " + StaticPlayers.p1 + "Actualizado");

		sqlQuery_ins = "UPDATE Jugador " +
			"SET Clicks = Clicks +" + Jugador2.Clicks + ", " +
			" TiempoDeJuego = TiempoDeJuego + " + Jugador2.TiempoDeJuego + ", " +
			" Victorias = Victorias + " + PartidaStats.VictoriasP2 + ", " +
			" Derrotas = Derrotas + " + PartidaStats.VictoriasP1 + " " +
			"WHERE Nickname = '" + StaticPlayers.p2 + "'";

		dbcmd_write.CommandText = sqlQuery_ins;
		dbcmd_write.ExecuteNonQuery ();

		sqlQuery_ins = "UPDATE PuntuacionJugador " +
			"SET Fecha = '" + DateTime.Now + "'," +
			" Puntuacion = Puntuacion + " + Puntuacion2 + " " +
			"WHERE NicknameJugador  = '" + StaticPlayers.p2 + "'";

		dbcmd_write.CommandText = sqlQuery_ins;
		dbcmd_write.ExecuteNonQuery ();
		Debug.Log ("Jugador " + StaticPlayers.p2 + "Actualizado");

		//Record Personal de rondas ganadas por partida

		string sqlQuery_record = "UPDATE Jugador " +
			"SET Record = " + PartidaStats.VictoriasP1 + " " +
			"WHERE Record < " + PartidaStats.VictoriasP1 + " and Nickname = '" + StaticPlayers.p1 + "'";
		
		dbcmd_write.CommandText = sqlQuery_record;
		dbcmd_write.ExecuteNonQuery ();

		sqlQuery_record = "UPDATE Jugador " +
			"SET Record = " + PartidaStats.VictoriasP2 + " " +
			"WHERE Record < " + PartidaStats.VictoriasP2 + " and Nickname = '" + StaticPlayers.p2 + "'";

		dbcmd_write.CommandText = sqlQuery_record;
		dbcmd_write.ExecuteNonQuery ();

		dbcmd_write.Dispose ();
		dbcmd_write = null;

	}

	void _ActualizarMatchUp (ref IDbConnection dbconn){

		Debug.Log ("Test");
		IDbCommand dbcmd_write = dbconn.CreateCommand (); //Comando Escritura
		string sqlQuery_ins = "UPDATE MatchUp " +
			"SET VictoriasJugador = " + PartidaStats.VictoriasP1 + ", " +
			" VictoriasRival = " + PartidaStats.VictoriasP2 + " " +
			"WHERE NicknameJugador = '" + StaticPlayers.p1 + "' and " +
			"Rival = '" + StaticPlayers.p2 + "' ";

		dbcmd_write.CommandText = sqlQuery_ins;
		dbcmd_write.ExecuteNonQuery ();
		Debug.Log ("MatchUp " + StaticPlayers.p1 + " vs " + StaticPlayers.p2 + " Actualizada");

		dbcmd_write.Dispose ();
		dbcmd_write = null;
		
	}

	void _ActualizarBaseDatos(int priority){ //La prioridad indica cual tabla es la que va actualizar

		RondaStats.enable = false;
		PartidaStats.enable = false;

		string conn = "URI=file:" + Application.dataPath + "/Plugins/Ronda.db";
		IDbConnection dbconn;
		dbconn = (IDbConnection)new SqliteConnection (conn);
		dbconn.Open ();

		if (priority == 1) {
			_InsertarRonda (ref dbconn);
			_VaciarRondaStats ();
			_VaciarCelulaStats ();
		}

		if (priority == 2) {
			_ActualizarPartida (ref dbconn);
			_VaciarPartidaStats ();
		}

		dbconn.Close();
		dbconn = null;


	}

	void _VaciarRondaStats(){
		RondaStats.Duracion = 0;
		RondaStats.Clicks = 0;
		RondaStats.Turnos = 0;
		RondaStats.Ganador = "TempG";
		RondaStats.IdPartida = 0;
	}

	void _VaciarPartidaStats(){
		PartidaStats.Duracion = 0;
		PartidaStats.Clicks = 0;
		PartidaStats.Turnos = 0;
		PartidaStats.Ganador = "TempP";
		PartidaStats.Perdedor = "TempG";
		PartidaStats.VictoriasP1 = 0;
		PartidaStats.VictoriasP2 = 0;
	}

	void _VaciarJugadorStats(){
		Jugador1.Clicks = 0;
		Jugador2.Clicks = 0;
		Jugador1.TiempoDeJuego = 0;
		Jugador2.TiempoDeJuego = 0;
	}

	void _VaciarCelulaStats(){
		CelulaStats.Desaparecidas = 0;
		CelulaStats.Rojas = 1;
		CelulaStats.Divididas = 0;
		CelulaStats.Desaparecidas = 0;
	}
}
