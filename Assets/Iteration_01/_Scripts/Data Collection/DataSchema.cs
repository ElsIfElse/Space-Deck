using System;
using Postgrest.Attributes;
using Postgrest.Models;

[Table("space_deck")]  // exact name of your table in Supabase
public class CardPickRecord : BaseModel
{
    [PrimaryKey("id",shouldInsert: false)]
    public int Id { get; set; }

    [Column("cardName")]
    public string CardName { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }

    [Column("count")]
    public int Count { get; set; }
}