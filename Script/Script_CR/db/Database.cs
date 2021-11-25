using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
using MySql.Data;
using MySql.Data.MySqlClient;
using System;


public class Database
{
    public static Database db = new Database();

    private MySqlConnection conn;
    public Database()
    {
        //
    }

    public MySqlConnection ConnectDB()
    {
        string connStr = "Server=database-1.cagqcdbbd6t9.us-east-2.rds.amazonaws.com;Port=3306;Database=VR_Project;Uid=admin;Pwd=admin999";
        MySqlConnection conn = new MySqlConnection(connStr);
        return conn;
    }

    public bool Id_Check(string entered_id)
    {
        MySqlConnection conn = ConnectDB();
        bool flag = true;
        conn.Open();

        string command = "SELECT id FROM User_Info;";
        MySqlCommand cmd = new MySqlCommand(command, conn);
        MySqlDataReader rdr = cmd.ExecuteReader();
        while (rdr.Read())
        {
            if (rdr.GetString(0) == entered_id)
            {
                flag = false;
                break;
            }
        }
        conn.Close();
        return flag;
    }

    public bool SignUp(string entered_id, string entered_pw)
    {
        MySqlConnection conn = ConnectDB();
        bool flag;
        conn.Open();

        string command = "INSERT INTO User_Info(id, password) VALUES ('" + entered_id + "', '" + entered_pw + "');";
        MySqlCommand cmd = new MySqlCommand(command, conn);
        int result = cmd.ExecuteNonQuery();
        if (result != 0)
        {
            flag = true;
        }
        else
        {
            flag = false;
        }
        conn.Close();
        return flag;
    }

    public bool Login()
    {
        MySqlConnection conn = ConnectDB();
        bool flag;
        conn.Open();

        Console.WriteLine("Open Success");

        string id = user_info.User_Info.Get_id();
        string command = "SELECT password FROM User_Info WHERE id = '" + id + "';";
        MySqlCommand cmd = new MySqlCommand(command, conn);
        object result = cmd.ExecuteScalar();
        if (result.ToString() == user_info.User_Info.Get_password())
        {
            flag = true;
        }
        else
        {
            flag = false;
        }
        conn.Close();
        return flag;
    }

    public int Get_Recent_Game_Number()
    {
        MySqlConnection conn = ConnectDB();
        int result = -1;
        conn.Open();
        string command = "SELECT game_number FROM Game_History ORDER BY game_number DESC LIMIT 1;";
        MySqlCommand cmd = new MySqlCommand(command, conn);
        MySqlDataReader rdr = cmd.ExecuteReader();
        while (rdr.Read())
        {
            result = rdr.GetInt32(0);
        }
        conn.Close();
        return result;
    }


    public bool Set_Map_and_Day_or_Night()
    {
        MySqlConnection conn = ConnectDB();
        bool flag = false;
        conn.Open();
        MySqlCommand cmd;
        string command1 = "INSERT INTO Game_History (id, game_number, game_map, day_or_night) VALUES ('" + user_info.User_Info.Get_id() + "', " + user_info.User_Info.Get_game_number() + ", '"
            + user_info.User_Info.Get_game_map() + "', " + user_info.User_Info.Get_day_or_night() + ");";

        cmd = new MySqlCommand(command1, conn);
        int result1 = cmd.ExecuteNonQuery();
        if (result1 != 0)
        {
            flag = true;
        }

        return flag;
    }

    public bool Set_Diff()
    {
        MySqlConnection conn = ConnectDB();
        bool flag;
        conn.Open();
        string command1 = "UPDATE Game_History SET game_diff = " + user_info.User_Info.Get_game_diff() + " WHERE game_number = " + user_info.User_Info.Get_game_number() + ";";
        MySqlCommand cmd = new MySqlCommand(command1, conn);
        int result = cmd.ExecuteNonQuery();
        if (result != 0)
        {
            flag = true;
        }
        else
        {
            flag = false;
        }
        conn.Close();
        return flag;
    }

}
