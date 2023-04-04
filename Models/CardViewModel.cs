namespace mtg_app.Models;

public class CardViewModel
{
    public long Id { get; set; }
    public String Name { get; set; }
    public String OriginalImageUrl { get; set; }
    public String OriginalText { get; set; }
    public String RarityCode { get; set; }
    
    public double Price { get; set; }
}