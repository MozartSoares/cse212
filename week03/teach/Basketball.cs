/*
 * CSE 212 Lesson 6C 
 * 
 * This code will analyze the NBA basketball data and create a table showing
 * the players with the top 10 career points.
 * 
 * Note about columns:
 * - Player ID is in column 0
 * - Points is in column 8
 * 
 * Each row represents the player's stats for a single season with a single team.
 */

using System.Security;
using Microsoft.VisualBasic.FileIO;

public class Basketball
{
    public static void Run()
    {
        var players = new Dictionary<string, int>();
        // {playerId:  points}

        using var reader = new TextFieldParser("basketball.csv");
        reader.TextFieldType = FieldType.Delimited;
        reader.SetDelimiters(",");
        reader.ReadFields(); // ignore header row
        while (!reader.EndOfData) {
            var fields = reader.ReadFields()!;
            var playerId = fields[0];
            var points = int.Parse(fields[8]);

            if (players.TryGetValue(playerId, out int storedPoints)) {
                players[playerId] = storedPoints + points;
                continue;
            } 
            players.Add(playerId,  points);
        }
        
        // Console.WriteLine($"Players: {string.Join(", ", players)}");

        var topPlayers = players.OrderByDescending(players => players.Value).Take(10);
        Console.WriteLine(string.Join("\n", topPlayers.Select(p => $"[{p.Key}, {p.Value}]")));
    }
}