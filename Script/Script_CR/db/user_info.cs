using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class user_info
{
    public static user_info User_Info = new user_info();
    private string id;
    private string password;
    private int game_number;
    //private DateTime game_date; 
    private int game_diff; //0:easy 1:normal 2: hard
    private string game_map; //"normal" "school zone" "parking" "mountain"
    private bool day_or_night; //true: day false: night
    private int game_score;
    //성공 여부 추가

    public void Init()
    {
        id = "";
        password = "";
        game_number = 0;
        game_diff = 0;
        game_map = "";
        day_or_night = true;
    }

    public void Set_id(string id) { this.id = id; }
    public void Set_password(string password) { this.password = password; }
    public void Set_game_number(int game_number) { this.game_number = game_number; }
    public void Set_game_diff(int game_diff) { this.game_diff = game_diff; }
    public void Set_game_map(string game_map) { this.game_map = game_map; }
    public void Set_day_or_night(bool day_or_night) { this.day_or_night = day_or_night; }

    public string Get_id() { return id; }

    public string Get_password() { return password; }
    public int Get_game_number() { return game_number; }
    public int Get_last_game_number() { return Database.db.Get_Recent_Game_Number(); }
    public int Get_game_diff() { return game_diff; }
    public string Get_game_map() { return game_map; }
    public bool Get_day_or_night() { return day_or_night; }
}
